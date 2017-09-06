Public Class frmTestName

    Private Sub btnOk_Click() Handles btnOk.Click
        strTestID = txtTestID.Text
        strCarrier1 = txbxCarrier1.Text
        strCarrier2 = txbxCarrier2.Text
        strTestType = combxTestType.Text
        NewTest(txtTestID.Text, txtOperatorIntitials.Text)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'hide form
        Me.Hide()
        'run end test routine
        EndTest()
    End Sub

    Private Sub txtOperatorIntitials_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOperatorIntitials.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If txtOperatorIntitials.Text = "" Then
                'do nothing
            ElseIf txtTestID.Text = "" Then
                txtTestID.Select()
            Else
                btnOk_Click()
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub txtTestID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTestID.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            If txtTestID.Text = "" Then
                '
            ElseIf txtOperatorIntitials.Text = "" Then
                txtOperatorIntitials.Select()
            Else
                btnOk_Click()
            End If
            e.Handled = True
        End If
    End Sub
    ' Name: NewTest()
    ' Variables:    strBatch, the batch number inputted by the user;
    '               strUserID, the user initials inputted by the user
    ' Description:  This function establishes communication with the measurement hardware, opens a data file, 
    '               saves user ID and Batch Number to the data file, saves hardware info to the datafile, configures
    '               the hardware for verification, and performs verification.
    Public Sub NewTest(strBatch As String, strUserID As String)

        'Hide frmTestInfo
        Me.Hide()

        '++++++++++Begin section to delete eventually++++++++++++++

        'Build filename from batch number and carriers and test type    'Added 06Jun2017
        strBatch = strBatch & "-" & strCarrier1 & strCarrier2 & "-" & strTestType

        'Open data file
        If Not OpenDataFile(cfgGlobal.DumpDirectory, strBatch & ".csv") Then
            MsgBox("Batch Number already in use.  Please choose another.", MsgBoxStyle.OkOnly, "File already exists.")
            Me.Show()
            Me.txtTestID.Select()
            Exit Sub
        End If

        'Write Test ID and User initials to data file
        WriteToDataFile("Batch Number:," & strBatch)
        WriteToDataFile("Slot 1 Carrier:," & strCarrier1)   'Added 06Jun2017
        WriteToDataFile("Slot 2 Carrier:," & strCarrier2)    'Added 06Jun2017
        WriteToDataFile("Test Type:," & strTestType)        'Added 06Jun2017
        WriteToDataFile("Operator Initials:," & strUserID)

        '+++++++++End section to delete+++++++++++++++ (legacy code for saving to csv file)


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

        'TestParameters to SQL - DB 13Jul2017
        intParametersID = GetParametersID()

        'Store Test Configuration to SQL - DB 13Jul2017
        intTestConfigID = GetTestConfigID()

        'Initialize the Carrier Arrays
        InitializeCarriers()

        'Check to see if frmSensorID should be used +++++++++++++++++++Delete eventually (legacy code for csv file)
        If cfgGlobal.SensorNaming = True Then
            'Show test info form
            frmSensorID.Show()
        Else
            strSensorIDHeader = SensorHeaderFromCarriers()
            frmTestForm.Show()
        End If

        '+++++++++++ This Code for 2-up, no hopscotch only+++++++++++++
        Dim intLotID As Int32
        intLotID = GetLotID(strBatch)
        lstCarrierCurrent(0).CarrierSQLID = intLotID
        lstCarrierCurrent(1).CarrierSQLID = intLotID
        lstCarrierCurrent(0).CarrierText = strCarrier1
        lstCarrierCurrent(1).CarrierText = strCarrier2
        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    End Sub


    Function SensorHeaderFromCarriers() As String
        Dim strTemp As String
        Dim i As Integer

        strTemp = "Time:,"
        'Add carrier 1 IDs
        For i = 1 To 16
            strTemp = strTemp & strCarrier1 & i & ","
        Next

        'Add carrier 2 IDs
        For i = 1 To 16
            strTemp = strTemp & strCarrier2 & i & ","
        Next

        Return strTemp

    End Function



    Private Sub InitializeCarriers()
        Dim i As Integer

        For i = 0 To 5
            lstCarrierIncoming.Add(New clsCarrier)
            lstCarrierCurrent.Add(New clsCarrier)
            lstCarrierNextPosition.Add(New clsCarrier)
        Next


    End Sub



End Class