-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jul 07, 2023 at 10:32 PM
-- Server version: 5.7.24
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `unitypub`
--

-- --------------------------------------------------------

--
-- Table structure for table `aime`
--

CREATE DATABASE unitypub;
USE unitypub;

CREATE TABLE `aime` (
  `id` int(11) NOT NULL,
  `userId` int(11) NOT NULL,
  `themeId` int(11) NOT NULL,
  `aime` tinyint(1) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `badge`
--

CREATE TABLE `badge` (
  `id` int(11) NOT NULL,
  `name` varchar(25) NOT NULL,
  `prix` int(11) NOT NULL,
  `titre` varchar(255) NOT NULL,
  `description` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `badge`
--

INSERT INTO `badge` (`id`, `name`, `prix`, `titre`, `description`) VALUES
(1, 'badge_1er_pub', 1, '1ére pub', 'Obtenu après avoir regarder votre première publicité'),
(3, 'badge_5eme_pub', 5, '5éme pub', 'Obtenu après avoir regarder votre cinquième publicité'),
(4, 'badge_10eme_pub', 10, '10éme pub', 'Obtenu après avoir regarder votre dixième publicité'),
(5, 'badge_finir_course', 1, 'Finir 1 course', 'Obtenu après avoir fini votre première course'),
(6, 'badge_like', 1, 'Liker un thème', 'Obtenu après avoir liker votre premier thème'),
(7, 'badge_premium', 1, 'Passer premium', 'Obtenu après avoir acheter le premium');

-- --------------------------------------------------------

--
-- Table structure for table `badgeusr`
--

CREATE TABLE `badgeusr` (
  `id` int(11) NOT NULL,
  `userID` int(11) NOT NULL,
  `badgeID` int(11) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `pub`
--

CREATE TABLE `pub` (
  `id` int(11) NOT NULL,
  `themeID` int(11) NOT NULL,
  `lien` varchar(50) NOT NULL,
  `websiteLink` varchar(255) NOT NULL,
  `isBanniere` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `pub`
--

INSERT INTO `pub` (`id`, `themeID`, `lien`, `websiteLink`, `isBanniere`) VALUES
(1, 3, 'astroes_race_pub_autre', '0', 1),
(2, 1, 'ESGI_Banierre', '0', 1),
(3, 2, 'pub_bonbon', '0', 0),
(4, 4, 'Premium_abo', '0', 1),
(5, 1, 'pub_informatique', '0', 0),
(6, 2, 'pub_glace', '0', 0),
(7, 2, 'pub_tea', '0', 0),
(8, 2, 'pub_resto', '0', 0),
(9, 1, 'pub_reparation', '0', 0),
(10, 1, 'pub_webdesign', '0', 0),
(11, 3, 'pub_sport', '0', 0),
(12, 3, 'pub_game_enquete', '0', 0),
(13, 3, 'pub_game_space', '0', 0);

-- --------------------------------------------------------

--
-- Table structure for table `theme`
--

CREATE TABLE `theme` (
  `id` int(11) NOT NULL,
  `nom` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `theme`
--

INSERT INTO `theme` (`id`, `nom`) VALUES
(1, 'informatique'),
(2, 'nourriture'),
(3, 'jeux'),
(4, 'premium');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(25) NOT NULL,
  `password` char(60) NOT NULL,
  `noPub` tinyint(1) DEFAULT '0',
  `nbPub` int(11) DEFAULT '0',
  `token` char(32) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `noPub`, `nbPub`, `token`) VALUES
(1, 'usr1', 'mdp', 0, 0, NULL),
(2, 'usr2', 'response', 1, 0, NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `aime`
--
ALTER TABLE `aime`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `badge`
--
ALTER TABLE `badge`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `badgeusr`
--
ALTER TABLE `badgeusr`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pub`
--
ALTER TABLE `pub`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `theme`
--
ALTER TABLE `theme`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `aime`
--
ALTER TABLE `aime`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `badge`
--
ALTER TABLE `badge`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `badgeusr`
--
ALTER TABLE `badgeusr`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `pub`
--
ALTER TABLE `pub`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `theme`
--
ALTER TABLE `theme`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
