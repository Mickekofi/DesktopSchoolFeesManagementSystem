‎=<p align="center">
‎  
‎    <img src="https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/efe_logo.png" width="130">
‎  </a>
‎  
‎  <h1 align="center"><strong>Mini Desktop School Fees Management System</strong></h1>
‎  </a>
‎  <p align="center">
‎    <a href="">
‎      <img src="https://img.shields.io/badge/Join-Community-blue.svg" alt="MIT License">
‎    </a>
‎    <a href="https://wa.me/233505994829?text=*Ucam_From_Github_User_💬Message_:*%20">
‎      <img src="https://img.shields.io/badge/Contact-Engineers-red.svg" alt="Build Status">
‎    </a>
‎  </p>
‎</p>
‎
‎---



# ඏ School Fees Management System (EFE)

**Electronic Finance (EFE)**: A desktop financial management information system (FMIS) designed for universities to automate student tuition fee management, payment processing, course registration eligibility verification, and comprehensive financial reporting.

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image1.png)

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [System Requirements](#system-requirements)
- [Installation & Setup](#installation--setup)
- [Quick Start Guide](#quick-start-guide)
- [How It Works](#how-it-works)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)
- [License](#license)

---


**Questions?** Review the [DATABASE.md](./DATABASE.md) and [ARCHITECTURE.md](./ARCHITECTURE.md) documentation files for deeper technical details.

**Database Schema Questions** → See [DATABASE.md](./DATABASE.md)

**System Architecture Questions** → See [ARCHITECTURE.md](./ARCHITECTURE.md)



## 📌 Overview

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image2.png)


### Purpose
EFE (Electronic Finance) is a centralized digital platform that enables universities to efficiently manage student tuition obligations, process payments, verify financial compliance, and control semester registration eligibility. Built as a Windows desktop application, it serves both students and university finance administrators with real-time financial tracking and automated eligibility verification.

### Key Objectives

✅ **Centralized Fee Management** — Maintain complete student tuition records, payment histories, and outstanding balance tracking in one secure system.

✅ **Student Self-Service Portal** — Enable students to view fee obligations, payment history, and registration eligibility through a secure login interface.

✅ **Automated Compliance Enforcement** — Automatically verify payment requirements and block/approve course registration based on university financial policies.

✅ **Transaction Transparency** — Maintain complete electronic payment records, audit logs, and receipts for full financial traceability.

✅ **Financial Analytics** — Provide administrators with detailed reports on fee collections, payment trends, outstanding balances, and student compliance metrics.

---

## ✨ Features

### Student Features

| Feature | Description |
|---------|-------------|
| **Secure Authentication** | Login with Index Number and Password with role-based access control |
| **Profile Management** | Update personal information including passport photo, contact details, and program selection |
| **Fee Dashboard** | Real-time view of total fees, paid amount, outstanding balance, and semester status |
| **Payment Processing** | Submit tuition payments with simulated payment confirmation and receipt generation |
| **Receipt Management** | Download and print payment receipts with transaction details and reference numbers |
| **Course Registration** | Register courses with automatic eligibility verification based on payment status and deadlines |
| **Payment History** | View complete payment transaction history and audit trail |

### Accountant & Administrator Features

| Feature | Description |
|---------|-------------|
| **Student Import** | Bulk upload student records from Excel files with automatic account creation |
| **Program Management** | Create and manage academic programs (e.g., BSc Information Technology, BSc Mathematics) |
| **Fee Structure Configuration** | Set annual tuition fees by program, semester, and academic year with registration deadlines |
| **Academic Calendar** | Configure academic years and semesters; activate/deactivate as needed |
| **Payment Monitoring** | Track all student payments, payment methods, and transaction statuses in real-time |
| **Financial Dashboard** | View key metrics: total students, total revenue, payment status, registration eligibility |
| **Receipt Verification** | Audit all generated receipts with complete transaction history and print logs |
| **Financial Reports** | Generate RDLC reports including: Student Name, Index Number, Program, Target Fee, Amount Paid, Deficit |
| **Audit Logging** | Complete audit trail of all system activities for compliance and financial verification |

---

## 🖥️ System Requirements

### Minimum Requirements

- **Operating System**: Microsoft Windows 10 or Windows 11 (64-bit)
- **Framework**: .NET Framework 4.7.2 or higher
- **RAM**: 4 GB minimum (8 GB recommended)
- **Storage**: 2 GB free disk space
- **Display**: 1024 x 768 minimum screen resolution

### Database Requirements

- **Database System**: MySQL 5.7 or higher / MySQL 8.0 (recommended)
- **Database Port**: Default 3306 (or custom as configured)
- **User Permissions**: Full CREATE, ALTER, DROP, SELECT, INSERT, UPDATE, DELETE permissions

### Development Tools (for setup & customization)

- **IDE**: Microsoft Visual Studio 2022 Community Edition Compatiblew Only
- **Language**: VB.NET
- **UI Framework**: Windows Forms (WinForms)
- **Database Driver**: MySQL Connector/NET

---

## ⚙️ Installation & Setup

### Step 1: Clone or Download the Project

```bash
# Clone the repository
git clone https://github.com/your-organization/DesktopSchoolFeesManagementSystem.git
cd DesktopSchoolFeesManagementSystem
```

### Step 2: Database Setup

1. **Open MySQL Command Line or MySQL Workbench**

2. **Create a new database:**
   ```sql
   CREATE DATABASE efe_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
   ```

3. **Import the database schema:**
   - Navigate to the `core/` folder in the project
   - Run `DatabaseQuery.txt` (contains all tables, relationships, and initial data)
   - Or import `efe_db.sql` if a pre-built database is provided

4. **Verify database creation:**
   ```sql
   USE efe_db;
   SHOW TABLES;
   ```

### Step 3: Configure Database Connection

1. **Open the project in Visual Studio 2022**
   - File → Open → DesktopSchoolFeesManagementSystem.sln

2. **Locate the Database Configuration File:**
   - Navigate to `shared/Database.vb`
   - This file contains the MySQL connection string

3. **Edit the Connection String:**
   ```vb
   ' Example connection string in Database.vb
   Public connectionString As String = "Server=localhost;Database=efe_db;Uid=root;Pwd=your_password;Port=3306"
   ```

   **Update the following parameters:**
   - `Server` — MySQL server address (usually `localhost` for local development)
   - `Database` — Database name (`efe_db`)
   - `Uid` — MySQL username (default `root`)
   - `Pwd` — MySQL password (set during MySQL installation)
   - `Port` — MySQL port (default `3306`)

4. **Save the file**

### Step 4: Verify Student Import File

1. **Locate the student import template:**
   - Path: `core/ApplicantData.xlsx`

2. **File structure (required columns):**
   | Column | Example | Notes |
   |--------|---------|-------|
   | Index Number | 5241570019 | Unique identifier for each student |
   | Program | BSc Information Technology | Must match program names created in system |
   | Admission Year | 2026 | Year student was admitted |
   | Default Password | ABC123 | Initial login password (students must change on first login) |

3. **Keep this file in `core/` folder** — Accountant will upload it during system initialization

### Step 5: Build and Run

1. **Build the solution:**
   - Visual Studio → Build → Build Solution (or press Ctrl+Shift+B)
   - Ensure no compilation errors

2. **Run the application:**
   - Press F5 or click Debug → Start Debugging
   - Application window opens with login screen

3. **Initial Login:**
   - **Default Admin Account:**
     - Username: `admin` or `admin2`
     - Password: `admin` (or check `efe_db.sql` for initial credentials)

---

## 🚀 Quick Start Guide

### For Administrators (First-Time Setup)

#### Phase 1: System Initialization

1. **Login as Accountant**
   - Username: `admin`
   - Password: (default credentials from database)
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image3.png)
   

2. **Create Academic Year**
   - Navigate: Dashboard → Academic Calendar → New Academic Year
   - Enter: `2026/2027`
   - Mark as Active (only one academic year active at a time)

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image9.png)


3. **Create Semesters**
   - Add: Semester 1, Semester 2
   - Set start and end dates
   
4. **Create Programs**
   - Examples: BSc Information Technology, BSc Mathematics, BSc Accounting, BSc Economics
   - Set program codes and faculty assignments

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image3.png)


5. **Configure Fee Structure**
   - For each Program:
     - Set annual fee amount (e.g., GH₵3,000)
     - Set registration deadline (e.g., 15-Oct-2026) (Excluded in this Current version)
     - Link to active academic year

     
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image4.png)


