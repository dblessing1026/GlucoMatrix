Option Explicit On
Imports System.IO

Public Class frm02MainMenu

    ' ------------------------------------------
    ' Event Handlers
    ' -------------------------------------------

    ' Name: frm02MainMenu_Load
    ' Handles: Opening of program
    ' Description: Attempts to load the configuration file and updates status box
    '              and buttons based on user access level
    Private Sub frm02MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Refresh the configuration
        LoadOrRefreshConfiguration()

        'Update the version number
        lblVersion.Text = "Version " & strApplicationVersion

        'Update the logged in user
        lblCurrentUser.Text = "Current User: " & strCurrentUser

        'Resize window/adjust controls based on user access level
        With Me
            If boolIsAdmin = False Then
                .btnManageUsers.Hide()
                .btnManageRecipes.Hide()
                .btnSetIPAddress.Hide()
                .btnSetEquipmentID.Hide()
                .Height = 325
            Else
                .btnManageUsers.Show()
                .btnManageRecipes.Show()
                .btnSetIPAddress.Show()
                .btnSetEquipmentID.Show()
                .Height = 575
            End If
        End With
    End Sub

    ' Name: btnLogOut_Click
    ' Handles: Clicking of btnLogOut
    ' Description: Clears variables related to user ID, closes all windows, and shows frm01Login
    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        'Check if test is running
        If boolIsTestRunning Then
            MsgBox("Test is running.  Please end test then try again.")
            Exit Sub
        End If

        'Clear user specific global variables
        intUserID = -1
        strCurrentUser = ""

        'close all windows (except frm01Login)
        My.Application.OpenForms.Cast(Of Form)() _
              .Except({frm01Login}) _
              .ToList() _
              .ForEach(Sub(form) form.Close())

        'show frm01Login and bring to front
        frm01Login.Show()
        frm01Login.TopMost = True
    End Sub

    Private Sub btnSetEquipmentID_Click(sender As Object, e As EventArgs) Handles btnSetEquipmentID.Click
        'Display input box asking for the equipment IP Address.  Use current value for the address as default.
        cfgGlobal.STAID = InputBox("Enter GlucoMatrix Equipment ID:", "GlucoMatrix GlucoMatrix Equipment ID", cfgGlobal.STAID)

        ' Attempt to write the configuration to file
        If (cfgGlobal.WriteToFile(strAppDir & Path.DirectorySeparatorChar & strConfigFileName)) Then
            boolConfigStatus = True
        Else
            MsgBox("Could not write configuration to file.")
        End If

    End Sub

    Private Sub btnSetIPAddress_Click(sender As Object, e As EventArgs) Handles btnSetIPAddress.Click
        'Display input box asking for the equipment IP Address.  Use current value for the address as default.
        cfgGlobal.Address = InputBox("Enter GlucoMatrix IP Address:", "GlucoMatrix IP Address", cfgGlobal.Address)

        ' Attempt to write the configuration to file
        If (cfgGlobal.WriteToFile(strAppDir & Path.DirectorySeparatorChar & strConfigFileName)) Then
            boolConfigStatus = True
        Else
            MsgBox("Could not write configuration to file.")
        End If

    End Sub

    Private Sub btnNewTest_Click(sender As Object, e As EventArgs) Handles btnNewTest.Click
        frm03RunTest.Show()
    End Sub
End Class