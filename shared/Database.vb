Imports MySql.Data.MySqlClient
Imports System.Data

Public Class Database
    ' =======================================================
    ' DATABASE CONFIGURATION 
    ' Change these values here for easy development setup.
    ' =======================================================
    Private Shared ReadOnly dbServer As String = "127.0.0.1"
    Private Shared ReadOnly dbUser As String = "root"
    Private Shared ReadOnly dbPassword As String = ""
    Private Shared ReadOnly dbName As String = "efe_db"

    ' Use the builder to safely construct the string without syntax errors
    Private Shared ReadOnly Property ConnectionString As String
        Get
            Dim builder As New MySqlConnectionStringBuilder() With {
                .Server = dbServer,
                .UserID = dbUser,
                .Password = dbPassword,
                .Database = dbName,
                .Pooling = True,
                .MinimumPoolSize = 0,
                .MaximumPoolSize = 50
            }
            Return builder.ConnectionString
        End Get
    End Property
    ' =======================================================

    ''' <summary>
    ''' Instantiates and returns a brand-new, freshly opened connection object.
    ''' Wrap this inside a "Using" block in your forms to ensure it closes automatically.
    ''' </summary>
    Public Shared Function CreateOpenConnection() As MySqlConnection
        Dim conn As New MySqlConnection(ConnectionString)
        Try
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
            Return conn
        Catch ex As MySqlException
            MessageBox.Show($"Database Connection Failure: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Throw
        End Try
    End Function



















    '    --======================================================================
    '-- 1. CORE CONFIGURATION TABLES (ACADEMIC CALENDAR & LOGISTICS)
    '--======================================================================

    'CREATE TABLE If Not EXISTS academic_years (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    year_label VARCHAR(20) Not NULL UNIQUE,
    '    is_active BOOLEAN DEFAULT FALSE,
    '    start_date DATE,
    '    end_date DATE,
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP
    ');

    'CREATE TABLE If Not EXISTS semesters (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    academic_year_id INT Not NULL,
    '    semester_name Enum('Semester 1', 'Semester 2') NOT NULL,
    '    semester_number INT Not NULL,
    '    start_date Date,
    '    end_date Date,
    '    is_active Boolean Default False,
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP,
    '    CONSTRAINT fk_semester_academic_year
    '        FOREIGN KEY (academic_year_id) REFERENCES academic_years(id)
    '        On UPDATE CASCADE ON DELETE CASCADE
    ');

    'CREATE TABLE If Not EXISTS programs (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    program_code VARCHAR(20) UNIQUE,
    '    program_name VARCHAR(150) Not NULL,
    '    faculty VARCHAR(100),
    '    level_duration_years INT Default 4,
    '    base_fee DECIMAL(10,2),
    '    status Enum('Active','Inactive') DEFAULT 'Active',
    '    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP
    ');

    '-- REFACTORED: Strict Annual Target Billing Architecture
    'CREATE TABLE If Not EXISTS fee_structures (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    program_id INT Not NULL,
    '    academic_year_id INT Not NULL,
    '    fee_amount DECIMAL(10,2) Not NULL,
    '    min_payment_percentage DECIMAL(5,2) DEFAULT 100.00,
    '    registration_deadline DATE,
    '    is_active BOOLEAN DEFAULT TRUE,
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP,
    '    CONSTRAINT fk_fee_program
    '        FOREIGN KEY(program_id) REFERENCES programs(id)
    '        On UPDATE CASCADE ON DELETE CASCADE,
    '    CONSTRAINT fk_fee_academic_year
    '        FOREIGN KEY(academic_year_id) REFERENCES academic_years(id)
    '        On UPDATE CASCADE ON DELETE CASCADE,
    '    -- Enforces that a program can only have ONE target fee per academic year
    '    UNIQUE KEY unique_annual_fee (program_id, academic_year_id)
    ');

    '--======================================================================
    '-- 2. IDENTITY And USER MANAGEMENT TABLES (STUDENTS & MULTI-ADMIN)
    '--======================================================================

    'CREATE TABLE If Not EXISTS students (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    index_number VARCHAR(20) Not NULL UNIQUE,
    '    default_password VARCHAR(255) Not NULL,
    '    password_hash VARCHAR(255),
    '    full_name VARCHAR(150),
    '    gender Enum('Male','Female','Other'),
    '    phone VARCHAR(20),
    '    email VARCHAR(100),
    '    passport_photo_path TEXT,
    '    program_id INT,
    '    admission_year YEAR,
    '    is_first_login Boolean Default True,
    '    account_status Enum('Active','Suspended','Disabled') DEFAULT 'Active',
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP,
    '    CONSTRAINT fk_students_program
    '        FOREIGN KEY (program_id) REFERENCES programs(id)
    '        On UPDATE CASCADE ON DELETE SET NULL
    ');

    'CREATE TABLE If Not EXISTS staff_users (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    username VARCHAR(50) Not NULL UNIQUE,
    '    password_hash VARCHAR(255) Not NULL,
    '    full_name VARCHAR(100) Not NULL,
    '    role Enum('Admin', 'Accountant') DEFAULT 'Admin',
    '    is_active BOOLEAN DEFAULT TRUE,
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP
    ');

    '--======================================================================
    '-- 3. FINANCIAL TRANSACTIONS & ACADEMIC ELIGIBILITY RECORDING
    '--======================================================================

    'CREATE TABLE If Not EXISTS payments (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    student_id INT Not NULL,
    '    fee_structure_id INT Not NULL, -- Now correctly links To an Annual Target row
    '    amount_paid DECIMAL(10,2) Not NULL,
    '    payment_method Enum('Cash', 'MoMo', 'Bank', 'Manual') DEFAULT 'Manual',
    '    transaction_reference VARCHAR(100),
    '    payment_status Enum('Pending', 'Completed', 'Failed') DEFAULT 'Completed',
    '    payment_date DATETIME Default CURRENT_TIMESTAMP,
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP,
    '    CONSTRAINT fk_payment_student
    '        FOREIGN KEY (student_id) REFERENCES students(id)
    '        On UPDATE CASCADE ON DELETE CASCADE,
    '    CONSTRAINT fk_payment_fee_structure
    '        FOREIGN KEY (fee_structure_id) REFERENCES fee_structures(id)
    '        On UPDATE CASCADE ON DELETE RESTRICT
    ');

    'CREATE TABLE If Not EXISTS receipts (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    receipt_number VARCHAR(50) Not NULL UNIQUE,
    '    payment_id INT Not NULL,
    '    student_name VARCHAR(150),
    '    index_number VARCHAR(20),
    '    amount DECIMAL(10,2) Not NULL,
    '    receipt_date DATETIME Default CURRENT_TIMESTAMP,
    '    is_printed BOOLEAN DEFAULT FALSE,
    '    print_count INT Default 0,
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP,
    '    CONSTRAINT fk_receipt_payment
    '        FOREIGN KEY(payment_id) REFERENCES payments(id)
    '        On UPDATE CASCADE ON DELETE CASCADE
    ');

    'CREATE TABLE If Not EXISTS course_registrations (
    '    id INT AUTO_INCREMENT PRIMARY KEY,
    '    student_id INT Not NULL,
    '    semester_id INT Not NULL,
    '    academic_year_id INT Not NULL,
    '    registration_status Enum('Pending', 'Approved', 'Blocked') DEFAULT 'Pending',
    '    eligibility_status ENUM('Eligible', 'Not Eligible') DEFAULT 'Not Eligible',
    '    required_amount Decimal(10,2),
    '    amount_paid_snapshot Decimal(10,2),
    '    deadline_status Enum('Within Deadline', 'Late', 'Closed'),
    '    registration_date DATETIME Default CURRENT_TIMESTAMP,
    '    created_at TIMESTAMP Default CURRENT_TIMESTAMP,
    '    updated_at TIMESTAMP Default CURRENT_TIMESTAMP On UPDATE CURRENT_TIMESTAMP,
    '    CONSTRAINT fk_reg_student
    '        FOREIGN KEY (student_id) REFERENCES students(id)
    '        On UPDATE CASCADE ON DELETE CASCADE,
    '    CONSTRAINT fk_reg_semester
    '        FOREIGN KEY (semester_id) REFERENCES semesters(id)
    '        On UPDATE CASCADE ON DELETE CASCADE,
    '    CONSTRAINT fk_reg_year
    '        FOREIGN KEY (academic_year_id) REFERENCES academic_years(id)
    '        On UPDATE CASCADE ON DELETE CASCADE
    ');

    '--======================================================================
    '-- 4. SEED DEMO ADMINISTRATOR ACCOUNTS
    '--======================================================================

    'INSERT INTO staff_users (username, password_hash, full_name, role)
    'VALUES 
    '('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'John Man', 'Admin'),
    '('admin2', '2407836a97cc3e3ec73ee291534479a29fc4de4c2813583ae00f576e316a30b4', 'Peter Boogle', 'Admin');









End Class