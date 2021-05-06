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
         private readonly IRepository _repository;

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(){

            var result = _repository.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var professor = _repository.GetAllProfessorById(id, false);

            if (professor == null)
                return BadRequest("O Professor não foi encontrado!");


            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
             _repository.Add(professor);

            if(_repository.SaveChanges()){
                return Ok(professor);
            }
            return BadRequest("Professor não cadastrado");
        }

          [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var professorId = _repository.GetAllProfessorById(id);
            if(professorId == null){
                return BadRequest("Aluno não encontrado");
            }

            _repository.Updtate(professor);

            if(_repository.SaveChanges()){
                return Ok(professor);
            }
            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {

            var professorId = _repository.GetAllProfessorById(id);
            if(professorId == null){
                return BadRequest("Professor não encontrado");
            }

            _repository.Updtate(professor);

            if(_repository.SaveChanges()){
                return Ok(professor);
            }
            return BadRequest("Professor não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professorId = _repository.GetAllProfessorById(id);
            if(professorId == null){
                return BadRequest("Professor não encontrado");
            }

            _repository.Delete(professorId);

            if(_repository.SaveChanges()){
                return Ok("Professor deletado");
            }
            return BadRequest("Professor não deletado");
        }

    }
}
