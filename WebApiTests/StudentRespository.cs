using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using WebApiTests.Models;

namespace WebApiTests
{
    /// <summary>
    /// StudentRespository
    /// </summary>
    public class StudentRespository : IRepository
    {
        private static IList<Student> _students;
        /// <summary>
        /// ctor
        /// </summary>
        public StudentRespository()
        {
            _students = GenenerateStudents();
        }
        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        public IList<Student> GetAll()
        {
            return _students;
        }

        /// <summary>
        /// get by
        /// </summary>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        public IEnumerable<Student> GetBy(Func<Student,bool> searchBy)
        {
            return _students.Where(searchBy);
        }

        private static IList<Student> GenenerateStudents()
        {
            var title = new[] {"Mr", "Mrs", "Miss", "Ms", "Dr"};
            int id = 0;
            return new Faker<Student>()
                .RuleFor(o => o.Id, f => ++id)
                .RuleFor(o => o.Title, f => f.PickRandom(title))
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Gender, f => Name.Gender.Male.ToString())
                .RuleFor(o => o.EmailAddress, f => f.Person.Email)
                .Generate(100);
        }
    }
}