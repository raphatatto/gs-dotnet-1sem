using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_aquaguard_dotnet.Data;
using api_aquaguard_dotnet.DTOs;
using api_aquaguard_dotnet.Models;

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
        var lista = await _context.Alertas
            .AsNoTracking()
            .Select(a => new AlertaDTO
            {
                IdAlerta = a.IdAlerta,
                NivelRisco = a.NivelRisco,
                DsAlerta = a.DsAlerta,
                DtAlerta = a.DtAlerta,
                IdRegiao = a.IdRegiao
            })
            .ToListAsync();

        return Ok(lista);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AlertaDTO>> GetById(int id)
    {
        var alerta = await _context.Alertas
            .AsNoTracking()
            .Where(a => a.IdAlerta == id)
            .Select(a => new AlertaDTO
            {
                IdAlerta = a.IdAlerta,
                NivelRisco = a.NivelRisco,
                DsAlerta = a.DsAlerta,
                DtAlerta = a.DtAlerta,
                IdRegiao = a.IdRegiao
            })
            .FirstOrDefaultAsync();

        if (alerta == null)
            return NotFound();

        return Ok(alerta);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AlertaDTO dto)
    {

        if (id != dto.IdAlerta)
            return BadRequest(new { mensagem = "O id na URL precisa ser igual ao IdAlerta do corpo." });

        var alerta = await _context.Alertas.FindAsync(id);
        if (alerta == null)
            return NotFound(new { mensagem = $"Alerta com id={id} não encontrado." });

        alerta.NivelRisco = dto.NivelRisco;
        alerta.DsAlerta = dto.DsAlerta;
        alerta.DtAlerta = dto.DtAlerta;
        alerta.IdRegiao = dto.IdRegiao;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Alertas.AnyAsync(a => a.IdAlerta == id))
                return NotFound(new { mensagem = $"Alerta com id={id} não encontrado durante a atualização." });
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var alerta = await _context.Alertas.FindAsync(id);
        if (alerta == null)
            return NotFound(new { mensagem = $"Alerta com id={id} não existe." });

        _context.Alertas.Remove(alerta);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }

    [HttpPost]
    public async Task<ActionResult<Alerta>> Create(AlertaCreateDTO dto)
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

}
