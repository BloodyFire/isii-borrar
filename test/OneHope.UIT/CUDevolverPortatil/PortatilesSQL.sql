
SET IDENTITY_INSERT [dbo].[Procesadores] ON
INSERT INTO [dbo].[Procesadores] ([Id], [ModeloProcesador]) VALUES (5, N'Intel 80486')
INSERT INTO [dbo].[Procesadores] ([Id], [ModeloProcesador]) VALUES (2, N'Intel I5 14500')
INSERT INTO [dbo].[Procesadores] ([Id], [ModeloProcesador]) VALUES (1, N'Intel I7 13700')
INSERT INTO [dbo].[Procesadores] ([Id], [ModeloProcesador]) VALUES (6, N'Pentium 4')
INSERT INTO [dbo].[Procesadores] ([Id], [ModeloProcesador]) VALUES (3, N'Ryzen 3  ')
INSERT INTO [dbo].[Procesadores] ([Id], [ModeloProcesador]) VALUES (4, N'Ryzen 5')
INSERT INTO [dbo].[Procesadores] ([Id], [ModeloProcesador]) VALUES (7, N'Snapdragon')
SET IDENTITY_INSERT [dbo].[Procesadores] OFF

SET IDENTITY_INSERT [dbo].[Rams] ON
INSERT INTO [dbo].[Rams] ([Id], [Capacidad]) VALUES (1, N'8Gb')
INSERT INTO [dbo].[Rams] ([Id], [Capacidad]) VALUES (2, N'4Gb')
INSERT INTO [dbo].[Rams] ([Id], [Capacidad]) VALUES (3, N'16Gb')
INSERT INTO [dbo].[Rams] ([Id], [Capacidad]) VALUES (4, N'32Gb')
INSERT INTO [dbo].[Rams] ([Id], [Capacidad]) VALUES (5, N'128Mb')
SET IDENTITY_INSERT [dbo].[Rams] OFF

SET IDENTITY_INSERT [dbo].[Marcas] ON
INSERT INTO [dbo].[Marcas] ([Id], [NombreMarca]) VALUES (1, N'HP')
INSERT INTO [dbo].[Marcas] ([Id], [NombreMarca]) VALUES (2, N'DELL')
INSERT INTO [dbo].[Marcas] ([Id], [NombreMarca]) VALUES (3, N'ASUS')
INSERT INTO [dbo].[Marcas] ([Id], [NombreMarca]) VALUES (4, N'TOASTER')
SET IDENTITY_INSERT [dbo].[Marcas] OFF

SET IDENTITY_INSERT [dbo].[Proveedores] ON
INSERT INTO [dbo].[Proveedores] ([Id], [Nombre], [CIF], [Direccion], [Email], [Telefono]) VALUES (1, N'Proveedores S.L.', N'44444444A', N'Calle providia numero 75', N'proveemos@proveedores.com', N'600000000')
INSERT INTO [dbo].[Proveedores] ([Id], [Nombre], [CIF], [Direccion], [Email], [Telefono]) VALUES (2, N'Portatiles Mayorista ', N'12345678T', N'Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000', N'pormay@yahoorespuestas.com', N'999555666')
INSERT INTO [dbo].[Proveedores] ([Id], [Nombre], [CIF], [Direccion], [Email], [Telefono]) VALUES (4, N'Empresaurio genérico S.A.', N'RA000000WR', N'Wall street (detras de donde venden churros)', N'empresaurio@rawr.com', N'+34 600 700 800')
SET IDENTITY_INSERT [dbo].[Proveedores] OFF

