using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_aquaguard_dotnet.Data;
using api_aquaguard_dotnet.Models;
using api_aquaguard_dotnet.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AlertaController : ControllerBase
{
    private readonly AquaGuardContext _context;

    public AlertaController(AquaGuardContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlertaDTO>>> GetAll()
    {
        return await _context.Alertas
            .AsNoTracking()
            .Include(a => a.Regiao)
            .Select(a => new AlertaDTO
            {
                IdAlerta = a.IdAlerta,
                NivelRisco = a.NivelRisco,
                DsAlerta = a.DsAlerta,
                DtAlerta = a.DtAlerta,
                IdRegiao = a.IdRegiao,
                NomeRegiao = a.Regiao.NmRegiao
            })
            .ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<AlertaDTO>> GetById(int id)
    {
        var alerta = await _context.Alertas
            .AsNoTracking()
            .Include(a => a.Regiao)
            .Where(a => a.IdAlerta == id)
            .Select(a => new AlertaDTO
            {
                IdAlerta = a.IdAlerta,
                NivelRisco = a.NivelRisco,
                DsAlerta = a.DsAlerta,
                DtAlerta = a.DtAlerta,
                IdRegiao = a.IdRegiao,
                NomeRegiao = a.Regiao.NmRegiao
            })
            .FirstOrDefaultAsync();

        if (alerta == null) return NotFound();
        return alerta;
    }


    [HttpPost]
    public async Task<ActionResult<Alerta>> Create(AlertaDTO dto)
    {
        var alerta = new Alerta
        {
            NivelRisco = dto.NivelRisco,
            DsAlerta = dto.DsAlerta,
            DtAlerta = dto.DtAlerta,
            IdRegiao = dto.IdRegiao
        };
        _context.Alertas.Add(alerta);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = alerta.IdAlerta }, alerta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AlertaDTO dto)
    {
        var alerta = await _context.Alertas.FindAsync(id);
        if (alerta == null) return NotFound();

        alerta.NivelRisco = dto.NivelRisco;
        alerta.DsAlerta = dto.DsAlerta;
        alerta.DtAlerta = dto.DtAlerta;
        alerta.IdRegiao = dto.IdRegiao;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var alerta = await _context.Alertas.FindAsync(id);
        if (alerta == null) return NotFound();

        _context.Alertas.Remove(alerta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
