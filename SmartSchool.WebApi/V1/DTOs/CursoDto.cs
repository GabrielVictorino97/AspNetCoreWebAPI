using System.Collections.Generic;

namespace SmartSchool.WebApi.V1.DTOs
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<DisciplinaDto> Disciplinas { get; set; }
    }
}