![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image8.png)



6. **Import Students**
   - Upload `core/ApplicantData.xlsx`
   - System automatically creates student accounts with:
     - Index Number (unique ID)
     - Default Password (students must change on first login)
     - Assigned Program
     - Account Status: Pending Activation

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image5.png)


![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image6.png)


![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image7.png)


#### Phase 2: System Ready for Students

Students can now login and begin using the system.

---

### For Students (First Login)

1. **First Time Access**
   - Index Number: `5241570019` (example)
   - Password: `123456` (default, provided by university)

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image12.png)
   


2. **Complete Profile**
   - Upload passport photo
   - Enter full name, gender, phone, email
   - Set new password
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image13.png)
     
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image14.png)
      
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image15.png)
  
3. **Dashboard Overview**
   - View Total Fees, Amount Paid, Outstanding Balance
   - Check Course Registration Eligibility Status
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image16.png)

4. **Pay Fees**
   - Enter payment amount
   - System simulates payment processing
   - Receive receipt with transaction reference
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image17.png)
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image19.png)
  
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image18.png)
 

1. 5. **Register Courses**
   - If eligible (meets minimum payment requirement): Course registration opens
   - If not eligible: Registration blocked until payment requirement met
   - Verify deadline status (registration may close even if eligible)
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image20.png)
   
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image21.png)
  
