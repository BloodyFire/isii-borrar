
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
INSERT INTO [dbo].[Proveedores] ([Id], [Nombre], [CIF], [Direccion], [Email], [Telefono]) VALUES (2, N'Portatiles Mayorista', N'12345678T', N'Poligono del unicornio, avenida de la purpurina numero 7. Narnia 02000', N'pormay@yahoorespuestas.com', N'999555666')
INSERT INTO [dbo].[Proveedores] ([Id], [Nombre], [CIF], [Direccion], [Email], [Telefono]) VALUES (4, N'Empresaurio genérico S.A.', N'RA000000WR', N'Wall street (detras de donde venden churros)', N'empresaurio@rawr.com', N'+34 600 700 800')
SET IDENTITY_INSERT [dbo].[Proveedores] OFF

SET IDENTITY_INSERT [dbo].[Portatiles] ON
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES 
	(1, N'HP-1151', 199.95, 1, 6.66, 1, N'HP 486 del pleistoceno', 100, 5, 13, 5, 1)
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES	(2, N'DELL-2222', 499.95, 2, 1/1, 2, N'DELL I5 para ofimatica', 250, 2, 29, 7, 2)
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES	(3, N'ASUS-3314', 2299.95, 3, 76.66, 3, N'ASUS PRO STATION 3000', 1150, 1, 16, 5, 4)
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES	(4, N'TOASTER-4461', 1699.95, 4, 56.66, 4, N'Pentium 4 fiable de toda la vida', 850, 6, 5, 2, 1)
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES	(5, N'HP-5132', 1999.95, 5, 66.66, 1, N'HP Quantum singstar edition plus', 1000, 3, 9, 9, 2)
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES	(6, N'DELL-1244', 1999.95, 1, 66.66, 2, N'DELL R5 gama alta', 1000, 4, 18, 1, 4)
INSERT INTO [dbo].[Portatiles] ([Id], [Modelo], [PrecioCompra], [RamId], [PrecioAlquiler], [MarcaId], [Nombre], [PrecioCoste], [ProcesadorId], [Stock], [StockAlquilar], [ProveedorId]) 
VALUES	(7, N'ASUS-2371', 599.95, 2, 20, 3, N'ASUS workstation con procesador de movil', 300, 7, 24, 7, 1)
SET IDENTITY_INSERT [dbo].[Portatiles] OFF