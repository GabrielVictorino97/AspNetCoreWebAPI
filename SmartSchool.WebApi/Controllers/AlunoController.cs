using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SmartSchool.Models;
using SmartSchool.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SmartSchool.WebApi.DTOs;
using AutoMapper;

namespace SmartSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public AlunoController(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var alunos = _repository.GetAllAlunos(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new AlunoRegistrarDto());
        }

        [HttpGet("byId")]
        public IActionResult Get(int id)
        {
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null)
                return BadRequest("O Aluno não foi encontrado!");

                var alunoDto = _mapper.Map<AlunoDto>(aluno);


            return Ok(alunoDto);
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);

            if(_repository.SaveChanges()){
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            var alunoId = _repository.GetAlunoById(id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

            _mapper.Map(model, alunoId);

           _repository.Update(alunoId);

            if(_repository.SaveChanges()){
                 return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(alunoId));
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {

             var alunoId = _repository.GetAlunoById(id);
            if(alunoId == null){
                return BadRequest("Aluno não encontrado");
            }

            _mapper.Map(model, alunoId);

           _repository.Update(alunoId);

            if(_repository.SaveChanges()){
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(alunoId));
            }
            return BadRequest("Aluno não atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alunoId = _repository.GetAlunoById(id);
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
