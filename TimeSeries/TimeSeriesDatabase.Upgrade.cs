﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Reclamation.TimeSeries
{
    public partial class TimeSeriesDatabase
    {

        private void UpgradeV1ToV2()
        {
            if (m_server.TableExists("piscesinfo") )
                
            {
                InitSettings();

                if (m_settings.GetDBVersion() != 1)
                    return;

                // save the old catalog in memory, create tables, 
                //, and translate data to new database.

                DataTable oldCatalog = m_server.Table("seriescatalog", "Select * from seriescatalog");

                // delete old series catalog...
                m_server.RunSqlCommand("drop table seriescatalog");

                // Create new Tables
                CreateTablesWithSQL();

                // translate.
                string[] oldColumnNames = {"sitedatatypeid","sitename", "source" };
                string[] newColumnName = {"id", "siteid","iconname" };

                var sc = this.GetSeriesCatalog();

                for (int i = 0; i < oldCatalog.Rows.Count; i++)
                {
                    var newRow = sc.NewRow();

                    for (int c = 0; c < sc.Columns.Count; c++)
                    {
                        string new_cn = sc.Columns[c].ColumnName;
                        string old_cn = new_cn;
                        int idx = oldCatalog.Columns.IndexOf(new_cn);

                        if( idx < 0) // look for mapping
                        {
                            idx = Array.IndexOf(newColumnName, new_cn);
                            if (idx >= 0)
                            {
                                old_cn = oldColumnNames[idx];
                            }
                            else
                            {// skip this column
                                continue;
                            }
                        }

                        newRow[new_cn] = oldCatalog.Rows[i][old_cn];
                    }
                    
                    sc.Rows.Add(newRow);
                }
                m_server.SaveTable(sc);
            }
        }



    }
}
