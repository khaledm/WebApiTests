using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
using WebApiTests.Models;

namespace WebApiTests.Controllers
{
    using System.Web.Http;
    using Microsoft.Web.Http;

    /// <summary>
    /// Students
    /// </summary>
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/catalogue")]
    public class StudentsController : ApiController
    {
        private readonly Faker<Student> _students;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentsController()
        {
            var title = new[] {"Mr", "Mrs", "Miss", "Ms", "Dr"};
            int id = 0;
            _students = new Faker<Student>()
                .RuleFor(o=> o.Id, f=> ++id)
                .RuleFor(o=> o.Title, f => f.PickRandom(title))
                .RuleFor(o=> o.FirstName, f=> f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Gender, f => Name.Gender.Male.ToString())
                .RuleFor(o => o.EmailAddress, f => f.Person.Email);
        }

        /// <summary>
        /// Get all students
        /// </summary>
        [Route("students")]
        [HttpGet]
        public List<Student> Get()
        {
            return _students.Generate(100);
        }
    }
}
