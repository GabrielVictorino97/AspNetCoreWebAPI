using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SmartSchool.Models;
using SmartSchool.WebApi.Data;
using Microsoft.EntityFrameworkCore;


namespace SmartSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;

        public AlunoController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Alunos);
        }

        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);

            if (aluno == null)
                return BadRequest("O Aluno não foi encontrado!");


            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetName(string nome, string sobrenome)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) || a.Sobrenome.Contains(sobrenome));

            if (aluno == null)
                return BadRequest("O Aluno não foi encontrado!");


            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alunoId = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {

            var alunoId = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

            _context.Update(aluno);
            _context.SaveChanges();
            
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alunoId = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

            _context.Remove(alunoId);
            _context.SaveChanges();
            return Ok("Removido com sucesso");
        }


    }
}
