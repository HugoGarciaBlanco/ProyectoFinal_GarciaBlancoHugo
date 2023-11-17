-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-11-2023 a las 12:42:42
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `thelasthunter`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `game_data`
--

CREATE TABLE `game_data` (
  `playerPositionX` float NOT NULL,
  `playerPositionY` float NOT NULL,
  `playerHealth` float NOT NULL,
  `playerDamage` float NOT NULL,
  `userName` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `game_data`
--

INSERT INTO `game_data` (`playerPositionX`, `playerPositionY`, `playerHealth`, `playerDamage`, `userName`) VALUES
(960, 540, 0, 0, 'unity');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `shop_data`
--

CREATE TABLE `shop_data` (
  `itemName` varchar(25) NOT NULL,
  `itemCost` float NOT NULL,
  `userName` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `userName` varchar(15) NOT NULL,
  `email` varchar(75) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`userName`, `email`, `password`) VALUES
('', '', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855'),
('hugo', 'hugo', 'edee29f882543b956620b26d0ee0e7e950399b1c4222f5de05e06425b4c995e9'),
('mateo', 'mateo', '318aee3fed8c9d040d35a7fc1fa776fb31303833aa2de885354ddf3d44d8fb69'),
('unity', 'unity', '0ffe1abd1a08215353c233d6e009613e95eec4253832a761af28ff37ac5a150c');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `game_data`
--
ALTER TABLE `game_data`
  ADD PRIMARY KEY (`userName`),
  ADD KEY `userName` (`userName`);

--
-- Indices de la tabla `shop_data`
--
ALTER TABLE `shop_data`
  ADD PRIMARY KEY (`userName`),
  ADD KEY `userName` (`userName`);

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userName`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `game_data`
--
ALTER TABLE `game_data`
  ADD CONSTRAINT `game_data_ibfk_1` FOREIGN KEY (`userName`) REFERENCES `users` (`userName`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `shop_data`
--
ALTER TABLE `shop_data`
  ADD CONSTRAINT `shop_data_ibfk_1` FOREIGN KEY (`userName`) REFERENCES `users` (`userName`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
