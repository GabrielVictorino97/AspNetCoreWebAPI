using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Models;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebApi.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
           _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
           _context.Update(entity);
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() > 0 );
        }


        //Alunos
        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()) ||
                                             aluno.Sobrenome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()));

            if (pageParams.Matricula > 0)
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);

            if (pageParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));

            // return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }


        public async Task<Aluno[]> GetAllAlunosByDisciplinaIdAsync(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return await query.ToArrayAsync();
        }


        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

           if(includeProfessor){
               query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
           }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Id == alunoId);

            return query.FirstOrDefault();
        }

        //Professores
        public Professor[] GetAllProfessores(bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos){
                 query = query.Include(a => a.Disciplinas)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(d => d.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();
            
        }
        public Professor[] GetProfessoresByAlunoId(int alunoId, bool includeAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos)
            {
                query = query.Include(p => p.Disciplinas)
                             .ThenInclude(d => d.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }

            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(aluno => aluno.Disciplinas.Any(
                             d => d.AlunosDisciplinas.Any(ad => ad.AlunoId == alunoId)
                         ));

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeProfessor = false)
        {
            IQueryable<Professor> query = _context.Professores;

             if(includeProfessor){
               query = query.Include(a => a.Disciplinas)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(d => d.Aluno);
           }

           
            query = query.AsNoTracking()
                         .OrderBy(a => a.Id)
                         .Where(professor => professor.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}