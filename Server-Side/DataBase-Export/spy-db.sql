-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jun 16, 2022 at 12:32 PM
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
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `username` varchar(10) NOT NULL,
  `name` varchar(20) CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL,
  `password` varchar(100) NOT NULL,
  `gmail` varchar(70) NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`username`, `name`, `password`, `gmail`) VALUES
('SaraEft', 'Sara', '9512357', 'Sara@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `suggested-words`
--

DROP TABLE IF EXISTS `suggested-words`;
CREATE TABLE IF NOT EXISTS `suggested-words` (
  `Word` varchar(20) CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL,
  `wg-code` int(5) NOT NULL,
  `wg-name` varchar(20) CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL,
  PRIMARY KEY (`Word`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

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
(5000101, 'روسیه'),
(5000102, 'فلسطين'),
(5000103, 'ارمنستان'),
(5000104, 'دانمارک'),
(5000105, 'هلند'),
(5000106, 'کره جنوبي'),
(5000107, 'کره شمالي'),
(5000108, 'يونان'),
(5000109, 'عمان'),
(5000110, 'لهستان'),
(5000111, 'ويتنام'),
(5000112, 'فنلاند'),
(5000113, 'عراق'),
(5000114, 'سويد'),
(5000115, 'تايلند'),
(5000116, 'يمن'),
(5000117, 'افغانستان'),
(5000118, 'شيلي'),
(5000119, 'ترکيه'),
(5000120, 'پاکستان'),
(5000121, 'مغولستان'),
(5000122, 'ايران'),
(5000123, 'ليبي'),
(5000124, 'عربستان'),
(5000125, 'الجزاير'),
(5000126, 'آرژانتين'),
(5000127, 'هند'),
(5000128, 'استراليا'),
(5000129, 'برزيل'),
(5000130, 'آمريکا'),
(5000131, 'چين'),
(5000132, 'کانادا'),
(5000133, 'قبرس'),
(5000134, 'مصر');

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
(5000201, 'بیمارستان'),
(5000202, 'داروخونه'),
(5000203, 'درمانگاه'),
(5000204, 'مطب'),
(5000205, 'تيمارستان'),
(5000206, 'پزشکي قانوني');

-- --------------------------------------------------------

--
-- Table structure for table `wg-03`
--

DROP TABLE IF EXISTS `wg-03`;
CREATE TABLE IF NOT EXISTS `wg-03` (
  `ID` int(7) NOT NULL,
  `Word` varchar(20) CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `wg-04`
--

DROP TABLE IF EXISTS `wg-04`;
CREATE TABLE IF NOT EXISTS `wg-04` (
  `ID` int(7) NOT NULL,
  `Word` varchar(20) CHARACTER SET utf8 COLLATE utf8_persian_ci NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `wg-04`
--

INSERT INTO `wg-04` (`ID`, `Word`) VALUES
(5000401, 'بیمارستان'),
(5000402, 'داروخانه'),
(5000403, 'ويلا'),
(5000404, 'استخر'),
(5000405, 'استاديوم'),
(5000406, 'ورزشگاه'),
(5000407, 'پارک'),
(5000408, 'فروشگاه'),
(5000409, 'تره بار');

-- --------------------------------------------------------

--
-- Table structure for table `wordsgroup-list`
--

DROP TABLE IF EXISTS `wordsgroup-list`;
CREATE TABLE IF NOT EXISTS `wordsgroup-list` (
  `name-code` text NOT NULL,
  `name` text CHARACTER SET utf16 COLLATE utf16_persian_ci NOT NULL,
  `ID` int(5) NOT NULL,
  `Count` int(3) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `wordsgroup-list`
--

INSERT INTO `wordsgroup-list` (`name-code`, `name`, `ID`, `Count`) VALUES
('wg-01', 'کشورها', 50001, 36),
('wg-02', 'پزشکی', 50002, 6),
('wg-03', 'گیاه خواری', 50003, 0),
('wg-04', 'اماکن عمومی', 50004, 9);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
