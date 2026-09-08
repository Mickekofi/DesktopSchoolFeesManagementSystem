-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 08, 2026 at 03:09 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `efe_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `academic_years`
--

CREATE TABLE `academic_years` (
  `id` int(11) NOT NULL,
  `year_label` varchar(20) NOT NULL,
  `is_active` tinyint(1) DEFAULT 0,
  `start_date` date DEFAULT NULL,
  `end_date` date DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `academic_years`
--

INSERT INTO `academic_years` (`id`, `year_label`, `is_active`, `start_date`, `end_date`, `created_at`, `updated_at`) VALUES
(1, '2026/2027', 1, NULL, NULL, '2026-07-19 21:06:38', '2026-09-08 00:40:23'),
(2, '2027/2028', 0, NULL, NULL, '2026-07-19 21:51:22', '2026-07-20 18:50:43'),
(3, '2025/2026', 0, NULL, NULL, '2026-07-20 18:50:43', '2026-07-28 18:52:48');

-- --------------------------------------------------------

--
-- Table structure for table `course_registrations`
--

CREATE TABLE `course_registrations` (
  `id` int(11) NOT NULL,
  `student_id` int(11) NOT NULL,
  `semester_id` int(11) NOT NULL,
  `academic_year_id` int(11) NOT NULL,
  `registration_status` enum('Pending','Approved','Blocked') DEFAULT 'Pending',
  `eligibility_status` enum('Eligible','Not Eligible') DEFAULT 'Not Eligible',
  `required_amount` decimal(10,2) DEFAULT NULL,
  `amount_paid_snapshot` decimal(10,2) DEFAULT NULL,
  `deadline_status` enum('Within Deadline','Late','Closed') DEFAULT NULL,
  `registration_date` datetime DEFAULT current_timestamp(),
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `course_registrations`
--

INSERT INTO `course_registrations` (`id`, `student_id`, `semester_id`, `academic_year_id`, `registration_status`, `eligibility_status`, `required_amount`, `amount_paid_snapshot`, `deadline_status`, `registration_date`, `created_at`, `updated_at`) VALUES
(5, 4, 4, 3, 'Approved', 'Not Eligible', NULL, NULL, NULL, '2026-07-20 23:25:26', '2026-07-20 23:25:26', '2026-07-20 23:25:26'),
(6, 4, 1, 1, 'Approved', 'Not Eligible', NULL, NULL, NULL, '2026-07-28 18:56:31', '2026-07-28 18:56:31', '2026-07-28 18:56:31'),
(7, 25, 1, 1, 'Approved', 'Not Eligible', NULL, NULL, NULL, '2026-09-08 00:58:47', '2026-09-08 00:58:47', '2026-09-08 00:58:47');

-- --------------------------------------------------------

--
-- Table structure for table `fee_structures`
--

CREATE TABLE `fee_structures` (
  `id` int(11) NOT NULL,
  `program_id` int(11) NOT NULL,
  `academic_year_id` int(11) NOT NULL,
  `fee_amount` decimal(10,2) NOT NULL,
  `min_payment_percentage` decimal(5,2) DEFAULT 100.00,
  `registration_deadline` date DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT 1,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `fee_structures`
--

INSERT INTO `fee_structures` (`id`, `program_id`, `academic_year_id`, `fee_amount`, `min_payment_percentage`, `registration_deadline`, `is_active`, `created_at`, `updated_at`) VALUES
(1, 1, 1, 5000.00, 50.00, NULL, 1, '2026-07-19 21:07:01', '2026-07-19 21:07:01'),
(2, 1, 2, 5000.00, 50.00, NULL, 1, '2026-07-19 21:51:34', '2026-07-19 21:51:34'),
(3, 1, 3, 6000.00, 50.00, NULL, 1, '2026-07-20 18:54:01', '2026-07-20 18:54:01'),
(4, 2, 3, 4000.00, 50.00, NULL, 1, '2026-07-20 18:54:01', '2026-07-20 18:54:01'),
(5, 3, 3, 3000.00, 50.00, NULL, 1, '2026-07-20 18:54:01', '2026-07-20 18:54:01'),
(6, 4, 3, 3500.00, 50.00, NULL, 1, '2026-07-20 18:54:01', '2026-07-20 18:54:01'),
(7, 2, 1, 4000.00, 50.00, NULL, 1, '2026-07-28 18:53:22', '2026-07-28 18:53:22'),
(8, 3, 1, 3000.00, 50.00, NULL, 1, '2026-07-28 18:53:22', '2026-07-28 18:53:22'),
(9, 4, 1, 3500.00, 50.00, NULL, 1, '2026-07-28 18:53:22', '2026-07-28 18:53:22');

-- --------------------------------------------------------

--
-- Table structure for table `payments`
--

CREATE TABLE `payments` (
  `id` int(11) NOT NULL,
  `student_id` int(11) NOT NULL,
  `fee_structure_id` int(11) NOT NULL,
  `amount_paid` decimal(10,2) NOT NULL,
  `payment_method` enum('Cash','MoMo','Bank','Manual') DEFAULT 'Manual',
  `transaction_reference` varchar(100) DEFAULT NULL,
  `payment_status` enum('Pending','Completed','Failed') DEFAULT 'Completed',
  `payment_date` datetime DEFAULT current_timestamp(),
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `payments`
--

INSERT INTO `payments` (`id`, `student_id`, `fee_structure_id`, `amount_paid`, `payment_method`, `transaction_reference`, `payment_status`, `payment_date`, `created_at`, `updated_at`) VALUES
(9, 4, 3, 3000.00, 'MoMo', 'TXN20260720232315860', 'Completed', '2026-07-20 23:23:15', '2026-07-20 23:23:15', '2026-07-20 23:23:15'),
(10, 4, 3, 1000.00, 'MoMo', 'TXN20260720232348500', 'Completed', '2026-07-20 23:23:48', '2026-07-20 23:23:48', '2026-07-20 23:23:48'),
(11, 4, 3, 500.00, 'MoMo', 'TXN20260720234007764', 'Completed', '2026-07-20 23:40:07', '2026-07-20 23:40:07', '2026-07-20 23:40:07'),
(12, 4, 3, 300.00, 'MoMo', 'TXN20260721000829590', 'Completed', '2026-07-21 00:08:29', '2026-07-21 00:08:29', '2026-07-21 00:08:29'),
(13, 4, 3, 222.00, 'MoMo', 'TXN20260721001936308', 'Completed', '2026-07-21 00:19:36', '2026-07-21 00:19:36', '2026-07-21 00:19:36'),
(14, 4, 1, 2500.00, 'MoMo', 'TXN20260728185457231', 'Completed', '2026-07-28 18:54:57', '2026-07-28 18:54:57', '2026-07-28 18:54:57'),
(15, 4, 1, 200.00, 'MoMo', 'TXN20260728185900594', 'Completed', '2026-07-28 18:59:00', '2026-07-28 18:59:00', '2026-07-28 18:59:00'),
(16, 25, 1, 2000.00, 'MoMo', 'TXN20260908004517600', 'Completed', '2026-09-08 00:45:17', '2026-09-08 00:45:17', '2026-09-08 00:45:17'),
(17, 25, 1, 1500.00, 'MoMo', 'TXN20260908005645476', 'Completed', '2026-09-08 00:56:45', '2026-09-08 00:56:45', '2026-09-08 00:56:45');

-- --------------------------------------------------------

--
-- Table structure for table `programs`
--

CREATE TABLE `programs` (
  `id` int(11) NOT NULL,
  `program_code` varchar(20) DEFAULT NULL,
  `program_name` varchar(150) NOT NULL,
  `faculty` varchar(100) DEFAULT NULL,
  `level_duration_years` int(11) DEFAULT 4,
  `base_fee` decimal(10,2) DEFAULT NULL,
  `status` enum('Active','Inactive') DEFAULT 'Active',
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `programs`
--

INSERT INTO `programs` (`id`, `program_code`, `program_name`, `faculty`, `level_duration_years`, `base_fee`, `status`, `created_at`, `updated_at`) VALUES
(1, NULL, 'Bsc. ICT', NULL, 4, 4500.00, 'Active', '2026-07-19 21:06:02', '2026-09-08 00:08:46'),
(2, NULL, 'Bsc. Mathematics', NULL, 4, 4000.00, 'Active', '2026-07-20 18:39:17', '2026-07-20 18:39:17'),
(3, NULL, 'Bsc. Physics', NULL, 4, 3000.00, 'Active', '2026-07-20 18:39:52', '2026-07-20 18:39:52'),
(4, NULL, 'Bsc. Biology', NULL, 4, 3500.00, 'Active', '2026-07-20 18:40:12', '2026-07-20 18:40:12');

-- --------------------------------------------------------

--
-- Table structure for table `receipts`
--

CREATE TABLE `receipts` (
  `id` int(11) NOT NULL,
  `receipt_number` varchar(50) NOT NULL,
  `payment_id` int(11) NOT NULL,
  `student_name` varchar(150) DEFAULT NULL,
  `index_number` varchar(20) DEFAULT NULL,
  `amount` decimal(10,2) NOT NULL,
  `receipt_date` datetime DEFAULT current_timestamp(),
  `is_printed` tinyint(1) DEFAULT 0,
  `print_count` int(11) DEFAULT 0,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `receipts`
--

INSERT INTO `receipts` (`id`, `receipt_number`, `payment_id`, `student_name`, `index_number`, `amount`, `receipt_date`, `is_printed`, `print_count`, `created_at`, `updated_at`) VALUES
(1, 'REC20260720234007', 11, 'John', '5241570012', 500.00, '2026-07-20 23:40:07', 0, 0, '2026-07-20 23:40:07', '2026-07-20 23:40:07'),
(2, 'REC20260721000829', 12, 'John', '5241570012', 300.00, '2026-07-21 00:08:29', 0, 0, '2026-07-21 00:08:29', '2026-07-21 00:08:29'),
(3, 'REC20260721001936', 13, 'John', '5241570012', 222.00, '2026-07-21 00:19:36', 0, 0, '2026-07-21 00:19:36', '2026-07-21 00:19:36'),
(4, 'REC20260728185457', 14, 'John', '5241570012', 2500.00, '2026-07-28 18:54:57', 0, 0, '2026-07-28 18:54:57', '2026-07-28 18:54:57'),
(5, 'REC20260728185900', 15, 'John', '5241570012', 200.00, '2026-07-28 18:59:00', 0, 0, '2026-07-28 18:59:00', '2026-07-28 18:59:00'),
(6, 'REC20260908004517', 16, 'Michael Appiah', '5241570019', 2000.00, '2026-09-08 00:45:17', 0, 0, '2026-09-08 00:45:17', '2026-09-08 00:45:17'),
(7, 'REC20260908005645', 17, 'Michael Appiah', '5241570019', 1500.00, '2026-09-08 00:56:45', 0, 0, '2026-09-08 00:56:45', '2026-09-08 00:56:45');

-- --------------------------------------------------------

--
-- Table structure for table `semesters`
--

CREATE TABLE `semesters` (
  `id` int(11) NOT NULL,
  `academic_year_id` int(11) NOT NULL,
  `semester_name` enum('Semester 1','Semester 2') NOT NULL,
  `semester_number` int(11) NOT NULL,
  `start_date` date DEFAULT NULL,
  `end_date` date DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT 0,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `semesters`
--

INSERT INTO `semesters` (`id`, `academic_year_id`, `semester_name`, `semester_number`, `start_date`, `end_date`, `is_active`, `created_at`, `updated_at`) VALUES
(1, 1, 'Semester 1', 1, NULL, NULL, 1, '2026-07-19 21:06:38', '2026-09-08 00:40:23'),
(2, 1, 'Semester 2', 2, NULL, NULL, 0, '2026-07-19 21:09:55', '2026-09-08 00:40:23'),
(3, 2, 'Semester 1', 1, NULL, NULL, 0, '2026-07-19 21:51:22', '2026-07-20 18:50:43'),
(4, 3, 'Semester 1', 1, NULL, NULL, 0, '2026-07-20 18:50:43', '2026-07-20 23:26:45'),
(5, 3, 'Semester 2', 2, NULL, NULL, 0, '2026-07-20 19:43:32', '2026-07-28 18:52:48');

-- --------------------------------------------------------

--
-- Table structure for table `staff_users`
--

CREATE TABLE `staff_users` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `role` enum('Admin','Accountant') DEFAULT 'Admin',
  `is_active` tinyint(1) DEFAULT 1,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `staff_users`
--

INSERT INTO `staff_users` (`id`, `username`, `password_hash`, `full_name`, `role`, `is_active`, `created_at`, `updated_at`) VALUES
(1, 'admin1', '2407836a97cc3e3ec73ee291534479a29fc4de4c2813583ae00f576e316a30b4', 'John Man', 'Admin', 1, '2026-07-19 20:47:09', '2026-07-19 20:47:09'),
(2, 'admin2', '2407836a97cc3e3ec73ee291534479a29fc4de4c2813583ae00f576e316a30b4', 'Peter Boogle', 'Admin', 1, '2026-07-19 20:47:09', '2026-07-19 20:47:09'),
(3, 'admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'John Man', 'Admin', 1, '2026-07-19 21:04:52', '2026-07-19 21:04:52');

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE `students` (
  `id` int(11) NOT NULL,
  `index_number` varchar(20) NOT NULL,
  `default_password` varchar(255) NOT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `full_name` varchar(150) DEFAULT NULL,
  `gender` enum('Male','Female','Other') DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `passport_photo_path` text DEFAULT NULL,
  `program_id` int(11) DEFAULT NULL,
  `admission_year` year(4) DEFAULT NULL,
  `is_first_login` tinyint(1) DEFAULT 1,
  `account_status` enum('Active','Suspended','Disabled') DEFAULT 'Active',
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `updated_at` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`id`, `index_number`, `default_password`, `password_hash`, `full_name`, `gender`, `phone`, `email`, `passport_photo_path`, `program_id`, `admission_year`, `is_first_login`, `account_status`, `created_at`, `updated_at`) VALUES
(4, '5241570012', '12345', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'John', 'Male', '0241770556', 'j@gmail.com', 'C:\\Users\\Michael Ubuntu\\Downloads\\School Fees Management System\\bin\\Debug\\net8.0-windows\\Passports\\5241570012_passport.jpg', 1, '2025', 0, 'Active', '2026-07-20 19:18:38', '2026-07-20 23:22:52'),
(5, '5251570017', '12345', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Simms', 'Male', '0241770556', 's@gmail..com', 'C:\\Users\\Michael Ubuntu\\Downloads\\School Fees Management System\\bin\\Debug\\net8.0-windows\\Passports\\5251570017_passport.jpg', 3, '2025', 0, 'Active', '2026-07-20 19:18:38', '2026-07-20 19:48:54'),
(6, '5251570035', '12345', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Shanel', NULL, NULL, NULL, NULL, 4, '2025', 1, 'Active', '2026-07-20 19:18:38', '2026-07-20 19:18:38'),
(7, '5251570011', '12345', '5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 'Bob Marley', NULL, NULL, NULL, NULL, 2, '2025', 1, 'Active', '2026-07-20 19:18:38', '2026-07-20 19:18:38'),
(24, '5241570000', '123', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'No Face', NULL, NULL, NULL, NULL, 2, '2025', 1, 'Active', '2026-09-08 00:13:45', '2026-09-08 00:13:45'),
(25, '5241570019', '123456', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Michael Appiah', 'Male', '0597326320', 'intelligent.edu.gh@gmail.com', 'C:\\SchoolFeesManagementSystem\\bin\\Debug\\net8.0-windows\\Passports\\5241570019_passport.jpg', 1, '2025', 0, 'Active', '2026-09-08 00:31:02', '2026-09-08 00:38:23');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `academic_years`
--
ALTER TABLE `academic_years`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `year_label` (`year_label`);

--
-- Indexes for table `course_registrations`
--
ALTER TABLE `course_registrations`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_reg_student` (`student_id`),
  ADD KEY `fk_reg_semester` (`semester_id`),
  ADD KEY `fk_reg_year` (`academic_year_id`);

