The `README.md` file for your Windows Desktop Application project. This version includes detailed instructions on setting up and using the application, as well as information on interacting with the JSON data for submission storage.

```markdown
## Windows Desktop Application
Compulsory Features
Create New Submission Form:

# Fields for Name, Email, Phone Number, GitHub Repository Link.
Stopwatch functionality that can be started and paused without resetting.
Submit button to send the data to the backend server.
Keyboard shortcuts (e.g., Ctrl + S to submit).
View Submissions Form:

# Displays a list of submissions.
Navigation buttons ("Previous" and "Next") to cycle through submissions.
Keyboard shortcuts for navigation (e.g., Ctrl + N for Next, Ctrl + P for Previous).
Option to edit and delete submissions.
Public GitHub Repository:

# The project code should be hosted on a public GitHub repository.

This repository contains the source code for a Windows Desktop application developed in Visual Basic using Visual Studio. The application provides functionalities to create new form submissions, view existing submissions, and navigate through them using keyboard shortcuts.

## Features

- **Create New Submission**: Allows users to input their name, email, phone number, and GitHub repository link. It includes a stopwatch that can be started and paused.
- **View Submissions**: Displays a list of submissions with navigation buttons to view previous or next submissions.
- **Keyboard Shortcuts**: Facilitates quick navigation and actions through keyboard shortcuts.
- **Submit to Backend**: Submissions are sent to a backend server for processing and storage.
- **Edit Submissions**: Users can edit existing submissions.
- **Delete Submissions**: Option to delete a submission.

## Prerequisites

- Windows Desktop Application Development environment in Visual Studio
- .NET Framework (version 7.0)
- Backend server running locally (configured as described in the Backend Server Description section)
- https://github.com/Nayankumar4986/Backend-Server

   ```

2. **Open the Project in Visual Studio**

   Open the solution file (`.sln`) in Visual Studio.

3. **Restore Dependencies**

   Restore the necessary NuGet packages for the project, including `Newtonsoft.Json` for JSON handling:

   ```plaintext
   Install-Package Newtonsoft.Json
   ```

4. **Configure Backend Server**

   Ensure that your backend server is running and accessible from your local machine. Update the API URLs in the application settings to match the backend server's address.

5. **Build the Application**

   Build the application in Visual Studio to ensure that all dependencies are correctly resolved and the application is ready to run.

6. **Run the Application**

   Start the application by pressing `F5` or by clicking on the "Start" button in Visual Studio.

## Usage

### Creating a New Submission

1. Click on "Create New Submission" to open the submission form.
2. Fill in the form fields with the required information.
3. Use the "Start" and "Pause" buttons on the stopwatch.
4. Click "Submit" to send the data to the backend server.

### Viewing Submissions

1. Click on "View Submissions" to open the submissions viewer.
2. Use the "Previous" and "Next" buttons to navigate through the submissions.
3. Keyboard shortcuts:
   - Ctrl + N: Next submission
   - Ctrl + P: Previous submission
   - Ctrl + E: Edit submission
   - Ctrl + D: Delete submission

### Editing and Deleting Submissions

1. In the submissions viewer, select a submission.
2. Click "Edit" to modify the submission.
3. Click "Delete" to remove the submission.

## Backend Interaction

### JSON Data Structure

The backend stores submissions in a JSON file (`db.json`) with the following structure:

```json
[
  {
    "name": "John Doe",
    "email": "johndoe@example.com",
    "phoneNumber": "+1234567890",
    "githubLink": "https://github.com/johndoe",
    "stopwatchTime": "00:30:00"
  },
  {
    "name": "Jane Doe",
    "email": "janedoe@example.com",
    "phoneNumber": "+0987654321",
    "githubLink": "https://github.com/janedoe",
    "stopwatchTime": "01:00:00"
  }
]
```

### Fetching Submissions

Submissions are fetched from the backend using the `/read` endpoint with the query parameter `index` to retrieve a specific submission:

```
GET /read?index=0
```

### Submitting Data

Form data is submitted to the backend using the `/submit` endpoint with parameters for each field:

```
POST /submit
{
  "name": "John Doe",
  "email": "johndoe@example.com",
  "phoneNumber": "+1234567890",
  "githubLink": "https://github.com/johndoe",
  "stopwatchTime": "00:30:00"
}
```

## Contributing

Contributions to this project are welcome. Please fork the repository, make your changes, and submit a pull request. For significant changes, please discuss your changes in an issue before proceeding.

## DeskTop App Images 

![Screenshot of the app interface](https://github.com/Nayankumar4986/Desktop-App/blob/main/img/1.png)
![Screenshot of the app interface](https://github.com/Nayankumar4986/Desktop-App/blob/main/img/2.png)
![Screenshot of the app interface](https://github.com/Nayankumar4986/Desktop-App/blob/main/img/3.png)
![Screenshot of the app interface](https://github.com/Nayankumar4986/Desktop-App/blob/main/img/4.png)
![Screenshot of the app interface](https://github.com/Nayankumar4986/Desktop-App/blob/main/img/5.png)
![Screenshot of the app interface](https://github.com/Nayankumar4986/Desktop-App/blob/main/img/6.png)




### Contact

For further information, feel free to contact the maintainer at nayanK092@gmail.com.

```

### Notes
- Ensure that the JSON structure and API endpoints used in your application match the described format and functionality.
- Replace the example API URLs and contact email with your actual project details.
- The `Newtonsoft.Json` package is used for JSON serialization and deserialization, essential for handling submissions in your application.
