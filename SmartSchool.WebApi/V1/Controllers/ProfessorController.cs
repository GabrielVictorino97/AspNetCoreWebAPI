using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Models;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.V1.DTOs;

namespace SmartSchool.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(){

            var result = _repository.GetAllProfessores(true);
             return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(result));
        }

        [HttpGet("getRegister")]
        public IActionResult GetRegister()
        {
            return Ok(new ProfessorRegistrarDto());
        }

         [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Professor = _repository.GetProfessorById(id, true);
            if (Professor == null) return BadRequest("O Professor não foi encontrado");

            var professorDto = _mapper.Map<ProfessorDto>(Professor);

            return Ok(Professor);
        }

         [HttpGet("byaluno/{alunoId}")]
        public IActionResult GetByAlunoId(int alunoId)
        {
            var Professores = _repository.GetProfessoresByAlunoId(alunoId, true);
            if (Professores == null) return BadRequest("Professores não encontrados");

            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(Professores));
        }

        [HttpPost]
        public IActionResult Post(Professor model)
        {
            var prof = _mapper.Map<Professor>(model);

            _repository.Add(prof);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não cadastrado");
        }

         [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);

            _repository.Update(prof);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não Atualizado");
        }

        // api/Professor
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model, prof);

            _repository.Update(prof);
            if (_repository.SaveChanges())
            {
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(prof));
            }

            return BadRequest("Professor não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.Delete(prof);
            if (_repository.SaveChanges())
            {
                return Ok("professor deletado");
            }

            return BadRequest("professor não deletado");
        }
    }
}