# 🏥 Saint Vicent Hospital System

![Language](https://img.shields.io/badge/C%23-8.0-blueviolet)  
![Framework](https://img.shields.io/badge/ASP.NET%20Core-8.0-green)  
![Database](https://img.shields.io/badge/Entity%20Framework-Core-blue)  

A **C# ASP.NET MVC web application** for managing hospital operations.  
The system allows you to handle **patients, doctors, appointments, and email notifications**, all within a structured and modern web interface.

---

## 📑 Table of Contents
- [⚙️ Installation](#️-installation)  
- [🛠️ Prerequisites](#️-prerequisites)  
- [📂 Project Structure](#-project-structure)  
- [💡 Features](#-features)  
- [💻 Technologies](#-technologies)  
- [🧠 Architecture](#-architecture)  
- [🤝 Contributing](#-contributing)  
- [👥 Authors](#-authors)  
- [🐞 Known Issues](#-known-issues)  

---

## ⚙️ Installation

1. Clone the repository:
   git clone https://github.com/yourusername/SaintVicentHospital.git
   
2. Open the solution in Visual Studio, Rider, or VS Code.

3. Restore dependencies:
   dotnet restore

4. Apply migrations to create the database:
   dotnet ef database update

5. Run the project:
   dotnet run

6. Open your browser at https://localhost:5001 or http://localhost:5000.

## 🛠️ Prerequisites

- .NET SDK 8.0 or later

- SQL Server or LocalDB

- Visual Studio / Rider / VS Code

- MailKit library installed:
  dotnet add package MailKit

  ## 📂 Project Structure

```
  SaintVicentHospital/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── AppointmentsController.cs
│   ├── PatientsController.cs
│   ├── MedicsController.cs
│   └── EmailController.cs
│
├── Models/
│   ├── Appointment.cs
│   ├── Patient.cs
│   ├── Medic.cs
│   └── EmailLog.cs
│
├── Views/
│   ├── Home/
│   ├── Appointments/
│   ├── Patients/
│   ├── Medics/
│   └── Shared/
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
│
└── appsettings.json
```

## 💡 Features

- Patient Management – Register, edit, and delete patients.

- Doctor Management – Manage doctors and their specialties.

- Appointments System – Create and update appointments, with automatic state control (Pending, Confirmed, Cancelled).

- Email Notifications – Automatic email sent when an appointment is scheduled (via MailKit).

- Email Log – Track every email with status: Sent or Not Sent.

- Modern UI – Responsive and professional frontend using Bootstrap and Razor Views.

| Category | Technology                     |
| -------- | ------------------------------ |
| Backend  | ASP.NET Core MVC 8.0           |
| Frontend | Razor, Bootstrap 5             |
| ORM      | Entity Framework Core          |
| Database | SQL Server                     |
| Email    | MailKit                        |
| IDEs     | Visual Studio, JetBrains Rider |
| Language | C# 12                          |

## 🧠 Architecture

- MVC pattern (Model–View–Controller) for clean separation of logic and UI.

- Entity Framework Core for data persistence.

- Dependency Injection for service management.

- Repository-style pattern within controllers.

- MailKit integration for email automation.

- Custom validation attributes for data integrity (e.g., unique email).

## 🤝 Contributing

### Contributions are welcome! 🚀

1. Fork the repository

2. Create your feature branch

3. Commit using Conventional Commits

4. Push your branch

5. Open a Pull Request

## 👥 Authors

### Saint Vicent Development Team

- Juan Steven Cardona Grisales
  CC 1000540387
  stevencardona2001@gmail.com
  Clan van Rossum 

## 🐞 Known Issues

No major issues reported yet. Feel free to open an issue if you find one.

### Unique project's likn 
https://github.com/ReijBak/Prueba-desempeno-csharp

🏗️ Built with dedication, precision, and teamwork to simulate a real hospital management system in ASP.NET Core.
