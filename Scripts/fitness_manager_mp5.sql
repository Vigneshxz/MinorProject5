-- MySQL dump 10.13  Distrib 9.4.0, for macos15 (arm64)
--
-- Host: localhost    Database: fitness_manager_mp5
-- ------------------------------------------------------
-- Server version	9.4.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `fitness_manager_mp5`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `fitness_manager_mp5` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `fitness_manager_mp5`;

--
-- Table structure for table `Clients`
--

DROP TABLE IF EXISTS `Clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Clients` (
  `ClientId` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Phone` varchar(30) DEFAULT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ClientId`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Clients`
--

LOCK TABLES `Clients` WRITE;
/*!40000 ALTER TABLE `Clients` DISABLE KEYS */;
INSERT INTO `Clients` VALUES (1,'Alexia','Paige','alexia@example.com','555-0101','2026-10-14 23:19:08'),(2,'Vignesh','mc','vignesh@example.com','555-0102','2026-10-14 23:19:08'),(3,'David','Wilson','david@example.com','555-0103','2026-10-14 23:19:08'),(4,'Maria','Garcia','maria@example.com','555-0104','2026-10-14 23:19:08'),(5,'James','Brown','james@example.com','555-0105','2026-10-14 23:19:08');
/*!40000 ALTER TABLE `Clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Exercises`
--

DROP TABLE IF EXISTS `Exercises`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Exercises` (
  `ExerciseId` int NOT NULL AUTO_INCREMENT,
  `ExerciseName` varchar(150) NOT NULL,
  `MuscleGroup` varchar(100) NOT NULL,
  `Equipment` varchar(100) NOT NULL,
  `Difficulty` varchar(50) NOT NULL,
  PRIMARY KEY (`ExerciseId`),
  UNIQUE KEY `ExerciseName` (`ExerciseName`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Exercises`
--

LOCK TABLES `Exercises` WRITE;
/*!40000 ALTER TABLE `Exercises` DISABLE KEYS */;
INSERT INTO `Exercises` VALUES (1,'Barbell Squat','Legs','Barbell','Intermediate'),(3,'Deadlift','Back','Barbell','Advanced'),(4,'Lat Pulldown','Back','Cable Machine','Beginner'),(5,'Shoulder Press','Shoulders','Dumbbells','Intermediate'),(6,'Biceps Curl','Arms','Dumbbells','Beginner'),(7,'Triceps Pushdown','Arms','Cable Machine','Beginner'),(8,'Leg Press','Legs','Machine','Beginner'),(9,'Romanian Deadlift','Hamstrings','Barbell','Intermediate'),(10,'Incline Dumbbell Press','Chest','Dumbbells','Intermediate'),(11,'Cable Fly','Chest','Cable Machine','Beginner'),(12,'Pull Up','Back','Pull Up Bar','Advanced'),(13,'Lateral Raise','Shoulders','Dumbbells','Beginner'),(14,'Walking Lunge','Legs','Dumbbells','Intermediate'),(16,'Temporary Cable Row','Back','Cable Machine','Intermediate');
/*!40000 ALTER TABLE `Exercises` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Settings`
--

DROP TABLE IF EXISTS `Settings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Settings` (
  `SettingId` int NOT NULL AUTO_INCREMENT,
  `SettingKey` varchar(100) NOT NULL,
  `SettingValue` varchar(255) NOT NULL,
  PRIMARY KEY (`SettingId`),
  UNIQUE KEY `SettingKey` (`SettingKey`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Settings`
--

LOCK TABLES `Settings` WRITE;
/*!40000 ALTER TABLE `Settings` DISABLE KEYS */;
INSERT INTO `Settings` VALUES (1,'theme','light');
/*!40000 ALTER TABLE `Settings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Trainers`
--

DROP TABLE IF EXISTS `Trainers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Trainers` (
  `TrainerId` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Specialty` varchar(150) NOT NULL,
  PRIMARY KEY (`TrainerId`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Trainers`
--

LOCK TABLES `Trainers` WRITE;
/*!40000 ALTER TABLE `Trainers` DISABLE KEYS */;
INSERT INTO `Trainers` VALUES (1,'Alex','Johnson','alex.trainer@example.com','Strength Training'),(2,'Maria','Garcia','maria.trainer@example.com','Hypertrophy'),(3,'James','Lee','james.trainer@example.com','Weight Loss'),(4,'Nina','Thomas','nina.trainer@example.com','Conditioning'),(5,'David','Clark','david.trainer@example.com','Bodybuilding'),(9,'Temporary','Trainer','mp5-trainer-test-20261031@example.com','Strength Training');
/*!40000 ALTER TABLE `Trainers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TrainingPlans`
--

DROP TABLE IF EXISTS `TrainingPlans`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TrainingPlans` (
  `TrainingPlanId` int NOT NULL AUTO_INCREMENT,
  `PlanName` varchar(150) NOT NULL,
  `ClientId` int NOT NULL,
  `TrainerId` int NOT NULL,
  `StartDate` date NOT NULL,
  `DurationWeeks` int NOT NULL DEFAULT '8',
  PRIMARY KEY (`TrainingPlanId`),
  UNIQUE KEY `PlanName` (`PlanName`),
  KEY `ClientId` (`ClientId`),
  KEY `TrainerId` (`TrainerId`),
  CONSTRAINT `trainingplans_ibfk_1` FOREIGN KEY (`ClientId`) REFERENCES `Clients` (`ClientId`),
  CONSTRAINT `trainingplans_ibfk_2` FOREIGN KEY (`TrainerId`) REFERENCES `Trainers` (`TrainerId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TrainingPlans`
--

LOCK TABLES `TrainingPlans` WRITE;
/*!40000 ALTER TABLE `TrainingPlans` DISABLE KEYS */;
INSERT INTO `TrainingPlans` VALUES (1,'Beginner Strength',1,1,'2026-10-01',8),(2,'Hypertrophy Phase 1',2,2,'2026-10-05',12),(3,'Fat Loss Foundation',3,3,'2026-10-07',8),(4,'Conditioning Block',4,4,'2026-10-10',6),(5,'Bodybuilding Base',5,5,'2026-10-12',12);
/*!40000 ALTER TABLE `TrainingPlans` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-10-31 13:33:33
