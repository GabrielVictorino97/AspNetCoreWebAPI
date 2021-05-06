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
        private readonly IRepository _repository;

        public AlunoController(IRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _repository.GetAllAlunos(true);

            return Ok(result);
        }

        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var aluno = _repository.GetAllAlunoById(id, false);
            if (aluno == null)
                return BadRequest("O Aluno não foi encontrado!");


            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repository.Add(aluno);

            if(_repository.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var alunoId = _repository.GetAllAlunoById(id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

           _repository.Updtate(aluno);

            if(_repository.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {

             var alunoId = _repository.GetAllAlunoById(id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

           _repository.Updtate(aluno);

            if(_repository.SaveChanges()){
                return Ok(aluno);
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alunoId = _repository.GetAllAlunoById(id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

           _repository.Delete(alunoId);

            if(_repository.SaveChanges()){
                return Ok("Aluno deletado");
            }
            return BadRequest("Aluno não deletado");
        }


    }
}
