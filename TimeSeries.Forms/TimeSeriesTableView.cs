using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Reclamation.Core;

namespace Reclamation.TimeSeries.Forms
{
  /// <summary>
  /// TimeSeriesTableView allows a user to view and edit time series
  /// data in a table format.
  /// </summary>
  public class TimeSeriesTableView : System.Windows.Forms.UserControl
  {
    //private bool isReadOnly;
    private DataTable table;
    private System.Windows.Forms.ToolStrip toolBar1;
    private System.Windows.Forms.ToolStripButton toolBarButtonCopy;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ToolStripButton toolBarButtonSave;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private DataGridView dataGridView1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem copyToolStripMenuItem;
    private ToolStripButton toolBarButtonExcel;
    private ToolStripMenuItem pasteToolStripMenuItem;
    private ToolStripButton toolBarButtonPaste;
    private ToolStripMenuItem interpolateToolStripMenuItem;
    private ToolStripMenuItem smoothToolStripMenuItem;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusMessage;
    private TableLayoutPanel tableLayoutPanel1;
    private System.ComponentModel.IContainer components;

    public TimeSeriesTableView()
    {
      InitializeComponent();
    }

    private bool _hasDataChanged;

    public bool HasDataChanged
    {
      get { return _hasDataChanged; }
    }

    SeriesList m_seriesList;
    public SeriesList SeriesList
    {
      get { return m_seriesList; }
      set
      {
        m_seriesList = value;


      }
    }

