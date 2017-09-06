Option Explicit On
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.DataVisualization.Charting


Public Class frm03RunTest
    Private intReadingCount As Int32 = 0        'The number of readings since last move
    Private stpTotalTime As New Stopwatch       'Time since hitting start
    Private stpMovementTime As New Stopwatch    'Time since the last move
    Private stpIntervalTime As New Stopwatch    'Time in the current reading interval

    Private boolCanRead As Boolean = False      'Test for if the reading set can be completed in the specified interval. Tested once after the start button is pressed.
    Private boolStopPressed As Boolean = False

    Private bwkrMainloop As New BackgroundWorker
    Private bwkrMoveCarriers As New BackgroundWorker
    Private bwkrStartTest As New BackgroundWorker


    ' Section titles by: http://www.network-science.de/ascii/   Font: 'Stop'

    ' -------------------------------------------------------------
    ' |  _____                      _____                 _       
    ' | |  ___|__  _ __ _ __ ___   | ____|_   _____ _ __ | |_ ___ 
    ' | | |_ / _ \| '__| '_ ` _ \  |  _| \ \ / / _ \ '_ \| __/ __|
    ' | |  _| (_) | |  | | | | | | | |___ \ V /  __/ | | | |_\__ \
    ' | |_|  \___/|_|  |_| |_| |_| |_____| \_/ \___|_| |_|\__|___/
    ' | 
    ' -------------------------------------------------------------

    Private Sub frm03RunTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Initialize Communication to the Keithleys.  Warn and close if it can't be done.
            If EstablishKeithleyIO(cfgGlobal.Address) = False Then
                MsgBox("Could not establish communication with the measurement hardware." & vbCr &
                   "Contact the Equipment Administrator.")
                Me.Close()
                Exit Sub
            End If

            'Initialize the carrier lists
            InitializeCarrierLists()

            'Populate the Recipe combobox
            PopulateRecipes()

            'Resize lstCarrierQueue
            lstCarrierQueue_Resize(sender, e)

            'Initialize Graphs
            InitializeCharts()

            ' Set the background worker to report progress so that it can make cross-thread communications to the chart updater
            bwkrMainloop.WorkerReportsProgress = True
            AddHandler bwkrMainloop.DoWork, AddressOf MainLoop_DoWork
            AddHandler bwkrMainloop.RunWorkerCompleted, AddressOf MainLoop_Completed

            bwkrMoveCarriers.WorkerReportsProgress = True
            AddHandler bwkrMoveCarriers.DoWork, AddressOf btnMoveCarriers_Click

            ' Set Keithley 2600 Text
            SwitchIOWrite("node[2].display.clear()")
            SwitchIOWrite("node[2].display.settext('Ready to test')")

            ' Update the status bar
            lblTestStatus.Text = "Communication initialized.  Ready to start test."

            ' Set the running flags
            boolIsTestRunning = False
            boolRunTest = False
            boolStopPressed = False

            ' disable the queue buttons
            btnAddToQueue.Enabled = False
            btnRemoveItem.Enabled = False
            btnMoveCarriers.Enabled = False

        Catch ex As COMException
            ComExceptionHandler(ex)
            Me.Close()
        Catch ex As Exception
            GenericExceptionHandler(ex)
            Me.Close()
        End Try





    End Sub



    Private Sub lstCarrierQueue_Resize(sender As Object, e As EventArgs) Handles lstCarrierQueue.Resize
        ColumnHeader1.Width = lstCarrierQueue.Width - 146
        ColumnHeader2.Width = 60
        ColumnHeader3.Width = 80
        ColumnHeader4.Width = 0
    End Sub



    Private Sub frm03RunTest_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ' Close the instrument connection when exiting the test form
            If Not boolIsTestRunning Then
                EndTest()
            Else
                e.Cancel = True
                MsgBox("Cannot close test form while test is running.  Stop test and close form.", vbOKOnly)
            End If
            '
        Catch comEx As COMException
            ComExceptionHandler(comEx)
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try

    End Sub




    ' -------------------------------------------------------------
    ' |  ____        _   _                _____                 _       
    ' | | __ ) _   _| |_| |_ ___  _ __   | ____|_   _____ _ __ | |_ ___ 
    ' | |  _ \| | | | __| __/ _ \| '_ \  |  _| \ \ / / _ \ '_ \| __/ __|
    ' | | |_) | |_| | |_| || (_) | | | | | |___ \ V /  __/ | | | |_\__ \
    ' | |____/ \__,_|\__|\__\___/|_| |_| |_____| \_/ \___|_| |_|\__|___/
    ' | 
    ' -------------------------------------------------------------


    Private Sub cbxRecipe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxRecipe.SelectedIndexChanged
        LoadRecipe()
    End Sub

    Private Sub btnMoveCarriers_Click(sender As Object, e As EventArgs) Handles btnMoveCarriers.Click
        Dim boolCarrierInSystem As Boolean

        'Check if this button is disabled.  This is to handle the call from bwkrMoveCarriers
        If btnMoveCarriers.Enabled = False Then
            Exit Sub
        End If

        'Check if test is running.  If not, then exit the move.
        If boolIsTestRunning = False Then
            MsgBox("Start test before moving carriers.")
            Exit Sub
        End If

        'Check if move has been triggered
        If boolMoveCarriers = True Then
            MsgBox("Move has been triggered.  Please wait for move to be completed.")
            Exit Sub
        End If

        'Check (1) if there is a carrier in the queue, or (2) there is a carrier in the system
        boolCarrierInSystem = CarrierCheck()

        If (boolCarrierInSystem = False AndAlso lstCarrierQueue.Items.Count = 0) Then
            MsgBox("Add a carrier to the queue before moving carriers.")
        End If

        'Check if the minimum number of reading have been collected
        If (intReadingCount <= cfgRecipe.MinimumReadings AndAlso boolCarrierInSystem = True) Then
            Select Case MsgBox("Minimum number of readings have not been collected." & vbCr &
                               "Are you sure you want to move the carriers?", MsgBoxStyle.YesNo,
                               "Move carriers before end of measurement interval?")
                Case MsgBoxResult.Yes
                    Exit Select
                Case MsgBoxResult.No
                    Exit Sub
            End Select
        End If


        'Disable the button
        btnMoveCarriers.Enabled = False

        'Update the incoming list
        UpdateCarrierMoveTo()

        'Prompt the user to move the carriers.
        Select Case MsgBox("Move the carriers to their new positions and press OK.",
                           MsgBoxStyle.OkCancel, "Move carriers")
            Case MsgBoxResult.Ok
                Exit Select
            Case MsgBoxResult.Cancel
                btnMoveCarriers.Enabled = True
                Exit Sub
        End Select

        'trigger the move on the next measurement cycle
        boolMoveCarriers = True


    End Sub



    Private Sub btnAddToQueue_Click(sender As Object, e As EventArgs) Handles btnAddToQueue.Click
        Dim intCarrierUserID As Integer
        Dim i As Integer

        'Verify that there is text in both text boxes
        If txbxLotToAdd.Text = "" Then
            MsgBox("Lot/Batch is required.")
            Exit Sub
        End If

        If txbxCarrierToAdd.Text = "" Then
            MsgBox("Carrier ID is required.")
            Exit Sub
        End If

        'Get the user ID of the individual adding the carrier to the queue
        intCarrierUserID = GetUserID()

        If intCarrierUserID > 0 Then
            'Check if the carrier already exists in the SQL table
            If CheckIfCarrierExists(txbxLotToAdd.Text, txbxCarrierToAdd.Text) Then
                MsgBox("Carrier already in use.  Please use a different designation (such as " &
                           txbxCarrierToAdd.Text.Substring(0, 1) & "1)")
                Exit Sub
            End If

            'Check if the carrier already exists in the Carrier Queue list
            For i = 0 To lstCarrierQueue.Items.Count - 1
                If txbxLotToAdd.Text = lstCarrierQueue.Items(i).SubItems(0).Text And
                   txbxCarrierToAdd.Text = lstCarrierQueue.Items(i).SubItems(1).Text Then
                    MsgBox("Carrier already in queue.  Please use a different designation (such as " &
                           txbxCarrierToAdd.Text.Substring(0, 1) & "1)")
                    Exit Sub
                End If
            Next

            'Add the lot/carrier to the list
            lstCarrierQueue.Items.Add(New ListViewItem({txbxLotToAdd.Text, txbxCarrierToAdd.Text, "", intCarrierUserID}))

            'Increment the carrier field (for convenience and to help prevent double additions)
            txbxCarrierToAdd.Text = Chr(Asc(txbxCarrierToAdd.Text) + 1)
        End If

        'Select the text of the txbxLotToAdd (For future use with barcode reader)
        txbxLotToAdd.SelectAll()
        txbxLotToAdd.Focus()

    End Sub

    Private Sub btnRemoveItem_Click(sender As Object, e As EventArgs) Handles btnRemoveItem.Click
        MsgBox(lstCarrierQueue.SelectedIndices)
        lstCarrierQueue.Items.RemoveAt(lstCarrierQueue.SelectedIndices(0))

    End Sub

    Private Sub btnStartTest_Click(sender As Object, e As EventArgs) Handles btnStartTest.Click
        Dim boolCarrierInSystem As Boolean

        Select Case btnStartTest.Text
            Case "Start Test"
                'Setup the backgroundworker that will get the test started
                bwkrStartTest.WorkerReportsProgress = True
                AddHandler bwkrStartTest.DoWork, AddressOf StartTest
                AddHandler bwkrStartTest.RunWorkerCompleted, AddressOf StartTestDone
                'Start the bwkr
                bwkrStartTest.RunWorkerAsync()
                'Disable the button so that it can't be pressed until the startup is complete
                btnStartTest.Enabled = False
                btnStartTest.Text = "Starting..."
                lblTestStatus.Text = "Getting hardware information..."
                'Disable the move button so it can't be pressed until the startup is complete
                btnMoveCarriers.Enabled = False
            Case "End Test"
                'Check if there is a carrier in the system
                boolCarrierInSystem = CarrierCheck()
                If boolCarrierInSystem Then
                    Select Case MsgBox("Carriers are still in the system." & vbCr &
                               "Are you sure you want to stop the test?", MsgBoxStyle.YesNo,
                               "Stop test with carriers in the system?")
                        Case MsgBoxResult.Yes
                            Exit Select
                        Case MsgBoxResult.No
                            Exit Sub
                    End Select
                End If

                btnStartTest.Enabled = False
                btnAddToQueue.Enabled = False
                btnMoveCarriers.Enabled = False
                btnRemoveItem.Enabled = False
                boolRunTest = False
                boolStopPressed = True
        End Select
    End Sub

    ' -------------------------------------------------------------
    '  _____ _                       _____                 _       
    ' |_   _(_)_ __ ___   ___ _ __  | ____|_   _____ _ __ | |_ ___ 
    '   | | | | '_ ` _ \ / _ \ '__| |  _| \ \ / / _ \ '_ \| __/ __|
    '   | | | | | | | | |  __/ |    | |___ \ V /  __/ | | | |_\__ \
    '   |_| |_|_| |_| |_|\___|_|    |_____| \_/ \___|_| |_|\__|___/
    ' 
    ' -------------------------------------------------------------

    ' --------------------------------------------
    ' Timer loop threads
    ' --------------------------------------------
    ' PeriodicUpdate triggered every 0.1 second by the tmrPeriodicUpdate components in the user form.
    ' There are corresponding stopwatches that are managed within the test control loop
    ' and these are referenced within the Tick events to update the elapsed time shown to the user.
    ' Other events that need to happen regularly are triggered as well.
    '---------------------------------------------

    ' Name: PeriodicUpdate()
    ' Handles: Tick event for tmrPeriodicUpdate.Tick
    ' Description: The tmrPeriodicUpdate triggers the Tick event every 0.1 second.  This sub runs when this event is triggered.  
    '           It grabs the the time elapsed in the stpTotalTime and stpMovement stopwatches, parses them into human-readable 
    '           format and updates other the user interface components
    Private Sub PeriodicUpdate() Handles tmrPeriodicUpdate.Tick
        txtTimeSinceStart.Text = Format(stpTotalTime.Elapsed.Hours, "00") & ":" & Format(stpTotalTime.Elapsed.Minutes, "00") & ":" & Format(stpTotalTime.Elapsed.Seconds, "00")
        txtTimeSinceMove.Text = Format(stpMovementTime.Elapsed.Hours, "00") & ":" & Format(stpMovementTime.Elapsed.Minutes, "00") & ":" & Format(stpMovementTime.Elapsed.Seconds, "00")
        If boolStopPressed = True Then
            btnStartTest.Text = "Stopping... " & CStr(cfgRecipe.ReadingInterval - stpIntervalTime.ElapsedMilliseconds \ 1000)
        End If
    End Sub

    Private Sub UpdateCarrierMoveTo()
        Dim count As Int16 = 0

        'count = lstCarrierQueue.Items.Count

        'If count > 5 Then
        '    count = 5
        'End If
        If lstCarrierQueue.Items.Count > 0 Then
            For i = 0 To 5
                If intIncomingPosition(i) > 0 Then
                    lstCarrierQueue.Items(count).SubItems(2).Text = intIncomingPosition(i)
                    lstCarrierNextPosition(i).CarrierSQLID = GetCarrierID(lstCarrierQueue.Items(count).SubItems(0).Text,
                                                                      lstCarrierQueue.Items(count).SubItems(1).Text,
                                                                      lstCarrierQueue.Items(count).SubItems(3).Text)
                    lstCarrierNextPosition(i).CarrierText = lstCarrierQueue.Items(count).SubItems(1).Text
                    lstCarrierNextPosition(i).UserID = lstCarrierQueue.Items(count).SubItems(3).Text
                Else
                    lstCarrierNextPosition(i).CarrierSQLID = -1
                    lstCarrierNextPosition(i).CarrierText = -1
                End If
                count = count + 1
                If count > lstCarrierQueue.Items.Count Then
                    Exit For
                End If
            Next

        End If


        For i = 0 To 5
            If intNextPosition(i) > 0 Then
                lstCarrierNextPosition(intNextPosition(i) - 1).CarrierText = lstCarrierCurrent(i).CarrierText
                lstCarrierNextPosition(intNextPosition(i) - 1).CarrierSQLID = lstCarrierCurrent(i).CarrierSQLID
                lstCarrierNextPosition(intNextPosition(i) - 1).UserID = lstCarrierCurrent(i).UserID
            End If
        Next

    End Sub





    Private Sub PopulateRecipes()
        'Request the list of active recipes and add to combobox
        Dim myConn As SqlConnection
        Dim myCmd As SqlCommand
        Dim myReader As SqlDataReader
        Dim results As Int32 = 0
        Dim text As String

        Try
            'Create a Connection object.
            myConn = New SqlConnection("Initial Catalog=" & strDatabase & ";" &
                "Data Source=" & cfgGlobal.SQLServer & ";Integrated Security=SSPI;")

            'Create a Command object.
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "SELECT distinct [recipename] FROM tblTestRecipes " &
                                "WHERE [active]=1"

            'Open the connection.
            myConn.Open()

            'Execute the command
            myReader = myCmd.ExecuteReader()

            'Store the results.
            Do While myReader.Read()
                text = myReader.GetString(0)
                cbxRecipe.Items.Add(text)
            Loop

            'Close the reader and the database connection.
            myReader.Close()
            myConn.Close()

        Catch ex As Exception

            GenericExceptionHandler(ex)
        End Try

        'Select the first item in the list
        cbxRecipe.SelectedIndex = 0

    End Sub

    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Private Sub LoadRecipe()
        Dim myConn As SqlConnection
        Dim myCmd As SqlCommand
        Dim myReader As SqlDataReader
        Dim results As Int32 = 0

        If cbxRecipe.Text = "" Then
            Exit Sub
        End If

        Try
            'Create a Connection object.
            myConn = New SqlConnection("Initial Catalog=" & strDatabase & ";" &
                "Data Source=" & cfgGlobal.SQLServer & ";Integrated Security=SSPI;")

            'Create a Command object.
            myCmd = myConn.CreateCommand
            myCmd.CommandText = "SELECT * FROM tblTestRecipes " &
                                "WHERE [recipename]='" & cbxRecipe.Text & "'"

            'Open the connection.
            myConn.Open()

            'Execute the command
            myReader = myCmd.ExecuteReader()

            'Store the results.
            'Do While myReader.Read()
            myReader.Read()
            cfgRecipe.RecipeID = myReader.GetInt32(0)
            cfgRecipe.RecipeName = myReader.GetString(1)
            cfgRecipe.BiasVoltage = myReader.GetDecimal(2)
            cfgRecipe.CurrentRange = myReader.GetInt32(3)
            cfgRecipe.NPLC = myReader.GetInt32(4)
            cfgRecipe.FilterType = myReader.GetString(5)
            cfgRecipe.FilterCount = myReader.GetInt32(6)
            cfgRecipe.SettlingTime = myReader.GetInt32(7)
            cfgRecipe.ReadingInterval = myReader.GetInt32(8)
            cfgRecipe.MinimumReadings = myReader.GetInt32(9)
            cfgRecipe.Bath1Analyte = myReader.GetString(10)
            cfgRecipe.Bath2Analyte = myReader.GetString(11)
            cfgRecipe.Bath3Analyte = myReader.GetString(12)
            cfgRecipe.Bath4Analyte = myReader.GetString(13)
            cfgRecipe.Bath5Analyte = myReader.GetString(14)
            cfgRecipe.Bath6Analyte = myReader.GetString(15)
            For i = 0 To 5
                cfgRecipe.BathSource(i) = myReader.GetInt32(16 + i)
                cfgRecipe.BathDestination(i) = myReader.GetInt32(22 + i)
            Next
            'Loop

            'Close the reader and the database connection.
            myReader.Close()
            myConn.Close()

        Catch ex As Exception
            GenericExceptionHandler(ex)

            '            MsgBox("Can't connect to the SQL Server.  Please check server address.")
            '             Me.Close()
        End Try

        'Update the movement pattern
        UpdateLabel(txtBath1MoveTo, cfgRecipe.BathDestination(0))
        UpdateLabel(txtBath2MoveTo, cfgRecipe.BathDestination(1))
        UpdateLabel(txtBath3MoveTo, cfgRecipe.BathDestination(2))
        UpdateLabel(txtBath4MoveTo, cfgRecipe.BathDestination(3))
        UpdateLabel(txtBath5MoveTo, cfgRecipe.BathDestination(4))
        UpdateLabel(txtBath6MoveTo, cfgRecipe.BathDestination(5))

        'Update the chart titles
        UpdateChartTitles()


    End Sub



    Private Sub UpdateLabel(ByVal lblLabel As Label, ByVal intVal As Int32)
        If intVal > 0 Then
            lblLabel.Text = "Bath " & intVal
        Else
            lblLabel.Text = "Out"
        End If

    End Sub



    Private Sub UpdateChartTitles()
        Dim aryBathAnalyte(5) As String
        aryBathAnalyte(0) = cfgRecipe.Bath1Analyte.Trim
        aryBathAnalyte(1) = cfgRecipe.Bath2Analyte.Trim
        aryBathAnalyte(2) = cfgRecipe.Bath3Analyte.Trim
        aryBathAnalyte(3) = cfgRecipe.Bath4Analyte.Trim
        aryBathAnalyte(4) = cfgRecipe.Bath5Analyte.Trim
        aryBathAnalyte(5) = cfgRecipe.Bath6Analyte.Trim

        For i = 0 To 5
            If lstCarrierCurrent(i).CarrierSQLID > 0 Then
                Chart1.Titles(i).Text = "Bath " & i + 1 & " — " & aryBathAnalyte(i) & " — " & RTrim(GetLotFromCarrierID(lstCarrierCurrent(i).CarrierSQLID)) & "-" & lstCarrierCurrent(i).CarrierText
            Else
                Chart1.Titles(i).Text = "Bath " & i + 1 & " — " & aryBathAnalyte(i) & " — No Carrier"

            End If
        Next
        '        Chart1.Titles(0).Text = "Bath 1 — " & cfgRecipe.Bath1Analyte.Trim & " — " & RTrim(GetLotFromID(lstCarrierCurrent(0).LotID)) & "-" & lstCarrierCurrent(0).CarrierID
        '       Chart1.Titles(1).Text = "Bath 2 — " & cfgRecipe.Bath2Analyte.Trim & " — " & RTrim(GetLotFromID(lstCarrierCurrent(1).LotID)) & "-" & lstCarrierCurrent(1).CarrierID
        '      Chart1.Titles(2).Text = "Bath 3 — " & cfgRecipe.Bath3Analyte.Trim & " — " & RTrim(GetLotFromID(lstCarrierCurrent(2).LotID)) & "-" & lstCarrierCurrent(2).CarrierID
        '     Chart1.Titles(3).Text = "Bath 4 — " & cfgRecipe.Bath4Analyte.Trim & " — " & RTrim(GetLotFromID(lstCarrierCurrent(3).LotID)) & "-" & lstCarrierCurrent(3).CarrierID
        '    Chart1.Titles(4).Text = "Bath 5 — " & cfgRecipe.Bath5Analyte.Trim & " — " & RTrim(GetLotFromID(lstCarrierCurrent(4).LotID)) & "-" & lstCarrierCurrent(4).CarrierID
        '   Chart1.Titles(5).Text = "Bath 6 — " & cfgRecipe.Bath6Analyte.Trim & " — " & RTrim(GetLotFromID(lstCarrierCurrent(5).LotID)) & "-" & lstCarrierCurrent(5).CarrierID


    End Sub

    Private Sub InitializeCarrierLists()
        Dim i As Integer
        'Re-initialize carrier lists
        lstCarrierIncoming.Clear()
        lstCarrierCurrent.Clear()
        lstCarrierNextPosition.Clear()

        For i = 0 To 5
            lstCarrierIncoming.Add(New clsCarrier)
            lstCarrierCurrent.Add(New clsCarrier)
            lstCarrierNextPosition.Add(New clsCarrier)
        Next
    End Sub

    Private Function CarrierCheck()
        Dim boolCarrierCheck As Boolean = False

        'Check if there are any carriers in the system
        For i = 0 To 5
            If lstCarrierCurrent(i).CarrierSQLID > 0 Then
                boolCarrierCheck = True
            End If
        Next

        Return boolCarrierCheck
    End Function

    Private Sub ClearCarrierMovementLists()
        lstCarrierIncoming.Clear()
        lstCarrierNextPosition.Clear()
        For i = 0 To 5
            lstCarrierIncoming.Add(New clsCarrier)
            lstCarrierNextPosition.Add(New clsCarrier)
        Next
    End Sub



    Private Sub InitializeCharts()
        Dim loop1 As Int32
        Dim loop2 As Int32

        Try
            'Clear the default or previous series and legends from the test chart
            Chart1.Series.Clear()
            Chart1.Legends.Clear()


            For loop1 = 1 To 6

                ' Configure the test chart
                With Chart1.ChartAreas(loop1 - 1)
                    '.CursorX.AutoScroll = False
                    '.CursorY.AutoScroll = False
                    '.CursorX.IsUserEnabled = True
                    '.CursorY.IsUserEnabled = True
                    '.CursorX.Interval = 0
                    '.CursorY.Interval = 0
                    '.AxisX.ScaleView.MinSize = 0
                    '.AxisY.ScaleView.MinSize = 0
                    .AxisY.Minimum = 0
                    .AxisX.Minimum = 0
                    .AxisX.Title = "Elapsed Time (s)"
                    .AxisY.Title = "Current (nA)"
                    ' Setting IsMarginVisible to false increases the accuracy of deep zooming.  If this is true then zooms are padded
                    ' and do not show the actual area selected
                    '.AxisX.IsMarginVisible = False
                    '.AxisY.IsMarginVisible = False
                    .Name = "Bath " & loop1
                    '.AxisY.IsStartedFromZero = False



                End With

                ' Populate the chart series and legend
                For loop2 = 1 To 16
                    Chart1.Series.Add(loop1 & "-" & loop2)
                    With Chart1.Series(loop1 & "-" & loop2)
                        .ChartType = SeriesChartType.FastLine
                        '.BorderWidth = 2
                        .ChartArea = Chart1.ChartAreas(loop1 - 1).Name
                        ' other properties go here later
                        .IsXValueIndexed = False

                    End With
                Next
            Next






        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try

    End Sub

    Function GetUserID()
        Dim intCarrierUserID As Int32
        Dim frmLogin As New frm31Login
        Dim Success As Boolean = False
        Dim strDomain As String = Environment.UserDomainName    ' Current domain login


        If frmLogin.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Return -1
            Exit Function
        End If


        'Check if network user/password is correct
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & strDomain, frmLogin.txtbxUserID.Text, frmLogin.txtbxPassword.Text)
        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)
        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel
        Try
            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)
        Catch
            Success = False
        End Try

        frmLogin.txtbxPassword.Text = ""

        If Success = False Then
            MsgBox("User ID or Password are incorrect")
            Return -1
            Exit Function
        End If

        'Check if user is in SQL Database
        Dim strQuery As String
        strQuery = "SELECT TOP 1 [userid] FROM tblUsers " &
                   "WHERE [user]='" & frmLogin.txtbxUserID.Text & "' " &
                   "ORDER BY [userid] DESC"

        intCarrierUserID = SQLQueryInt(strQuery)

        If intCarrierUserID = -1 Then
            MsgBox("User not found.  Contact Test Administrator.")
            Return -1
            Exit Function
        End If

        'Check if user is active
        strQuery = "SELECT CAST([active] AS int) FROM tblUsers " &
                   "WHERE [userid]=" & intCarrierUserID

        If SQLQueryInt(strQuery) = 0 Then
            MsgBox("User is not active.  Contact Test Administrator.")
            Return -1
            Exit Function
        End If

        Return intCarrierUserID

    End Function


    ' -------------------------------------------------------------
    '  ____             _                                   _ 
    ' | __ )  __ _  ___| | ____ _ _ __ ___  _   _ _ __   __| |
    ' |  _ \ / _` |/ __| |/ / _` | '__/ _ \| | | | '_ \ / _` |
    ' | |_) | (_| | (__|   < (_| | | | (_) | |_| | | | | (_| |
    ' |____/ \__,_|\___|_|\_\__, |_|  \___/ \__,_|_| |_|\__,_|
    '                       |___/                             
    ' __        __         _             
    ' \ \      / /__  _ __| | _____ _ __ 
    '  \ \ /\ / / _ \| '__| |/ / _ \ '__|
    '   \ V  V / (_) | |  |   <  __/ |   
    '    \_/\_/ \___/|_|  |_|\_\___|_|   
    ' 
    ' -------------------------------------------------------------


    Private Sub StartTest()
        'Write Hardware Info to SQL
        'SourceMeter Info to SQL - DB 12Jul2017
        intSourceMeterID = GetSourceMeterID()

        'Switch Info to SQL - DB 12Jul2017
        intSwitchID = GetSwitchID()

        'Switch Card Info to SQL - DB 12Jul2017
        intCard1CountsID = GetCardInfoID(1)
        intCard2CountsID = GetCardInfoID(2)
        intCard3CountsID = GetCardInfoID(3)
        intCard4CountsID = GetCardInfoID(4)
        intCard5CountsID = GetCardInfoID(5)
        intCard6CountsID = GetCardInfoID(6)

        'Store Test Configuration to SQL - DB 13Jul2017
        intTestConfigID = GetTestConfigID()

        'Initialize the Carrier Arrays
        InitializeCarrierLists()

        'Get the CommonChannel Lot Primary Key
        intCommonChannelID = GetCarrierID("CommonChannelA", "CA", intUserID)

        'Start the measurement Loop
        bwkrMainloop.RunWorkerAsync()
    End Sub
    Private Sub StartTestDone()
        'Update status
        lblTestStatus.Text = "Test running."

        'Set the start time
        txtTestStart.Text = DateTime.Now

        'Start total time timer and periodic update
        stpTotalTime.Start()
        tmrPeriodicUpdate.Start()

        'Enable the buttons
        btnStartTest.Enabled = True
        btnStartTest.Text = "End Test"
        btnMoveCarriers.Enabled = True
        btnAddToQueue.Enabled = True
        btnRemoveItem.Enabled = True



    End Sub
    Private Sub MainLoop_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        MeasurementLoop(sender, e)
    End Sub

    Private Sub MeasurementLoop(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        'Local Variables
        Dim intLoopSensor As Int32
        Dim intBathSensor As New clsBathSensor
        Dim decReading() As Decimal = {0, 0}
        Dim strDataInsert As String
        Dim strCurrentGraphID As String
        Dim boolCheckReadingTime As Boolean = True
        Dim decProgress As Decimal



        ' To simplify interaction with user interface in main thread and timer threads, disable prevention of
        ' cross-thread calls.  Because of the simplicity of the test sequence and its form this can be done safely
        ' though in general, allowing the program to make cross-thread calls can lead to problematic and unexpected results
        ' such as duplicate assignment of variable values or attempts to simultaneously access the same resource.
        Control.CheckForIllegalCrossThreadCalls = False

        ' Update the instrument front displays
        SwitchIOWrite("node[2].display.clear()")
        SwitchIOWrite("node[1].display.clear()")
        SwitchIOWrite("node[2].display.settext('Test Running')")
        SwitchIOWrite("node[1].display.settext('Test Running')")


        ' Configure SourceMeter
        modShared.ConfigureHardware(cfgRecipe.BiasVoltage, cfgRecipe.CurrentRange / 1000000000, cfgRecipe.FilterType, cfgRecipe.FilterCount, cfgRecipe.NPLC)

        ' Reset the interval timer
        stpMovementTime.Restart()

        ' Set the flag for running the loop
        boolRunTest = True
        boolIsTestRunning = True

        ' Close all the switches
        SwitchIOWrite("node[1].channel.exclusiveslotclose('Sensor97')")

        ' Reset the reading count
        intReadingCount = 1

        'Run the testloop until the boolTestStop variable returns false (the user clicks stop test)
        Do While boolIsTestRunning
            'Start/Reset the interval timer
            stpIntervalTime.Restart()

            'Stop graph updates
            Chart1.Invoke(Sub() Chart1.Series.SuspendUpdates())

            'Reset the progress bar
            pbLoopProgress.Value = 0

            'Loop through the sensor readings
            For intLoopSensor = 1 To 96
                'Close the appropriate swithch pattern
                'SwitchIOWrite("node[1].channel.exclusiveslotclose('Sensor" & intLoopSensor & "')")
                'SwitchIOWrite("node[1].display.clear()")
                'SwitchIOWrite("node[1].display.settext('Sensor" & intLoopSensor & "')")

                ' Allow settling time
                'Delay(cfgGlobal.SettlingTime)

                ' Read V and I from buffer
                decReading = ReadIV(intLoopSensor)

                ' Determine Bath and Sensor ID
                intBathSensor.SequenceNumber = intLoopSensor

                ' Add reading to the SQL database if there's a carrier
                If lstCarrierCurrent(intBathSensor.Bath - 1).CarrierSQLID > 0 Then
                    strDataInsert = "INSERT INTO tblTestReadings " &
                                    "(carrierid, sensor, bath, readingid," &
                                    " [current], voltage, readingdatetime) " &
                                    "VALUES " &
                                    "(" & lstCarrierCurrent(intBathSensor.Bath - 1).CarrierSQLID & "," & intBathSensor.Sensor & "," & intBathSensor.Bath & "," & intReadingCount &
                                    "," & decReading(0) & "," & decReading(1) & ",'" & DateTime.Now & "')"
                    SQLInsert(strDataInsert)
                End If

                ' Update the Chart
                strCurrentGraphID = intBathSensor.Bath & "-" & intBathSensor.Sensor
                AddGraphData(strCurrentGraphID, intReadingCount, decReading(0))

                ' Update the progress bar
                decProgress = stpIntervalTime.ElapsedMilliseconds / 10 / cfgRecipe.ReadingInterval
                If decProgress > 100 Then
                    decProgress = 100
                End If
                Invoke(Sub() pbLoopProgress.Value = decProgress)

            Next




            'Update the control strip with the channel b voltage
            lblChannelBVoltage.Text = "CBV: " & Format(decReading(1), "0.00")

            ' Record the voltage and current across all sensors
            ' Close all Switches
            'SwitchIOWrite("node[1].channel.exclusiveclose('Sensor97')")
            'Debug.Print("node[1].channel.exclusiveclose('Sensor97')")

            ' Allow settling time
            Delay(cfgGlobal.SettlingTime)

            ' Record IV readings for SMUA
            decReading = ReadIV(97)

            ' Write string to data file
            If CarrierCheck() = True Then
                strDataInsert = "INSERT INTO tblTestReadings " &
                                    "(carrierid, sensor, bath, " &
                                    " readingid, [current], voltage, readingdatetime) " &
                                    "VALUES " &
                                    "(" & intCommonChannelID & ",1,7," & intReadingCount &
                                    "," & decReading(0) & "," & decReading(1) & ",'" & DateTime.Now & "')"
                SQLInsert(strDataInsert)
            End If

            'Update the chart  --  Note: the suspend/resume was added to address the problem of the graph being 
            '                            redrawn each time a data point was added.  This quickly becomes a problem
            '                            when a data point it added while the graph is being redrawn.  With one 
            '                            redraw per acqusition period, this should not be a problem.
            Chart1.Invoke(Sub() Chart1.Series.ResumeUpdates())
            'Delay(50)
            ' Invoke(Sub() Chart1.Invalidate())
            'Stop graph updates

            ' Update the progress bar
            decProgress = stpIntervalTime.ElapsedMilliseconds / 10 / cfgRecipe.ReadingInterval
            If decProgress > 100 Then
                decProgress = 100
            End If
            Invoke(Sub() pbLoopProgress.Value = decProgress)

            'Update Status strip with Channel A readings
            Invoke(Sub() lblChannelACurrent.Text = "CAC: " & Format(decReading(0), "0.00"))
            Invoke(Sub() lblChannelAVoltage.Text = "CAV: " & Format(decReading(1), "0.00"))



            'increment the reading count and prompt moveing the carriers if applicable
            intReadingCount = intReadingCount + 1

            'update the progress bar
            decProgress = stpIntervalTime.ElapsedMilliseconds / 10 / cfgRecipe.ReadingInterval
            If decProgress > 100 Then
                decProgress = 100
            End If
            Invoke(Sub() pbLoopProgress.Value = decProgress)

            ' Check if the readings can all be completed within the interval specified by the recipe
            If boolCheckReadingTime = True Then
                If stpIntervalTime.ElapsedMilliseconds >= cfgRecipe.ReadingInterval * 1000 Then
                    MsgBox("Readings could not be collected within the specified measurement interval." &
                           "Contact the system administrator.", MsgBoxStyle.Exclamation)
                    Exit Do
                Else
                    boolCheckReadingTime = False
                End If
            End If
            lblLastIntervalTime.Text = "Last Interval (sec): " & Format(stpIntervalTime.ElapsedMilliseconds / 1000, "0.00")


            While stpIntervalTime.ElapsedMilliseconds < cfgRecipe.ReadingInterval * 1000

                'Check if the test should be stopped
                If boolRunTest = False Then
                    boolIsTestRunning = False
                End If

                'Check if carriers should be moved
                If intReadingCount > cfgRecipe.MinimumReadings And CarrierCheck() = True Then
                    If bwkrMoveCarriers.IsBusy = False Then
                        bwkrMoveCarriers.RunWorkerAsync()
                    End If
                End If


                'Update the Progress Bar
                decProgress = stpIntervalTime.ElapsedMilliseconds / 10 / cfgRecipe.ReadingInterval
                If decProgress > 100 Then
                    decProgress = 100
                End If
                Invoke(Sub() pbLoopProgress.Value = decProgress)
            End While

            'check if the carriers have been moved
            If boolMoveCarriers = True Then
                For i = 0 To 5
                    lstCarrierCurrent(i).CarrierSQLID = lstCarrierNextPosition(i).CarrierSQLID
                    lstCarrierCurrent(i).CarrierText = lstCarrierNextPosition(i).CarrierText
                    lstCarrierCurrent(i).UserID = lstCarrierNextPosition(i).UserID
                Next
                ClearCarrierMovementLists()
                Chart1.Invoke(Sub() Chart1.Series.SuspendUpdates())
                Chart1.Invoke(Sub() UpdateChartTitles())
                'Remove the carriers from the queue
                For i = lstCarrierQueue.Items.Count - 1 To 0 Step -1
                    If lstCarrierQueue.Items(i).SubItems(2).Text = "" Then
                        ' Do nothing
                    Else
                        lstCarrierQueue.Items(i).Remove()
                    End If
                Next
                intReadingCount = 1
                boolMoveCarriers = False
                'reset the graphs
                For i = 0 To 95
                    Chart1.Invoke(Sub(ByRef a As Integer) Chart1.Series(a).Points.Clear(), i)
                Next
                Chart1.Invoke(Sub() Chart1.Series.ResumeUpdates())

                'enable the move button
                btnMoveCarriers.Enabled = True
                'Reset the movement timer
                stpMovementTime.Restart()
            End If


        Loop

        EndTest()
        boolIsTestRunning = False
        boolRunTest = False

    End Sub

    ' Name: AddGraphData()
    ' Parameters:
    '           strSensorID: String of sensor ID in the form of "SensorX", where X is a positive integer
    '           intTimePoint: Time index in seconds, a multiple of "interval" seconds
    '           dblCurrentReading: Current reading corresponding to the reading at intTimePoint
    ' Description: 
    Public Delegate Sub AddGraphDataDelegate(ByVal strSensorID As String, ByVal intTimePoint As Integer, ByVal decCurrentReading As Decimal)
    Public Sub AddGraphData(strSensorID As String, intTimePoint As Integer, decCurrentReading As Decimal)
        Dim intTime As Integer
        Dim decData As Decimal
        Dim dpData As DataPoint

        'Add data point using the variables passed to the subroutines
        Try
            If Chart1.InvokeRequired Then
                Chart1.Invoke(New AddGraphDataDelegate(AddressOf AddGraphData), strSensorID, intTimePoint, decCurrentReading)
            End If
            intTime = (intTimePoint - 1) * cfgRecipe.ReadingInterval
            decData = Math.Round(decCurrentReading * 10 ^ 9, 2)
            If decData < Decimal.MinValue Then
                decData = Decimal.MinValue
            ElseIf decData > Decimal.MaxValue Then
                decData = Decimal.MaxValue
            End If
            Chart1.Series(strSensorID).Points.AddXY(intTime, decData)
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub

    Private Sub MainLoop_Completed()
        tmrPeriodicUpdate.Stop()
        btnStartTest.Text = "Test Complete"
        lblTestStatus.Text = "Test Complete"
        btnStartTest.Enabled = False
        btnAddToQueue.Enabled = False
        btnMoveCarriers.Enabled = False
        btnRemoveItem.Enabled = False
    End Sub


End Class