SET IDENTITY_INSERT [dbo].[Portatiles] ON
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES 
	(1, N'HP-1151', 199.95, 1, 6.66, 1, N'HP 486 del pleistoceno', 100, 5, 13, 5, 1),
	(2, N'DELL-2222', 499.95, 2, 1/1, 2, N'DELL I5 para ofimatica', 250, 2, 29, 7, 2),
	(3, N'ASUS-3314', 2299.95, 3, 76.66, 3, N'ASUS PRO STATION 3000', 1150, 1, 16, 5, 4),
	(4, N'TOASTER-4461', 1699.95, 4, 56.66, 4, N'Pentium 4 fiable de toda la vida', 850, 6, 5, 2, 1),
	(5, N'HP-5132', 1999.95, 5, 66.66, 1, N'HP Quantum singstar edition plus', 1000, 3, 9, 9, 2),
	(6, N'DELL-1244', 1999.95, 1, 66.66, 2, N'DELL R5 gama alta', 1000, 4, 18, 1, 4),
	(7, N'ASUS-2371', 599.95, 2, 20, 3, N'ASUS workstation con procesador de movil', 300, 7, 24, 7, 1),
	(8, N'TOASTER-3452', 2099.95, 3, 70, 4, N'Tostadora de otra era.', 1050, 5, 23, 3, 2),
	(9, N'HP-4124', 1099.95, 4, 36.66, 1, N'Intel HP pro ultra con lucecitas.', 550, 2, 6, 9, 4),
	(10, N'DELL-5211', 1999.95, 5, 66.66, 2, N'DELL gaming pro. Sin luces. (Campaña sensibilizacion contra la epilepsia)', 1000, 1, 17, 1, 1),
	(11, N'ASUS-1362', 699.95, 1, 23.33, 3, N'portatilote grandote marca asus perfecto para ir de camping.', 350, 6, 15, 5, 2),
	(12, N'TOASTER-2434', 899.95, 2, 30, 4, N'GRILL BBQ 3000W GAMING', 450, 3, 19, 0, 4),
	(13, N'HP-3141', 699.95, 3, 23.33, 1, N'HP intel core ryzen 5 workstation', 350, 4, 6, 2, 1),
	(14, N'DELL-4272', 1399.95, 4, 46.66, 2, N'DELL ultraluna portatil sin teclado fancy.', 700, 7, 22, 1, 2),
	(15, N'ASUS-5354', 1599.95, 5, 53.33, 3, N'ASUS-tado GAMING pro', 800, 5, 21, 7, 4),
	(16, N'TOASTER-1421', 1299.95, 1, 43.33, 4, N'Portatil de 82 pulgadas. (Para empotrar en la pared)', 650, 2, 15, 6, 1),
	(17, N'HP-2112', 599.95, 2, 20, 1, N'HP “delicias de la programación” ', 300, 1, 7, 4, 2),
	(18, N'DELL-3264', 1099.95, 3, 36.66, 2, N'Portatil DELL perfecto para IS2', 550, 6, 17, 1, 4),
	(19, N'ASUS-4331', 1099.95, 4, 36.66, 3, N'Portatil ASUS UNICORN FORCE EXTREME +18', 550, 3, 21, 2, 1),
	(20, N'TOASTER-5442', 899.95, 5, 30, 4, N'Portatil Ultrafiable con SAIS incorporado. Levantar con las piernas, no con la espalda.', 450, 4, 25, 3, 2),
	(21, N'HP-1174', 299.95, 1, 10, 1, N'HP edicion HP con varita mágica.', 150, 7, 4, 10, 4),
	(22, N'DELL-2251', 699.95, 2, 23.33, 2, N'Portatil DELL no tan perfecto para IS2', 350, 5, 20, 3, 1),
	(23, N'ASUS-3322', 1399.95, 3, 46.66, 3, N'Portatil ASUS futurista sin pantalla', 700, 2, 9, 3, 2),
	(24, N'TOASTER-4414', 799.95, 4, 26.66, 4, N'TOASTER WORKSTATION Skynet edition', 400, 1, 12, 2, 4),
	(25, N'HP-5161', 1099.95, 5, 36.66, 1, N'Portatil HP resistente a golpes suaves.', 550, 6, 7, 9, 1),
	(26, N'DELL-1232', 2099.95, 1, 70, 2, N'Portatil DELL ultraseguridad anti-hack. Sin tarjeta de red ni usb.', 1050, 3, 18, 0, 2),
	(27, N'ASUS-2344', 1899.95, 2, 63.33, 3, N'Portatil ASUS cúbico. Parece WALL-E.', 950, 4, 19, 10, 4),
	(28, N'TOASTER-3471', 999.95, 3, 33.33, 4, N'Portatil ultra. Especial para sacar la ingeniería.', 500, 7, 2, 10, 1),
	(29, N'HP-4152', 599.95, 4, 20, 1, N'Portatil HP que sale en la peli de iron man.', 300, 5, 1, 10, 2),
	(30, N'DELL-5224', 999.95, 5, 33.33, 2, N'¡OFERTA! Portatil DELL ¡OFERTA!', 500, 2, 0, 5, 4),
	(31, N'ASUS-1311', 299.95, 1, 10, 3, N'DELL I7 32Tb 16Gb 3200 y muchos más numeros.', 150, 1, 22, 5, 1),
	(32, N'TOASTER-2462', 899.95, 2, 30, 4, N'Portatil KONAMI. El teclado solo incluye las flechas de direccion, A y B.', 450, 6, 28, 5, 2),
	(33, N'HP-3134', 1199.95, 3, 40, 1, N'HP Visual Studio Enterprise', 600, 3, 13, 3, 4),
	(34, N'DELL-4241', 999.95, 4, 33.33, 2, N'Portatil DELL de apenas 12 años.', 500, 4, 25, 10, 1),
	(35, N'ASUS-5372', 1999.95, 5, 66.66, 3, N'Portatil reguapo así como negro con colorines.', 1000, 7, 13, 4, 2),
	(36, N'TOASTER-1454', 1599.95, 1, 53.33, 4, N'Toaster edicion cyberpunk. No incluye gráfica.', 800, 5, 29, 3, 4),
	(37, N'HP-2121', 2299.95, 2, 76.66, 1, N'HP cream! ahora con el doble de crema y más chocolate.', 1150, 2, 17, 9, 1),
	(38, N'DELL-3212', 1099.95, 3, 36.66, 2, N'Portatil DELL supercompuglobalmeganet.', 550, 1, 5, 7, 2),
	(39, N'ASUS-4364', 1299.95, 4, 43.33, 3, N'Portatil ASUS DojoMojoCasaHouse P4 especial hogar.', 650, 6, 0, 7, 4),
	(40, N'TOASTER-5431', 2199.95, 5, 73.33, 4, N'Portatil edición limitada con brilli brilli', 1100, 3, 20, 10, 1),
	(41, N'HP-1142', 1499.95, 1, 50, 1, N'HP ultima generacion. Perfecto para facebook.', 750, 4, 16, 10, 2),
	(42, N'DELL-2274', 1799.95, 2, 60, 2, N'Portatil ultra fino 0.5cm de grosor. 2Kg', 900, 7, 0, 4, 4),
	(43, N'ASUS-3351', 1599.95, 3, 53.33, 3, N'Portatil ASUS pixie dust to neverland.', 800, 5, 11, 2, 1),
	(44, N'TOASTER-4422', 199.95, 4, 6.66, 4, N'All-In-One de 5 pulgadas', 100, 2, 20, 6, 2),
	(45, N'HP-5114', 1899.95, 5, 63.33, 1, N'Portatroll HP. Diseñado por y para twitterx', 950, 1, 4, 3, 4),
	(46, N'DELL-1261', 1399.95, 1, 46.66, 2, N'HAL 9000', 700, 6, 2, 10, 1),
	(47, N'ASUS-2332', 799.95, 2, 26.66, 3, N'ASUS workstation 3500 plus con subscripcion a netflix de 3 dias.', 400, 3, 30, 2, 2),
	(48, N'TOASTER-3444', 1399.95, 3, 46.66, 4, N'Ultrabook no tan ultra y sin libro.', 700, 4, 24, 5, 4),
	(49, N'HP-4171', 299.95, 4, 10, 1, N'Portatil HP barato a más no poder', 150, 7, 27, 9, 1),
	(50, N'DELL-5252', 1699.95, 5, 56.66, 2, N'Portail DELL con ultron en el ssd.', 850, 5, 15, 7, 2)
