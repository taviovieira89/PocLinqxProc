CREATE TABLE Pedidos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT,
    DataPedido DATETIME,
    ValorTotal DECIMAL(10,2),
    Status VARCHAR(20)
);

CREATE PROCEDURE sp_TotalPedidosConfirmadosPorCliente
    @DataInicio DATETIME,
    @DataFim DATETIME
AS
BEGIN
    SELECT 
        ClienteId,
        SUM(ValorTotal) AS Total
    FROM 
        Pedidos
    WHERE 
        Status = 'Confirmado' AND
        DataPedido BETWEEN @DataInicio AND @DataFim
    GROUP BY 
        ClienteId
END;