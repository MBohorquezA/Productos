# Instrucciones para el proyecto Productos

1. Importar el proyecto Productos, esta hecho en NetCore 8

2. Seleccionar la capa "Productos.DAL" como proyecto de inicio

3. Abrir la consola de administrador de paquetes y ejecutar este codigo Update-Database, la base de datos esta configurada a localhost con la autentificacion del windows, la linea de conexion se encuentra en DAL se llama "appsettings.json"

4. ejecutar este código en la consola de SqlServer apuntando a la BDProductos 
```
CREATE PROCEDURE dbo.InsertProduct
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Price DECIMAL(18, 2),
    @Stock INT,
    @ProductId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Product (Name, Description, Price, Stock)
    VALUES (@Name, @Description, @Price, @Stock);

    -- Asignar el valor del ID recién insertado al parámetro OUTPUT
    SET @ProductId = SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE dbo.GetProductById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id, 
        Name, 
        Description, 
        Price, 
        Stock
    FROM 
        Product
    WHERE 
        Id = @Id;
END;
GO

CREATE PROCEDURE dbo.GetAllProducts
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id, 
        Name, 
        Description, 
        Price, 
        Stock
    FROM 
        Product;
END;
GO```

4. Al terminar de ejecutar la migracion, es volver a poner el proyecto de inicio a "Productos.WebApi" 

