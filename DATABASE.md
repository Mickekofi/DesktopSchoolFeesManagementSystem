# 🗄️ Database Schema & Documentation

**School Fees Management System (EFE) — Database Design Reference**

Complete documentation of the MySQL database structure, relationships, constraints, and data dictionary for the School Fees Management System.

---


![Preview](https://github.com/Mickekofi/DesktopSchoolFeesManagementSystem/blob/master/db_image_.png)




## 📋 Table of Contents

- [Database Overview](#database-overview)
- [Core Concepts](#core-concepts)
- [Entity Relationship Diagram](#entity-relationship-diagram)
- [Complete Schema Reference](#complete-schema-reference)
  - [Academic Configuration Tables](#academic-configuration-tables)
  - [Identity & User Management](#identity--user-management)
  - [Financial & Transaction Tables](#financial--transaction-tables)
- [Data Dictionary](#data-dictionary)
- [Relationships & Constraints](#relationships--constraints)
- [Indexes & Performance](#indexes--performance)
- [Initialization & Sample Data](#initialization--sample-data)
- [SQL Scripts & Deployment](#sql-scripts--deployment)
- [Best Practices](#best-practices)

---

## 📊 Database Overview

### Database Name
```sql
efe_db
```

### Database Properties
- **Character Set**: UTF8MB4 (supports international characters)
- **Collation**: UTF8MB4_UNICODE_CI (case-insensitive)
- **Engine**: InnoDB (supports transactions and foreign keys)
- **Total Tables**: 8 core tables + relationships

### Key Database Features
✅ **ACID Compliance** — All transactions maintain integrity
✅ **Referential Integrity** — Foreign key constraints ensure data consistency
✅ **Cascading Updates/Deletes** — Related records update automatically
✅ **Audit Logging** — Timestamps track all record creation and updates
✅ **Role-Based Isolation** — Student and Accountant data separated by permissions

---

## 🎯 Core Concepts

### 1. Academic Calendar Structure

The system organizes time into a hierarchical structure:

```
Academic Year (2026/2027)
├── Semester 1
│   └── Courses registered here
└── Semester 2
    └── Courses registered here
```

**Key Principle**: Only ONE academic year can be active at a time.

### 2. Fee Structure Model

Fees are defined at the **Annual Target Level** (not per-semester):

```
Program: BSc Information Technology
├── Academic Year: 2026/2027
│   ├── Target Fee: GH₵3,000 (annual)
│   └── Registration Deadline: 15-Oct-2026
```

**Why This Design?**
- Universities charge students a **yearly target fee**
- Payment requirements differ by semester:
  - **Semester 1**: Student must pay ≥50% of annual fee
  - **Semester 2**: Student must pay 100% of annual fee
- Single fee record per (Program, Academic Year) pair

### 3. Eligibility Engine

Course registration is gated by two rules:

| Rule | Semester | Requirement | Status |
|------|----------|-------------|--------|
| **Rule A** | Semester 1 | Student paid ≥50% of annual fee | Eligible/Not Eligible |
| **Rule B** | Semester 2 | Student paid 100% of annual fee | Eligible/Not Eligible |
| **Deadline** | All | Today ≤ registration deadline | Within Deadline/Closed |

**Example**:
- Program fee: GH₵1,500 (annual)
- Semester 1 requirement: GH₵750 (50%)
- Student paid: GH₵800 ✅ ELIGIBLE
- Student paid: GH₵700 ❌ NOT ELIGIBLE (short GH₵50)

---

## 📐 Entity Relationship Diagram

```
┌─────────────────────────┐
│   academic_years        │
├─────────────────────────┤
│ id (PK)                 │
│ year_label (UNIQUE)     │ ◄──────────────────────────┐
│ is_active               │                            │
│ start_date              │                            │
│ end_date                │                            │
│ created_at              │                            │
│ updated_at              │                            │
└────────┬────────────────┘                            │
         │ (1:M)                                       │
         │                                             │
    ┌────▼──────────────────────┐                      │
    │    semesters              │                      │
    ├───────────────────────────┤                      │
    │ id (PK)                   │                      │
    │ academic_year_id (FK)     │                      │
    │ semester_name             │ ◄──────┐             │
    │ semester_number           │        │             │
    │ start_date                │        │             │
    │ end_date                  │        │             │
    │ is_active                 │        │             │
    │ created_at                │        │             │
    │ updated_at                │        │             │
    └────┬──────────────────────┘        │             │
         │ (1:M)                         │             │
         │                               │             │
    ┌────▼─────────────────────────┐    │             │
    │ course_registrations         │    │             │
    ├──────────────────────────────┤    │             │
    │ id (PK)                      │    │             │
    │ student_id (FK)              │    │             │
    │ semester_id (FK) ────────────┼────┘             │
    │ academic_year_id (FK) ───────┼──────────────────┘
    │ registration_status          │
    │ eligibility_status           │
    │ deadline_status              │
    │ registration_date            │
    └──────────────────────────────┘

┌─────────────────────────┐
│   programs              │
├─────────────────────────┤
│ id (PK)                 │
│ program_code (UNIQUE)   │ ◄──────────────┐
│ program_name            │                │
│ faculty                 │                │
│ level_duration_years    │                │
│ base_fee                │                │
│ status                  │                │
│ created_at              │                │
│ updated_at              │                │
└────┬────────────────────┘                │
     │ (1:M)                               │
     │                                     │
┌────▼──────────────────────────────┐      │
│ fee_structures                     │      │
├────────────────────────────────────┤      │
│ id (PK)                            │      │
│ program_id (FK) ───────────────────┼──────┘
│ academic_year_id (FK) ─────┐       │
│ fee_amount                  │       │
│ min_payment_percentage      │       │
│ registration_deadline       │       │
│ is_active                   │       │
│ UNIQUE(program_id, academic_year_id)
│ created_at                  │       │
│ updated_at                  │       │
└────────┬────────────────────┘       │
         │ (1:M)                      │
         │                            │
    ┌────▼──────────────────────┐     │
    │ payments                   │     │
    ├────────────────────────────┤     │
    │ id (PK)                    │     │
    │ student_id (FK)            │     │
    │ fee_structure_id (FK) ─────┼──────┘
    │ amount_paid                │
    │ payment_method             │
    │ transaction_reference      │
    │ payment_status             │
    │ payment_date               │
    │ created_at                 │
    │ updated_at                 │
    └────┬──────────────────────┘
         │ (1:M)
         │
    ┌────▼──────────────────────┐
    │ receipts                   │
    ├────────────────────────────┤
    │ id (PK)                    │
    │ receipt_number (UNIQUE)    │
    │ payment_id (FK)            │
    │ student_name               │
    │ index_number               │
    │ amount                     │
    │ receipt_date               │
    │ is_printed                 │
    │ print_count                │
    │ created_at                 │
    │ updated_at                 │
    └────────────────────────────┘

┌─────────────────────────┐
│   students              │
├─────────────────────────┤
│ id (PK)                 │
│ index_number (UNIQUE)   │ ◄────┐
│ default_password        │      │
│ password_hash           │      │
│ full_name               │      │
│ gender                  │      │
│ phone                   │      │
│ email                   │      │
│ passport_photo_path     │      │
│ program_id (FK)         │      │
│ admission_year          │      │
│ is_first_login          │      │
│ account_status          │      │
│ created_at              │      │
│ updated_at              │      │
└─────────────────────────┘      │
                                  │
                    ┌─────────────┘
                    │
┌───────────────────▼──────────┐
│   staff_users                │
├────────────────────────────────┤
│ id (PK)                        │
│ username (UNIQUE)              │
│ password_hash                  │
│ full_name                      │
│ role (Admin, Accountant)       │
│ is_active                      │
│ created_at                     │
│ updated_at                     │
└────────────────────────────────┘

Legend:
PK  = Primary Key
FK  = Foreign Key
M   = Many
1:M = One-to-Many relationship
```

---

## 🔐 Complete Schema Reference

### SECTION 1: Academic Configuration Tables

These tables define the academic calendar, programs, and fee structure.

#### TABLE: `academic_years`

Represents university academic years (e.g., 2026/2027).

```sql
CREATE TABLE academic_years (
    id INT AUTO_INCREMENT PRIMARY KEY,
    year_label VARCHAR(20) NOT NULL UNIQUE,
    is_active BOOLEAN DEFAULT FALSE,
    start_date DATE,
    end_date DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique identifier |
| `year_label` | VARCHAR(20) | NOT NULL, UNIQUE | Academic year format (e.g., "2026/2027") |
| `is_active` | BOOLEAN | DEFAULT FALSE | Only ONE academic year active at a time |
| `start_date` | DATE | NULLABLE | Academic year start date |
| `end_date` | DATE | NULLABLE | Academic year end date |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Record creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Constraints**:
- ✅ Only one row can have `is_active = TRUE`
- ✅ `year_label` must be unique (no duplicate years)

**Example Data**:
```sql
INSERT INTO academic_years (year_label, is_active, start_date, end_date)
VALUES ('2026/2027', TRUE, '2026-09-01', '2027-08-31');
```

---

#### TABLE: `semesters`

Represents academic semesters within an academic year (Semester 1, Semester 2).

```sql
CREATE TABLE semesters (
    id INT AUTO_INCREMENT PRIMARY KEY,
    academic_year_id INT NOT NULL,
    semester_name ENUM('Semester 1', 'Semester 2') NOT NULL,
    semester_number INT NOT NULL,
    start_date DATE,
    end_date DATE,
    is_active BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_semester_academic_year
        FOREIGN KEY (academic_year_id) REFERENCES academic_years(id)
        ON UPDATE CASCADE ON DELETE CASCADE
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique identifier |
| `academic_year_id` | INT | NOT NULL, FOREIGN KEY | References `academic_years.id` |
| `semester_name` | ENUM | NOT NULL | "Semester 1" or "Semester 2" |
| `semester_number` | INT | NOT NULL | 1 or 2 (numeric representation) |
| `start_date` | DATE | NULLABLE | Semester start date |
| `end_date` | DATE | NULLABLE | Semester end date |
| `is_active` | BOOLEAN | DEFAULT FALSE | Whether semester is currently active |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Record creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Foreign Key Constraint**:
- `academic_year_id` → `academic_years.id`
- **ON UPDATE CASCADE**: If academic year ID changes, update this record
- **ON DELETE CASCADE**: If academic year deleted, delete this semester

**Example Data**:
```sql
INSERT INTO semesters (academic_year_id, semester_name, semester_number, start_date, end_date, is_active)
VALUES (1, 'Semester 1', 1, '2026-09-01', '2026-12-15', TRUE);
```

---

#### TABLE: `programs`

Represents academic programs offered by the university.

```sql
CREATE TABLE programs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    program_code VARCHAR(20) UNIQUE,
    program_name VARCHAR(150) NOT NULL,
    faculty VARCHAR(100),
    level_duration_years INT DEFAULT 4,
    base_fee DECIMAL(10,2),
    status ENUM('Active','Inactive') DEFAULT 'Active',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique identifier |
| `program_code` | VARCHAR(20) | UNIQUE | Program code (e.g., "ICTE") |
| `program_name` | VARCHAR(150) | NOT NULL | Full program name (e.g., "BSc Information Technology") |
| `faculty` | VARCHAR(100) | NULLABLE | Faculty/Department (e.g., "School of Engineering") |
| `level_duration_years` | INT | DEFAULT 4 | Program duration in years |
| `base_fee` | DECIMAL(10,2) | NULLABLE | Base annual tuition fee |
| `status` | ENUM | DEFAULT 'Active' | "Active" or "Inactive" |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Record creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Example Data**:
```sql
INSERT INTO programs (program_code, program_name, faculty, level_duration_years, base_fee, status)
VALUES 
('ICTE', 'BSc Information Technology', 'School of Engineering', 4, 3000.00, 'Active'),
('MATH', 'BSc Mathematics', 'School of Science', 4, 2500.00, 'Active'),
('ACCT', 'BSc Accounting', 'School of Business', 4, 2800.00, 'Active');
```

---

#### TABLE: `fee_structures`

**CRITICAL**: Defines the annual fee amount for each Program × Academic Year combination.

This is the **master fee record**. All student payments and eligibility verification reference this table.

```sql
CREATE TABLE fee_structures (
    id INT AUTO_INCREMENT PRIMARY KEY,
    program_id INT NOT NULL,
    academic_year_id INT NOT NULL,
    fee_amount DECIMAL(10,2) NOT NULL,
    min_payment_percentage DECIMAL(5,2) DEFAULT 100.00,
    registration_deadline DATE,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_fee_program
        FOREIGN KEY (program_id) REFERENCES programs(id)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_fee_academic_year
        FOREIGN KEY (academic_year_id) REFERENCES academic_years(id)
        ON UPDATE CASCADE ON DELETE CASCADE,
    -- Enforces one fee per program per academic year
    UNIQUE KEY unique_annual_fee (program_id, academic_year_id)
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique identifier |
| `program_id` | INT | NOT NULL, FOREIGN KEY | References `programs.id` |
| `academic_year_id` | INT | NOT NULL, FOREIGN KEY | References `academic_years.id` |
| `fee_amount` | DECIMAL(10,2) | NOT NULL | Annual tuition fee (e.g., 3000.00) |
| `min_payment_percentage` | DECIMAL(5,2) | DEFAULT 100.00 | Minimum payment % required (used for rules) |
| `registration_deadline` | DATE | NULLABLE | Last day students can register courses |
| `is_active` | BOOLEAN | DEFAULT TRUE | Whether this fee structure is current |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Record creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Critical Unique Constraint**:
```sql
UNIQUE KEY unique_annual_fee (program_id, academic_year_id)
```
✅ Ensures each program has **exactly ONE fee record per academic year**
❌ Prevents duplicate fee entries

**Example Data**:
```sql
INSERT INTO fee_structures (program_id, academic_year_id, fee_amount, min_payment_percentage, registration_deadline, is_active)
VALUES 
(1, 1, 3000.00, 100.00, '2026-10-15', TRUE),   -- BSc IT, 2026/2027, GH₵3,000
(2, 1, 2500.00, 100.00, '2026-10-15', TRUE),   -- BSc Math, 2026/2027, GH₵2,500
(3, 1, 2800.00, 100.00, '2026-10-15', TRUE);   -- BSc Accounting, 2026/2027, GH₵2,800
```

---

### SECTION 2: Identity & User Management

These tables manage student and staff user accounts, authentication, and profiles.

#### TABLE: `students`

Represents all university students registered in the system.

```sql
CREATE TABLE students (
    id INT AUTO_INCREMENT PRIMARY KEY,
    index_number VARCHAR(20) NOT NULL UNIQUE,
    default_password VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255),
    full_name VARCHAR(150),
    gender ENUM('Male','Female','Other'),
    phone VARCHAR(20),
    email VARCHAR(100),
    passport_photo_path TEXT,
    program_id INT,
    admission_year YEAR,
    is_first_login BOOLEAN DEFAULT TRUE,
    account_status ENUM('Active','Suspended','Disabled') DEFAULT 'Active',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_students_program
        FOREIGN KEY (program_id) REFERENCES programs(id)
        ON UPDATE CASCADE ON DELETE SET NULL
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique internal ID |
| `index_number` | VARCHAR(20) | NOT NULL, UNIQUE | Student ID (e.g., "5241570019") |
| `default_password` | VARCHAR(255) | NOT NULL | Initial password from Excel import |
| `password_hash` | VARCHAR(255) | NULLABLE | Hashed password after first login (SHA-256) |
| `full_name` | VARCHAR(150) | NULLABLE | Student full name |
| `gender` | ENUM | NULLABLE | "Male", "Female", or "Other" |
| `phone` | VARCHAR(20) | NULLABLE | Contact phone number |
| `email` | VARCHAR(100) | NULLABLE | Email address |
| `passport_photo_path` | TEXT | NULLABLE | Path to uploaded passport photo (e.g., "Images/Students/5241570019.jpg") |
| `program_id` | INT | NULLABLE, FOREIGN KEY | References `programs.id` |
| `admission_year` | YEAR | NULLABLE | Year of admission (e.g., 2026) |
| `is_first_login` | BOOLEAN | DEFAULT TRUE | Flag: TRUE if student hasn't completed profile yet |
| `account_status` | ENUM | DEFAULT 'Active' | "Active", "Suspended", or "Disabled" |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Account creation timestamp (during Excel import) |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last profile update timestamp |

**Workflow**:
1. Admin imports Excel file → `index_number`, `default_password`, `program_id`, `admission_year` populated
2. Student first login → `is_first_login = TRUE`
3. Student completes profile → `full_name`, `email`, `phone`, `passport_photo_path` populated; `password_hash` created; `is_first_login = FALSE`

**Example Data**:
```sql
INSERT INTO students (index_number, default_password, program_id, admission_year, is_first_login, account_status)
VALUES ('5241570019', 'ABC123', 1, 2026, TRUE, 'Active');

-- After student completes profile:
UPDATE students
SET full_name = 'John Mensah',
    email = 'john.mensah@uni.edu',
    phone = '+233501234567',
    password_hash = UNHEX(SHA2(CONCAT('ABC123', 'salt'), 256)),
    passport_photo_path = 'Images/Students/5241570019.jpg',
    is_first_login = FALSE,
    updated_at = NOW()
WHERE index_number = '5241570019';
```

---

#### TABLE: `staff_users`

Represents university accountants and administrators who manage the system.

```sql
CREATE TABLE staff_users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    role ENUM('Admin', 'Accountant') DEFAULT 'Admin',
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique identifier |
| `username` | VARCHAR(50) | NOT NULL, UNIQUE | Login username (e.g., "admin") |
| `password_hash` | VARCHAR(255) | NOT NULL | Hashed password (SHA-256) |
| `full_name` | VARCHAR(100) | NOT NULL | Staff member full name |
| `role` | ENUM | DEFAULT 'Admin' | "Admin" or "Accountant" |
| `is_active` | BOOLEAN | DEFAULT TRUE | Whether account is active |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Account creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Roles**:
- **Admin**: Full system access (create users, programs, fees, reports)
- **Accountant**: Financial management (student import, fee configuration, payment monitoring)

**Example Data**:
```sql
INSERT INTO staff_users (username, password_hash, full_name, role, is_active)
VALUES 
('admin', UNHEX(SHA2('admin', 256)), 'John Man', 'Admin', TRUE),
('admin2', UNHEX(SHA2('admin2', 256)), 'Peter Boogle', 'Admin', TRUE);
```

---

### SECTION 3: Financial & Transaction Tables

These tables track all student payments, receipts, and course registration eligibility.

#### TABLE: `payments`

Records every student fee payment transaction.

```sql
CREATE TABLE payments (
    id INT AUTO_INCREMENT PRIMARY KEY,
    student_id INT NOT NULL,
    fee_structure_id INT NOT NULL,
    amount_paid DECIMAL(10,2) NOT NULL,
    payment_method ENUM('Cash', 'MoMo', 'Bank', 'Manual') DEFAULT 'Manual',
    transaction_reference VARCHAR(100),
    payment_status ENUM('Pending', 'Completed', 'Failed') DEFAULT 'Completed',
    payment_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_payment_student
        FOREIGN KEY (student_id) REFERENCES students(id)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_payment_fee_structure
        FOREIGN KEY (fee_structure_id) REFERENCES fee_structures(id)
        ON UPDATE CASCADE ON DELETE RESTRICT
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique payment ID |
| `student_id` | INT | NOT NULL, FOREIGN KEY | References `students.id` |
| `fee_structure_id` | INT | NOT NULL, FOREIGN KEY | References `fee_structures.id` (annual fee record) |
| `amount_paid` | DECIMAL(10,2) | NOT NULL | Payment amount in local currency |
| `payment_method` | ENUM | DEFAULT 'Manual' | How payment was received |
| `transaction_reference` | VARCHAR(100) | NULLABLE | External transaction ID (if from gateway) |
| `payment_status` | ENUM | DEFAULT 'Completed' | "Pending", "Completed", or "Failed" |
| `payment_date` | DATETIME | DEFAULT CURRENT_TIMESTAMP | Date/time payment was processed |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Record creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Important Note**:
- All payments in MVP are **simulated** (status always "Completed")
- `fee_structure_id` links to the annual fee record (not per-semester)
- Multiple payment records may exist for one student as they pay in installments

**Example Scenario**:

Student 5241570019 enrolled in BSc IT (program_id=1), academic year 2026/2027 (academic_year_id=1)
- Annual fee (fee_structures.id=1): GH₵3,000
- Semester 1 requirement: 50% = GH₵1,500

```sql
-- Payment 1: Student pays GH₵800 in October
INSERT INTO payments (student_id, fee_structure_id, amount_paid, payment_method, payment_status, payment_date)
VALUES (1, 1, 800.00, 'Manual', 'Completed', '2026-10-05 10:30:00');

-- Payment 2: Student pays additional GH₵1,200 in November
INSERT INTO payments (student_id, fee_structure_id, amount_paid, payment_method, payment_status, payment_date)
VALUES (1, 1, 1200.00, 'Manual', 'Completed', '2026-11-10 14:15:00');

-- Total paid so far: GH₵2,000 (67% of GH₵3,000)
-- Semester 1 eligible? YES (paid GH₵2,000 > GH₵1,500 requirement)
```

---

#### TABLE: `receipts`

Generates and tracks printed receipts for each payment.

```sql
CREATE TABLE receipts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    receipt_number VARCHAR(50) NOT NULL UNIQUE,
    payment_id INT NOT NULL,
    student_name VARCHAR(150),
    index_number VARCHAR(20),
    amount DECIMAL(10,2) NOT NULL,
    receipt_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    is_printed BOOLEAN DEFAULT FALSE,
    print_count INT DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_receipt_payment
        FOREIGN KEY (payment_id) REFERENCES payments(id)
        ON UPDATE CASCADE ON DELETE CASCADE
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique receipt ID |
| `receipt_number` | VARCHAR(50) | NOT NULL, UNIQUE | Receipt number (e.g., "RCP-2026-10-001") |
| `payment_id` | INT | NOT NULL, FOREIGN KEY | References `payments.id` |
| `student_name` | VARCHAR(150) | NULLABLE | Student name (snapshot) |
| `index_number` | VARCHAR(20) | NULLABLE | Student index number (snapshot) |
| `amount` | DECIMAL(10,2) | NOT NULL | Amount paid (snapshot) |
| `receipt_date` | DATETIME | DEFAULT CURRENT_TIMESTAMP | Receipt creation date/time |
| `is_printed` | BOOLEAN | DEFAULT FALSE | Whether receipt has been printed |
| `print_count` | INT | DEFAULT 0 | Number of times receipt has been printed |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Record creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Receipt Generation Workflow**:
1. Payment submitted and approved
2. Receipt record created with unique `receipt_number`
3. Student downloads/prints receipt
4. `is_printed = TRUE`, `print_count` incremented each print

**Example Data**:
```sql
INSERT INTO receipts (receipt_number, payment_id, student_name, index_number, amount, receipt_date, is_printed, print_count)
VALUES ('RCP-2026-10-001', 1, 'John Mensah', '5241570019', 800.00, '2026-10-05 10:30:00', TRUE, 2);
```

---

#### TABLE: `course_registrations`

Records course registration attempts and eligibility determination.

```sql
CREATE TABLE course_registrations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    student_id INT NOT NULL,
    semester_id INT NOT NULL,
    academic_year_id INT NOT NULL,
    registration_status ENUM('Pending', 'Approved', 'Blocked') DEFAULT 'Pending',
    eligibility_status ENUM('Eligible', 'Not Eligible') DEFAULT 'Not Eligible',
    required_amount DECIMAL(10,2),
    amount_paid_snapshot DECIMAL(10,2),
    deadline_status ENUM('Within Deadline', 'Late', 'Closed'),
    registration_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT fk_reg_student
        FOREIGN KEY (student_id) REFERENCES students(id)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_reg_semester
        FOREIGN KEY (semester_id) REFERENCES semesters(id)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_reg_year
        FOREIGN KEY (academic_year_id) REFERENCES academic_years(id)
        ON UPDATE CASCADE ON DELETE CASCADE
);
```

**Column Details**:

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `id` | INT | PRIMARY KEY, AUTO_INCREMENT | Unique registration ID |
| `student_id` | INT | NOT NULL, FOREIGN KEY | References `students.id` |
| `semester_id` | INT | NOT NULL, FOREIGN KEY | References `semesters.id` |
| `academic_year_id` | INT | NOT NULL, FOREIGN KEY | References `academic_years.id` |
| `registration_status` | ENUM | DEFAULT 'Pending' | "Pending", "Approved", or "Blocked" |
| `eligibility_status` | ENUM | DEFAULT 'Not Eligible' | "Eligible" or "Not Eligible" (based on payment) |
| `required_amount` | DECIMAL(10,2) | NULLABLE | Required payment amount for semester |
| `amount_paid_snapshot` | DECIMAL(10,2) | NULLABLE | Amount student had paid (snapshot) |
| `deadline_status` | ENUM | NULLABLE | "Within Deadline", "Late", or "Closed" |
| `registration_date` | DATETIME | DEFAULT CURRENT_TIMESTAMP | When student attempted registration |
| `created_at` | TIMESTAMP | DEFAULT CURRENT_TIMESTAMP | Record creation timestamp |
| `updated_at` | TIMESTAMP | AUTO-UPDATE | Last modification timestamp |

**Eligibility Logic** (executed when student clicks "Register Courses"):

```
IF deadline_status == "Closed":
  → registration_status = "Blocked"
  → eligibility_status = "Eligible" (if paid) OR "Not Eligible" (if not paid)
  → Message: "Registration deadline has passed"

ELSE IF semester_number == 1:
  required_amount = fee_structure.fee_amount * 0.50  (50%)
  IF amount_paid >= required_amount:
    → eligibility_status = "Eligible"
    → registration_status = "Approved"
  ELSE:
    → eligibility_status = "Not Eligible"
    → registration_status = "Blocked"

ELSE IF semester_number == 2:
  required_amount = fee_structure.fee_amount * 1.00  (100%)
  IF amount_paid >= required_amount:
    → eligibility_status = "Eligible"
    → registration_status = "Approved"
  ELSE:
    → eligibility_status = "Not Eligible"
    → registration_status = "Blocked"
```

**Example Scenario**:

```sql
-- Semester 1 Registration (Annual fee: GH₵3,000)
-- Student paid: GH₵1,800 (60%)
-- Required for Semester 1: GH₵1,500 (50%)

INSERT INTO course_registrations 
(student_id, semester_id, academic_year_id, registration_status, eligibility_status, required_amount, amount_paid_snapshot, deadline_status, registration_date)
VALUES 
(1, 1, 1, 'Approved', 'Eligible', 1500.00, 1800.00, 'Within Deadline', NOW());

-- Student 1 is ELIGIBLE for Semester 1 (paid GH₵1,800 > GH₵1,500 requirement)
```

---

## 📊 Data Dictionary

Complete field-by-field reference for all tables.

### Currency & Monetary Values

All fee and payment amounts are stored as `DECIMAL(10,2)`:
- **Total digits**: 10
- **Decimal places**: 2
- **Range**: 0.00 to 99,999,999.99
- **Examples**: 3000.00, 1500.50, 0.99

### Timestamps

All timestamp columns use `TIMESTAMP DEFAULT CURRENT_TIMESTAMP`:
- Automatically set to current database time on row insertion
- Set to current database time on every update (with `ON UPDATE CURRENT_TIMESTAMP`)
- Timezone: Server local time (UTC recommended for production)

### Enumerations (ENUM)

Fixed-value fields using `ENUM` type:

| Table | Column | Allowed Values |
|-------|--------|-----------------|
| `semesters` | `semester_name` | "Semester 1", "Semester 2" |
| `semesters` | `semester_number` | 1, 2 |
| `programs` | `status` | "Active", "Inactive" |
| `students` | `gender` | "Male", "Female", "Other" |
| `students` | `account_status` | "Active", "Suspended", "Disabled" |
| `students` | `is_first_login` | TRUE, FALSE |
| `staff_users` | `role` | "Admin", "Accountant" |
| `payments` | `payment_method` | "Cash", "MoMo", "Bank", "Manual" |
| `payments` | `payment_status` | "Pending", "Completed", "Failed" |
| `receipts` | `is_printed` | TRUE, FALSE |
| `course_registrations` | `registration_status` | "Pending", "Approved", "Blocked" |
| `course_registrations` | `eligibility_status` | "Eligible", "Not Eligible" |
| `course_registrations` | `deadline_status` | "Within Deadline", "Late", "Closed" |

---

## 🔗 Relationships & Constraints

### Foreign Key Relationships

All foreign keys use `ON UPDATE CASCADE ON DELETE CASCADE` unless otherwise noted:

| Constraint | From Table | From Column | To Table | To Column | Action |
|-----------|-----------|------------|----------|-----------|--------|
| `fk_semester_academic_year` | `semesters` | `academic_year_id` | `academic_years` | `id` | CASCADE |
| `fk_fee_program` | `fee_structures` | `program_id` | `programs` | `id` | CASCADE |
| `fk_fee_academic_year` | `fee_structures` | `academic_year_id` | `academic_years` | `id` | CASCADE |
| `fk_students_program` | `students` | `program_id` | `programs` | `id` | SET NULL |
| `fk_payment_student` | `payments` | `student_id` | `students` | `id` | CASCADE |
| `fk_payment_fee_structure` | `payments` | `fee_structure_id` | `fee_structures` | `id` | RESTRICT |
| `fk_receipt_payment` | `receipts` | `payment_id` | `payments` | `id` | CASCADE |
| `fk_reg_student` | `course_registrations` | `student_id` | `students` | `id` | CASCADE |
| `fk_reg_semester` | `course_registrations` | `semester_id` | `semesters` | `id` | CASCADE |
| `fk_reg_year` | `course_registrations` | `academic_year_id` | `academic_years` | `id` | CASCADE |

### Unique Constraints

| Table | Constraint | Columns | Purpose |
|-------|-----------|---------|---------|
| `academic_years` | UNIQUE | `year_label` | Prevent duplicate years |
| `semesters` | (implicit) | `academic_year_id` + `semester_number` | One semester per academic year |
| `programs` | UNIQUE | `program_code` | Prevent duplicate program codes |
| `fee_structures` | UNIQUE | `program_id` + `academic_year_id` | One fee per program per year |
| `students` | UNIQUE | `index_number` | Prevent duplicate student IDs |
| `staff_users` | UNIQUE | `username` | Prevent duplicate usernames |
| `receipts` | UNIQUE | `receipt_number` | Prevent duplicate receipt numbers |

### Cascade Behavior Examples

**Example 1: Delete Academic Year**
```sql
DELETE FROM academic_years WHERE id = 1;

-- Cascades to:
-- → All semesters for year 1 deleted
-- → All fee_structures for year 1 deleted
-- → All course_registrations for year 1 deleted
-- (but NOT payments — payments RESTRICT deletion)
```

**Example 2: Delete Student**
```sql
DELETE FROM students WHERE id = 1;

-- Cascades to:
-- → All payments for student 1 deleted
-- → All receipts for those payments deleted
-- → All course_registrations for student 1 deleted
```

---

## 🚀 Indexes & Performance

### Primary Keys (Automatically Indexed)

Every table has a clustered index on `id` (primary key):

```sql
PRIMARY KEY (id)
```

### Recommended Secondary Indexes

For optimal query performance, create these indexes:

```sql
-- Students table
CREATE INDEX idx_students_index_number ON students(index_number);
CREATE INDEX idx_students_program_id ON students(program_id);
CREATE INDEX idx_students_account_status ON students(account_status);

-- Staff users table
CREATE INDEX idx_staff_users_username ON staff_users(username);

-- Payments table
CREATE INDEX idx_payments_student_id ON payments(student_id);
CREATE INDEX idx_payments_fee_structure_id ON payments(fee_structure_id);
CREATE INDEX idx_payments_payment_date ON payments(payment_date);
CREATE INDEX idx_payments_payment_status ON payments(payment_status);

-- Receipts table
CREATE INDEX idx_receipts_payment_id ON receipts(payment_id);
CREATE INDEX idx_receipts_receipt_number ON receipts(receipt_number);

-- Course registrations table
CREATE INDEX idx_course_reg_student_id ON course_registrations(student_id);
CREATE INDEX idx_course_reg_semester_id ON course_registrations(semester_id);
CREATE INDEX idx_course_reg_registration_date ON course_registrations(registration_date);

-- Fee structures table
CREATE INDEX idx_fee_structures_program_id ON fee_structures(program_id);
CREATE INDEX idx_fee_structures_academic_year_id ON fee_structures(academic_year_id);

-- Academic calendar indexes
CREATE INDEX idx_academic_years_is_active ON academic_years(is_active);
CREATE INDEX idx_semesters_academic_year_id ON semesters(academic_year_id);
CREATE INDEX idx_semesters_is_active ON semesters(is_active);
```

### Query Performance Tips

1. **Always filter by active academic year** when querying current data:
   ```sql
   SELECT * FROM academic_years WHERE is_active = TRUE LIMIT 1;
   ```

2. **Use fee_structures.id** (not program_id) when referencing student fees:
   ```sql
   -- Correct: Links to annual fee for specific academic year
   SELECT * FROM payments WHERE fee_structure_id = 1;
   
   -- Inefficient: Requires joining multiple tables
   SELECT * FROM payments p
   JOIN students s ON p.student_id = s.id
   WHERE s.program_id = 1;
   ```

3. **Snapshot amounts in course_registrations** to preserve historical eligibility calculations:
   ```sql
   -- Good: Stores exact amounts used for eligibility decision
   SELECT required_amount, amount_paid_snapshot, eligibility_status
   FROM course_registrations WHERE id = 1;
   ```

---

## 🌱 Initialization & Sample Data

### Fresh Database Setup

To initialize a clean database from scratch:

1. **Create database and tables** (from `core/DatabaseQuery.txt`):
   ```sql
   CREATE DATABASE IF NOT EXISTS efe_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
   USE efe_db;
   
   -- Run all CREATE TABLE statements from DatabaseQuery.txt
   ```

2. **Insert seed admin accounts** (from `efe_db.sql`):
   ```sql
   INSERT INTO staff_users (username, password_hash, full_name, role, is_active)
   VALUES 
   ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'John Man', 'Admin', TRUE),
   ('admin2', '2407836a97cc3e3ec73ee291534479a29fc4de4c2813583ae00f576e316a30b4', 'Peter Boogle', 'Admin', TRUE);
   ```
   
   *Note: Passwords are pre-hashed SHA-256*

3. **Create minimal academic structure**:
   ```sql
   -- Create academic year
   INSERT INTO academic_years (year_label, is_active)
   VALUES ('2026/2027', TRUE);
   
   -- Create programs
   INSERT INTO programs (program_code, program_name, faculty, status)
   VALUES 
   ('ICTE', 'BSc Information Technology', 'Engineering', 'Active'),
   ('MATH', 'BSc Mathematics', 'Science', 'Active'),
   ('ACCT', 'BSc Accounting', 'Business', 'Active'),
   ('ECON', 'BSc Economics', 'Business', 'Active');
   
   -- Create semesters
   INSERT INTO semesters (academic_year_id, semester_name, semester_number)
   VALUES 
   (1, 'Semester 1', 1),
   (1, 'Semester 2', 2);
   
   -- Create fee structures
   INSERT INTO fee_structures (program_id, academic_year_id, fee_amount, registration_deadline, is_active)
   VALUES 
   (1, 1, 3000.00, '2026-10-15', TRUE),
   (2, 1, 2500.00, '2026-10-15', TRUE),
   (3, 1, 2800.00, '2026-10-15', TRUE),
   (4, 1, 2600.00, '2026-10-15', TRUE);
   ```

4. **Import students from Excel** (via application):
   - Upload `core/ApplicantData.xlsx`
   - System creates student records with index numbers and default passwords

---

## 🔧 SQL Scripts & Deployment

### Backup Database

```bash
mysqldump -u root -p efe_db > efe_db_backup_$(date +%Y%m%d_%H%M%S).sql
```

### Restore Database

```bash
mysql -u root -p efe_db < efe_db_backup_20260915_143022.sql
```

### Reset Database (Development Only)

```bash
# Drop and recreate
mysql -u root -p -e "DROP DATABASE IF EXISTS efe_db; CREATE DATABASE efe_db CHARACTER SET utf8mb4;"

# Re-import schema
mysql -u root -p efe_db < core/DatabaseQuery.txt
```

### Check Database Integrity

```sql
-- Check all tables exist
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'efe_db';

-- Check referential integrity
SELECT CONSTRAINT_NAME, TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE TABLE_SCHEMA = 'efe_db' AND REFERENCED_TABLE_NAME IS NOT NULL;

-- Verify no orphaned records
SELECT * FROM payments p
LEFT JOIN fee_structures f ON p.fee_structure_id = f.id
WHERE f.id IS NULL;
```

---

## ✅ Best Practices

### Data Integrity

✅ **Always use parameterized queries** to prevent SQL injection
```vb
' Good: Parameterized query
Dim query = "SELECT * FROM students WHERE index_number = @index"
cmd.Parameters.AddWithValue("@index", studentIndex)

' Bad: String concatenation
Dim query = "SELECT * FROM students WHERE index_number = '" & studentIndex & "'"
```

✅ **Validate foreign key references** before inserting/updating
✅ **Use transactions** for multi-step operations (payment + receipt)
✅ **Archive old data** rather than deleting (add `is_archived` flag)

### Query Optimization

✅ **Index frequently-searched columns** (index_number, student_id, payment_status)
✅ **Use LIMIT clauses** when fetching lists
✅ **Avoid N+1 queries** — join tables instead of looping queries
✅ **Cache academic year lookups** (only one active at a time)

### Security

✅ **Hash passwords** using SHA-256 or bcrypt (never store plaintext)
✅ **Encrypt sensitive files** (student photos)
✅ **Restrict database user permissions** (application should not have DROP privileges)
✅ **Use SSL/TLS** for database connections in production

### Maintenance

✅ **Regular backups** (daily in production)
✅ **Monitor transaction logs** for anomalies
✅ **Archive receipts** after 7 years (legal compliance)
✅ **Update student photos** policy (replace old passport photos)

---

## 📚 Related Documentation

- **README.md** — System overview and setup guide
- **ARCHITECTURE.md** — System architecture and design patterns
- **efe_db.sql** — Pre-built database export with sample data
- **DatabaseQuery.txt** — Raw SQL for table creation from scratch

---

**Version**: 1.0  
**Database Engine**: MySQL 5.7 / 8.0  
**Last Updated**: September 2026

🔐 *Secure, scalable, and fully auditable university financial database.*
