Public Class frm31Login
    Private Sub frm31Login_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtbxUserID.Text = strCurrentUser
    End Sub

    Private Sub frm31Login_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Me.Focus()
    End Sub


End Class