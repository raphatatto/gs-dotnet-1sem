using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_aquaguard_dotnet.Data;
using api_aquaguard_dotnet.Models;
using api_aquaguard_dotnet.DTOs;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private readonly AquaGuardContext _context;

    public SensorController(AquaGuardContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sensor>>> GetAll()
    {
        return await _context.Sensores.Include(s => s.Regioes).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sensor>> GetById(int id)
    {
        var sensor = await _context.Sensores
            .Include(s => s.Regioes)
            .FirstOrDefaultAsync(s => s.IdSensor == id);

        if (sensor == null) return NotFound();
        return sensor;
    }

    [HttpPost]
    public async Task<ActionResult<Sensor>> Create(SensorDTO dto)
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SensorDTO dto)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor == null) return NotFound();

        sensor.Tipo = dto.Tipo;
        sensor.Status = dto.Status;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sensor = await _context.Sensores.FindAsync(id);
        if (sensor == null) return NotFound();

        _context.Sensores.Remove(sensor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