![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image22.png)

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image23.png)

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image24.png)

---

## 🔄 How It Works

### Operational Workflow

#### Six Phases of System Operation

**PHASE 1: University Prepares the System**
- Accountant creates academic calendar, programs, and fee structures
- System is configured before student activation

**PHASE 2: Student First-Time Activation**
- Student receives login credentials (Index Number + Default Password)
- Student completes profile on first login
- Password is hashed and stored securely

**PHASE 3: Student Dashboard**
- Student views real-time fee summary
- Dashboard displays: Total Fees, Amount Paid, Outstanding Balance, Eligibility Status

**PHASE 4: Fee Payment**
- Student enters payment amount
- System shows confirmation dialog
- Payment is processed (simulated in MVP)
- Receipt is generated automatically


![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image25.png)



**PHASE 5: Course Registration Eligibility Engine**
This is the intelligent core of the system:

| Semester | Requirement | Condition |
|----------|-------------|-----------|
| **Semester 1** | 50% or more of semester fee | Student must pay at least 50% before registering courses |
| **Semester 2** | 100% of yearly fee | Student must pay full year's fees before registering |



Additionally:
- System checks registration deadline(Excluded from this current version)
- Registration closes after deadline, even if student is eligible
- Status: Eligible, Not Eligible, or Deadline Closed

**PHASE 6: Accountant Monitoring and Reporting**
- Dashboard displays: Total Students, Total Revenue, Students Paid, Students Owing, Registration Eligible, Registration Blocked
- RDLC reports provide detailed financial analytics

![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image11.png)



![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/AppImages/image10.png)



---

### User Roles & Access Control

#### Student Role
- **Permissions**: View own profile, pay fees, register courses, download receipts
- **Restrictions**: Cannot access other students' data, cannot modify fee structures

#### Accountant Role
- **Permissions**: Manage all students, create programs, set fees, generate reports, view audit logs
- **Restrictions**: Cannot modify system settings, cannot override eligibility rules

#### Authentication Method
- **Students**: Index Number + Password
- **Accountant**: Username + Password
- **Security**: Passwords are hashed using SHA-256 algorithm before storage

---

## 🛠️ Technology Stack