--
-- Indexes for table `fee_structures`
--
ALTER TABLE `fee_structures`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `unique_annual_fee` (`program_id`,`academic_year_id`),
  ADD KEY `fk_fee_academic_year` (`academic_year_id`);

--
-- Indexes for table `payments`
--
ALTER TABLE `payments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_payment_student` (`student_id`),
  ADD KEY `fk_payment_fee_structure` (`fee_structure_id`);

--
-- Indexes for table `programs`
--
ALTER TABLE `programs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `program_code` (`program_code`);

--
-- Indexes for table `receipts`
--
ALTER TABLE `receipts`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `receipt_number` (`receipt_number`),
  ADD KEY `fk_receipt_payment` (`payment_id`);

--
-- Indexes for table `semesters`
--
ALTER TABLE `semesters`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_semester_academic_year` (`academic_year_id`);

--
-- Indexes for table `staff_users`
--
ALTER TABLE `staff_users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `index_number` (`index_number`),
  ADD KEY `fk_students_program` (`program_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `academic_years`
--
ALTER TABLE `academic_years`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `course_registrations`
--
ALTER TABLE `course_registrations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `fee_structures`
--
ALTER TABLE `fee_structures`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `payments`
--
ALTER TABLE `payments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `programs`
--
ALTER TABLE `programs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `receipts`
--
ALTER TABLE `receipts`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `semesters`
--
ALTER TABLE `semesters`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `staff_users`
--
ALTER TABLE `staff_users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `students`
--
ALTER TABLE `students`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `course_registrations`
--
ALTER TABLE `course_registrations`
  ADD CONSTRAINT `fk_reg_semester` FOREIGN KEY (`semester_id`) REFERENCES `semesters` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_reg_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_reg_year` FOREIGN KEY (`academic_year_id`) REFERENCES `academic_years` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `fee_structures`
--
ALTER TABLE `fee_structures`
  ADD CONSTRAINT `fk_fee_academic_year` FOREIGN KEY (`academic_year_id`) REFERENCES `academic_years` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_fee_program` FOREIGN KEY (`program_id`) REFERENCES `programs` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `payments`
--
ALTER TABLE `payments`
  ADD CONSTRAINT `fk_payment_fee_structure` FOREIGN KEY (`fee_structure_id`) REFERENCES `fee_structures` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_payment_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `receipts`
--
ALTER TABLE `receipts`
  ADD CONSTRAINT `fk_receipt_payment` FOREIGN KEY (`payment_id`) REFERENCES `payments` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `semesters`
--
ALTER TABLE `semesters`
  ADD CONSTRAINT `fk_semester_academic_year` FOREIGN KEY (`academic_year_id`) REFERENCES `academic_years` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `students`
--
ALTER TABLE `students`
  ADD CONSTRAINT `fk_students_program` FOREIGN KEY (`program_id`) REFERENCES `programs` (`id`) ON DELETE SET NULL ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
