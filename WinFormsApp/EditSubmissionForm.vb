Imports WinFormsApp.SubmissionNamespace

Public Class EditSubmissionForm
    Inherits Form

    Private _submission As Submission

    Public Property Submission As Submission
        Get
            Return _submission
        End Get
        Private Set(value As Submission)
            _submission = value
        End Set
    End Property

    Private WithEvents txtName As TextBox
    Private WithEvents txtEmail As TextBox
    Private WithEvents txtPhoneNumber As TextBox
    Private WithEvents txtGithubLink As TextBox
    Private WithEvents txtStopwatchTime As TextBox
    Private WithEvents btnSave As Button
    Private WithEvents btnCancel As Button

    Public Sub New(submission As Submission)
        Me._submission = submission
        InitializeComponent()
        DisplaySubmission()
    End Sub

    Private Sub DisplaySubmission()
        txtName.Text = _submission.Name
        txtEmail.Text = _submission.Email
        txtPhoneNumber.Text = _submission.PhoneNumber
        txtGithubLink.Text = _submission.GithubLink
        txtStopwatchTime.Text = _submission.StopwatchTime
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Update submission details
        _submission.Name = txtName.Text
        _submission.Email = txtEmail.Text
        _submission.PhoneNumber = txtPhoneNumber.Text
        _submission.GithubLink = txtGithubLink.Text
        _submission.StopwatchTime = txtStopwatchTime.Text

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Edit Submission"
        Me.Size = New Size(400, 350) ' Adjusted size to ensure all elements fit comfortably

        txtName = New TextBox() With {.Location = New Point(150, 30), .Size = New Size(220, 22)}
        txtEmail = New TextBox() With {.Location = New Point(150, 70), .Size = New Size(220, 22)}
        txtPhoneNumber = New TextBox() With {.Location = New Point(150, 110), .Size = New Size(220, 22)}
        txtGithubLink = New TextBox() With {.Location = New Point(150, 150), .Size = New Size(220, 22)}
        txtStopwatchTime = New TextBox() With {.Location = New Point(150, 190), .Size = New Size(220, 22)}

        btnSave = New Button() With {.Location = New Point(150, 230), .Size = New Size(100, 30), .Text = "Save", .BackColor = Color.LightGreen}
        btnCancel = New Button() With {.Location = New Point(270, 230), .Size = New Size(100, 30), .Text = "Cancel", .BackColor = Color.LightCoral}

        Me.Controls.Add(New Label() With {.Location = New Point(30, 30), .Size = New Size(52, 20), .Text = "Name:"})
        Me.Controls.Add(txtName)
        Me.Controls.Add(New Label() With {.Location = New Point(30, 70), .Size = New Size(49, 20), .Text = "Email:"})
        Me.Controls.Add(txtEmail)
        Me.Controls.Add(New Label() With {.Location = New Point(30, 110), .Size = New Size(111, 20), .Text = "Phone Number:"})
        Me.Controls.Add(txtPhoneNumber)
        Me.Controls.Add(New Label() With {.Location = New Point(30, 150), .Size = New Size(89, 20), .Text = "GitHub Link:"})
        Me.Controls.Add(txtGithubLink)
        Me.Controls.Add(New Label() With {.Location = New Point(30, 190), .Size = New Size(119, 20), .Text = "Stopwatch Time:"})
        Me.Controls.Add(txtStopwatchTime)
        Me.Controls.Add(btnSave)
        Me.Controls.Add(btnCancel)
    End Sub
End Class
