--PRIMERA
SELECT P.ProductID[Id producto], P.ProductName[Nombre del Producto], P.SupplierID[Id del proveedor],
c.CategoryName[Categoria], P.QuantityPerUnit[Cantidad por unidad], P.UnitPrice[Precio unitario],
P.UnitsInStock[Unidades en Stok], p.UnitsOnOrder[Unidades En Pedido], P.ReorderLevel[Nivel de grabacion], 
P.Discontinued[Descontinuado]
FROM Products P
INNER JOIN Categories C ON P.CategoryID = C.CategoryID 
WHERE Discontinued = 0
ORDER BY ProductName 

--SEGUNDA
SELECT C.ContactName[Nombre del cliente] FROM Employees E
LEFT JOIN Orders O ON E.EmployeeID = O.EmployeeID
INNER JOIN Customers C ON O.CustomerID = C.CustomerID
WHERE FirstName = 'Nancy' AND LastName = 'Davolio'
--WHERE E.EmployeeID = 1 

SELECT ContactName AS 'Nombre del Cliente'
FROM Customers
WHERE CustomerID IN (
    SELECT CustomerID
    FROM Orders
    WHERE EmployeeID = (
        SELECT EmployeeID
        FROM Employees
        WHERE FirstName = 'Nancy' AND LastName = 'Davolio'
    )
);

--TERCERA
SELECT YEAR(OrderDate)[Año], SUM([Order Details].UnitPrice * [Order Details].Quantity)[Total Facturado]
FROM Orders O
INNER JOIN Employees E ON O.EmployeeID = E.EmployeeID
INNER JOIN [Order Details]  ON O.OrderID = [Order Details].OrderID
WHERE E.FirstName = 'Steven' AND E.LastName = 'Buchanan'
--WHERE E.EmployeeID = 5
GROUP BY YEAR(OrderDate)
ORDER BY YEAR(OrderDate);

--CUARTA
WITH EMPLEADOS_DIRECTOS AS (
    SELECT EmployeeID, FirstName, LastName, ReportsTo
    FROM Employees
    WHERE FirstName = 'Andrew' AND LastName = 'Fuller'
    UNION ALL
    SELECT E.EmployeeID, E.FirstName, E.LastName, E.ReportsTo
    FROM Employees E
    JOIN EMPLEADOS_DIRECTOS EH ON E.ReportsTo = EH.EmployeeID
)
SELECT CONCAT(FirstName, ' ', LastName)[Nombre del Empleado]
FROM EMPLEADOS_DIRECTOS;

--Esta consulta utiliza una cláusula CTE (Common Table Expression) recursiva llamada "EMPLEADOS_DIRECTOS" para construir una jerarquía de empleados basada en la columna "ReportsTo" en la tabla "Employees". En el primer nivel, selecciona al empleado "Andrew Fuller" y, a continuación, realiza un JOIN recursivo para encontrar a los empleados que dependen directa o indirectamente de él.