SET IDENTITY_INSERT [dbo].[Portatiles] OFF

SET IDENTITY_INSERT [dbo].[Compras] ON
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (1, 1, N'Francisco', N'Sánchez Cano', N'2023-04-23 00:00:00', N'c/ Torres Quevedo, nº 2', 1, 499.95001220703125)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (7, 2, N'Ana', N'Martínez López', N'2023-06-02 00:00:00', N'c/ Rosario, nº 34', 0, 1099.949951171875)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (9, 3, N'Carmen', N'García Bueno', N'2023-10-30 00:00:00', N'c/ Circunvalación, nº 23', 2, 1199.9000244140625)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (10, 4, N'Julia', N'Alfaro Soria', N'2023-04-08 00:00:00', N'Avenida España, nº 53', 0, 899.9000244140625)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (11, 5, N'Alberto', N'Molina Jiménez', N'2023-04-25 00:00:00', N'c/ Maria Marin, nº 15', 2, 199.94999694824219)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (13, 6, N'Juan', N'López Serrano', N'2023-05-30 00:00:00', N'c/ Ferrocarril, nº 4', 2, 1399.949951171875)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (14, 7, N'Paula', N'Moreno Lozano', N'2023-05-14 00:00:00', N'c/ Mayor, nº 31', 1, 1999.949951171875)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (15, 8, N'Luis', N'Donate Sáez', N'2023-10-02 00:00:00', N'c/ Simon Abril, nº 8', 1, 1799.8499755859375)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (16, 9, N'María', N'Segovia Sánchez', N'2022-01-20 00:00:00', N'c/ Feria, nº 17', 0, 799.95001220703125)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (17, 3, N'Carmen', N'García Bueno', N'2023-11-29 00:00:00', N'c/Rosario, nº 23', 0, 1199.9)
INSERT INTO [dbo].[Compras] ([Id], [CustomerId], [NombreCliente], [Apellidos], [FechaCompra], [Direccion], [MetodoPago], [Total]) VALUES (18, 3, N'Carmen', N'García Bueno', N'2023-11-28 00:00:00', N'c/Rosario, nº 23', 1, 499.95)
SET IDENTITY_INSERT [dbo].[Compras] OFF

