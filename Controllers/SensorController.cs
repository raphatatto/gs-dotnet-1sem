using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_aquaguard_dotnet.Data;
using api_aquaguard_dotnet.DTOs;
using api_aquaguard_dotnet.Models;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private readonly AquaGuardContext _context;

    public SensorController(AquaGuardContext context)
    {
        _context = context;
    }

    // GET: /api/Sensor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SensorDTO>>> GetAll()
    {
        // Projeta apenas os campos do DTO (evita ciclos de navegação)
        var lista = await _context.Sensores
            .AsNoTracking()
            .Select(s => new SensorDTO
            {
                IdSensor = s.IdSensor,
                Tipo = s.Tipo,
                Status = s.Status
                // Se quiser contar quantas regiões um sensor tem:
                // RegiaoCount = s.Regioes.Count 
            })
            .ToListAsync();

        return Ok(lista);
    }

    // GET: /api/Sensor/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SensorDTO>> GetById(int id)
    {
        var sensor = await _context.Sensores
            .AsNoTracking()
            .Where(s => s.IdSensor == id)
            .Select(s => new SensorDTO
            {
                IdSensor = s.IdSensor,
                Tipo = s.Tipo,
                Status = s.Status
            })
            .FirstOrDefaultAsync();

        if (sensor == null)
            return NotFound();

        return Ok(sensor);
    }

    // POST: /api/Sensor
    [HttpPost]
    public async Task<ActionResult<Sensor>> Create(SensorCreateDTO dto)
    {
        var sensor = new Sensor
        {
            Tipo = dto.Tipo,
            Status = dto.Status
        };

        _context.Sensores.Add(sensor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = sensor.IdSensor }, sensor);
    }

    // PUT: /api/Sensor/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SensorDTO dto)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor == null)
            return NotFound();

        // Atualiza apenas os campos simples
        sensor.Tipo = dto.Tipo;
        sensor.Status = dto.Status;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: /api/Sensor/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor == null)
            return NotFound();

        _context.Sensores.Remove(sensor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
