<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm02MainMenu
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
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblAppName = New System.Windows.Forms.Label()
        Me.btnNewTest = New System.Windows.Forms.Button()
        Me.btnLogOut = New System.Windows.Forms.Button()
        Me.btnManageUsers = New System.Windows.Forms.Button()
        Me.btnManageRecipes = New System.Windows.Forms.Button()
        Me.btnSetIPAddress = New System.Windows.Forms.Button()
        Me.btnSetEquipmentID = New System.Windows.Forms.Button()
        Me.lblCurrentUser = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(38, 61)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(42, 13)
        Me.lblVersion.TabIndex = 10
        Me.lblVersion.Text = "Version"
        '
        'lblAppName
        '
        Me.lblAppName.AutoSize = True
        Me.lblAppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppName.Location = New System.Drawing.Point(12, 9)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Size = New System.Drawing.Size(279, 55)
        Me.lblAppName.TabIndex = 9
        Me.lblAppName.Text = "GlucoMatrix"
        '
        'btnNewTest
        '
        Me.btnNewTest.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewTest.Location = New System.Drawing.Point(41, 147)
        Me.btnNewTest.Name = "btnNewTest"
        Me.btnNewTest.Size = New System.Drawing.Size(300, 50)
        Me.btnNewTest.TabIndex = 11
        Me.btnNewTest.Text = "New Test"
        Me.btnNewTest.UseVisualStyleBackColor = True
        '
        'btnLogOut
        '
        Me.btnLogOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogOut.Location = New System.Drawing.Point(41, 208)
        Me.btnLogOut.Name = "btnLogOut"
        Me.btnLogOut.Size = New System.Drawing.Size(300, 50)
        Me.btnLogOut.TabIndex = 12
        Me.btnLogOut.Text = "Log out"
        Me.btnLogOut.UseVisualStyleBackColor = True
        '
        'btnManageUsers
        '
        Me.btnManageUsers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManageUsers.Location = New System.Drawing.Point(41, 269)
        Me.btnManageUsers.Name = "btnManageUsers"
        Me.btnManageUsers.Size = New System.Drawing.Size(300, 50)
        Me.btnManageUsers.TabIndex = 13
        Me.btnManageUsers.Text = "Manage Users"
        Me.btnManageUsers.UseVisualStyleBackColor = True
        '
        'btnManageRecipes
        '
        Me.btnManageRecipes.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManageRecipes.Location = New System.Drawing.Point(41, 330)
        Me.btnManageRecipes.Name = "btnManageRecipes"
        Me.btnManageRecipes.Size = New System.Drawing.Size(300, 50)
        Me.btnManageRecipes.TabIndex = 14
        Me.btnManageRecipes.Text = "Manage Recipes"
        Me.btnManageRecipes.UseVisualStyleBackColor = True
        '
        'btnSetIPAddress
        '
        Me.btnSetIPAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetIPAddress.Location = New System.Drawing.Point(41, 391)
        Me.btnSetIPAddress.Name = "btnSetIPAddress"
        Me.btnSetIPAddress.Size = New System.Drawing.Size(300, 50)
        Me.btnSetIPAddress.TabIndex = 15
        Me.btnSetIPAddress.Text = "Set Equipment IP Address"
        Me.btnSetIPAddress.UseVisualStyleBackColor = True
        '
        'btnSetEquipmentID
        '
        Me.btnSetEquipmentID.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetEquipmentID.Location = New System.Drawing.Point(41, 452)
        Me.btnSetEquipmentID.Name = "btnSetEquipmentID"
        Me.btnSetEquipmentID.Size = New System.Drawing.Size(300, 50)
        Me.btnSetEquipmentID.TabIndex = 16
        Me.btnSetEquipmentID.Text = "Set Equipment ID"
        Me.btnSetEquipmentID.UseVisualStyleBackColor = True
        '
        'lblCurrentUser
        '
        Me.lblCurrentUser.AutoSize = True
        Me.lblCurrentUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentUser.Location = New System.Drawing.Point(38, 102)
        Me.lblCurrentUser.Name = "lblCurrentUser"
        Me.lblCurrentUser.Size = New System.Drawing.Size(108, 20)
        Me.lblCurrentUser.TabIndex = 17
        Me.lblCurrentUser.Text = "Current User: "
        '
        'frm02MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 537)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblCurrentUser)
        Me.Controls.Add(Me.btnSetEquipmentID)
        Me.Controls.Add(Me.btnSetIPAddress)
        Me.Controls.Add(Me.btnManageRecipes)
        Me.Controls.Add(Me.btnManageUsers)
        Me.Controls.Add(Me.btnLogOut)
        Me.Controls.Add(Me.btnNewTest)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblAppName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm02MainMenu"
        Me.Text = "Main Menu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblVersion As Label
    Friend WithEvents lblAppName As Label
    Friend WithEvents btnNewTest As Button
    Friend WithEvents btnLogOut As Button
    Friend WithEvents btnManageUsers As Button
    Friend WithEvents btnManageRecipes As Button
    Friend WithEvents btnSetIPAddress As Button
    Friend WithEvents btnSetEquipmentID As Button
    Friend WithEvents lblCurrentUser As Label
End Class
