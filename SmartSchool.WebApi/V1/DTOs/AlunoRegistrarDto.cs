using System;

namespace SmartSchool.WebApi.V1.DTOs
{
    public class AlunoRegistrarDto
    {
        public int Id { get; set; }   
        public int Matricula { get; set; }     
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataIni { get; set; }
        public DateTime? DataFim { get; set; }
        public bool Ativo { get; set; }  = true;
    }
}