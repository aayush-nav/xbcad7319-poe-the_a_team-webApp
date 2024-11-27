
<h1 align="center">XBCAD7319 Final POE - SparkLine - The A Team 👋</h1>

## SparkLineHR Web Portal

### Table of Contents

-   [Description of the Web Portal](#description-of-the-web-portal)
-   [Features](#features)
-   [Design Considerations](#design-considerations)
    -   [User Interface (UI) and User Experience (UX)](#user-interface-ui-and-user-experience-ux)
    -   [Data Handling](#data-handling)
    -   [Testing and Quality Assurance](#testing-and-quality-assurance)
-   [Installation](#installation)
    -   [Prerequisites](#prerequisites)
    -   [Steps](#steps)
-   [Usage](#usage)
-   [Authors and Acknowledgement](#authors-and-acknowledgement)
-   [Support](#support)
-   [Project Status](#project-status)

----------

## Description of the Web Portal

The SparkLineHR Admin Web Portal is designed to empower the HR team of SparkLine Finance by digitizing employee management and eliminating paper-based workflows. This portal provides HR administrators with a comprehensive interface to manage employee data, track requests, review performance, upload payslips, and post training materials.

The portal ensures efficiency, accuracy, and ease of use, improving HR operations and the employee experience. It is built using the ASP.NET C# MVC framework and integrates seamlessly with the Android app and Firebase database for a unified system.

<p align="right">(<a href="#table-of-contents">back to top</a>)</p>

----------

## Features

-   **Employee Management**: Add, update, and manage employee details including personal information, roles, and onboarding.
-   **Request Handling**: Review, approve, or reject overtime and leave requests submitted by employees.
-   **Payslip Management**: Upload and release monthly payslips for employees to access on their mobile app.
-   **Training Management**: Post completed training courses and assign them to specific employees.
-   **Performance Reviews**: Upload and manage employee performance reviews.
-   **Incident Reports**: Review employee-submitted incidents, including anonymous reports.


<p align="right">(<a href="#table-of-contents">back to top</a>)</p>

----------

## Design Considerations

When developing the admin portal, several critical factors were considered:

### User Interface (UI) and User Experience (UX)

-   **Consistency with Branding**: The portal's design aligns with the organization's branding, ensuring a professional and user-friendly interface.
-   **Responsive Design**: The web portal supports various screen sizes, providing seamless access across devices.
-   **Intuitive Dashboard**: The main dashboard offers a quick overview of pending tasks, employee statistics, and system alerts.

### Data Handling

-   **Firebase Integration**: The portal connects to Firebase Firestore to manage employee data, document uploads, and request statuses.
-   **Secure APIs**: It utilizes APIs hosted on Render for handling onboarding, login, and notifications. These APIs ensure secure communication using HTTPS and token-based authentication.
-   **Data Validation**: Frontend and backend validation is implemented to ensure accurate and clean data entries.

### Testing and Quality Assurance

-   **Unit Testing**: Key features such as data retrieval from Firebase, form submissions, and request handling are tested to ensure robustness.
-   **UI Testing**: Conducted to verify consistent user experience across different browsers and resolutions.
-   **Load Testing**: The application is tested to handle high traffic scenarios, ensuring reliability during peak usage.


<p align="right">(<a href="#table-of-contents">back to top</a>)</p>

----------

## Installation

### Prerequisites

-   .NET Core SDK
-   Visual Studio (or compatible IDE)
-   Access to Firebase Firestore credentials

### Steps

1.  **Clone the Repository**:
    
    ```bash
    git clone https://github.com/aayush-nav/xbcad7319-poe-the_a_team-webApp.git
    
    ```
    
2.  **Open the Solution**:  
    Open the project in Visual Studio.
    
3.  **Configure Firebase**:  
    Replace the placeholder Firebase configuration in `appsettings.json` with your project credentials.
    
4.  **Restore Dependencies**:  
    Run the following command in the terminal:
    
    ```bash
    dotnet restore
    
    ```
    
5.  **Run the Project**:  
    Start the project using Visual Studio or run:
    
    ```bash
    dotnet run
    
    ```
    
6.  **Access the Application**:  
    Open your browser and navigate to `http://localhost:<port>`.
    

<p align="right">(<a href="#table-of-contents">back to top</a>)</p>

----------

## Usage

HR administrators can use the portal to:

-   Manage employee records and upload important documents (e.g., ID proofs, payslips).
-   Review and respond to timesheet and leave requests.
-   Upload and assign performance reviews and training materials.
-   Track incidents reported by employees.
-   Access real-time analytics on employee performance and pending tasks.

----------

## Authors and Acknowledgement

👤 **ST10048211** - Anjali Sunil Morar  
👤 **ST10071160** - Aidan Johann Schwoerer  
👤 **ST10104776** - Mohamad Aslam Mustufa Khalifa  
👤 **ST10243270** - Aayush Navsariwala  
👤 **ST10062860** - Abdullah Gadatia

**Final POE Links:**

-   **Project Links:**  
    Final Project Web App: [https://github.com/aayush-nav/xbcad7319-poe-the_a_team-webApp.git](https://github.com/aayush-nav/xbcad7319-poe-the_a_team-webApp.git)  
    
    Final Project Android: [https://github.com/ST10071160/xbcad-poe-theateam-android.git](https://github.com/ST10071160/xbcad-poe-theateam-android.git)
    
-   **YouTube Links:**  
   Android App Demonstration Video:  [https://youtu.be/kR5jqtYVerQ](https://youtu.be/kR5jqtYVerQ)

	API and Web App Demonstration Video:  [https://youtu.be/wh2zRh5yZTw](https://youtu.be/wh2zRh5yZTw)


<p align="right">(<a href="#table-of-contents">back to top</a>)</p>

----------

## Support

Email - [the.a.team.bcad@gmail.com](mailto:the.a.team.bcad@gmail.com)


<p align="right">(<a href="#table-of-contents">back to top</a>)</p>

----------

## Project Status

XBCAD7319 - Project Completion


<p align="right">(<a href="#table-of-contents">back to top</a>)</p>

----------

