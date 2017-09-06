Option Explicit On
Imports System.Data.SqlClient


'This module is for common SQL queries to the database

Module modSQLCom
    ' Name: SQLQueryInt()
    ' Variables:    strCommand, the SQL Query to be sent to the database;
    ' Returns:      int, the Primary Key matching the query string
    ' Description:  This function is sent a string containing the command to send to the SQL database, 
    '               and returns an integer of the primary key matching the query.  If no match is found,
    '               the function returns the integer "-1".
    '   NOTE:   This is not a general purpose query function.  For finding primary keys only (or other
    '           queries that will return only one match that is an integer).
    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function SQLQueryInt(ByRef strCommand As String)
        'Note: Code from https://support.microsoft.com/en-us/help/308656/how-to-open-a-sql-server-database-by-using-the-sql-server-.net-data-pr
        Dim myConn As SqlConnection
        Dim myCmd As SqlCommand
        Dim myReader As SqlDataReader
        Dim results As Int32

        Try
            'Create a Connection object.
            myConn = New SqlConnection("Initial Catalog=" & strDatabase & ";" &
                "Data Source=" & cfgGlobal.SQLServer & ";Integrated Security=SSPI;")

            'Create a Command object.
            myCmd = myConn.CreateCommand
            myCmd.CommandText = strCommand

            'Open the connection.
            myConn.Open()

            'Execute the command
            myReader = myCmd.ExecuteReader()

            'Store the results.
            If myReader.Read() Then             'Test to see if there is data in the reader
                results = myReader.GetInt32(0)  'Note:  if more than one match, only returns the first match
            Else
                results = -1                    'Returns "-1" if no match
            End If

            'Close the reader and the database connection.
            myReader.Close()
            myConn.Close()

        Catch ex As Exception
            GenericExceptionHandler(ex)
            'MsgBox("Can't connect to the SQL Server.  Please check server address.")
        End Try

        'Return the results.
        Return results

    End Function

    ' Name: SQLQueryStr()
    ' Variables:    strCommand, the SQL Query to be sent to the database;
    ' Returns:      string, the result matching the query string
    ' Description:  This function is sent a string containing the command to send to the SQL database, 
    '               and returns a string from the query matching the query.  If no match is found,
    '               the function returns "", empty string.
    '   NOTE:   This is not a general purpose query function.  For finding single results only (or other
    '           queries where you want the first result as a string).
    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Function SQLQueryStr(ByRef strCommand As String)
        'Note: Code from https://support.microsoft.com/en-us/help/308656/how-to-open-a-sql-server-database-by-using-the-sql-server-.net-data-pr
        Dim myConn As SqlConnection
        Dim myCmd As SqlCommand
        Dim myReader As SqlDataReader
        Dim results As String = "-1" 'Default: '-1' means "no match"

        Try
            'Create a Connection object.
            myConn = New SqlConnection("Initial Catalog=" & strDatabase & ";" &
                "Data Source=" & cfgGlobal.SQLServer & ";Integrated Security=SSPI;")

            'Create a Command object.
            myCmd = myConn.CreateCommand
            myCmd.CommandText = strCommand

            'Open the connection.
            myConn.Open()

            'Execute the command
            myReader = myCmd.ExecuteReader()

            'Store the results.
            If myReader.Read() Then             'Test to see if there is data in the reader
                results = myReader.GetString(0)  'Note:  if more than one match, only returns the first match
            End If

            'Close the reader and the database connection.
            myReader.Close()
            myConn.Close()

        Catch
            MsgBox("Can't connect to the SQL Server.  Please check server address.")
        End Try

        'Return the results.
        Return results

    End Function


    ' Name: SQLInsert()
    ' Variables:    strCommand, the SQL command to be sent to the database;
    ' Description:  This function is sent a string containing the command to send to the SQL database, 
    '               typically containing an INSERT command.  It could be used in other situations that only
    '               do not expect a reply from the server.
    <CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")>
    Public Sub SQLInsert(ByRef strCommand As String)
        'Note: Code modified from https://support.microsoft.com/en-us/help/308656/how-to-open-a-sql-server-database-by-using-the-sql-server-.net-data-pr
        Dim myConn As SqlConnection
        Dim myCmd As SqlCommand

        'Create a Connection object.
        myConn = New SqlConnection("Initial Catalog=" & strDatabase & ";" &
                "Data Source=" & strSQLServerAddress & ";Integrated Security=SSPI;")

        'Create a Command object.
        myCmd = myConn.CreateCommand
        myCmd.CommandText = strCommand

        'Open the connection.
        myConn.Open()

        'Execute the command
        myCmd.ExecuteNonQuery()

        'Close the reader and the database connection.
        myConn.Close()

    End Sub

    Function GetSourceMeterID()
        Dim dateCalDate As Date = Date.Parse("January 1 1970")
        Dim dateCalDue As Date = Date.Parse("January 1 1970")
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intSMID As Int32
        Dim strModel As String
        Dim strSerial As String
        Dim strFirmware As String
        Dim intCalDate As Int32
        Dim intDueDate As Int32

        strModel = SwitchIOWriteRead("print(node[2].model)")
        strSerial = SwitchIOWriteRead("print(node[2].serialno)")
        strFirmware = SwitchIOWriteRead("print(node[2].revision)")
        intCalDate = SwitchIOWriteRead("print(node[2].smua.cal.date)")
        intDueDate = SwitchIOWriteRead("print(node[2].smua.cal.due)")

        'Convert linux Epoch to date
        dateCalDate = dateCalDate.AddSeconds(intCalDate)
        dateCalDue = dateCalDue.AddSeconds(intDueDate)

        strSQLCommand = "SELECT sourcemeterid FROM tblSourceMeter " &
                        "WHERE (sourcemetermodel='" & strModel & "' AND " &
                               "sourcemeterserial='" & strSerial & "' AND " &
                               "sourcemeterfirmware='" & strFirmware & "' AND " &
                               "sourcemetercaldate='" & dateCalDate.ToString & "' AND " &
                               "sourcemeterduedate='" & dateCalDue.ToString & "')"

        intSMID = SQLQueryInt(strSQLCommand)

        If intSMID = -1 Then
            strSQLInsert = "INSERT INTO tblSourceMeter " &
                           "(sourcemetermodel, sourcemeterserial, sourcemeterfirmware, sourcemetercaldate, sourcemeterduedate) " &
                           "VALUES " &
                           "('" & strModel & "','" & strSerial & "','" & strFirmware & "','" & dateCalDate.ToString & "','" & dateCalDue.ToString & "')"
            SQLInsert(strSQLInsert)

            intSMID = SQLQueryInt(strSQLCommand)

        End If

        Return intSMID
    End Function

    Function GetSwitchID()
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intSID As Int32
        Dim strModel As String
        Dim strSerial As String
        Dim strFirmware As String

        strModel = SwitchIOWriteRead("print(localnode.model)")
        strSerial = SwitchIOWriteRead("print(localnode.serialno)")
        strFirmware = SwitchIOWriteRead("print(localnode.revision)")

        strSQLCommand = "SELECT systemswitchid FROM tblSystemSwitch " &
                        "WHERE (systemswitchmodel='" & strModel & "' AND " &
                               "systemswitchserial='" & strSerial & "' AND " &
                               "systemswitchfirmware='" & strFirmware & "')"

        intSID = SQLQueryInt(strSQLCommand)

        If intSID = -1 Then
            strSQLInsert = "INSERT INTO tblSystemSwitch " &
                           "(systemswitchmodel, systemswitchserial, systemswitchfirmware) " &
                           "VALUES " &
                           "('" & strModel & "','" & strSerial & "','" & strFirmware & "')"
            SQLInsert(strSQLInsert)

            intSID = SQLQueryInt(strSQLCommand)

        End If

        Return intSID
    End Function

    Function GetSwitchCardID(ByRef intSlot As Int16)
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intSCID As Int32
        Dim strModel As String
        Dim strSerial As String
        Dim strFirmware As String
        Dim strIDNString As String
        Dim aryIDN() As String

        'Request the card data from the system switch
        strIDNString = SwitchIOWriteRead("print(slot[" & intSlot & "].idn)")
        If strIDNString.Contains("Empty Slot") Then
            Return 0
            Exit Function
        Else
            'extrac Model, SN, and Rev from the string
            aryIDN = Split(strIDNString, ",")
            strModel = aryIDN(0)
            strSerial = aryIDN(2)
            strFirmware = aryIDN(3)
        End If

        strSQLCommand = "SELECT switchcardid FROM tblSwitchCard " &
                        "WHERE (switchcardmodel='" & strModel & "' AND " &
                               "switchcardserial='" & strSerial & "' AND " &
                               "switchcardfirmware='" & strFirmware & "')"

        intSCID = SQLQueryInt(strSQLCommand)

        If intSCID = -1 Then
            strSQLInsert = "INSERT INTO tblSwitchCard " &
                           "(switchcardmodel, switchcardserial, switchcardfirmware) " &
                           "VALUES " &
                           "('" & strModel & "','" & strSerial & "','" & strFirmware & "')"
            SQLInsert(strSQLInsert)

            intSCID = SQLQueryInt(strSQLCommand)

        End If

        Return intSCID
    End Function

    Function GetCardInfoID(ByRef intSlot As Int16)
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intSID As Int32
        Dim intSwitchCardID As Int32
        Dim intCounts As Int32


        'First, get the SwitchCardID
        intSwitchCardID = GetSwitchCardID(intSlot)

        'Start the SQL Queries
        strSQLCommand = "SELECT switchcountsid FROM tblSwitchCounts " &
                        "WHERE (switchcardid=" & intSwitchCardID
        strSQLInsert = "INSERT INTO tblSwitchCounts " &
                       "(switchcardid, row1ave, row2ave, row3ave, row4ave, row5ave, row6ave, row1backplane," &
                       " row2backplane, row3backplane, row4backplane, row5backplane, row6backplane) " &
                       "VALUES (" & intSwitchCardID

        'Calculate Ave closure counts for each row
        'and build the two queries as the loop progresses
        For intRow = 1 To 6
            intCounts = 0
            For intCol = 1 To 16
                intCounts = intCounts + SwitchIOWriteRead("print(channel.getcount('" & intSlot & intRow & Format(intCol, "00") & "'))")
            Next
            intCounts = intCounts / 16
            strSQLCommand = strSQLCommand & " AND row" & intRow & "ave=" & intCounts
            strSQLInsert = strSQLInsert & "," & intCounts
        Next

        'Get backplan closure counts for each row
        For intRow = 1 To 6
            intCounts = SwitchIOWriteRead("print(channel.getcount('" & intSlot & "91" & intRow & "'))")
            strSQLCommand = strSQLCommand & " AND row" & intRow & "backplane=" & intCounts
            strSQLInsert = strSQLInsert & "," & intCounts
        Next

        'Close the queries strings
        strSQLCommand = strSQLCommand & ")"
        strSQLInsert = strSQLInsert & ")"

        intSID = SQLQueryInt(strSQLCommand)

        If intSID = -1 Then
            SQLInsert(strSQLInsert)
            intSID = SQLQueryInt(strSQLCommand)
        End If

        Return intSID
    End Function

    Function GetParametersID()
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intPID As Int32

        MsgBox("GetParametersID Should not be used!!!!!!!!!!!!!!!!!!")

        strSQLCommand = "SELECT testparametersid FROM tblTestParameters " &
                        "WHERE (biasvoltage='" & cfgGlobal.Bias & "' AND " &
                               "currentrange='" & cfgGlobal.Range & "' AND " &
                               "nplc='" & cfgGlobal.NPLC & "' AND " &
                               "filtertype='" & cfgGlobal.Filter & "' AND " &
                               "filtercount='" & cfgGlobal.Samples & "' AND " &
                               "settlingtime='" & cfgGlobal.SettlingTime & "' AND " &
                               "bath1analyte='" & cfgGlobal.Bath1 & "' AND " &
                               "bath2analyte='" & cfgGlobal.Bath2 & "' AND " &
                               "bath3analyte='" & cfgGlobal.Bath3 & "' AND " &
                               "bath4analyte='" & cfgGlobal.Bath4 & "' AND " &
                               "bath5analyte='" & cfgGlobal.Bath5 & "' AND " &
                               "bath6analyte='" & cfgGlobal.Bath6 & "' AND " &
                               "readinginterval='" & cfgGlobal.RecordInterval & "' AND " &
                               "userid='" & Environment.UserName & "')"

        intPID = SQLQueryInt(strSQLCommand)

        If intPID = -1 Then
            strSQLInsert = "INSERT INTO tblTestParameters " &
                       "(biasvoltage, currentrange, nplc, filtertype, " &
                        "filtercount, settlingtime, bath1analyte, bath2analyte, " &
                        "bath3analyte, bath4analyte, bath5analyte, bath6analyte, " &
                        "readinginterval, userid) " &
                       "VALUES " &
                       "(" & cfgGlobal.Bias & "," & cfgGlobal.Range & "," & cfgGlobal.NPLC & ",'" & cfgGlobal.Filter &
                       "'," & cfgGlobal.Samples & "," & cfgGlobal.SettlingTime & ",'" & cfgGlobal.Bath1 & "','" & cfgGlobal.Bath2 &
                       "','" & cfgGlobal.Bath3 & "','" & cfgGlobal.Bath4 & "','" & cfgGlobal.Bath5 & "','" & cfgGlobal.Bath6 &
                       "'," & cfgGlobal.RecordInterval & ",'" & Environment.UserName & "')"
            SQLInsert(strSQLInsert)

            intPID = SQLQueryInt(strSQLCommand)
        End If

        Return intPID
    End Function

    Function GetTestConfigID()
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intPID As Int32

        strSQLCommand = "SELECT configid FROM tblTestConfig " &
                        "WHERE (equipmentid='" & cfgGlobal.STAID & "' AND " &
                               "sourcemeterid=" & intSourceMeterID & " AND " &
                               "systemswitchid=" & intSwitchID & " AND " &
                               "card1=" & intCard1CountsID & " AND " &
                               "card2=" & intCard2CountsID & " AND " &
                               "card3=" & intCard3CountsID & " AND " &
                               "card4=" & intCard4CountsID & " AND " &
                               "card5=" & intCard5CountsID & " AND " &
                               "card6=" & intCard6CountsID & " AND " &
                               "testrecipeid=" & cfgRecipe.RecipeID & " AND " &
                               "userid=" & intUserID & " AND " &
                               "softwarerev='" & strApplicationVersion & "' AND " &
                               "testdatetime='" & DateTime.Now & "')"

        intPID = SQLQueryInt(strSQLCommand)

        If intPID = -1 Then
            strSQLInsert = "INSERT INTO tblTestConfig " &
                       "(equipmentid, sourcemeterid, systemswitchid, card1, " &
                        "card2, card3, card4, card5, " &
                        "card6, testrecipeid, userid, softwarerev, " &
                        "testdatetime) " &
                       "VALUES " &
                       "('" & cfgGlobal.STAID & "'," & intSourceMeterID & "," & intSwitchID & "," & intCard1CountsID &
                       "," & intCard2CountsID & "," & intCard3CountsID & "," & intCard4CountsID & "," & intCard5CountsID &
                       "," & intCard6CountsID & "," & cfgRecipe.RecipeID & ",'" & intUserID & "','" & strApplicationVersion &
                       "','" & DateTime.Now & "')"
            SQLInsert(strSQLInsert)

            intPID = SQLQueryInt(strSQLCommand)
        End If

        Return intPID
    End Function



    Function GetLotID(ByRef strLot As String)
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intLID As Int32

        strSQLCommand = "SELECT lotid FROM tblLot " &
                        "WHERE (lot='" & strLot & "' AND " &
                               "configid=" & intTestConfigID & ")"

        intLID = SQLQueryInt(strSQLCommand)

        If intLID = -1 Then
            strSQLInsert = "INSERT INTO tblLot " &
                       "(lot, configid) " &
                       "VALUES " &
                       "('" & strLot & "'," & intTestConfigID & ")"
            SQLInsert(strSQLInsert)

            intLID = SQLQueryInt(strSQLCommand)
        End If

        Return intLID
    End Function



    ' GetLotFromCarrierID will take the CarrierSQLID from the carrier tracker and return a lot number string.
    ' If the CarrierID is "-1", there is no carrier and the function returns "No Carrier"
    Function GetLotFromCarrierID(ByRef intCarrierID As Int32)
        Dim strSQLCommand As String

        strSQLCommand = "SELECT lot FROM tblLot TL " &
                        "INNER JOIN tblCarriers TC ON TL.lotid=TC.lotid " &
                        "WHERE (carrierid=" & intCarrierID & ")"

        If intCarrierID = -1 Then
            Return "No Carrier"
            Exit Function
        Else
            Return SQLQueryStr(strSQLCommand)
        End If
    End Function





    Function CheckIfCarrierExists(strLot As String, strCarrier As String)
    Dim strSQLCommand As String
    Dim intLID As Int32

        strSQLCommand = "SELECT carrierid FROM tblCarriers TC " &
                        "INNER JOIN tblLot TL ON TC.lotid=TL.lotid " &
                        "WHERE (lot='" & strLot & "' AND " &
                               "carrier='" & strCarrier & "' AND configid=" & intTestConfigID & ")"

        intLID = SQLQueryInt(strSQLCommand)

    If intLID = -1 Then
        Return False
    Else
        Return True
    End If

End Function



    Function GetCarrierID(strLot As String, strCarrier As String, intUserID As Integer)
        Dim strSQLCommand As String
        Dim strSQLInsert As String
        Dim intLID As Int32
        Dim intCID As Int32

        intLID = GetLotID(strLot)

        strSQLCommand = "SELECT carrierid FROM tblCarriers " &
                        "WHERE (lotid='" & intLID & "' AND " &
                               "carrier='" & strCarrier & "' AND " &
                               "userid=" & intUserID & ")"

        intCID = SQLQueryInt(strSQLCommand)

        If intCID = -1 Then
            strSQLInsert = "INSERT INTO tblCarriers " &
                       "(lotid, carrier, userid) " &
                       "VALUES " &
                       "(" & intLID & ",'" & strCarrier & "'," & intUserID & ")"
            SQLInsert(strSQLInsert)

            intCID = SQLQueryInt(strSQLCommand)
        Else
            MsgBox("Serious Issue.  This shouldn't have happened, but the carrier you are trying to" & vbCr &
                   "move into the test system is already in the database.  Please rename and try again.")
        End If

        Return intCID
    End Function


End Module
