using System;
using System.IO;
using System.Collections.Generic;
using Reclamation.Core;
namespace Reclamation.TimeSeries.RiverWare
{
    public class RiverWareTree
    {

        /**********************************
          *
          * Create a tree file that is usable by pisces
          *
          * input :  riverware rdf file
          * output : comma seperated tree file.
          *
          *
         // example portion of input file.
         (this input file is using snapshots in riverware)
         -----------------------------------
         object_type: SnapShotObj
         object_name: Most Likely 2
         slot_type: SeriesSlot
         slot_name: Andrews Gage 12447390 at RM 3_5_Gage Outflow
         END_SLOT_PREAMBLE
         units: cfs

         // example output.  There is no nesting of tree levels for now.
         RiverwareName,Description,RiverwareDataType,Level,Units
         Riverware Results,,,0,
         Yakima River at Parker PARW,Yakima River at Parker PARW,Gage Outflow,1,cfs
         Yakima River at Grandview,Yakima River at Grandview,Gage Inflow,1,cfs
         ...

          **********************************/
        public static void AddRiverWareFileToDatabase(string rdfFilename, PiscesFolder parent, 
            TimeSeriesDatabase db)
        {
            TextFile tf = new TextFile(rdfFilename);

            int number_of_runs = LookupNumberOfRuns(tf);
            PiscesFolder folder = parent;
            if (number_of_runs == 1)
                folder = db.AddFolder(parent, Path.GetFileNameWithoutExtension(rdfFilename));
            
            int idxObjectName = tf.IndexOf("object_name:", 0);
            int idxSlotName = tf.IndexOf("slot_name:", idxObjectName);
            int idxUnits = tf.IndexOf("units:", idxSlotName);
            
            var objectList = new List<string>(); //list to avoid duplicates in tree
            Performance p1 = new Performance();
            Performance p2 = new Performance();
            p2.Pause();
            int counter = 0;

            db.SuspendTreeUpdates();
            var sc = db.GetSeriesCatalog();
            Dictionary<string, int> objTypeID = new Dictionary<string, int>();
            Dictionary<string, int> objNameID = new Dictionary<string, int>();
            while (idxObjectName < tf.Length && idxObjectName > 0)
            {
                string object_type = tf[idxObjectName - 1].Substring(13);
                string object_name = tf[idxObjectName].Substring(13);
                string slot_name = tf[idxSlotName].Substring(11);
                string units = tf[idxUnits].Substring(6).Trim();

                string tag = object_name + ":" + slot_name;
                if (!objectList.Contains(tag))
                {
                    int scenarioNumber = -1;
                    if (number_of_runs > 1)
                        scenarioNumber = 1;

                    RiverWareSeries s;
                    if (object_type == "SnapShotObj")
                        s = new RiverWareSeries(rdfFilename, "", slot_name, scenarioNumber, true, units);
                    else
                        s = new RiverWareSeries(rdfFilename, object_name, slot_name, scenarioNumber, false, units);
                    s.Units = units;
                    s.ConnectionString = ConnectionStringUtility.MakeFileNameRelative(s.ConnectionString, db.DataSource);
                    p2.Continue();


                    if (object_type.Contains("Reservoir"))
                    {
                        object_type = "Reservoir";
                    }
                    else if (object_type.Contains("Reach"))
                    {
                        object_type = "Reach";
                    }
                    else if (object_type.Contains("Diversion"))
                    {
                        object_type = "Diversion";
                    }
                    else if (object_type.Contains("Canal"))
                    {
                        object_type = "Canal";
                    }

                    int id = sc.NextID();
                    if (!sc.FolderExists(object_type, folder.ID))
                    {
                        objTypeID.Add(object_type, id);
                        sc.AddFolder(object_type, id, folder.ID);
                        id++;
                    }
                    if (!sc.FolderExists(object_name, objTypeID[object_type]))
                    {
                        objNameID.Add(object_name, id);
                        sc.AddFolder(object_name, id, objTypeID[object_type]);
                        id++;
                    }
                    sc.AddSeriesCatalogRow(s, id, objNameID[object_name], "");
                    objectList.Add(tag);
                }
                
                idxObjectName = tf.IndexOf("object_name:", idxUnits);
                if (idxObjectName > 0)
                {
                    idxSlotName = tf.IndexOf("slot_name:", idxObjectName);
                    idxUnits = tf.IndexOf("units:", idxSlotName);
                }

                counter++;
            }
            p1.Report("total");
            p2.Report("db.add()");
            //398.7732813 seconds elapsed. total
            //384.6792607 seconds elapsed. db.add()

            // disable tree refresh (doubles perf)
            // 255.9736646 seconds elapsed. total
            // 241.7702669 seconds elapsed. db.add()

            // implemented member ExternalDataSource
            //34.8756696 seconds elapsed. total
            //20.3753912 seconds elapsed. db.add()

            var convention = ImportRiverWare.ScenarioConvention.Default;
            if (number_of_runs > 1) // Multiple runs.
            {// show dialog to allow water year naming or traces
                var dlg = new ImportRiverWare();
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    convention = dlg.NamingConvention;
                }


                // Add Scenarios.
                var tblScen = db.GetScenarios();
                for (int i = 0; i < number_of_runs; i++)
                {
                    string name = $"Run{i}";
                    if (convention == ImportRiverWare.ScenarioConvention.ByYear)
                    {
                        name = (dlg.FirstYear + i ).ToString();
                    }
                    //string scenarioPath = ConnectionStringUtility.MakeFileNameRelative("FileName=" + item, DB.Filename);
                    tblScen.AddScenarioRow(name, true, $"ScenarioNumber={i + 1}", 0);
                }
                db.Server.SaveTable(tblScen);
            }
            db.Server.SaveTable(sc);
            db.ResumeTreeUpdates();
            db.RefreshFolder(parent);
            
        }

        private static int LookupNumberOfRuns(TextFile tf)
        {
            int number_of_runs = 1;
            //:53
            //number_of_runs:53
            int index = tf.IndexOf("number_of_runs:");
            if (index == -1)
            {
                Logger.WriteLine("Error: number_of_runs is not defined in file " +tf.FileName);
            }
            else
            {
                number_of_runs = Convert.ToInt32(tf[index].Substring(15));
            }
            return number_of_runs;
        }

    }
}
