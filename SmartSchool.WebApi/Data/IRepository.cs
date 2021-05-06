using SmartSchool.Models;

namespace SmartSchool.WebApi.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;
         void Updtate<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();

        //Alunos
         Aluno[] GetAllAlunos(bool includeProfessor = false);
         Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
         Aluno GetAllAlunoById(int alunoId, bool includeProfessor = false);
      

        //Professores
         Professor[] GetAllProfessores(bool includeAlunos = false);
         Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
         Professor GetAllProfessorById(int professorId, bool includeProfessor = false);
    }
}