| Layer | Technology | Version |
|-------|-----------|---------|
| **IDE** | Microsoft Visual Studio | 2022 |
| **Language** | VB.NET | .NET Framework 4.7.2+ |
| **UI Framework** | Windows Forms (WinForms) | Built-in |
| **Database** | MySQL | 5.7 / 8.0 |
| **Database Driver** | MySQL Connector/NET | Latest |
| **Reporting** | RDLC (Report Definition Language Client-side) | SQL Server Reporting Services |
| **Application Type** | Desktop Financial Management System | Single Solution |
| **Architecture** | Single-tier Windows Application | Monolithic |

---

## 📂 Project Structure

```
SchoolFeesManagementSystem/
├── bin/                                    # Compiled executable files
├── core/
│   ├── PasswordHasher.vb                   # Password hashing utility
│   ├── ApplicantData.xlsx                  # Student import template
│   ├── DatabaseQuery.txt                   # SQL for fresh database setup
│   └── efe_db.sql                          # Pre-built database export
│   └── School Fees Management System(Original) # Draw IO diagram

├── images/                                 # Student passport photo storage
│   └── Students/                           # Individual student photos
│
├── shared/
│   ├── Database.vb                         # Database connection configuration
│   ├── DataGridViewHelper.vb               # Data grid utilities
│   ├── NavButtons.vb                       # Navigation controls
│   └── Session.vb                          # User session management
│
├── features/
│   ├── auth/                               # Authentication module
│   │   └── LoginForm.vb & SplashScreen.vb                    # Login interface
│   │
│   ├── accountantUser/                     # Accountant dashboard
│   │   ├── AdminDashboard.vb                    # Main accountant dashboard
│   │       etc...
│   │  
│   │  
│   │   
│   │
│   └── studentUser/                        # Student portal
│       ├── StudentDashboard.vb                    # Student dashboard
│       ├── Register.vb            # Profile update form
│           etc...
│
│
│
├── Reports/                                # RDLC report definitions
│   ├── FinancialSummary.rdlc               # Fee & payment summary report
│   
│   
│
├── ApplicationEvents.vb                    # Application startup events
├── dsDummySchema.xsd                       # Dataset schema definitions
├── dsDummySchema.Designer.vb               # Dataset generated code
├── FinancialReportModel.vb                 # Report data model
├── .gitignore                              # Git exclusion rules
├── LICENSE                                 # License file
└── SchoolFeesManagementSystem.sln          # Visual Studio Solution file
```

---

## 🔧 Configuration

### Database Configuration

**File**: `shared/Database.vb`

```vb
Public Class DatabaseManager
    Public Shared connectionString As String = "Server=localhost;Database=efe_db;Uid=root;Pwd=your_password;Port=3306"
    
    ' Connection string parameters:
    ' Server     — MySQL server hostname or IP address
    ' Database   — Database name (efe_db)
    ' Uid        — MySQL username
    ' Pwd        — MySQL password
    ' Port       — MySQL port (default 3306)
End Class
```

**To modify connection:**
1. Open `shared/Database.vb` in Visual Studio
2. Update connection string with your MySQL credentials
3. Save and rebuild solution

### Payment Processing Configuration

**Current Status**: All payment processing is **simulated** in this MVP version.
- No real payment gateway integration (no Stripe, PayPal, MoMo API)
- Payments are recorded in database as "Completed" for testing purposes
- For production: Integrate actual payment gateway in `features/studentUser/PaymentForm.vb`

### Report Configuration

**RDLC Reports Location**: `Reports/` folder

Available reports:
- `FinancialSummary.rdlc` — Overall fee collection summary
- `StudentStatement.rdlc` — Individual student fee statement
- `PaymentAudit.rdlc` — Complete payment transaction audit trail

**To customize reports:**
1. Open report file in Visual Studio Report Designer
2. Modify layout, fields, or calculations
3. Deploy with application

---

## 🐛 Troubleshooting

### Database Connection Issues

**Problem**: "Cannot connect to database"
```
Solution:
1. Verify MySQL server is running
2. Check connection string in Database.vb
3. Verify username and password are correct
4. Ensure database 'efe_db' exists: SHOW DATABASES;
5. Test connection: mysql -h localhost -u root -p
```

