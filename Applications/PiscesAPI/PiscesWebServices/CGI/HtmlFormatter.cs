﻿using Reclamation.Core;
using Reclamation.TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PiscesWebServices.CGI
{
    class HtmlFormatter: Formatter
    {
        bool m_printHeader = true;
        bool m_printDescriptions;
        string m_title;
        public HtmlFormatter(TimeInterval interval, bool printFlags,
            bool printHeader, bool printDescriptions, string title)
             : base(interval, printFlags)
         {
             m_printHeader = printHeader;
             m_printDescriptions = printDescriptions;
            m_title = title;
         }

         public override void PrintRow(string t0, string[] vals, string[] flags)
         {
             StringBuilder sb = new StringBuilder(vals.Length * 8);
             sb.Append("<tr>");
             sb.Append("<td>" + t0 + "</td>");
             for (int i = 0; i < vals.Length; i++)
             {
                 sb.Append("<td>" + vals[i] + "</td>");
                 if( PrintFlags)
                     sb.Append("<td>" + flags[i] + "</td>");

             }
             sb.Append("</tr>");
             WriteLine(sb.ToString());
         }

         public override string FormatNumber(object o)
         {
             var rval = "";
             if (o == DBNull.Value || o.ToString() == "")
                 rval = "";//.PadLeft(11);
             else
                 rval = Convert.ToDouble(o).ToString("F02");
             return rval;
         }


         public override string FormatFlag(object o)
         {
             if (o == DBNull.Value)
                 return "";
             else
                 return o.ToString();
         }

        public override string FormatDate(object o)
         {
             var rval = "";
             var t = Convert.ToDateTime(o);
             if (Interval == TimeInterval.Irregular || Interval == TimeInterval.Hourly)
                 rval = t.ToString("yyyy-MM-dd HH:mm");
             else
                 rval = t.ToString("yyyy-MM-dd");
             return rval;
         }
        public override void WriteSeriesHeader(SeriesList list)
        {
            if( m_printDescriptions)
            {
                HydrometWebUtility.PrintDisclamerLink();

                WriteLine("<h3><h3>" + m_title + "</h3></h3>");

                // LRS FB = Clear Lake Dam, CA - Reservoir Elevation - Feet
                // LRS FB2 = Clear Lake Dam, CA - Forebay Elevation below fish screens - Feet
                foreach (var s in list)
	            {
                    Logger.WriteLine(s.Name+" count = "+s.Count);
                    var str = s.SiteID + " " + s.Parameter + " = "
                     + s.SiteDescription() + " "
                     + s.SeriesDescription() + "<br/>";
                    WriteLine(str);
                }

            }
            WriteLine("<table  border=1>");

            if (m_printHeader)
            {
                WriteLine("<tr>");
                WriteLine("<th>DateTime</th>");
                for (int i = 0; i < list.Count; i++)
                {
                    TimeSeriesName tn = new TimeSeriesName(list[i].Table.TableName);
                    WriteLine("<th>" + tn.siteid + "_" + tn.pcode + "</th>");
                    if( PrintFlags )
                        WriteLine("<th>flag</th>");
                }
                WriteLine("</tr>");
            }

        }

        public override void WriteSeriesTrailer()
        {
            WriteLine("</table>");
        }
    }
}
