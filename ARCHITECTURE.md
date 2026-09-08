# 🏗️ System Architecture & Design

**School Fees Management System (EFE) — Technical Architecture Reference**

Complete documentation of the system architecture, component design, data flows, design patterns, and technical implementation details for the School Fees Management System.

---

## 📋 Table of Contents

- [Architecture Overview](#architecture-overview)
- [System Architecture Diagram](#system-architecture-diagram)
- [Technology Stack](#technology-stack)
- [Application Layers](#application-layers)
- [Core Modules & Components](#core-modules--components)
- [Data Flow & Workflows](#data-flow--workflows)
- [Design Patterns](#design-patterns)
- [Authentication & Authorization](#authentication--authorization)
- [Security Architecture](#security-architecture)
- [File Storage & Image Management](#file-storage--image-management)
- [Reporting & Analytics](#reporting--analytics)
- [Error Handling & Logging](#error-handling--logging)
- [Performance Optimization](#performance-optimization)
- [Scalability & Future Enhancements](#scalability--future-enhancements)
- [Development Standards](#development-standards)

---

## 🎯 Architecture Overview

### System Type

**Single-Tier Desktop Financial Management Information System (FMIS)**

- **Deployment Model**: Monolithic Windows Desktop Application
- **Architecture Pattern**: Single Solution (one .sln file)
- **Execution Model**: Single-threaded WinForms UI with database backend
- **Scalability**: Designed for institutional (university) use; handles 1-10K student records

### Core Principles

✅ **Centralized** — All data flows through single MySQL database
✅ **Role-Based** — Student and Accountant interfaces segregated by login role
✅ **Automated** — Eligibility verification engine runs without manual intervention
✅ **Auditable** — Every transaction logged with timestamps and user context
✅ **Offline-Capable** — Read operations work; payments require live connection

### Key Architectural Decisions

| Decision | Rationale | Trade-Off |
|----------|-----------|-----------|
| **Single Solution** | Simple deployment, unified codebase | Limited horizontal scalability |
| **Windows Desktop App** | Direct institutional control, no hosting costs | Windows-only (no cloud/mobile) |
| **Monolithic** | Rapid development, tight integration | Difficult to split services later |
| **Simulated Payments** | MVP focus on eligibility engine | Must integrate real gateway for production |
| **MySQL Database** | Industry-standard, easy setup, affordable | Not ideal for petabyte-scale analytics |

---

## 🔀 System Architecture Diagram

### High-Level Component Architecture

```
┌──────────────────────────────────────────────────────────────┐
│                   SCHOOL FEES MANAGEMENT SYSTEM (EFE)         │
│                      Windows Desktop Application              │
└──────────────────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────────────────────────┐
│                            PRESENTATION LAYER (WinForms UI)                 │
├────────────────────────────┬─────────────────────────────────────────────────┤
│                            │                                                 │
│   ACCOUNTANT UI             │              STUDENT UI                        │
│   ─────────────────         │              ──────────                        │
│   • Dashboard               │              • Dashboard                       │
│   • Student Import          │              • Profile                         │
│   • Program Management      │              • Pay Fees                        │
│   • Fee Configuration       │              • Register Courses               │
│   • Payment Monitoring      │              • Receipts                        │
│   • Financial Reports       │                                                │
│                             │                                                │
└────────────────────────────┴─────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────────────────────────┐
│                    SHARED SERVICES LAYER (Common Logic)                    │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐         │
│  │ Database Manager │  │ Session Manager  │  │ Authentication   │         │
│  │ (MySQL Queries)  │  │ (User Context)   │  │ (Password Hash)  │         │
│  └──────────────────┘  └──────────────────┘  └──────────────────┘         │
│                                                                             │
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐         │
│  │ UI Helpers       │  │ Data Grid Utils  │  │ Navigation       │         │
│  │                  │  │                  │  │ Controls         │         │
│  └──────────────────┘  └──────────────────┘  └──────────────────┘         │
│                                                                             │
└────────────────────────────────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────────────────────────┐
│                    BUSINESS LOGIC LAYER (Eligibility Engine)               │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  ┌───────────────────────────────────────────────────────────────┐        │
│  │ ELIGIBILITY VERIFICATION ENGINE                              │        │
│  │                                                               │        │
│  │  Rule A (Semester 1): Payment >= 50% of annual fee ✓         │        │
│  │  Rule B (Semester 2): Payment >= 100% of annual fee ✓        │        │
│  │  Rule C (Deadline):   Today <= registration deadline ✓       │        │
│  │                                                               │        │
│  │  Output: eligible | not_eligible | deadline_closed           │        │
│  └───────────────────────────────────────────────────────────────┘        │
│                                                                             │
│  ┌───────────────────┐  ┌─────────────────────┐  ┌───────────────┐       │
│  │ Payment Engine    │  │ Receipt Generator   │  │ Report Engine │       │
│  │ (Simulated)       │  │ (Auto-Receipt)      │  │ (RDLC)        │       │
│  └───────────────────┘  └─────────────────────┘  └───────────────┘       │
│                                                                             │
└────────────────────────────────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────────────────────────┐
│                        DATA ACCESS LAYER (Repository)                      │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  • MySQL Connector/NET (Database Driver)                                   │
│  • Connection String: Database.vb                                          │
│  • Query Execution: SqlCommand with Parameters                             │
│  • Data Mapping: DataSet/DataTable → Domain Objects                       │
│                                                                             │
└────────────────────────────────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────────────────────────┐
│                      PERSISTENCE LAYER (MySQL Database)                    │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐        │
│  │ Academic Config  │  │ User Management  │  │ Financial Data   │        │
│  │ Tables           │  │ Tables           │  │ Tables           │        │
│  │                  │  │                  │  │                  │        │
│  │ • academic_years │  │ • students       │  │ • payments       │        │
│  │ • semesters      │  │ • staff_users    │  │ • receipts       │        │
│  │ • programs       │  │                  │  │ • course_regs    │        │
│  │ • fee_structures │  │                  │  │                  │        │
│  └──────────────────┘  └──────────────────┘  └──────────────────┘        │
│                                                                             │
│  MySQL 5.7 / 8.0 Database (efe_db)                                         │
│  Character Set: UTF8MB4 | Engine: InnoDB | ACID Compliant                 │
│                                                                             │
└────────────────────────────────────────────────────────────────────────────┘

┌────────────────────────────────────────────────────────────────────────────┐
│                    EXTERNAL RESOURCES (File System)                         │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  • Images/Students/ — Student passport photos                              │
│  • Reports/         — RDLC report definitions (.rdlc files)               │
│  • core/            — Initialization scripts and templates                │
│                                                                             │
└────────────────────────────────────────────────────────────────────────────┘
```

---

## 🛠️ Technology Stack

### Frontend (User Interface)

| Component | Technology | Version | Purpose |
|-----------|-----------|---------|---------|
| **UI Framework** | Windows Forms (WinForms) | .NET Framework 4.7.2 | Desktop application UI |
| **Language** | VB.NET | .NET | Business logic implementation |
| **IDE** | Visual Studio | 2022 | Development environment |
| **Data Binding** | DataSet / DataTable | Built-in | UI ↔ Database binding |

### Backend (Business Logic)

| Component | Technology | Version | Purpose |
|-----------|-----------|---------|---------|
| **Language** | VB.NET | .NET Framework 4.7.2 | Application logic |
| **Authentication** | SHA-256 Hashing | Cryptographic Standard | Password security |
| **Session Management** | In-Memory Session Object | Custom | User context tracking |
| **Validation** | Custom Validation Logic | VB.NET | Business rule enforcement |

### Data Layer

| Component | Technology | Version | Purpose |
|-----------|-----------|---------|---------|
| **Database** | MySQL | 5.7 / 8.0 | Data persistence |
| **Database Driver** | MySQL Connector/NET | Latest | Database connectivity |
| **Query Method** | Parameterized SQL | T-SQL Standard | SQL injection prevention |
| **Character Set** | UTF8MB4 | Unicode | International character support |

### Reporting

| Component | Technology | Version | Purpose |
|-----------|-----------|---------|---------|
| **Report Engine** | RDLC (Report Definition Language Client-side) | SQL Server Reporting Services | Report generation & export |
| **Report Format** | .rdlc Files | XML-based | Report definitions |
| **Output Formats** | PDF, Excel, Print | Built-in | Report delivery |

---

## 📚 Application Layers

### Layer 1: Presentation Layer (User Interface)

**Location**: `features/` folder

**Core Responsibility**: Render UI components and capture user input

**Components**:
- Authentication forms (login screen)
- Student dashboard (fee overview, course registration, receipts)
- Accountant dashboard (student import, fee configuration, reports)
- Data entry forms (profile updates, payment submission)

---

### Layer 2: Shared Services Layer

**Location**: `shared/` folder

**Core Responsibility**: Provide common utilities and infrastructure

Key classes:
- **Database.vb** — MySQL connection management, query execution
- **Session.vb** — Current user session tracking
- **PasswordHasher.vb** — SHA-256 password hashing
- **DataGridViewHelper.vb** — Data grid formatting utilities

---

### Layer 3: Business Logic Layer

**Location**: Embedded in form event handlers

**Core Responsibility**: Implement business rules and algorithms

Key logic:
- **Eligibility Verification Engine** — Three-rule system (50%, 100%, deadline)
- **Payment Processing** — Simulated payment submission and recording
- **Receipt Generation** — Automatic receipt creation with unique numbers
- **Audit Logging** — Track all transactions with timestamps

---

### Layer 4: Data Access Layer

**Location**: `shared/Database.vb`

**Pattern**: Parameterized SQL queries (no ORM)

**Methods**:
- ExecuteQuery() — SELECT statements returning DataTable
- ExecuteNonQuery() — INSERT/UPDATE/DELETE statements
- BulkInsert() — Batch operations for bulk student imports

---

### Layer 5: Persistence Layer

**Location**: MySQL database server (localhost:3306)

**Database**: efe_db (8 core tables with relationships)

**Features**: ACID compliance, foreign key constraints, automatic timestamps

---

## 🔄 Data Flow & Workflows

### Workflow 1: Student Login & Dashboard

```
User Login Input
    ↓
Validate Index Number + Password
    ├→ Query: students WHERE index_number = ?
    ├→ Compare SHA-256 hashes
    └→ Session initialization
    ↓
Student Dashboard
    ├→ Fetch total fees (from fee_structures)
    ├→ Fetch payments sum (from payments)
    ├→ Calculate outstanding balance
    └→ Render UI with metrics
```

### Workflow 2: Pay Fees → Receipt

```
Payment Form Submission
    ↓
ProcessPayment()
    ├→ Validate amount > 0
    ├→ Simulate payment (MVP)
    └→ INSERT into payments table
    ↓
ReceiptFactory.CreateReceipt()
    ├→ Generate receipt number
    ├→ Snapshot student name, amount, date
    └→ INSERT into receipts table
    ↓
Eligibility Recalculation
    └→ Update course registration status
```

### Workflow 3: Course Registration Eligibility

```
CourseRegistration Form Submit
    ↓
VerifyEligibility()
    ├→ Query: fee_structures (get annual fee)
    ├→ Query: payments SUM (total student paid)
    ├→ Check deadline: today <= registration_deadline?
    │   ├─ YES → Check payment requirement
    │   └─ NO → Return "Deadline Closed"
    ├→ If semester 1: required = 50% of annual fee
    └─ If semester 2: required = 100% of annual fee
    ↓
Compare Logic
    ├→ If total_paid >= required → "Eligible"
    └→ If total_paid < required → "Not Eligible"
    ↓
Log Eligibility Decision
    └→ INSERT into course_registrations (audit trail)
```

---

## 🎨 Design Patterns

### 1. Repository Pattern (Data Access)

**Problem**: Business logic scattered in UI event handlers

**Solution**: Centralized database methods in `Database.vb`

```vb
' All queries flow through one class
Public Class DatabaseManager
    ' Query execution methods
    Public Shared Function ExecuteQuery(...) As DataTable
    Public Shared Function ExecuteNonQuery(...) As Integer
End Class
```

### 2. Session Management Pattern

**Problem**: User context must be available throughout application

**Solution**: Static `SessionManager` class holds current user

```vb
Public Class SessionManager
    Public Shared CurrentStudentId As Integer
    Public Shared CurrentRole As String
End Class
```

### 3. Eligibility Engine Pattern

**Problem**: Complex conditional logic for course registration scattered

**Solution**: Centralized VerifyEligibility() method

```vb
Public Function VerifyEligibility(...) As EligibilityResult
    ' All eligibility rules in one place
    ' Rule A: 50% for semester 1
    ' Rule B: 100% for semester 2
    ' Rule C: Check deadline
End Function
```

### 4. Factory Pattern (Object Creation)

**Problem**: Receipt creation requires multiple steps

**Solution**: ReceiptFactory encapsulates creation logic

```vb
Public Class ReceiptFactory
    Public Shared Function CreateReceipt(paymentId) As Receipt
        ' Retrieve payment data
        ' Generate unique receipt number
        ' Insert receipt record
        ' Return receipt object
    End Function
End Class
```

---

## 🔐 Authentication & Authorization

### Authentication (Login)

**Method**: SHA-256 password hashing

```
User password: "ABC123"
    ↓ [Hashed with SHA-256]
Stored hash: "a665a45920422f9d417e..."

Login attempt: "ABC123"
    ↓ [Hashed with SHA-256]
Input hash: "a665a45920422f9d417e..."
    ↓ [Compare]
Match? YES → Login successful
```

### Authorization (Role-Based Access)

**Roles**:
- **Student** — View own fees, pay fees, register courses
- **Accountant** — Manage all students, configure fees, generate reports
- **Admin** — Full system access

**Implementation**:

```vb
' Hide/show UI elements based on role
Select Case SessionManager.CurrentRole
    Case "Student"
        btnPayFees.Visible = True
        btnStudentImport.Visible = False
    Case "Accountant", "Admin"
        btnPayFees.Visible = False
        btnStudentImport.Visible = True
End Select
```

---

## 🛡️ Security Architecture

### Input Validation

✅ **Parameterized Queries** (prevent SQL injection)
```vb
' Safe: Parameters are escaped automatically
cmd.Parameters.AddWithValue("@index", userInput)

' Unsafe: String concatenation
"WHERE index_number = '" & userInput & "'"
```

### Password Storage

✅ **SHA-256 Hashing** (one-way cryptography)
- Passwords never stored as plaintext
- Hash comparison for verification

### Session Security

✅ **In-Memory Sessions** (lost on application exit)
✅ **Role-Based Authorization** (check permissions before operations)
✅ **Audit Logging** (track who did what and when)

### Data Access Control

✅ **Row-Level Security** (students see only own data)
```vb
' Only allow viewing own payments
If requestingStudentId <> targetStudentId Then
    Throw New UnauthorizedAccessException()
End If
```

---

## 📁 File Storage & Image Management

**Student Photo Storage**:
```
Images/
└── Students/
    ├── 5241570019.jpg
    ├── 5241570020.jpg
    └── ...
```

**Process**:
1. Student uploads passport photo via ProfileManagement form
2. File saved to `Images/Students/{IndexNumber}.jpg`
3. Path stored in database: `passport_photo_path`
4. Display on forms by loading from disk

---

## 📊 Reporting & Analytics

**RDLC Reports**:
- FinancialSummary.rdlc — Total revenue, collections by program
- StudentStatement.rdlc — Individual student fee statement
- PaymentAudit.rdlc — Transaction audit trail

**Process**:
1. Query database for report data
2. Load RDLC report definition
3. Bind DataTable to report
4. Render in report viewer
5. Export to PDF/Excel/Print

---

## ⚠️ Error Handling & Logging

**Pattern**: Try-Catch-Finally with centralized logging

```vb
Try
    ' Perform operation
Catch ex As SqlException
    ' Log database errors
Catch ex As ArgumentException
    ' Log business logic errors
Catch ex As Exception
    ' Log unexpected errors
Finally
    ' Cleanup resources
End Try
```

**Logging**: Errors written to file + database (audit trail)

---

## ⚡ Performance Optimization

### Database Indexing

```sql
CREATE INDEX idx_students_index_number ON students(index_number);
CREATE INDEX idx_payments_student_id ON payments(student_id);
```

### Query Caching

Cache frequently-accessed data (academic year, programs) with 5-minute TTL

### Batch Operations

Import 1000 students using database transactions (not individual queries)

---

## 🚀 Scalability & Future Enhancements

### Current Limitations

- Single machine (no load balancing)
- Windows desktop only (no mobile/web)
- Monolithic (hard to split)
- Simulated payments (no real gateway)

### Phase 2 Enhancements

- Real payment gateway integration
- Email/SMS notifications
- Excel export for reports
- Payment history graphs

### Phase 3+ Enhancements

- Web portal (ASP.NET Core)
- Mobile app (Flutter)
- Microservices architecture
- Cloud deployment (AWS/Azure)
- Advanced analytics (ML models)

---

## 📝 Development Standards

### Naming Conventions

- **Classes**: PascalCase (StudentDashboard)
- **Methods**: PascalCase (ProcessPayment)
- **Variables**: camelCase (studentId, totalPaid)
- **Constants**: UPPER_SNAKE_CASE (MAX_PAYMENT_AMOUNT)
- **Form Controls**: Hungarian notation (btnSubmit, txtName, lblMessage)

### Code Organization

```
features/              # User interfaces
  ├── auth/            # Login form
  ├── accountantUser/  # Accountant features
  └── studentUser/     # Student features

shared/                # Reusable services
  ├── Database.vb
  ├── Session.vb
  └── Utilities.vb

core/                  # Database & templates
  ├── DatabaseQuery.txt
  └── PasswordHasher.vb

Reports/               # RDLC report definitions
```

### Code Comments

```vb
' Explain WHY, not WHAT
' Semester 1 requires 50% per university policy
Dim required = feeAmount * 0.50

''' <summary>
''' Verifies course registration eligibility
''' </summary>
Public Function VerifyEligibility(...) As EligibilityResult
End Function
```

---

## 🔗 Related Documentation

- **README.md** — Project overview and setup
- **DATABASE.md** — Complete database schema reference
- `core/DatabaseQuery.txt` — SQL table creation
- `efe_db.sql` — Pre-built database export

---

**Architecture Type**: Single-Tier Monolithic Desktop FMIS  
**Last Updated**: September 2026
