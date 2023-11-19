using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private static List<Models.Filme> filmes = new List<Models.Filme>();
    private static int id = 0;

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)
    {
        filme.Id = id++;
        filmes.Add(filme);
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id },
                filme);
    }

    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        return filmes.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        //var consulta = filmes
        //        .Where(x => x.Id == id);

        var filme = filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null) return NotFound();
        return Ok();
    }
}
