using SmartSchool.Models;
using SmartSchool.WebAPI.Helpers;
using System.Threading.Tasks;

namespace SmartSchool.WebApi.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         bool SaveChanges();

        //Alunos
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
        Aluno[] GetAllAlunos(bool includeProfessor = false);
        Task<Aluno[]> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);
      

        //Professores
         Professor[] GetAllProfessores(bool includeAlunos = false);
         Professor[] GetProfessoresByAlunoId(int disciplinaId, bool includeAlunos = false);
         Professor GetProfessorById(int professorId, bool includeProfessor = false);
    }
}