﻿using Newtonsoft.Json;
using Reclamation.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ConnectionString = Reclamation.Core.ConnectionStringUtility;

namespace Reclamation.TimeSeries.IDWR
{
    /// <summary>
    /// Idwr data type, HST=Historical, ALC=Allocation Model Data
    /// </summary>
    public enum DataType
    {
        HST,
        ALC
    }

    /// <summary>
    /// IDWR Daily Data from API web service
    /// </summary>
    public class IDWRDailySeries : Series
    {
        static string idwrAPI = @"https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting";
        public static RestClient idwrClient = new RestClient(idwrAPI);
        string station;
        string parameter;
        DataType datatype;

        public IDWRDailySeries(string station, string parameter, DataType datatype)
        {
            this.Source = "IDWR";
            this.Provider = "IDWRDailySeries";
            this.station = station;
            this.parameter = parameter;
            this.datatype = datatype;
            this.TimeInterval = TimeInterval.Daily;
            switch (parameter)
            {
                case "FB":
                case "GH":
                    Units = "feet";
                    break;
                case "QD":
                    Units = "cfs";
                    break;
                case "AF":
                    Units = "acre-feet";
                    break;
                default:
                    Units = "cfs";
                    break;
            }
            this.Name = $"{datatype}.{parameter}_{station}";
            this.Table.TableName = $"IdwrDaily{datatype}{parameter}_{station}";
            this.ConnectionString = $"Source=IDWR;SiteID={station};Parameter={parameter};DataType={datatype}";
        }

        public IDWRDailySeries(TimeSeriesDatabase db, TimeSeriesDatabaseDataSet.SeriesCatalogRow sr) : base(db, sr)
        {

        }


        /// <summary>
        /// Reads IDWR Series from pdb Database.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        protected override void ReadCore(DateTime t1, DateTime t2)
        {
            if (m_db != null)
            {
                base.ReadCore(t1, t2);
            }
            else
            {
                Add(IdwrApiDownload(station, parameter, datatype, t1, t2));
            }
        }


        protected override Series CreateFromConnectionString()
        {
            DataType datatype;
            if (!Enum.TryParse(ConnectionStringUtility.GetToken(ConnectionString, "DataType", ""), out datatype))
                return new Series();

            IDWRDailySeries s = new IDWRDailySeries(
               ConnectionStringUtility.GetToken(ConnectionString, "SiteID", ""),
               ConnectionStringUtility.GetToken(ConnectionString, "Parameter", ""), datatype);
            return s;
        }

        //private static List<TsData> IdwrApiQuerySiteData(RiverSite riverSite, string yearList)
        //{
        //    // Working API Call
        //    //https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting/history?siteid=10055500&yearlist=2016,2015&yeartype=CY&f=json

        //    return IdwrApiQuerySiteData(riverSite.SiteID, yearList);
        //}

        private List<TsData> IdwrApiQuerySiteDataHST(string siteID, string yearList)
        {
            // Working API Call
            //https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting/history?siteid=10055500&yearlist=2016,2015&yeartype=CY&f=json

            var request = new RestRequest("history?", Method.GET);
            request.AddParameter("siteid", siteID);
            request.AddParameter("yearlist", yearList);
            request.AddParameter("yeartype", "CY");
            request.AddParameter("f", "json");
            IRestResponse restResponse = idwrClient.Execute(request);
            return JsonConvert.DeserializeObject<List<TsData>>(restResponse.Content);
        }

        private List<TsDataALC> IdwrApiQuerySiteDataALC(string siteID, string yearList)
        {
            // Working API Call
            //https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting/model?siteid=10055500&yearlist=2016,2015&yeartype=CY&f=json

            var request = new RestRequest("model?", Method.GET);
            request.AddParameter("siteid", siteID);
            request.AddParameter("yearlist", yearList);
            request.AddParameter("yeartype", "CY");
            request.AddParameter("f", "json");
            IRestResponse restResponse = idwrClient.Execute(request);
            return JsonConvert.DeserializeObject<List<TsDataALC>>(restResponse.Content);
        }