**Problem**: "Access denied for user 'root'@'localhost'"
```
Solution:
1. Verify MySQL password in Database.vb is correct
2. Reset MySQL password if forgotten
3. Grant permissions: GRANT ALL PRIVILEGES ON efe_db.* TO 'root'@'localhost';
4. Flush privileges: FLUSH PRIVILEGES;
```

### Application Won't Start

**Problem**: "Application failed to start"
```
Solution:
1. Ensure .NET Framework 4.7.2 is installed
2. Check Visual Studio Build output for compilation errors
3. Verify all project files are present
4. Clean solution: Build → Clean Solution
5. Rebuild: Build → Build Solution
```

### Student Import Failures

**Problem**: "Excel file import failed"
```
Solution:
1. Verify Excel file format is .xlsx (not .xls)
2. Check that all required columns exist:
   - Index Number
   - Program (must match created programs)
   - Admission Year
   - Default Password
3. Ensure no duplicate index numbers in file
4. Verify program names exactly match system programs
```

### Course Registration Not Working

**Problem**: "Cannot register courses despite paying fees"
```
Solution:
1. Verify payment amount meets semester requirement:
   - Semester 1: 50% or more of semester fee
   - Semester 2: 100% of yearly fees
2. Check registration deadline: System blocks registration after deadline
3. Verify student's program is correctly assigned
4. Check semester is marked as "active" in academic calendar
```

### Report Generation Issues

**Problem**: "RDLC reports not displaying data"
```
Solution:
1. Verify dataset is properly connected to report
2. Check report data source in Report Designer
3. Ensure student payment records exist in database
4. Refresh report data: Close and reopen report
5. Check file permissions for report definitions folder
```

---

## 👥 Contributing

### How to Contribute

1. **Report Bugs** — Find an issue? Open a GitHub Issue with:
   - Steps to reproduce
   - Expected vs. actual behavior
   - Screenshot if applicable
   - Database & system information

2. **Suggest Features** — Have ideas? Start a Discussion thread or open a Feature Request issue

3. **Submit Code** — Want to contribute?
   ```bash
   # Fork the repository
   # Create a feature branch: git checkout -b feature/YourFeatureName
   # Commit changes: git commit -m "Add feature description"
   # Push to branch: git push origin feature/YourFeatureName
   # Open a Pull Request
   ```

4. **Code Standards**
   - Follow existing VB.NET naming conventions
   - Add comments for complex logic
   - Test changes in multiple scenarios
   - Document any new features

### Development Workflow

1. **Clone** the repository
2. **Create a feature branch** from `main`
3. **Make changes** and test thoroughly
4. **Update documentation** if adding features
5. **Submit PR** with clear description

---


## 🎓 Academic Origin

**Project From:** University of Education, Winneba  
**Department:** BSc. Information & Communications Technology Education (ICTE)  
**Supervised By:** Dr. Daniel Danso Essel

---

## 📞 CONTACT & SUPPORT

For questions or support:
- **WhatsApp**: [Contact Engineers](https://wa.me/233507326320?text=*UCAM_From_Github_💬Message_:*%20)
- **GitHub**: Open an issue in the repository

---



**Issue Tracking**: GitHub Issues (DesktopSchoolFeesManagementSystem)

**Questions?** Review the [DATABASE.md](./DATABASE.md) and [ARCHITECTURE.md](./ARCHITECTURE.md) documentation files for deeper technical details.

**Database Schema Questions** → See [DATABASE.md](./DATABASE.md)

**System Architecture Questions** → See [ARCHITECTURE.md](./ARCHITECTURE.md)

---

## 🎯 Keywords for Search Optimization

`student fee management system`, `university tuition management`, `financial management information system`, `FMIS`, `VB.NET WinForms`, `MySQL database`, `automated fee collection`, `course registration eligibility`, `financial reporting`, `payment processing system`, `academic financial system`, `institutional finance management`, `electronic fee management`

---


**Last Updated**: September 2026
**Built With**: Microsoft Visual Studio 2022 VB.NET | Windows Forms | MySQL