SET IDENTITY_INSERT [dbo].[LineaCompra] ON
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (11, 2, 1, 1, 499.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (13, 5, 14, 1, 1999.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (14, 7, 13, 3, 599.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (15, 14, 13, 1, 1399.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (20, 9, 7, 1, 1099.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (21, 17, 9, 2, 599.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (22, 12, 10, 1, 899.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (23, 1, 11, 1, 199.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (24, 17, 17, 2, 599.95)
INSERT INTO [dbo].[LineaCompra] ([IdLinea], [IdPortatil], [IdCompra], [Cantidad], [PrecioUnitario]) VALUES (25, 2, 18, 1, 499.95)
SET IDENTITY_INSERT [dbo].[LineaCompra] OFF

SET IDENTITY_INSERT [dbo].[Devoluciones] ON
INSERT INTO [dbo].[Devoluciones] ([IdDevolucion], [Fecha], [CuantiaDevolucion], [DireccionRecogida], [NotaRepartidor], [MotivoDevolucion]) VALUES (1, N'2023-11-01 00:00:00', 599.95, N'c/ Circunvalacion, nº 23', N'recogerlo sobre las 5', N'necesito uno de mas capacidad')
SET IDENTITY_INSERT [dbo].[Devoluciones] OFF

SET IDENTITY_INSERT [dbo].[LineaDevolucion] ON
INSERT INTO [dbo].[LineaDevolucion] ([IdLinea], [Cantidad], [LineaCompraId], [IdDevolucion]) VALUES (1, 1, 21, 1)
SET IDENTITY_INSERT [dbo].[LineaDevolucion] OFF
