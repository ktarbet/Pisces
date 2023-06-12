﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Reclamation.TimeSeries.Hydromet;
using Reclamation.Core;

namespace Reclamation.TimeSeries.Forms.Hydromet
{
    public partial class ServerSelection : UserControl
    {
        public string CustomIP 
        { 
            get { return this.textBoxCustomSource.Text; } 
            set { this.textBoxCustomSource.Text = value; } 
        }

        public ServerSelection()
        {
            InitializeComponent();
            ReadSettings();
        }

        private void SaveToUserPref()
        {
            if (this.radioButtonPnHydromet.Checked)
            {
                UserPreference.Save("HydrometServer", HydrometHost.PN.ToString());
            }
            else if (this.radioButtonBoiseLinux.Checked)
            {
                UserPreference.Save("HydrometServer", HydrometHost.PNLinux.ToString());
            }
            else if (this.radioButtonYakHydromet.Checked)
            {
                UserPreference.Save("HydrometServer", HydrometHost.Yakima.ToString());
            }
            else if (this.radioButtonGP.Checked)
            {
                UserPreference.Save("HydrometServer", HydrometHost.GreatPlains.ToString());
            }
            else if (this.radioButtonYakLinux.Checked)
            {
                UserPreference.Save("HydrometServer", HydrometHost.YakimaLinux.ToString());
            }

            
            UserPreference.Save("TimeSeriesDatabaseName", this.textBoxDbName.Text);
        }

        private void ReadSettings()
        {
            var svr = HydrometInfoUtility.HydrometServerFromPreferences();

            // retiring PN 
            if (svr == HydrometHost.PNLinux || svr == HydrometHost.PN)
            {
                this.radioButtonBoiseLinux.Checked = true;
            }
            else if (svr == HydrometHost.Yakima)
            {
                this.radioButtonYakHydromet.Checked = true;
            }
            else if (svr == HydrometHost.GreatPlains)
            {
                this.radioButtonGP.Checked = true;
            }
            else if (svr == HydrometHost.YakimaLinux)
            {
                this.radioButtonYakLinux.Checked = true;
            }

            Boolean.TryParse(UserPreference.Lookup("HydrometCustomServerChecked", ""), out bool customSourceChecked);
            this.checkBoxCustomSource.Checked = customSourceChecked;
            CustomIP = UserPreference.Lookup("HydrometCustomServer", "");

            this.textBoxDbName.Text = UserPreference.Lookup("TimeSeriesDatabaseName", "timeseries");
        }

        private void serverChanged(object sender, EventArgs e)
        {
            SaveToUserPref();
        }

        private void checkBoxCustomSource_CheckedChanged(object sender, EventArgs e)
        {
            UserPreference.Save("HydrometCustomServerChecked", this.checkBoxCustomSource.Checked.ToString());
        }

        private void textBoxCustomSource_TextChanged(object sender, EventArgs e)
        {
            UserPreference.Save("HydrometCustomServer", CustomIP);
        }
    }
}
