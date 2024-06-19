Imports System.IO
Imports Newtonsoft.Json
Imports System.Diagnostics
Imports System.Windows.Forms

Public Class CreateSubmissionForm
    Inherits Form

    Private ReadOnly stopwatch As New Stopwatch()
    Private isRunning As Boolean = False

    ' Add a timer to periodically update the elapsed time display
    Private ReadOnly updateTimer As New Timer()

    ' Control declarations
    Private WithEvents TxtName As TextBox
    Private WithEvents TxtEmail As TextBox
    Private WithEvents TxtPhoneNumber As TextBox
    Private WithEvents TxtGithubLink As TextBox
    Private WithEvents BtnToggleStopwatch As Button
    Private WithEvents BtnSubmit As Button
    Private WithEvents LblElapsedTime As Label
    Private WithEvents LblStopwatchTime As Label

    Public Sub New()
        ' Call the InitializeComponent method to setup the form controls
        InitializeComponent()

        ' Set up the update timer
        updateTimer.Interval = 1000 ' Update every second
        AddHandler updateTimer.Tick, AddressOf UpdateTimer_Tick

        ' Ensure db.json exists
        Dim dbPath As String = "db.json"
        If Not File.Exists(dbPath) Then
            Using writer As New StreamWriter(dbPath)
                ' Create an empty array or object if needed
                writer.WriteLine("[]")
            End Using
        End If
    End Sub

    Private Sub InitializeComponent()
        ' Initialize the controls and set their properties
        Me.TxtName = New TextBox()
        Me.TxtEmail = New TextBox()
        Me.TxtPhoneNumber = New TextBox()
        Me.TxtGithubLink = New TextBox()
        Me.BtnToggleStopwatch = New Button()
        Me.BtnSubmit = New Button()
        Me.LblElapsedTime = New Label()
        Me.LblStopwatchTime = New Label()

        ' Set control properties (example)
        Dim labelWidth As Integer = 150
        Dim textBoxWidth As Integer = 250
        Dim buttonWidth As Integer = 280 ' Adjusted button width
        Dim startX As Integer = 50
        Dim startY As Integer = 30
        Dim gapY As Integer = 40
        Dim controlHeight As Integer = 39

        ' TxtName
        Me.TxtName.Location = New Point(startX + labelWidth + 10, startY)
        Me.TxtName.Size = New Size(textBoxWidth, controlHeight)
        Me.TxtName.BackColor = Color.White

        Dim lblName As New Label()
        lblName.Text = "Name"
        lblName.Location = New Point(startX, startY)
        lblName.Size = New Size(labelWidth, controlHeight)
        Me.Controls.Add(lblName)

        ' TxtEmail
        Me.TxtEmail.Location = New Point(startX + labelWidth + 10, startY + gapY)
        Me.TxtEmail.Size = New Size(textBoxWidth, controlHeight)
        Me.TxtEmail.BackColor = Color.White

        Dim lblEmail As New Label()
        lblEmail.Text = "Email"
        lblEmail.Location = New Point(startX, startY + gapY)
        lblEmail.Size = New Size(labelWidth, controlHeight)
        Me.Controls.Add(lblEmail)

        ' TxtPhoneNumber
        Me.TxtPhoneNumber.Location = New Point(startX + labelWidth + 10, startY + 2 * gapY)
        Me.TxtPhoneNumber.Size = New Size(textBoxWidth, controlHeight)
        Me.TxtPhoneNumber.BackColor = Color.White

        Dim lblPhoneNumber As New Label()
        lblPhoneNumber.Text = "Phone Number"
        lblPhoneNumber.Location = New Point(startX, startY + 2 * gapY)
        lblPhoneNumber.Size = New Size(labelWidth, controlHeight)
        Me.Controls.Add(lblPhoneNumber)

        ' TxtGithubLink
        Me.TxtGithubLink.Location = New Point(startX + labelWidth + 10, startY + 3 * gapY)
        Me.TxtGithubLink.Size = New Size(textBoxWidth, controlHeight)
        Me.TxtGithubLink.BackColor = Color.White

        Dim lblGithubLink As New Label()
        lblGithubLink.Text = "Github Link For Task 2"
        lblGithubLink.Location = New Point(startX, startY + 3 * gapY)
        lblGithubLink.Size = New Size(labelWidth, controlHeight)
        Me.Controls.Add(lblGithubLink)

        ' BtnToggleStopwatch
        Me.BtnToggleStopwatch.Location = New Point(startX, startY + 4 * gapY)
        Me.BtnToggleStopwatch.Size = New Size(280, controlHeight) ' Adjusted button width
        Me.BtnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
        Me.BtnToggleStopwatch.BackColor = Color.Yellow
        AddHandler Me.BtnToggleStopwatch.Click, AddressOf BtnToggleStopwatch_Click

        ' LblStopwatchTime
        Me.LblStopwatchTime.Location = New Point(startX + 320, startY + 4 * gapY) ' Adjusted X position
        Me.LblStopwatchTime.Size = New Size(120, controlHeight)
        Me.LblStopwatchTime.BackColor = Color.LightGray
        Me.LblStopwatchTime.TextAlign = ContentAlignment.MiddleCenter

        ' BtnSubmit
        Me.BtnSubmit.Location = New Point(startX, startY + 5.5 * gapY)
        Me.BtnSubmit.Size = New Size(450, 47) ' Adjusted button width
        Me.BtnSubmit.Text = "SUBMIT (Ctrl + S)"
        Me.BtnSubmit.BackColor = Color.LightBlue
        AddHandler Me.BtnSubmit.Click, AddressOf BtnSubmit_Click

        ' Add controls to the form
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.TxtEmail)
        Me.Controls.Add(Me.TxtPhoneNumber)
        Me.Controls.Add(Me.TxtGithubLink)
        Me.Controls.Add(Me.BtnToggleStopwatch)
        Me.Controls.Add(Me.BtnSubmit)
        Me.Controls.Add(Me.LblElapsedTime)
        Me.Controls.Add(Me.LblStopwatchTime)

        ' Set form properties
        Me.ClientSize = New Size(500, 300) ' Adjusted form size
        Me.Text = "Create New Submission"
    End Sub

    Private Sub BtnToggleStopwatch_Click(sender As Object, e As EventArgs)
        ToggleStopwatch()
    End Sub

    Private Sub BtnSubmit_Click(sender As Object, e As EventArgs)
        SaveSubmissionData()
    End Sub

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        AddHandler Me.KeyDown, AddressOf CreateSubmissionForm_KeyDown
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.S Then
            SaveSubmissionData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.T Then
            ToggleStopwatch()
        End If
    End Sub

    Private Sub ToggleStopwatch()
        If isRunning Then
            stopwatch.Stop()
            BtnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
            updateTimer.Stop()
        Else
            stopwatch.Start()
            BtnToggleStopwatch.Text = "STOPWATCH RUNNING (CTRL + T)"
            updateTimer.Start()
        End If
        isRunning = Not isRunning
    End Sub

    Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs)
        If isRunning Then
            LblStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
        End If
    End Sub


    Private Sub SaveSubmissionData()
        Try
            ' Create an object to store submission data
            Dim submissionData As New SubmissionNamespace.Submission With {
            .Name = TxtName.Text,
            .Email = TxtEmail.Text,
            .PhoneNumber = TxtPhoneNumber.Text,
            .GithubLink = TxtGithubLink.Text,
            .StopwatchTime = stopwatch.Elapsed.ToString("hh\:mm\:ss")
        }

            ' Define a custom path for the Data Stored directory
            Dim customDataDirectory As String = "C:\Users\nayan\source\repos\WinFormsApp\WinFormsApp\Data Stored"
            Dim dbPath As String = Path.Combine(customDataDirectory, "db.json")

            ' Ensure the custom directory exists
            If Not Directory.Exists(customDataDirectory) Then
                Directory.CreateDirectory(customDataDirectory)
                Debug.WriteLine("Created new Data Stored directory at custom path.")
            End If

            ' Read existing data from db.json if it exists
            Dim submissions As List(Of SubmissionNamespace.Submission)
            If File.Exists(dbPath) Then
                Dim json As String = File.ReadAllText(dbPath)
                submissions = JsonConvert.DeserializeObject(Of List(Of SubmissionNamespace.Submission))(json)
                If submissions Is Nothing Then submissions = New List(Of SubmissionNamespace.Submission)()
            Else
                submissions = New List(Of SubmissionNamespace.Submission)()
            End If

            ' Add new submission data
            submissions.Add(submissionData)

            ' Serialize the updated list to JSON and write it back to db.json
            Dim updatedJson As String = JsonConvert.SerializeObject(submissions, Formatting.Indented)
            File.WriteAllText(dbPath, updatedJson)

            MessageBox.Show("Submission saved successfully!")

            ' Clear form fields after successful submission
            TxtName.Clear()
            TxtEmail.Clear()
            TxtPhoneNumber.Clear()
            TxtGithubLink.Clear()
            LblStopwatchTime.Text = "00:00:00"
            stopwatch.Reset()
            isRunning = False
            BtnToggleStopwatch.Text = "TOGGLE STOPWATCH (CTRL + T)"
            updateTimer.Stop()

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub



End Class

Namespace SubmissionNamespace
    Public Class Submission
        Public Property Name As String
        Public Property Email As String
        Public Property PhoneNumber As String
        Public Property GithubLink As String
        Public Property StopwatchTime As String
    End Class
End Namespace
