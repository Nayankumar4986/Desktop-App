Imports System.IO
Imports Newtonsoft.Json
Imports WinFormsApp.SubmissionNamespace

Public Class ViewSubmissionsForm
    Inherits Form

    Private currentIndex As Integer = 0
    Private submissions As List(Of Submission)

    ' Read-only labels
    Private WithEvents lblName As Label
    Private WithEvents lblNameValue As Label
    Private WithEvents lblEmail As Label
    Private WithEvents lblEmailValue As Label
    Private WithEvents lblPhoneNumber As Label
    Private WithEvents lblPhoneNumberValue As Label
    Private WithEvents lblGithubLink As Label
    Private WithEvents lblGithubLinkValue As Label
    Private WithEvents lblStopwatchTime As Label
    Private WithEvents lblStopwatchTimeValue As Label

    ' Navigation buttons
    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button
    Private WithEvents btnEdit As Button

    ' Delete button
    Private WithEvents btnDelete As Button

    Public Sub New()
        InitializeComponent()
        LoadSubmissions()
        DisplaySubmission(0)
    End Sub

    Private Sub LoadSubmissions()
        Dim dbPath As String = "C:\Users\nayan\source\repos\WinFormsApp\WinFormsApp\Data Stored\db.json"
        If File.Exists(dbPath) Then
            Try
                Dim json As String = File.ReadAllText(dbPath)
                submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)
                If submissions Is Nothing Then
                    submissions = New List(Of Submission)()
                End If
            Catch ex As Exception
                MessageBox.Show("Error loading submissions: " & ex.Message)
                submissions = New List(Of Submission)()
            End Try
        Else
            submissions = New List(Of Submission)()
        End If
    End Sub

    Private Sub DisplaySubmission(index As Integer)
        If index >= 0 AndAlso index < submissions.Count Then
            lblNameValue.Text = submissions(index).Name
            lblEmailValue.Text = submissions(index).Email
            lblPhoneNumberValue.Text = submissions(index).PhoneNumber
            lblGithubLinkValue.Text = submissions(index).GithubLink
            lblStopwatchTimeValue.Text = submissions(index).StopwatchTime
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Using editForm As New EditSubmissionForm(submissions(currentIndex))
                If editForm.ShowDialog() = DialogResult.OK Then
                    ' Update the current submission and save to file
                    submissions(currentIndex) = editForm.Submission
                    SaveSubmissions()
                    DisplaySubmission(currentIndex)
                End If
            End Using
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim confirmResult As DialogResult = MessageBox.Show("Are you sure to delete this submission?", "Confirm Delete", MessageBoxButtons.YesNo)
            If confirmResult = DialogResult.Yes Then
                ' The user chose Yes, so proceed with deletion
                submissions.RemoveAt(currentIndex)
                SaveSubmissions()

                If submissions.Count > 0 Then
                    ' Adjust currentIndex if needed
                    currentIndex = Math.Min(currentIndex, submissions.Count - 1)
                    DisplaySubmission(currentIndex)
                Else
                    ' No submissions left, reset all labels and disable controls
                    ResetFormState()
                End If
            End If
        End If
    End Sub

    Private Sub ResetFormState()
        lblNameValue.Text = ""
        lblEmailValue.Text = ""
        lblPhoneNumberValue.Text = ""
        lblGithubLinkValue.Text = ""
        lblStopwatchTimeValue.Text = ""
        btnPrevious.Enabled = False
        btnNext.Enabled = False
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        MessageBox.Show("No submissions available.", "Information")
    End Sub


    Private Sub SaveSubmissions()
        Dim dbPath As String = "C:\Users\nayan\source\repos\WinFormsApp\WinFormsApp\Data Stored\db.json"
        Try
            Dim json As String = JsonConvert.SerializeObject(submissions, Formatting.Indented)
            File.WriteAllText(dbPath, json)
        Catch ex As Exception
            MessageBox.Show("Error saving submissions: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        AddHandler Me.KeyDown, AddressOf ViewSubmissionsForm_KeyDown
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            btnEdit.PerformClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnDelete.PerformClick()
        End If
    End Sub

    Private Sub InitializeComponent()
        ' Initialize components
        lblName = New Label() With {.Location = New Point(30, 30), .Size = New Size(52, 20), .Text = "Name:"}
        lblNameValue = New Label() With {.Location = New Point(150, 30), .Size = New Size(220, 22), .BackColor = Color.LightGray, .BorderStyle = BorderStyle.FixedSingle}

        lblEmail = New Label() With {.Location = New Point(30, 70), .Size = New Size(49, 20), .Text = "Email:"}
        lblEmailValue = New Label() With {.Location = New Point(150, 70), .Size = New Size(220, 22), .BackColor = Color.LightGray, .BorderStyle = BorderStyle.FixedSingle}

        lblPhoneNumber = New Label() With {.Location = New Point(30, 110), .Size = New Size(111, 20), .Text = "Phone Number:"}
        lblPhoneNumberValue = New Label() With {.Location = New Point(150, 110), .Size = New Size(220, 22), .BackColor = Color.LightGray, .BorderStyle = BorderStyle.FixedSingle}

        lblGithubLink = New Label() With {.Location = New Point(30, 150), .Size = New Size(89, 20), .Text = "GitHub Link:"}
        lblGithubLinkValue = New Label() With {.Location = New Point(150, 150), .Size = New Size(220, 22), .BackColor = Color.LightGray, .BorderStyle = BorderStyle.FixedSingle}

        lblStopwatchTime = New Label() With {.Location = New Point(30, 190), .Size = New Size(119, 20), .Text = "Stopwatch Time:"}
        lblStopwatchTimeValue = New Label() With {.Location = New Point(150, 190), .Size = New Size(220, 22), .BackColor = Color.LightGray, .BorderStyle = BorderStyle.FixedSingle}

        btnPrevious = New Button() With {.Location = New Point(22, 230), .Size = New Size(147, 50), .Text = "Previous (Ctrl + P)", .BackColor = Color.LightBlue}
        btnNext = New Button() With {.Location = New Point(175, 230), .Size = New Size(130, 50), .Text = "Next (Ctrl + N)", .BackColor = Color.Yellow}
        btnEdit = New Button() With {.Location = New Point(311, 230), .Size = New Size(134, 50), .Text = "Edit (Ctrl + E)"}
        btnDelete = New Button() With {.Location = New Point(450, 230), .Size = New Size(134, 50), .Text = "Delete (Ctrl + D)", .BackColor = Color.LightCoral}

        ' Adding event handlers
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click
        AddHandler btnNext.Click, AddressOf btnNext_Click
        AddHandler btnEdit.Click, AddressOf btnEdit_Click
        AddHandler btnDelete.Click, AddressOf btnDelete_Click

        ' Form settings
        Me.ClientSize = New Size(600, 324)
        Me.Controls.Add(lblName)
        Me.Controls.Add(lblNameValue)
        Me.Controls.Add(lblEmail)
        Me.Controls.Add(lblEmailValue)
        Me.Controls.Add(lblPhoneNumber)
        Me.Controls.Add(lblPhoneNumberValue)
        Me.Controls.Add(lblGithubLink)
        Me.Controls.Add(lblGithubLinkValue)
        Me.Controls.Add(lblStopwatchTime)
        Me.Controls.Add(lblStopwatchTimeValue)
        Me.Controls.Add(btnPrevious)
        Me.Controls.Add(btnNext)
        Me.Controls.Add(btnEdit)
        Me.Controls.Add(btnDelete)
        Me.Text = "View Submissions"
    End Sub
End Class
