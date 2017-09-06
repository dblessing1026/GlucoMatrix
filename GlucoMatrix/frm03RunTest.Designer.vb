<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm03RunTest
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim Title2 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim Title3 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim Title4 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim Title5 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim Title6 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblTestStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbLoopProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblLastIntervalTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblChannelAVoltage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblChannelACurrent = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblChannelBVoltage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lstCarrierQueue = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnAddToQueue = New System.Windows.Forms.Button()
        Me.txbxCarrierToAdd = New System.Windows.Forms.TextBox()
        Me.txbxLotToAdd = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRemoveItem = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtTestStart = New System.Windows.Forms.Label()
        Me.txtTimeSinceStart = New System.Windows.Forms.Label()
        Me.txtTimeSinceMove = New System.Windows.Forms.Label()
        Me.lblTestStart = New System.Windows.Forms.Label()
        Me.lblTimeSinceStart = New System.Windows.Forms.Label()
        Me.lblTimeSinceInjection = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtBath6MoveTo = New System.Windows.Forms.Label()
        Me.txtBath5MoveTo = New System.Windows.Forms.Label()
        Me.txtBath4MoveTo = New System.Windows.Forms.Label()
        Me.txtBath3MoveTo = New System.Windows.Forms.Label()
        Me.txtBath2MoveTo = New System.Windows.Forms.Label()
        Me.txtBath1MoveTo = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxRecipe = New System.Windows.Forms.ComboBox()
        Me.btnMoveCarriers = New System.Windows.Forms.Button()
        Me.btnStartTest = New System.Windows.Forms.Button()
        Me.tmrPeriodicUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1.SuspendLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblTestStatus, Me.ToolStripStatusLabel1, Me.pbLoopProgress, Me.lblLastIntervalTime, Me.lblChannelAVoltage, Me.lblChannelACurrent, Me.lblChannelBVoltage})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 706)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1008, 24)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblTestStatus
        '
        Me.lblTestStatus.Name = "lblTestStatus"
        Me.lblTestStatus.Size = New System.Drawing.Size(416, 19)
        Me.lblTestStatus.Spring = True
        Me.lblTestStatus.Text = "Waiting for initialization..."
        Me.lblTestStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.AutoSize = False
        Me.ToolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(100, 19)
        Me.ToolStripStatusLabel1.Text = "Reading Progress:"
        Me.ToolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbLoopProgress
        '
        Me.pbLoopProgress.Name = "pbLoopProgress"
        Me.pbLoopProgress.Size = New System.Drawing.Size(100, 18)
        Me.pbLoopProgress.Step = 1
        Me.pbLoopProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'lblLastIntervalTime
        '
        Me.lblLastIntervalTime.AutoSize = False
        Me.lblLastIntervalTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.lblLastIntervalTime.Name = "lblLastIntervalTime"
        Me.lblLastIntervalTime.Size = New System.Drawing.Size(150, 19)
        Me.lblLastIntervalTime.Text = "Last Interval (sec):"
        Me.lblLastIntervalTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblChannelAVoltage
        '
        Me.lblChannelAVoltage.AutoSize = False
        Me.lblChannelAVoltage.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.lblChannelAVoltage.Name = "lblChannelAVoltage"
        Me.lblChannelAVoltage.Size = New System.Drawing.Size(75, 19)
        Me.lblChannelAVoltage.Text = "CAV:"
        Me.lblChannelAVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblChannelACurrent
        '
        Me.lblChannelACurrent.AutoSize = False
        Me.lblChannelACurrent.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.lblChannelACurrent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblChannelACurrent.Name = "lblChannelACurrent"
        Me.lblChannelACurrent.Size = New System.Drawing.Size(75, 19)
        Me.lblChannelACurrent.Text = "CAC:"
        Me.lblChannelACurrent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblChannelBVoltage
        '
        Me.lblChannelBVoltage.AutoSize = False
        Me.lblChannelBVoltage.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.lblChannelBVoltage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblChannelBVoltage.Name = "lblChannelBVoltage"
        Me.lblChannelBVoltage.Size = New System.Drawing.Size(75, 19)
        Me.lblChannelBVoltage.Text = "CBV:"
        Me.lblChannelBVoltage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        ChartArea2.Name = "ChartArea2"
        ChartArea3.Name = "ChartArea3"
        ChartArea4.Name = "ChartArea4"
        ChartArea5.Name = "ChartArea5"
        ChartArea6.Name = "ChartArea6"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Me.Chart1.ChartAreas.Add(ChartArea2)
        Me.Chart1.ChartAreas.Add(ChartArea3)
        Me.Chart1.ChartAreas.Add(ChartArea4)
        Me.Chart1.ChartAreas.Add(ChartArea5)
        Me.Chart1.ChartAreas.Add(ChartArea6)
        Me.Chart1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.MaximumSize = New System.Drawing.Size(2000, 2000)
        Me.Chart1.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright
        Series1.ChartArea = "ChartArea1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(750, 706)
        Me.Chart1.TabIndex = 2
        Me.Chart1.TabStop = False
        Me.Chart1.Text = "Chart1"
        Title1.Alignment = System.Drawing.ContentAlignment.TopCenter
        Title1.DockedToChartArea = "ChartArea1"
        Title1.IsDockedInsideChartArea = False
        Title1.Name = "Title1"
        Title1.Text = "Bath 1"
        Title2.Alignment = System.Drawing.ContentAlignment.TopCenter
        Title2.DockedToChartArea = "ChartArea2"
        Title2.IsDockedInsideChartArea = False
        Title2.Name = "Title2"
        Title2.Text = "Bath 2"
        Title3.Alignment = System.Drawing.ContentAlignment.TopCenter
        Title3.DockedToChartArea = "ChartArea3"
        Title3.IsDockedInsideChartArea = False
        Title3.Name = "Title3"
        Title3.Text = "Bath 3"
        Title4.Alignment = System.Drawing.ContentAlignment.TopCenter
        Title4.DockedToChartArea = "ChartArea4"
        Title4.IsDockedInsideChartArea = False
        Title4.Name = "Title4"
        Title4.Text = "Bath 4"
        Title5.DockedToChartArea = "ChartArea5"
        Title5.IsDockedInsideChartArea = False
        Title5.Name = "Title5"
        Title5.Text = "Bath 5"
        Title6.DockedToChartArea = "ChartArea6"
        Title6.IsDockedInsideChartArea = False
        Title6.Name = "Title6"
        Title6.Text = "Bath 6"
        Me.Chart1.Titles.Add(Title1)
        Me.Chart1.Titles.Add(Title2)
        Me.Chart1.Titles.Add(Title3)
        Me.Chart1.Titles.Add(Title4)
        Me.Chart1.Titles.Add(Title5)
        Me.Chart1.Titles.Add(Title6)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.MinimumSize = New System.Drawing.Size(240, 708)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstCarrierQueue)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Panel1MinSize = 240
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Chart1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1008, 708)
        Me.SplitContainer1.SplitterDistance = 252
        Me.SplitContainer1.TabIndex = 4
        '
        'lstCarrierQueue
        '
        Me.lstCarrierQueue.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lstCarrierQueue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCarrierQueue.FullRowSelect = True
        Me.lstCarrierQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstCarrierQueue.Location = New System.Drawing.Point(3, 356)
        Me.lstCarrierQueue.Margin = New System.Windows.Forms.Padding(10)
        Me.lstCarrierQueue.MultiSelect = False
        Me.lstCarrierQueue.Name = "lstCarrierQueue"
        Me.lstCarrierQueue.Size = New System.Drawing.Size(244, 174)
        Me.lstCarrierQueue.TabIndex = 4
        Me.lstCarrierQueue.UseCompatibleStateImageBehavior = False
        Me.lstCarrierQueue.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Lot"
        Me.ColumnHeader1.Width = 100
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Carrier ID"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Move to bath:"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 80
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "UserID"
        Me.ColumnHeader4.Width = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnAddToQueue)
        Me.Panel3.Controls.Add(Me.txbxCarrierToAdd)
        Me.Panel3.Controls.Add(Me.txbxLotToAdd)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.btnRemoveItem)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(3, 530)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(244, 173)
        Me.Panel3.TabIndex = 3
        '
        'btnAddToQueue
        '
        Me.btnAddToQueue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddToQueue.Location = New System.Drawing.Point(91, 119)
        Me.btnAddToQueue.Name = "btnAddToQueue"
        Me.btnAddToQueue.Size = New System.Drawing.Size(120, 30)
        Me.btnAddToQueue.TabIndex = 5
        Me.btnAddToQueue.Text = "Add to Queue"
        Me.btnAddToQueue.UseVisualStyleBackColor = True
        '
        'txbxCarrierToAdd
        '
        Me.txbxCarrierToAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txbxCarrierToAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbxCarrierToAdd.Location = New System.Drawing.Point(91, 85)
        Me.txbxCarrierToAdd.MaxLength = 2
        Me.txbxCarrierToAdd.Name = "txbxCarrierToAdd"
        Me.txbxCarrierToAdd.Size = New System.Drawing.Size(45, 26)
        Me.txbxCarrierToAdd.TabIndex = 4
        '
        'txbxLotToAdd
        '
        Me.txbxLotToAdd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txbxLotToAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txbxLotToAdd.Location = New System.Drawing.Point(91, 51)
        Me.txbxLotToAdd.MaxLength = 50
        Me.txbxLotToAdd.Name = "txbxLotToAdd"
        Me.txbxLotToAdd.Size = New System.Drawing.Size(141, 26)
        Me.txbxLotToAdd.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Carrier ID:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Lot/Batch:"
        '
        'btnRemoveItem
        '
        Me.btnRemoveItem.Location = New System.Drawing.Point(3, 3)
        Me.btnRemoveItem.Name = "btnRemoveItem"
        Me.btnRemoveItem.Size = New System.Drawing.Size(133, 21)
        Me.btnRemoveItem.TabIndex = 0
        Me.btnRemoveItem.Text = "Remove Selected Item"
        Me.btnRemoveItem.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtTestStart)
        Me.Panel2.Controls.Add(Me.txtTimeSinceStart)
        Me.Panel2.Controls.Add(Me.txtTimeSinceMove)
        Me.Panel2.Controls.Add(Me.lblTestStart)
        Me.Panel2.Controls.Add(Me.lblTimeSinceStart)
        Me.Panel2.Controls.Add(Me.lblTimeSinceInjection)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.txtBath6MoveTo)
        Me.Panel2.Controls.Add(Me.txtBath5MoveTo)
        Me.Panel2.Controls.Add(Me.txtBath4MoveTo)
        Me.Panel2.Controls.Add(Me.txtBath3MoveTo)
        Me.Panel2.Controls.Add(Me.txtBath2MoveTo)
        Me.Panel2.Controls.Add(Me.txtBath1MoveTo)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.cbxRecipe)
        Me.Panel2.Controls.Add(Me.btnMoveCarriers)
        Me.Panel2.Controls.Add(Me.btnStartTest)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(244, 353)
        Me.Panel2.TabIndex = 1
        '
        'txtTestStart
        '
        Me.txtTestStart.AutoSize = True
        Me.txtTestStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtTestStart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtTestStart.Location = New System.Drawing.Point(68, 328)
        Me.txtTestStart.Margin = New System.Windows.Forms.Padding(3)
        Me.txtTestStart.Name = "txtTestStart"
        Me.txtTestStart.Size = New System.Drawing.Size(56, 16)
        Me.txtTestStart.TabIndex = 28
        Me.txtTestStart.Text = "00:00:00"
        '
        'txtTimeSinceStart
        '
        Me.txtTimeSinceStart.AutoSize = True
        Me.txtTimeSinceStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtTimeSinceStart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtTimeSinceStart.Location = New System.Drawing.Point(114, 306)
        Me.txtTimeSinceStart.Margin = New System.Windows.Forms.Padding(3)
        Me.txtTimeSinceStart.Name = "txtTimeSinceStart"
        Me.txtTimeSinceStart.Size = New System.Drawing.Size(56, 16)
        Me.txtTimeSinceStart.TabIndex = 27
        Me.txtTimeSinceStart.Text = "00:00:00"
        '
        'txtTimeSinceMove
        '
        Me.txtTimeSinceMove.AutoSize = True
        Me.txtTimeSinceMove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.txtTimeSinceMove.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtTimeSinceMove.Location = New System.Drawing.Point(114, 285)
        Me.txtTimeSinceMove.Margin = New System.Windows.Forms.Padding(3)
        Me.txtTimeSinceMove.Name = "txtTimeSinceMove"
        Me.txtTimeSinceMove.Size = New System.Drawing.Size(56, 16)
        Me.txtTimeSinceMove.TabIndex = 26
        Me.txtTimeSinceMove.Text = "00:00:00"
        '
        'lblTestStart
        '
        Me.lblTestStart.AutoSize = True
        Me.lblTestStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.lblTestStart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTestStart.Location = New System.Drawing.Point(4, 328)
        Me.lblTestStart.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTestStart.Name = "lblTestStart"
        Me.lblTestStart.Size = New System.Drawing.Size(68, 16)
        Me.lblTestStart.TabIndex = 25
        Me.lblTestStart.Text = "Test Start:"
        '
        'lblTimeSinceStart
        '
        Me.lblTimeSinceStart.AutoSize = True
        Me.lblTimeSinceStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.lblTimeSinceStart.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTimeSinceStart.Location = New System.Drawing.Point(4, 306)
        Me.lblTimeSinceStart.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTimeSinceStart.Name = "lblTimeSinceStart"
        Me.lblTimeSinceStart.Size = New System.Drawing.Size(109, 16)
        Me.lblTimeSinceStart.TabIndex = 24
        Me.lblTimeSinceStart.Text = "Time Since Start:"
        '
        'lblTimeSinceInjection
        '
        Me.lblTimeSinceInjection.AutoSize = True
        Me.lblTimeSinceInjection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.lblTimeSinceInjection.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblTimeSinceInjection.Location = New System.Drawing.Point(4, 285)
        Me.lblTimeSinceInjection.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTimeSinceInjection.Name = "lblTimeSinceInjection"
        Me.lblTimeSinceInjection.Size = New System.Drawing.Size(116, 16)
        Me.lblTimeSinceInjection.TabIndex = 23
        Me.lblTimeSinceInjection.Text = "Time Since Move:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(59, 119)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(127, 13)
        Me.Label16.TabIndex = 22
        Me.Label16.Text = "Carrier Movement Pattern"
        '
        'txtBath6MoveTo
        '
        Me.txtBath6MoveTo.AutoSize = True
        Me.txtBath6MoveTo.Location = New System.Drawing.Point(140, 218)
        Me.txtBath6MoveTo.Name = "txtBath6MoveTo"
        Me.txtBath6MoveTo.Size = New System.Drawing.Size(29, 13)
        Me.txtBath6MoveTo.TabIndex = 21
        Me.txtBath6MoveTo.Text = "Bath"
        '
        'txtBath5MoveTo
        '
        Me.txtBath5MoveTo.AutoSize = True
        Me.txtBath5MoveTo.Location = New System.Drawing.Point(140, 202)
        Me.txtBath5MoveTo.Name = "txtBath5MoveTo"
        Me.txtBath5MoveTo.Size = New System.Drawing.Size(29, 13)
        Me.txtBath5MoveTo.TabIndex = 20
        Me.txtBath5MoveTo.Text = "Bath"
        '
        'txtBath4MoveTo
        '
        Me.txtBath4MoveTo.AutoSize = True
        Me.txtBath4MoveTo.Location = New System.Drawing.Point(140, 186)
        Me.txtBath4MoveTo.Name = "txtBath4MoveTo"
        Me.txtBath4MoveTo.Size = New System.Drawing.Size(29, 13)
        Me.txtBath4MoveTo.TabIndex = 19
        Me.txtBath4MoveTo.Text = "Bath"
        '
        'txtBath3MoveTo
        '
        Me.txtBath3MoveTo.AutoSize = True
        Me.txtBath3MoveTo.Location = New System.Drawing.Point(140, 170)
        Me.txtBath3MoveTo.Name = "txtBath3MoveTo"
        Me.txtBath3MoveTo.Size = New System.Drawing.Size(29, 13)
        Me.txtBath3MoveTo.TabIndex = 18
        Me.txtBath3MoveTo.Text = "Bath"
        '
        'txtBath2MoveTo
        '
        Me.txtBath2MoveTo.AutoSize = True
        Me.txtBath2MoveTo.Location = New System.Drawing.Point(140, 154)
        Me.txtBath2MoveTo.Name = "txtBath2MoveTo"
        Me.txtBath2MoveTo.Size = New System.Drawing.Size(29, 13)
        Me.txtBath2MoveTo.TabIndex = 17
        Me.txtBath2MoveTo.Text = "Bath"
        '
        'txtBath1MoveTo
        '
        Me.txtBath1MoveTo.AutoSize = True
        Me.txtBath1MoveTo.Location = New System.Drawing.Point(140, 138)
        Me.txtBath1MoveTo.Name = "txtBath1MoveTo"
        Me.txtBath1MoveTo.Size = New System.Drawing.Size(29, 13)
        Me.txtBath1MoveTo.TabIndex = 16
        Me.txtBath1MoveTo.Text = "Bath"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(69, 218)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Bath 6   ─→"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(69, 202)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Bath 5   ─→"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(69, 186)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Bath 4   ─→"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(69, 170)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Bath 3   ─→"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(69, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Bath 2   ─→"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(69, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Bath 1   ─→"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 20)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Recipe:"
        '
        'cbxRecipe
        '
        Me.cbxRecipe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxRecipe.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxRecipe.FormattingEnabled = True
        Me.cbxRecipe.Location = New System.Drawing.Point(10, 31)
        Me.cbxRecipe.Name = "cbxRecipe"
        Me.cbxRecipe.Size = New System.Drawing.Size(224, 28)
        Me.cbxRecipe.TabIndex = 8
        '
        'btnMoveCarriers
        '
        Me.btnMoveCarriers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMoveCarriers.Location = New System.Drawing.Point(65, 246)
        Me.btnMoveCarriers.Name = "btnMoveCarriers"
        Me.btnMoveCarriers.Size = New System.Drawing.Size(120, 30)
        Me.btnMoveCarriers.TabIndex = 7
        Me.btnMoveCarriers.Text = "Move Carriers"
        Me.btnMoveCarriers.UseVisualStyleBackColor = True
        '
        'btnStartTest
        '
        Me.btnStartTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartTest.Location = New System.Drawing.Point(62, 69)
        Me.btnStartTest.Name = "btnStartTest"
        Me.btnStartTest.Size = New System.Drawing.Size(120, 30)
        Me.btnStartTest.TabIndex = 6
        Me.btnStartTest.Text = "Start Test"
        Me.btnStartTest.UseVisualStyleBackColor = True
        '
        'tmrPeriodicUpdate
        '
        '
        'frm03RunTest
        '
        Me.AcceptButton = Me.btnAddToQueue
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 730)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "frm03RunTest"
        Me.Text = "Run Test"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents lstCarrierQueue As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnAddToQueue As Button
    Friend WithEvents txbxCarrierToAdd As TextBox
    Friend WithEvents txbxLotToAdd As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnRemoveItem As Button
    Friend WithEvents btnMoveCarriers As Button
    Friend WithEvents btnStartTest As Button
    Friend WithEvents Label16 As Label
    Friend WithEvents txtBath6MoveTo As Label
    Friend WithEvents txtBath5MoveTo As Label
    Friend WithEvents txtBath4MoveTo As Label
    Friend WithEvents txtBath3MoveTo As Label
    Friend WithEvents txtBath2MoveTo As Label
    Friend WithEvents txtBath1MoveTo As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cbxRecipe As ComboBox
    Friend WithEvents txtTestStart As Label
    Friend WithEvents txtTimeSinceStart As Label
    Friend WithEvents txtTimeSinceMove As Label
    Friend WithEvents lblTestStart As Label
    Friend WithEvents lblTimeSinceStart As Label
    Friend WithEvents lblTimeSinceInjection As Label
    Friend WithEvents tmrPeriodicUpdate As Timer
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents lblTestStatus As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents pbLoopProgress As ToolStripProgressBar
    Friend WithEvents lblLastIntervalTime As ToolStripStatusLabel
    Friend WithEvents lblChannelAVoltage As ToolStripStatusLabel
    Friend WithEvents lblChannelACurrent As ToolStripStatusLabel
    Friend WithEvents lblChannelBVoltage As ToolStripStatusLabel
End Class
