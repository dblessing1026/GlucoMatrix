<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm21ModifyUsers
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
        Me.lstvUsers = New System.Windows.Forms.ListView()
        Me.clmUserPK = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clmUserID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clmAdmin = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clmActive = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clmModBy = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lstvUsers
        '
        Me.lstvUsers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clmUserPK, Me.clmUserID, Me.clmAdmin, Me.clmActive, Me.clmModBy})
        Me.lstvUsers.Location = New System.Drawing.Point(58, 59)
        Me.lstvUsers.Name = "lstvUsers"
        Me.lstvUsers.Size = New System.Drawing.Size(500, 300)
        Me.lstvUsers.TabIndex = 0
        Me.lstvUsers.UseCompatibleStateImageBehavior = False
        Me.lstvUsers.View = System.Windows.Forms.View.Details
        '
        'clmUserPK
        '
        Me.clmUserPK.Text = "User PK"
        Me.clmUserPK.Width = 50
        '
        'clmUserID
        '
        Me.clmUserID.Text = "User ID"
        Me.clmUserID.Width = 175
        '
        'clmAdmin
        '
        Me.clmAdmin.Text = "Administrator"
        Me.clmAdmin.Width = 50
        '
        'clmActive
        '
        Me.clmActive.Text = "Active"
        Me.clmActive.Width = 50
        '
        'clmModBy
        '
        Me.clmModBy.Text = "Modified By"
        Me.clmModBy.Width = 175
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(408, 386)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(150, 30)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Close"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(58, 386)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(150, 30)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Add/Modify User"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frm21ModifyUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lstvUsers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frm21ModifyUsers"
        Me.Text = "Modify or Add Users"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstvUsers As ListView
    Friend WithEvents clmUserPK As ColumnHeader
    Friend WithEvents clmUserID As ColumnHeader
    Friend WithEvents clmAdmin As ColumnHeader
    Friend WithEvents clmActive As ColumnHeader
    Friend WithEvents clmModBy As ColumnHeader
    Friend WithEvents btnCancel As Button
    Friend WithEvents Button1 As Button
End Class
