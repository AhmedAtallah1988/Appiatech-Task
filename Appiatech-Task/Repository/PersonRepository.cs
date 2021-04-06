using Appiatech_Task.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appiatech_Task.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        public IEnumerable<Person> GetAllPerson(int? pageNumber, int? pageSize, Func<Person, bool> predicate = null);
    }
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(AppiatechDBContext Context) : base(Context)
        {
        }
        public IEnumerable<Person> GetAllPerson(int? pageNumber, int? pageSize, Func<Person, bool> predicate = null)
        {
            pageNumber = pageNumber == null ? 0 : pageNumber;
            pageSize = pageSize == null ? 100 : pageSize;

            if (predicate != null)
            {
                return db.Person.Where(predicate).Skip(pageSize.Value * pageNumber.Value).Take(pageSize.Value);
            }
            return db.Person.Skip(pageSize.Value * pageNumber.Value).Take(pageSize.Value);
        }
    }
}