        /// <summary>
        /// Method to call the data download API, get the JSON reponse, and convert to Series()
        /// </summary>
        /// <param name="station"></param>
        /// <param name="parameter"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        private Series IdwrApiDownload(string station, string parameter, DataType datatype,
            DateTime t1, DateTime t2)
        {
            var rval = new Series();

            var yearlist = YearsToQuery(station, t1, t2);

            try
            {
                switch (datatype)
                {
                    case DataType.HST:
                        var jsonResponseHST = IdwrApiQuerySiteDataHST(station, yearlist);
                        foreach (var item in jsonResponseHST)
                        {
                            var t = DateTime.Parse(item.Date);
                            if (t >= t1.Date && t <= t2.Date)
                            {
                                string value = "";
                                switch (parameter)
                                {
                                    case ("GH"):
                                        value = item.GH;
                                        break;
                                    case ("FB"):
                                        value = item.FB;
                                        break;
                                    case ("AF"):
                                        value = item.AF;
                                        break;
                                    case ("QD"):
                                        value = item.QD;
                                        break;
                                    default:
                                        value = "NaN";
                                        break;
                                }
                                if (value == "NaN")
                                {
                                    rval.AddMissing(t);
                                }
                                else
                                {
                                    rval.Add(item.Date, Convert.ToDouble(value));
                                }
                            }
                        }
                        break;
                    case DataType.ALC:
                        var jsonResponseALC = IdwrApiQuerySiteDataALC(station, yearlist);
                        foreach (var item in jsonResponseALC)
                        {
                            var t = DateTime.Parse(item.Date);
                            if (t >= t1.Date && t <= t2.Date)
                            {
                                string value = "";
                                switch (parameter)
                                {
                                    case ("NATQ"):
                                        value = item.NATQ;
                                        break;
                                    case ("ACTQ"):
                                        value = item.ACTQ;
                                        break;
                                    case ("STRQ"):
                                        value = item.STRQ;
                                        break;
                                    case ("GANQ"):
                                        value = item.GANQ;
                                        break;
                                    case ("EVAP"):
                                        value = item.EVAP;
                                        break;
                                    case ("TOTEVAP"):
                                        value = item.TOTEVAP;
                                        break;
                                    case ("STORACC"):
                                        value = item.STORACC;
                                        break;
                                    case ("TOTACC"):
                                        value = item.TOTACC;
                                        break;
                                    case ("CURSTOR"):
                                        value = item.CURSTOR;
                                        break;
                                    case ("DIV"):
                                        value = item.DIV;
                                        break;
                                    case ("TOTDIVVOL"):
                                        value = item.TOTDIVVOL;
                                        break;
                                    case ("STORDIV"):
                                        value = item.STORDIV;
                                        break;
                                    case ("STORDIVVOL"):
                                        value = item.STORDIVVOL;
                                        break;
                                    case ("STORBAL"):
                                        value = item.STORBAL;
                                        break;
                                    default:
                                        value = "NaN";
                                        break;
                                }
                                if (value == "NaN")
                                {
                                    rval.AddMissing(t);
                                }
                                else
                                {
                                    rval.Add(item.Date, Convert.ToDouble(value));
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
            catch
            {
                return rval;
            }

            return rval;
        }

        private string YearsToQuery(string station, DateTime t1, DateTime t2)
        {
            var rval = "";
            if (t1 == TimeSeriesDatabase.MinDateTime || t2 == TimeSeriesDatabase.MaxDateTime)
            {
                // assume we want all available data
                var availYears = IdwrApiQuerySiteYears(station, datatype);
                rval = string.Join(",", availYears);
            }
            else
            {
                // get years from t1 to t2
                var years = Enumerable.Range(t1.Year, t2.Year - t1.Year + 1);
                rval = string.Join(",", years);
            }

            return rval;
        }

        private static List<RiverItems> IdwrApiQueryRiverList()
        {
            // Working API Call
            //https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting/RiverSystems

            var request = new RestRequest("RiverSystems/", Method.GET);
            IRestResponse restResponse = IDWRDailySeries.idwrClient.Execute(request);
            return JsonConvert.DeserializeObject<List<RiverItems>>(restResponse.Content);
        }

        private static List<RiverSite> IdwrApiQueryRiverSites(string riverItem)
        {
            // Working API Call
            //https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting/SitesByRiver?river=BAR

            var request = new RestRequest("SitesByRiver?", Method.GET);
            request.AddParameter("river", riverItem);
            IRestResponse restResponse = IDWRDailySeries.idwrClient.Execute(request);
            return JsonConvert.DeserializeObject<List<RiverSite>>(restResponse.Content);
        }

        private static List<SiteInfo> IdwrApiQuerySiteInfo(string riverSite)
        {
            // Working API Call
            //https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting/SiteDetails?sitelist=10039500

            var request = new RestRequest("SiteDetails?", Method.GET);
            request.AddParameter("sitelist", riverSite);
            IRestResponse restResponse = IDWRDailySeries.idwrClient.Execute(request);
            return JsonConvert.DeserializeObject<List<SiteInfo>>(restResponse.Content);
        }

        private static List<string> IdwrApiQuerySiteYears(string riverSite, DataType dataType)
        {
            // Working API Call
            //https://research.idwr.idaho.gov/apps/Shared/WaterServices/Accounting/HistoryAvailableYearsBySiteId?siteid=10039500&yeartype=CY&f=json

            var request = new RestRequest("HistoryAvailableYearsBySiteId?", Method.GET);
            if (dataType == DataType.ALC)
            {
                request = new RestRequest("ModelAvailableYearsBySiteId?", Method.GET);
            }
            request.AddParameter("siteid", riverSite);
            request.AddParameter("yeartype", "CY");
            request.AddParameter("f", "json");
            IRestResponse restResponse = IDWRDailySeries.idwrClient.Execute(request);
            return JsonConvert.DeserializeObject<List<string>>(restResponse.Content);
        }

        /// <summary>
        /// Get all IDWR River Systems from Web API
        /// </summary>
        /// <returns></returns>
        public static DataTable GetIdwrRiverSystems()
        {
            var dTab = new DataTable();
            var data = IdwrApiQueryRiverList();
            dTab.Columns.Add("River", typeof(string));
            dTab.Columns.Add("Name", typeof(string));
            dTab.Columns.Add("WD", typeof(string));
            foreach (var item in data)
            {
                var dRow = dTab.NewRow();
                dRow["River"] = item.River;
                dRow["Name"] = item.Name;
                dRow["WD"] = item.WD;
                dTab.Rows.Add(dRow);
            }
            return dTab;
        }


        /// <summary>
        /// Get IDWR Sites from Web API given IDWR River System
        /// </summary>
        /// <param name="riverItem"></param>
        /// <returns></returns>
        public static DataTable GetIdwrRiverSites(string riverItem)
        {
            var dTab = new DataTable();
            var data = IdwrApiQueryRiverSites(riverItem);
            dTab.Columns.Add("SiteID", typeof(string));
            dTab.Columns.Add("SiteType", typeof(string));
            dTab.Columns.Add("SiteName", typeof(string));
            dTab.Columns.Add("HSTCount", typeof(string));
            dTab.Columns.Add("ALCCount", typeof(string));
            dTab.Columns.Add("SiteLabel", typeof(string));
            foreach (var item in data)
            {
                var dRow = dTab.NewRow();
                dRow["SiteID"] = item.SiteID;
                dRow["SiteType"] = item.SiteType;
                dRow["SiteName"] = item.SiteName;
                dRow["HSTCount"] = item.HSTCount;
                dRow["ALCCount"] = item.ALCCount;
                dRow["SiteLabel"] = item.SiteName + " (" + item.SiteID + ")";
                dTab.Rows.Add(dRow);
            }
            return dTab;
        }


        /// <summary>
        /// Get IDWR Site Info from Web API given IDWR Site ID
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public static DataTable GetIdwrSiteInfo(string siteID, Reclamation.TimeSeries.IDWR.DataType dType)
        {
            var dTab = new DataTable();
            dTab.Columns.Add("SiteID", typeof(string));
            dTab.Columns.Add("SiteType", typeof(string));
            dTab.Columns.Add("StationName", typeof(string));
            dTab.Columns.Add("FullName", typeof(string));
            dTab.Columns.Add("Years", typeof(string));
            var sInfo = IdwrApiQuerySiteInfo(siteID);
            var sYears = IdwrApiQuerySiteYears(siteID, dType);// DataType.HST);
            var dRow = dTab.NewRow();
            dRow["SiteID"] = sInfo[0].SiteId;
            dRow["SiteType"] = sInfo[0].SiteType;
            dRow["StationName"] = sInfo[0].StationName;
            dRow["FullName"] = sInfo[0].FullName;
            dRow["Years"] = string.Join(",", sYears);
            dTab.Rows.Add(dRow);
            return dTab;
        }
    }
}
