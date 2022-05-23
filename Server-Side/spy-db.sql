-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 23, 2022 at 10:23 AM
-- Server version: 5.7.36
-- PHP Version: 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `spy-db`
--

-- --------------------------------------------------------

--
-- Table structure for table `wg-01`
--

DROP TABLE IF EXISTS `wg-01`;
CREATE TABLE IF NOT EXISTS `wg-01` (
  `ID` int(7) NOT NULL,
  `Word` varchar(20) CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `wg-01`
--

INSERT INTO `wg-01` (`ID`, `Word`) VALUES
(5000101, 'تره بار'),
(5000102, 'بیمارستان'),
(5000103, 'مدرسه'),
(5000104, 'دانشگاه');

-- --------------------------------------------------------

--
-- Table structure for table `wg-02`
--

DROP TABLE IF EXISTS `wg-02`;
CREATE TABLE IF NOT EXISTS `wg-02` (
  `ID` int(7) NOT NULL,
  `Word` varchar(20) CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `wg-02`
--

INSERT INTO `wg-02` (`ID`, `Word`) VALUES
(5000202, 'بیمارستان'),
(5000201, 'داروخانه');

-- --------------------------------------------------------

--
-- Table structure for table `wordsgroup-list`
--

DROP TABLE IF EXISTS `wordsgroup-list`;
CREATE TABLE IF NOT EXISTS `wordsgroup-list` (
  `name-code` varchar(5) NOT NULL,
  `name` varchar(20) CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL,
  `ID` int(5) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `wordsgroup-list`
--

INSERT INTO `wordsgroup-list` (`name-code`, `name`, `ID`) VALUES
('wg-01', 'پیش فرض', 50001),
('wg-02', 'پزشکی', 50002);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
