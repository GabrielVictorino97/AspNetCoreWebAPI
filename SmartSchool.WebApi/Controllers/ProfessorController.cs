using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Models;
using SmartSchool.WebApi.Data;


namespace SmartSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
         private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll(){
            return Ok(_context.Professores);
        }

        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Id == id);

            if (professor == null)
                return BadRequest("O Professor não foi encontrado!");


            return Ok(professor);
        }

        [HttpGet("byName")]
        public IActionResult GetName(string nome)
        {
            var professor = _context.Professores.FirstOrDefault(a => a.Nome.Contains(nome));

            if (professor == null)
                return BadRequest("O Professor não foi encontrado!");


            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _context.Add(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

          [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var professorId = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(professorId == null){
                return BadRequest("Aluno não encontrado");
            }

            _context.Update(professor);
            _context.SaveChanges();
            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {

            var professorId = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(professorId == null){
                return BadRequest("Professor não encontrado");
            }

            _context.Update(professor);
            _context.SaveChanges();
            
            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professorId = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(professorId == null){
                return BadRequest("Professor não encontrado");
            }

            _context.Remove(professorId);
            _context.SaveChanges();
            return Ok("Removido com sucesso");
        }

    }
}
