Option Explicit On
Imports System.DirectoryServices
Imports System.IO
Imports System.Xml.Serialization
Public Class frm01Login

    ' ------------------------------------------
    ' Event Handlers
    ' -------------------------------------------

    ' Name: frm01Login_Load
    ' Handles: Opening of program
    ' Description: Attempts to load the configuration file and updates status box
    Private Sub frm01Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadOrRefreshConfiguration()
        lblVersion.Text = "Version " & strApplicationVersion
    End Sub



    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim Success As Boolean = False
        Dim strDomain As String = Environment.UserDomainName    ' Current domain login

        'Check if network user/password is correct
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & strDomain, txtbxUserID.Text, txtbxPassword.Text)
        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)
        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel
        Try
            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)
        Catch
            Success = False
        End Try

        txtbxPassword.Text = ""

        If Success = False Then
            MsgBox("User ID or Password are incorrect")
            Exit Sub
        End If

        'Check if user is in SQL Database
        Dim strQuery As String
        strQuery = "SELECT TOP 1 [userid] FROM tblUsers " &
                   "WHERE [user]='" & txtbxUserID.Text & "' " &
                   "ORDER BY [userid] DESC"

        intUserID = SQLQueryInt(strQuery)

        If intUserID = -1 Then
            MsgBox("User not found.  Contact Test Administrator.")
            Exit Sub
        End If

        'Check if user is active
        strQuery = "SELECT CAST([active] AS int) FROM tblUsers " &
                   "WHERE [userid]=" & intUserID

        If SQLQueryInt(strQuery) = 0 Then
            MsgBox("User is not active.  Contact Test Administrator.")
            Exit Sub
        End If

        'Set Admin flag, if applicable
        boolIsAdmin = False
        strQuery = "SELECT CAST([admin] AS int) FROM tblUsers " &
                   "WHERE [userid]=" & intUserID
        If SQLQueryInt(strQuery) = 1 Then
            boolIsAdmin = True
        Else
            boolIsAdmin = false
        End If

        'Set the current user
        strCurrentUser = txtbxUserID.Text

        'Show the main menu, hide this form
        frm02MainMenu.Show()
        Me.Hide()


    End Sub

    Public Sub btnConnectionSettings_Click(sender As Object, e As EventArgs) Handles btnConnectionSettings.Click
        'Display input box asking for SQL Server address.  Use current value for the address as default.
        cfgGlobal.SQLServer = InputBox("Enter SQL Server Address:", "SQL Server Address", cfgGlobal.SQLServer)

        ' Attempt to write the configuration to file
        If (cfgGlobal.WriteToFile(strAppDir & Path.DirectorySeparatorChar & strConfigFileName)) Then
            boolConfigStatus = True
        Else
            MsgBox("Could not write configuration to file.")
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class