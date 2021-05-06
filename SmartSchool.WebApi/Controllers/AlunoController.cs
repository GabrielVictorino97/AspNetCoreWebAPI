using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSchool.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        public List<Aluno> Alunos = new List<Aluno>()
        {
            new Aluno()
            {
                Id = 1,
                Nome = "Gabriel",
                Sobrenome = "Victorino",
                Telefone = "123456"

            },
            new Aluno()
            {
                Id = 2,
                Nome = "Luana",
                Sobrenome = "Martins",
                Telefone = "1234565476"

            },
            new Aluno()
            {
                Id = 3,
                Nome = "José",
                Sobrenome = "Almeida",
                Telefone = "123456549"

            }
        };
    
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Alunos);
        }

        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);

            if (aluno == null)
                return BadRequest("O Aluno não foi encontrado!");


            return Ok(aluno);
        }

        [HttpGet("byName")]
        public IActionResult GetName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) || a.Sobrenome.Contains(sobrenome));

            if (aluno == null)
                return BadRequest("O Aluno não foi encontrado!");


            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, Aluno aluno)
        {
            return Ok();
        }


    }
}
