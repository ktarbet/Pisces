﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Reclamation.TimeSeries;
using Reclamation.Core;

namespace Reclamation.Riverware
{
    /// <summary>
    /// Exports daily timeseries data from a Pisces
    /// database into text files, ready to import into
    /// RiverWare through a DMI.
    /// </summary>
    public class PiscesDMI
    {
        ControlFile m_controlFile;
        string m_dbName;
        List<string> m_seriesName;
        List<string> m_fileName;
        DateTime m_t1;
        DateTime m_t2;

        public PiscesDMI(string dbName, string controlFilename, DateTime t1, DateTime t2)
        {
            m_controlFile = new ControlFile(controlFilename);
            m_dbName = dbName;
            m_t1 = t1;
            m_t2 = t2;
        }

        public void ExportTextFiles()
        {
            ParseControlFile();
            ReadFromPisces();
        }

        private void ReadFromPisces()
        {
            Logger.WriteLine("opening " + m_dbName);
            SQLiteServer svr = new SQLiteServer(m_dbName);
            TimeSeriesDatabase db = new TimeSeriesDatabase(svr);

            SeriesList list = new SeriesList();

            for (int i = 0; i < m_seriesName.Count; i++)
            {
                Logger.WriteLine("looking for series '" + m_seriesName[i] + "'");
                var s = db.GetSeriesFromName(m_seriesName[i]);
                if (s != null)
                {
                    s.Read(m_t1, m_t2);
                    list.Add(s);
                }
                else
                {
                    throw new Exception("unable to find series '" + m_seriesName[i] + "' in pisces database '" + m_dbName + "'");
                }
            }
            
            WriteToRiverwareFiles(list);
        }

        private void WriteToRiverwareFiles(SeriesList sl)
        {
            Logger.WriteLine("saving " + sl.Count + " files to riverware format");
            for (int i = 0; i < sl.Count; i++)
            {
                Series s = sl[i];
                if (s.Count <= 0)
                    continue;
                
                StreamWriter sw = new StreamWriter(m_fileName[i]);
                sw.WriteLine("# this data was imported from " + m_dbName + " on " + DateTime.Now.ToString());
                sw.WriteLine("# series name: " + m_seriesName[i]);

                // Check Series' time interval
                if (s.TimeInterval == TimeInterval.Monthly)
                {
                    // Monthly dates are read as EOM midnight values - David Neumann
                    sw.WriteLine("start_date: " + m_t1.EndOfMonth().ToString("yyyy-MM-dd") + " 24:00");
                }
                else if (s.TimeInterval == TimeInterval.Daily)
                {
                    // Daily dates are read as EOD midnight values
                    sw.WriteLine("start_date: " + m_t1.ToString("yyyy-MM-dd") + " 24:00");
                }
                else
                {
                    Console.WriteLine(s.TimeInterval.ToString() + " not supported in RiverWareDMI");
                    throw new NotImplementedException(s.TimeInterval.ToString() + " not supported in RiverWareDMI");
                }

                DateTime t = m_t1;
                while (t <= m_t2)
                {
                    int idx = -1;
                    if (s.TimeInterval == TimeInterval.Monthly)
                    {
                        idx = GetMonthIdx(s, t);
                    }
                    else
                    {
                        idx = s.IndexOf(t);
                    }

                    if (idx < 0)
                    {
                        sw.WriteLine("NaN");
                    }
                    else
                    {
                        var pt = s[idx];
                        if (pt.IsMissing)
                        {
                            Console.WriteLine(m_seriesName[i] + " Error: missing data " + pt.ToString());
                            sw.WriteLine("NaN");
                        }
                        else
                        {
                            sw.WriteLine(pt.Value);
                        }
                    }

                    t = s.IncremetDate(t);
                }

                sw.Close();
            }
        }

        private int GetMonthIdx(Series s, DateTime t)
        {
            var rval = -1;

            var startOfMonth = new DateTime(t.Year, t.Month, 1);
            var endOfMonth = new DateTime(t.Year, t.Month, DateTime.DaysInMonth(t.Year, t.Month));

            var s2 = s.Subset(startOfMonth, endOfMonth);
            
            if (s2.Count > 1)
            {
                var msg = string.Format("Error: duplicate monthly values found in {0:MMM-yyyy}", t);
                throw new DataException(msg);
            }

            if (s2.Count == 1)
            {
                rval = s.IndexOf(s2[0].DateTime);
            }
            return rval;
        }

        private void ParseControlFile()
        {
            m_fileName = new List<string>();
            m_seriesName = new List<string>();

            string fname = "";
            string sname = "";
            for (int i = 0; i < m_controlFile.Length; i++)
            {
                if (!m_controlFile.TryParse(i, "file", out fname) ||
                    !m_controlFile.TryParse(i, "series_name", out sname))
                {
                    continue;
                }
                else
                {
                    m_fileName.Add(fname);
                    m_seriesName.Add(sname);
                }
            }
        }
    }
}
