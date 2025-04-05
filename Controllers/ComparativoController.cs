[ApiController]
[Route("[controller]")]
public class ComparativoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ComparativoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("linq")]
    public async Task<IActionResult> GetPorLinq(DateTime inicio, DateTime fim)
    {
        var stopwatch = Stopwatch.StartNew();

        var resultado = await _context.Pedidos
            .Where(p => p.Status == "Confirmado" && p.DataPedido >= inicio && p.DataPedido <= fim)
            .GroupBy(p => p.ClienteId)
            .Select(g => new {
                ClienteId = g.Key,
                Total = g.Sum(p => p.ValorTotal)
            })
            .ToListAsync();

        stopwatch.Stop();

        return Ok(new { resultado, tempoMs = stopwatch.ElapsedMilliseconds });
    }

    [HttpGet("procedure")]
    public async Task<IActionResult> GetPorProcedure(DateTime inicio, DateTime fim)
    {
        var stopwatch = Stopwatch.StartNew();

        var dataInicioParam = new SqlParameter("@DataInicio", inicio);
        var dataFimParam = new SqlParameter("@DataFim", fim);

        var resultado = await _context.Pedidos
            .FromSqlRaw("EXEC sp_TotalPedidosConfirmadosPorCliente @DataInicio, @DataFim", dataInicioParam, dataFimParam)
            .ToListAsync();

        stopwatch.Stop();

        return Ok(new { resultado, tempoMs = stopwatch.ElapsedMilliseconds });
    }
}