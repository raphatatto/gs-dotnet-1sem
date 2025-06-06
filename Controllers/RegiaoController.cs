using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_aquaguard_dotnet.Data;
using api_aquaguard_dotnet.Models;
using api_aquaguard_dotnet.DTOs;

[ApiController]
[Route("api/[controller]")]
public class RegiaoController : ControllerBase
{
    private readonly AquaGuardContext _context;

    public RegiaoController(AquaGuardContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegiaoDTO>>> GetAll()
    {
        return await _context.Regioes
            .AsNoTracking()
            .Select(r => new RegiaoDTO
            {
                IdRegiao = r.IdRegiao,
                NmRegiao = r.NmRegiao,
                NmCidade = r.NmCidade,
                CoordenadasLat = r.CoordenadasLat,
                CoordenadasLng = r.CoordenadasLng,
                IdSensor = r.IdSensor
            }).ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<RegiaoDTO>> GetById(int id)
    {
        var regiao = await _context.Regioes
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.IdRegiao == id);

        if (regiao == null) return NotFound();

        return new RegiaoDTO
        {
            IdRegiao = regiao.IdRegiao,
            NmRegiao = regiao.NmRegiao,
            NmCidade = regiao.NmCidade,
            CoordenadasLat = regiao.CoordenadasLat,
            CoordenadasLng = regiao.CoordenadasLng,
            IdSensor = regiao.IdSensor
        };
    }


    [HttpPost]
    public async Task<ActionResult<Regiao>> Create(RegiaoDTO dto)
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


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RegiaoDTO dto)
    {
        var regiao = await _context.Regioes.FindAsync(id);
        if (regiao == null) return NotFound();

        regiao.NmRegiao = dto.NmRegiao;
        regiao.NmCidade = dto.NmCidade;
        regiao.CoordenadasLat = dto.CoordenadasLat;
        regiao.CoordenadasLng = dto.CoordenadasLng;
        regiao.IdSensor = dto.IdSensor;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var regiao = await _context.Regioes.FindAsync(id);
        if (regiao == null) return NotFound();

        _context.Regioes.Remove(regiao);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