    public void Draw()
    {
      table = m_seriesList.ToDataTable(m_seriesList.Count > 1);

      // m_seriesList.

      table.RowChanged += new DataRowChangeEventHandler(table_RowChanged);
      table.RowDeleted += new DataRowChangeEventHandler(table_RowDeleted);
      dataGridView1.DataSource = null;
      dataGridView1.DataSource = table;
      this.dataGridView1.ReadOnly = SeriesList.ReadOnly || SeriesList.Count > 1;
      this.dataGridView1.AllowUserToAddRows = !SeriesList.ReadOnly;
      toolBarButtonSave.Enabled = !SeriesList.ReadOnly;



      dataGridView1.Columns[0].DefaultCellStyle.Format = m_seriesList.DateFormat;
      if (m_seriesList.Count == 1
          && dataGridView1.Columns.Count > 1
          && dataGridView1.Columns[1].HeaderText == "value"
          )
        dataGridView1.Columns[1].HeaderText = m_seriesList[0].Name;

    }


    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.table = null;
        this.dataGridView1.DataSource = null;
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code
    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeSeriesTableView));
      this.toolBar1 = new System.Windows.Forms.ToolStrip();
      this.toolBarButtonCopy = new System.Windows.Forms.ToolStripButton();
      this.toolBarButtonPaste = new System.Windows.Forms.ToolStripButton();
      this.toolBarButtonSave = new System.Windows.Forms.ToolStripButton();
      this.toolBarButtonExcel = new System.Windows.Forms.ToolStripButton();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.interpolateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.smoothToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolBar1
      // 
      this.toolBar1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.toolBarButtonCopy,
            this.toolBarButtonPaste,
            this.toolBarButtonSave,
            this.toolBarButtonExcel});
      //this.toolBar1.DropDownArrows = true;
      this.toolBar1.ImageList = this.imageList1;
      this.toolBar1.Location = new System.Drawing.Point(0, 0);
      this.toolBar1.Name = "toolBar1";
      this.toolBar1.Size = new System.Drawing.Size(460, 36);
      this.toolBar1.TabIndex = 4;
      // 
      // toolBarButtonCopy
      // 
      this.toolBarButtonCopy.ImageIndex = 1;
      this.toolBarButtonCopy.Name = "toolBarButtonCopy";
      this.toolBarButtonCopy.ToolTipText = "copy to clipboard";
      this.toolBarButtonCopy.Click += ToolBarButtonCopy_Click;
      // 
      // toolBarButtonPaste
      // 
      this.toolBarButtonPaste.ImageIndex = 4;
      this.toolBarButtonPaste.Name = "toolBarButtonPaste";
      this.toolBarButtonPaste.Click += ToolBarButtonPaste_Click;
      // 
      // toolBarButtonSave
      // 
      this.toolBarButtonSave.ImageIndex = 0;
      this.toolBarButtonSave.Name = "toolBarButtonSave";
      this.toolBarButtonSave.ToolTipText = "save your changes";
      this.toolBarButtonSave.Click += ToolBarButtonSave_Click;
      // 
      // toolBarButtonExcel
      // 
      this.toolBarButtonExcel.ImageIndex = 2;
      this.toolBarButtonExcel.Name = "toolBarButtonExcel";
      this.toolBarButtonExcel.ToolTipText = "open with excel";
      this.toolBarButtonExcel.Click += ToolBarButtonExcel_Click;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "");
      this.imageList1.Images.SetKeyName(1, "");
      this.imageList1.Images.SetKeyName(2, "EXCEL_257.ico");
      this.imageList1.Images.SetKeyName(3, "excelsmall.bmp");
      this.imageList1.Images.SetKeyName(4, "PasteHH.bmp");
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.Filter = "CSV (comma delimited) (*.csv)|*.csv";
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(3, 3);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new System.Drawing.Size(454, 462);
      this.dataGridView1.TabIndex = 6;
      this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
      this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.interpolateToolStripMenuItem,
            this.smoothToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(190, 92);
      // 
      // copyToolStripMenuItem
      // 
      this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
      this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
      this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
      this.copyToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.copyToolStripMenuItem.Text = "Copy";
      this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
      // 
      // pasteToolStripMenuItem
      // 
      this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
      this.pasteToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.pasteToolStripMenuItem.Text = "Paste";
      this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
      // 
      // interpolateToolStripMenuItem
      // 
      this.interpolateToolStripMenuItem.Name = "interpolateToolStripMenuItem";
      this.interpolateToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.interpolateToolStripMenuItem.Text = "&Interpolate";
      this.interpolateToolStripMenuItem.Click += new System.EventHandler(this.interpolateToolStripMenuItem_Click);
      // 
      // smoothToolStripMenuItem
      // 
      this.smoothToolStripMenuItem.Name = "smoothToolStripMenuItem";
      this.smoothToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.smoothToolStripMenuItem.Text = "Smooth preserve sum";
      this.smoothToolStripMenuItem.Click += new System.EventHandler(this.smoothToolStripMenuItem_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusMessage});
      this.statusStrip1.Location = new System.Drawing.Point(0, 504);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(460, 22);
      this.statusStrip1.SizingGrip = false;
      this.statusStrip1.TabIndex = 7;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripStatusMessage
      // 
      this.toolStripStatusMessage.Name = "toolStripStatusMessage";
      this.toolStripStatusMessage.Size = new System.Drawing.Size(0, 17);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 468F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(460, 468);
      this.tableLayoutPanel1.TabIndex = 8;
      // 
      // TimeSeriesTableView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.toolBar1);
      this.Name = "TimeSeriesTableView";
      this.Size = new System.Drawing.Size(460, 526);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private void ToolBarButtonExcel_Click(object sender, EventArgs e)
    {
      buttonClick(toolBarButtonExcel);
    }

    private void ToolBarButtonSave_Click(object sender, EventArgs e)
    {
      buttonClick(toolBarButtonSave);
    }

    private void ToolBarButtonPaste_Click(object sender, EventArgs e)
    {
      buttonClick(toolBarButtonPaste);
    }

    private void ToolBarButtonCopy_Click(object sender, EventArgs e)
    {
      buttonClick(toolBarButtonCopy);
    }
    #endregion

    private void buttonClick(ToolStripButton b)
    {
      Cursor = Cursors.WaitCursor;
      try
      {
        if (b == toolBarButtonCopy)
        {// copy to clipboard.

          if (dataGridView1.SelectedCells.Count > 0)
          {

            dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

            Clipboard.SetDataObject(
                    this.dataGridView1.GetClipboardContent());
          }

        }
        else if (b == toolBarButtonSave)
        {

          if (m_seriesList.ReadOnly || m_seriesList.Count > 1)
            return;

          m_seriesList.Save();

        }
        else if (b == toolBarButtonExcel)
        {
          string tmpFilename = FileUtility.GetTempFileName(".csv");

          //DataTableOutput.Write(this.table, tmpFilename, false);
          DataTableOutput.Write(this.table, tmpFilename, false, false);
          System.Diagnostics.Process.Start(tmpFilename);
        }
        else if (b == toolBarButtonPaste)
        {
          this.pasteToolStripMenuItem_Click(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
      Cursor = Cursors.Default;
    }

    //public event EventHandler SaveInDatabase;



    /// <summary>
    /// Fires when user makes an edit.
    /// </summary>
    public event EventHandler Changed;

    private void table_RowChanged(object sender, DataRowChangeEventArgs e)
    {
      _hasDataChanged = true;
      if (Changed != null)
        Changed(this, e);
    }

    private void table_RowDeleted(object sender, DataRowChangeEventArgs e)
    {
      _hasDataChanged = true;
      if (Changed != null)
        Changed(this, e);
    }

    private bool _hideFlags = false;

    public bool HideFlagColumn
    {
      get { return _hideFlags; }
      set { _hideFlags = value; }
    }


    private void copyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Clipboard.SetDataObject(
                          this.dataGridView1.GetClipboardContent());

    }

    private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      MessageBox.Show(e.Exception.Message);
    }





    public void Clear()
    {
      m_seriesList = new SeriesList();
      Draw();
    }

    private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      PasteFromClipBoard();
    }
    public void PasteFromClipBoard()
    {
      try
      {
        DataGridViewCell cell = this.dataGridView1.CurrentCell;

        if (cell != null)
        {
          if (cell.ColumnIndex == 0)
          {
            MessageBox.Show("Pasting in the Date column is not supported");
            return;
          }
        }
        DataGridViewUtility u = new DataGridViewUtility(this.dataGridView1);
        u.PasteFromClipboard();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void interpolateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var interpolate = new DataGridSelection(this.dataGridView1);

      interpolate.InterpolateAndFlag(PointFlag.Interpolated);
    }

    private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var sel = new DataGridSelection(this.dataGridView1);
      if (sel.ValidInterpolationSelection && this.SeriesList.Count == 1)
      {
        Series s = this.SeriesList[0];
        SeriesRange sr = new SeriesRange(s, sel.T1, sel.T2);
        sr.SmoothPreservingSum();
      }
    }

    private void dataGridView1_SelectionChanged(object sender, EventArgs e)
    {
      var sel = new DataGridSelection(dataGridView1);
      toolStripStatusMessage.Text = sel.ComputeSelectedStats();
    }

  }
}
