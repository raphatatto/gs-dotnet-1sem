using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_aquaguard_dotnet.Data;
using api_aquaguard_dotnet.DTOs;
using api_aquaguard_dotnet.Models;

[ApiController]
[Route("api/[controller]")]
public class RegiaoController : ControllerBase
{
    private readonly AquaGuardContext _context;

    public RegiaoController(AquaGuardContext context)
    {
        _context = context;
    }

    // GET: /api/Regiao
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegiaoDTO>>> GetAll()
    {
        var lista = await _context.Regioes
            .AsNoTracking()
            .Select(r => new RegiaoDTO
            {
                IdRegiao = r.IdRegiao,
                NmRegiao = r.NmRegiao,
                NmCidade = r.NmCidade,
                CoordenadasLat = r.CoordenadasLat,
                CoordenadasLng = r.CoordenadasLng,
                IdSensor = r.IdSensor
            })
            .ToListAsync();

        return Ok(lista);
    }

    // GET: /api/Regiao/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RegiaoDTO>> GetById(int id)
    {
        var regiao = await _context.Regioes
            .AsNoTracking()
            .Where(r => r.IdRegiao == id)
            .Select(r => new RegiaoDTO
            {
                IdRegiao = r.IdRegiao,
                NmRegiao = r.NmRegiao,
                NmCidade = r.NmCidade,
                CoordenadasLat = r.CoordenadasLat,
                CoordenadasLng = r.CoordenadasLng,
                IdSensor = r.IdSensor
            })
            .FirstOrDefaultAsync();

        if (regiao == null)
            return NotFound();

        return Ok(regiao);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RegiaoDTO dto)
    {
        if (id != dto.IdRegiao)
            return BadRequest(new { mensagem = "O id na URL precisa ser igual ao IdRegiao do corpo." });

        var regiao = await _context.Regioes.FindAsync(id);
        if (regiao == null)
            return NotFound(new { mensagem = $"Região com id={id} não encontrada." });

        // Atualiza apenas os campos permitidos
        regiao.NmRegiao = dto.NmRegiao;
        regiao.NmCidade = dto.NmCidade;
        regiao.CoordenadasLat = dto.CoordenadasLat;
        regiao.CoordenadasLng = dto.CoordenadasLng;
        regiao.IdSensor = dto.IdSensor;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Regioes.AnyAsync(r => r.IdRegiao == id))
                return NotFound(new { mensagem = $"Região com id={id} não encontrada durante a atualização." });
            throw;
        }

        return NoContent(); // 204
    }

    // DELETE: api/Regiao/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var regiao = await _context.Regioes.FindAsync(id);
        if (regiao == null)
            return NotFound(new { mensagem = $"Região com id={id} não existe." });

        _context.Regioes.Remove(regiao);
        await _context.SaveChangesAsync();
        return NoContent(); // 204
    }

    // POST: /api/Regiao
    [HttpPost]
    public async Task<ActionResult<Regiao>> Create(RegiaoCreateDTO dto)
    {
        var regiao = new Regiao
        {
            NmRegiao = dto.NmRegiao,
            NmCidade = dto.NmCidade,
            CoordenadasLat = dto.CoordenadasLat,
            CoordenadasLng = dto.CoordenadasLng,
            IdSensor = dto.IdSensor
        };
        _context.Regioes.Add(regiao);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = regiao.IdRegiao }, regiao);
    }

    // PUT e DELETE semelhantes, usando apenas RegiaoDTO
}
