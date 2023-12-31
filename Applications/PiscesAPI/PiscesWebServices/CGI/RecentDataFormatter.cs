﻿using System.Data;
using Reclamation.TimeSeries;
using System;

namespace PiscesWebServices.CGI
{
    /// <summary>
    /// Prints the 'last' or most recent data
    /// </summary>
    internal class RecentDataFormatter : Formatter
    {

        public static DataTable CustomNames { get; set; }

        public RecentDataFormatter(TimeInterval interval,bool printFlags) : base(interval, printFlags)
        {
        }

        public override void PrintDataTable(SeriesList list, DataTable table)
        {

            for (int idx = 0; idx < list.Count; idx++)
            {
                var s = list[idx];
                var tn = s.Table.TableName;
                var x = s.SiteID.ToUpper().PadRight(8) + " # ";
                WriteLine(x+GetLast(table, tn));
            }

        }

        /// <summary>
        /// get most recent value for this table, or 'missing'
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        private string GetLast(DataTable table, string tablename)
        {
            TimeSeriesName tn = new TimeSeriesName(tablename);
            var x = table.Select("tablename ='" + tablename + "'","datetime");
            if (x.Length > 0)
            {
              var r = x[x.Length - 1];
                var t = DateTime.Parse(r["datetime"].ToString());
                var val = Double.Parse(r["value"].ToString());
                Point p = new Point(t, val, r["flag"].ToString());
                string rval = p.DateTime.ToString("MMM dd  HH:mm") + " "
                 + tn.pcode.ToUpper() + " " + p.Value.ToString("F2").PadLeft(11) + p.Flag;
                return rval;
            }

            return " ".PadRight(14)+ tn.pcode.ToUpper().PadRight(7)+"Missing";
        }

        public override string FormatDate(object o)
        {
            return "";
        }

        public override string FormatFlag(object o)
        {
            return "";
        }

        public override string FormatNumber(object o)
        {
            return "";
        }

        public override void PrintRow(string t0, string[] vals, string[] flags)
        {
        }

        public override void WriteSeriesHeader(SeriesList list)
        {
            WriteLine("<PRE>");
        }

        public override void WriteSeriesTrailer()
        {
        }
    }
}