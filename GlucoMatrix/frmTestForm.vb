Option Explicit On
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Runtime.InteropServices
' --------------------------------------------------------------------------------------------------
' frmTestForm is the interface presented to the user during the actual conduct of a sensor test.
' It contains the following major components:
' 1. Current vs. Time chart with traces for each sensor being tested
' 2. Show/Hide menu allowing user to toggle the display of each sensor individually
' 3. A set of elements allowing the user to customize the look of the current vs. time chart
' 4. Basic information about the test (test name, elapsed time, etc)
' 5. A summary of user-noted injections
' 6. Buttons to start and stop test
' 7. Button to note analyte injections
' --------------------------------------------------------------------------------------------------
Public Class frmTestForm
    Dim dblBias As Double
    Dim dblRecordInterval As Double
    Dim strCurrentRange As String
    Dim strFilterType As String
    Dim lngSamples As Long
    Dim lngNPLC As Long
    Dim strName As String
    Dim strSTAID As String
    Dim strDumpDir As String
    Dim strCardConfig As String
    Dim intTimeInterval As Integer      ' IntTimeInterval = intReading * intInterval

    Dim stpInjectionTime As New Stopwatch    ' Stopwatch to track the time since the last noted injection
    Dim stpTotalTime As New Stopwatch        ' Stopwatch to track the total elapsed time in the test
    Dim intInjectionCounter As Integer       ' Counter for the number of injections that have been performed


    ' All variables prefaced with current- are declared with
    ' module-level scope so that cross-thread references can be made without throwing an exception
    Dim lngCurrentTime As Long              ' Timestamp for last gathered reading
    Dim dblCurrentCurrent As Double         ' Current for last gathered reading
    Dim intCurrentSlot As Integer           ' Card slow for last gathered reading
    Dim intCurrentColumn As Integer         ' Card column for last gathered reading
    Public strCurrentID As String              ' SensorID for last gathered reading








    ' Name: btnNoteInjection_Click
    ' Handles: User clicks 'Note Injection' button
    ' Description: Adds a new timestamp to the TestFile Injections array and resets the stpInjectionTime stopwatch.
    Private Sub btnNoteInjection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNoteInjection.Click
        ' Add the current time to the test file injections array
        Try
            Dim txtNewInjection As New Label
            txtNewInjection.Text = Format(stpTotalTime.Elapsed.Hours, "00") & ":" & Format(stpTotalTime.Elapsed.Minutes, "00")
            flwInjections.Controls.Add(txtNewInjection)
            intInjectionCounter = intInjectionCounter + 1

            If (stpInjectionTime.IsRunning) Then
                stpInjectionTime.Restart()
            Else
                stpInjectionTime.Start()
            End If
            MsgBox("Injection noted", vbOKOnly)
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    ' Sub: btnStartTest_Click()
    ' Handles: User clicks the Start/Stop button
    ' If a test is not already running, this sub starts the test thread (BackgroundWorker1)
    ' If the test is running it initiates the test stop sequence by setting the boolIsTestRunning
    ' flag to false and disabling further user interaction with the start/stop button
    Private Sub btnStartTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStartTest.Click
        Try
            If (boolIsTestRunning) Then
                boolIsTestRunning = False
                btnStartTest.Text = "Ending Test"
                btnStartTest.Enabled = False
                btnNoteInjection.Enabled = False
            Else
                boolIsTestRunning = True
                btnStartTest.Text = "Stop"
                MainLoop.RunWorkerAsync()
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub

    ' ----------------------------------------------
    ' Utility functions
    ' ----------------------------------------------
    Private Sub prepareForm()
        Try
            ' Clear the default or previous series and legends from the test chart
            TestChart.Series.Clear()

            TestChart.Legends.Clear()
            txtTestName.Text = "Test Name: " & frmTestName.txtTestID.Text
            txtOperator.Text = "Operator: " & frmTestName.txtOperatorIntitials.Text
            ' Configure the test chart
            With TestChart.ChartAreas(0)
                .CursorX.AutoScroll = False
                .CursorY.AutoScroll = False
                .CursorX.IsUserEnabled = True
                .CursorY.IsUserEnabled = True
                .CursorX.Interval = 0
                .CursorY.Interval = 0
                .AxisX.ScaleView.MinSize = 0
                .AxisY.ScaleView.MinSize = 0
                .AxisY.Minimum = 0
                .AxisX.Minimum = 0
                .AxisX.Title = "Elapsed Time (s)"
                .AxisY.Title = "Current (nA)"
                ' Setting IsMarginVisible to false increases the accuracy of deep zooming.  If this is true then zooms are padded
                ' and do not show the actual area selected
                .AxisX.IsMarginVisible = False
                .AxisY.IsMarginVisible = False
                .Name = "Main"
            End With
            ' Populate the chart series and legend
            For i = 1 To 32
                TestChart.Series.Add("Sensor" & i)
                With TestChart.Series("Sensor" & i)
                    .ChartType = SeriesChartType.Line
                    .BorderWidth = 2
                    ' other properties go here later
                End With
                TestChart.Legends.Add("Sensor" & i)
                With TestChart.Legends("Sensor" & i)
                    .Title = "Sensor" & i
                    .BorderColor = Color.Black
                    .BorderWidth = 2
                    .LegendStyle = LegendStyle.Column
                    .DockedToChartArea = "Main"
                    .IsDockedInsideChartArea = True
                End With
                Dim chkNewBox As New CheckBox
                With chkNewBox
                    .Width = 140
                    .Name = "Sensor" & i
                    .Text = "Sensor" & i
                    .Enabled = True
                    .Visible = True
                    .Checked = True
                    AddHandler chkNewBox.Click, AddressOf UpdateTraces
                End With
                HideShowSensors.Controls.Add(chkNewBox)
            Next
            TestChart.ChartAreas(0).AxisX.Maximum = 300
            TestChart.ChartAreas(0).AxisX.Interval = 60

            Dim btnShowAllButton As New Button
            With btnShowAllButton
                .Name = "btnShowAllSensors"
                .Text = "Show All"
                .Font = New Font("Microsoft Sans Serif", 12)
                .AutoSize = True
            End With
            HideShowSensors.Controls.Add(btnShowAllButton)
            Dim btnHideAllButton As New Button
            With btnHideAllButton
                .Name = "btnHideAllSensors"
                .Text = "Hide All"
                .Font = New Font("Microsoft Sans Serif", 12)
                .AutoSize = True
            End With
            HideShowSensors.Controls.Add(btnHideAllButton)
            AddHandler btnShowAllButton.Click, AddressOf showAllButton_Click
            AddHandler btnHideAllButton.Click, AddressOf hideAllButton_Click
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    ' Name: AddGraphData()
    ' Parameters:
    '           strSensorID: String of sensor ID in the form of "SensorX", where X is a positive integer
    '           intTimePoint: Time index in seconds, a multiple of "interval" seconds
    '           dblCurrentReading: Current reading corresponding to the reading at intTimePoint
    ' Description: 
    Public Sub AddGraphData(strSensorID As String, intTimePoint As Integer, dblCurrentReading As Double)
        'Add data point using the variables passed to the subroutines
        TestChart.Series(strSensorID).Points.AddXY(CDbl(intTimePoint), Math.Round(dblCurrentReading, 2))
    End Sub

    ' Name: RefreshGraph()
    ' Parameters:
    '           strSensorID: String of sensor ID in the form of "SensorX", where X is a positive integer
    '           intTimePoint: Time index in seconds, a multiple of "interval" seconds
    '           dblCurrentReading: Current reading corresponding to the reading at intTimePoint
    ' Description: 
    Public Sub RefreshGraph()
        If Not TestChart.ChartAreas(0).AxisY.ScaleView.IsZoomed Then
            TestChart.ChartAreas(0).AxisY.Interval = Math.Round(TestChart.ChartAreas(0).AxisY.Maximum / 10, 1)
        Else
            ' do nothing
        End If

        If Not TestChart.ChartAreas(0).AxisX.ScaleView.IsZoomed Then
            TestChart.ChartAreas(0).AxisX.Maximum = ((intTimeInterval \ 300) + 1) * 300
            TestChart.ChartAreas(0).AxisX.Interval = ((intTimeInterval \ 300) + 1) * 300 / 5
        Else
            ' do nothing
        End If

        TestChart.Update()
    End Sub

    Public Sub MeasurementLoop(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Try
            ' --------------------------------------
            ' Declare locally-scoped variables
            ' --------------------------------------
            Dim stpIntervalTimer As New Stopwatch ' Tracks elapsed time for current measurement interval
            Dim intInterval As Integer = cfgGlobal.RecordInterval ' Length of measurement interval in seconds
            Dim intMilliseconds As Integer      ' Measurement interval in milliseconds
            Dim intCardCount As Integer = cfgGlobal.CardConfig
            Dim intSensorCount As Integer       ' Number of Sensors to scan
            Dim strVoltageReadings As String    ' String for building list of voltage readings
            Dim strCurrentReadings As String    ' String for building list of current readings
            Dim intReading As Integer = 0       ' Integer for counting the number of measurements; used for calculating
            Dim intLoopSensor As Integer        ' Loop counter for main measurement loop
            Dim decReading() As Decimal = {0, 0}
            Dim intBathSensor As New clsBathSensor
            Dim strData As String               ' local variable for holding data to be written to file 
            Dim strDataInsert As String         ' string for the sql command to insert the data point

            ' Define Variables
            intMilliseconds = intInterval * 1000
            intSensorCount = 16 * cfgGlobal.CardConfig

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
            ConfigureHardware(cfgGlobal.Bias, cfgGlobal.Range * 0.000000001, cfgGlobal.Filter, cfgGlobal.Samples, cfgGlobal.NPLC)

            ' Add headers to the data file:
            '       Start with a blank line and the date/time of test start:
            WriteToDataFile(vbCr & "Test Start:," & DateTime.Now() & vbCr)

            '       Next, add in headers for current or voltage
            strData = ","
            For intLoopSensor = 1 To cfgGlobal.CardConfig * 16
                strData = strData & "Current (nA),"
            Next
            strData = strData & ",Injection,,"
            For intLoopSensor = 1 To cfgGlobal.CardConfig * 16
                strData = strData & "Voltage (V),"
            Next
            strData = strData & ",Current (nA), Voltage (V)"

            WriteToDataFile(strData)

            '       Next, add in the sensor IDs
            'strData = frmSensorID.SensorHeader
            WriteToDataFile("Time:," & strSensorIDHeader & ",Period,," & strSensorIDHeader & ",SMUA,SMUA")

            '' '' Start all timers
            ElapsedTimer.Start() ' Start the form timer component
            stpIntervalTimer.Start() ' Current interval stopwatch
            stpTotalTime.Start() ' total test time stopwatch
            ' ''fCurrentTestFile.TestStart = DateTime.Now()

            'Set the flags for running the loop
            boolIsTestRunning = True

            'Run the testloop until the boolTestStop variable returns false (the user clicks Abort)
            Do While boolIsTestRunning
                ' Determine the "Time" for the set of measurements, and add it to the current measurement string
                intTimeInterval = intReading * intInterval
                strCurrentReadings = CStr(intTimeInterval) + ","
                ' Reset the voltage measurement string
                strVoltageReadings = ","

                'loop through sensor readings
                For intLoopSensor = 1 To intSensorCount
                    'Close the appropriate switch pattern
                    SwitchIOWrite("node[1].channel.exclusiveclose('Sensor" & intLoopSensor & "')")
                    Debug.Print("node[1].channel.exclusiveclose('Sensor" & intLoopSensor & "')")

                    ' Allow settling time
                    Delay(cfgGlobal.SettlingTime)

                    ' Read V and I from buffer
                    decReading = ReadIV(intLoopSensor)

                    ' Determine Bath and Sensor ID

                    intBathSensor = GetBathSensor(intLoopSensor)

                    ' Add reading to current and voltage strings
                    strCurrentReadings = strCurrentReadings + CStr(decReading(0)) + ","
                    strVoltageReadings = strVoltageReadings + CStr(decReading(1)) + ","

                    ' Add reading to the SQL database

                    strDataInsert = "INSERT INTO tblTestReadings " &
                                    "(lotid, sensor, bath, " &
                                    " readingid, [current], voltage, datetime) " &
                                    "VALUES " &
                                    "(" & lstCarrierCurrent(intBathSensor.Bath - 1).CarrierSQLID & "," & intBathSensor.Sensor & "," & intInjectionCounter + 1 &
                                    "," & intReading & "," & decReading(0) & "," & decReading(1) & ",'" & DateTime.Now & "')"
                    SQLInsert(strDataInsert)

                    ' Update the Chart
                    strCurrentID = "Sensor" & intLoopSensor
                    AddGraphData(strCurrentID, intTimeInterval, decReading(0))
                Next

                ' Record the voltage and current across all sensors
                ' Close all Switches
                SwitchIOWrite("node[1].channel.exclusiveclose('Sensor" & intSensorCount + 1 & "')")
                Debug.Print("node[1].channel.exclusiveclose('Sensor" & intSensorCount + 1 & "')")

                ' Allow settling time
                Delay(cfgGlobal.SettlingTime)

                ' Record IV readings for SMUA
                decReading = ReadIV("a")

                ' Write string to data file
                WriteToDataFile(strCurrentReadings & "," & intInjectionCounter & "," & strVoltageReadings & "," & decReading(0) & "," & decReading(1))

                RefreshGraph()

                intReading = intReading + 1

                ' Check if measurements complete within the interval period
                If (stpIntervalTimer.ElapsedMilliseconds > intMilliseconds) Then
                    MsgBox("Could not finish measurements within injection interval specified")
                    boolIsTestRunning = False
                End If
                Dim intLast As Integer

                'Wait for the remainder of the interval period to elapse
                Do Until stpIntervalTimer.ElapsedMilliseconds >= intMilliseconds
                    ' do nothing.  This is to ensure that the interval elapses before another round of measurements
                    If Not boolIsTestRunning Then
                        If intLast = 0 And (intMilliseconds - stpIntervalTimer.ElapsedMilliseconds) / 1000 = 0 Then
                            btnStartTest.Text = "Test Complete"
                        Else
                            If Not ((intMilliseconds - stpIntervalTimer.ElapsedMilliseconds) / 1000 = intLast) Then
                                intLast = (intMilliseconds - stpIntervalTimer.ElapsedMilliseconds) / 1000
                                btnStartTest.Text = "Ending In " & intLast
                            End If
                        End If
                    End If
                Loop

                'Restart the interval timer, then repeat the loop
                stpIntervalTimer.Restart()
            Loop

            ' After test is complete, reset state, do the following:
            ' Write end test time to file
            WriteToDataFile(vbCr & "Test End:," & DateTime.Now())

            ' Change the text of the start test button to test complete
            btnStartTest.Text = "Test Complete"

            ' Update the displays of the measurement hardware
            SwitchIOWrite("node[2].display.clear()")
            SwitchIOWrite("node[1].display.clear()")
            SwitchIOWrite("node[2].display.settext('Standby')")
            SwitchIOWrite("node[1].display.settext('Standby')")

            ' Update the booleans to reflect the 
            boolIsTestRunning = False

            ' Stop the total time timer
            stpTotalTime.Stop()

            ' Turn off the sourceMeter
            SwitchIOWrite("node[2].smub.source.output = 0 node[2].smua.source.output = 0")

        Catch ex As COMException
            ComExceptionHandler(ex)
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub

    Public Sub TestChart_AxisViewChanged(sender As Object, e As ViewEventArgs) Handles TestChart.AxisViewChanged
        Dim dblMin As Double
        dblMin = TestChart.ChartAreas(0).AxisX.ScaleView.Position
        If dblMin < 0 Then
            TestChart.ChartAreas(0).AxisX.ScaleView.Position = 0
        Else
            TestChart.ChartAreas(0).AxisX.ScaleView.Position = dblMin \ 1
        End If
        dblMin = TestChart.ChartAreas(0).AxisY.ScaleView.Position
        If dblMin < 0 Then
            TestChart.ChartAreas(0).AxisY.ScaleView.Position = 0
        End If
        TestChart.ChartAreas(0).AxisY.ScaleView.Position = Math.Round(TestChart.ChartAreas(0).AxisY.ScaleView.Position, 1)
        ' Adjust the interval so there are 10 lines shown, and round this interval to an integer
        TestChart.ChartAreas(0).AxisX.Interval = TestChart.ChartAreas(0).AxisX.ScaleView.Size \ 10
        ' Adjust the y interval so 10 lines are shown and round interval to the nearest 0.1
        TestChart.ChartAreas(0).AxisY.Interval = Math.Round(TestChart.ChartAreas(0).AxisY.ScaleView.Size / 10, 1)
    End Sub
    Public Sub TestChart_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TestChart.MouseDown
        ' Call hit test method
        Try
            Dim result As HitTestResult = TestChart.HitTest(e.X, e.Y)
            If result.ChartElementType <> ChartElementType.DataPoint Then
                If (result.ChartElementType <> ChartElementType.LegendItem) Then
                    Return
                End If
            End If
            Dim strSeriesName As String = result.Series.Name
            If TestChart.Series(strSeriesName).BorderWidth = 2 Then
                For Each aSeries As Series In TestChart.Series
                    aSeries.BorderWidth = 2
                Next
                For Each aLegend As Legend In TestChart.Legends
                    aLegend.BorderWidth = 2
                Next
                TestChart.Legends(strSeriesName).BorderWidth = 6
                TestChart.Series(strSeriesName).BorderWidth = 6
            Else
                TestChart.Legends(strSeriesName).BorderWidth = 2
                TestChart.Series(strSeriesName).BorderWidth = 2
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub

    Public Sub btnZoomReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomReset.Click
        Try
            With TestChart
                .ChartAreas(0).AxisX.ScaleView.ZoomReset(0)
                .ChartAreas(0).AxisY.ScaleView.ZoomReset(0)
                .ChartAreas(0).AxisX.Maximum = Double.NaN
                .ChartAreas(0).AxisX.Minimum = 0
                .ChartAreas(0).AxisY.Maximum = Double.NaN
                .ChartAreas(0).AxisY.Minimum = 0
            End With

            With Me
                .txtXMax.Text = ""
                .txtYMax.Text = ""
                .txtXMin.Text = ""
                .txtYMin.Text = ""
            End With


        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    Public Sub chkZoomEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkZoomEnabled.CheckedChanged
        Try
            If (chkZoomEnabled.Checked) Then
                With TestChart.ChartAreas(0)
                    .AxisX.ScaleView.Zoomable = True
                    .AxisY.ScaleView.Zoomable = True
                    .AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Number
                    .AxisX.ScaleView.MinSize = cfgGlobal.RecordInterval * 3
                    .AxisY.ScaleView.MinSizeType = DateTimeIntervalType.Number
                    .AxisY.ScaleView.MinSize = 0.5
                    .AxisY.RoundAxisValues()
                    .AxisX.RoundAxisValues()
                    .CursorY.IsUserSelectionEnabled = True
                    .CursorX.IsUserSelectionEnabled = True
                End With

            Else
                With TestChart.ChartAreas(0)
                    .AxisX.ScaleView.Zoomable = False
                    .AxisY.ScaleView.Zoomable = False
                    .CursorY.IsUserSelectionEnabled = False
                    .CursorX.IsUserSelectionEnabled = False
                End With
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    Public Sub chkScrollEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkScrollEnabled.CheckedChanged
        Try
            If (chkZoomEnabled.Checked) Then
                With TestChart.ChartAreas(0)
                    .CursorX.AutoScroll = True
                    .CursorY.AutoScroll = True
                End With
            Else
                With TestChart.ChartAreas(0)
                    .CursorX.AutoScroll = False
                    .CursorY.AutoScroll = False
                End With
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    Public Sub btnZoomOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomOut.Click
        Try
            TestChart.ChartAreas(0).AxisX.ScaleView.ZoomReset()
            TestChart.ChartAreas(0).AxisY.ScaleView.ZoomReset()
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    Public Sub btnApply_Click() Handles btnApply.Click
        Try
            If Not (txtXMax.Text = "") Then
                TestChart.ChartAreas(0).AxisX.Maximum = txtXMax.Text
            Else
                TestChart.ChartAreas(0).AxisX.Maximum = Double.NaN
            End If
            If Not (txtYMax.Text = "") Then
                TestChart.ChartAreas(0).AxisY.Maximum = txtYMax.Text
            End If
            If Not (txtXMin.Text = "") Then
                TestChart.ChartAreas(0).AxisX.Minimum = txtXMin.Text
            End If
            If Not (txtYMin.Text = "") Then
                TestChart.ChartAreas(0).AxisY.Minimum = txtYMin.Text
            End If
            TestChart.ChartAreas(0).AxisX.ScaleView.ZoomReset(0)
            TestChart.ChartAreas(0).AxisY.ScaleView.ZoomReset(0)
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    Public Sub showAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each aSeries In TestChart.Series
                aSeries.Enabled = True
                aSeries.IsVisibleInLegend = True
            Next
            For Each ctrl In HideShowSensors.Controls
                If ctrl.GetType.ToString = "System.Windows.Forms.CheckBox" Then
                    ctrl.Checked = True
                End If
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    Public Sub hideAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            For Each aSeries In TestChart.Series
                aSeries.Enabled = False
                aSeries.IsVisibleInLegend = False
            Next
            For Each ctrl In HideShowSensors.Controls
                If ctrl.GetType.ToString = "System.Windows.Forms.CheckBox" Then
                    ctrl.Checked = False
                End If
            Next
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub
    Public Sub UpdateTraces(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If (sender.Checked) Then
                TestChart.Series(sender.Text).Enabled = True
                TestChart.Series(sender.Text).IsVisibleInLegend = True
            Else
                TestChart.Series(sender.Text).Enabled = False
                TestChart.Series(sender.Text).IsVisibleInLegend = False
            End If
        Catch ex As Exception
            GenericExceptionHandler(ex)
        End Try
    End Sub

    Private Sub txtXMin_Enter(sender As Object, e As EventArgs) Handles txtXMin.Enter
        btnApply_Click()
    End Sub

    Private Sub txtXMax_Enter(sender As Object, e As EventArgs) Handles txtXMax.Enter
        btnApply_Click()
    End Sub

    Private Sub txtYMin_Enter(sender As Object, e As EventArgs) Handles txtYMin.Enter
        btnApply_Click()
    End Sub

    Private Sub txtYMax_Enter(sender As Object, e As EventArgs) Handles txtYMax.Enter
        btnApply_Click()
    End Sub

    Private Sub frmTestForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    End Sub
End Class