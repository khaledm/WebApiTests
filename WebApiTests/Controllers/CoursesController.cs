using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using WebApiTests.Models;

namespace WebApiTests.Controllers
{
    using System.Web.Http;
    using Microsoft.Web.Http;

    /// <inheritdoc />
    /// <summary>
    /// Courses
    /// </summary>
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/catalogue")]
    public class CoursesController : ApiController
    {
        private readonly Faker<Course> _courses;

        /// <inheritdoc />
        /// <summary>
        /// Courses
        /// </summary>
        public CoursesController()
        {
            var title = new[] { "Mr", "Mrs", "Miss", "Ms", "Dr" };
            int id = 0;
            var students = new Faker<Student>()
                .RuleFor(o => o.Id, f => ++id)
                .RuleFor(o => o.Title, f => f.PickRandom(title))
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.Gender, f => Name.Gender.Male.ToString())
                .RuleFor(o => o.EmailAddress, f => f.Person.Email);

            _courses = new Faker<Course>()
                .RuleFor(o => o.Id, f => ++id)
                .RuleFor(o=> o.Title, f => f.Company.CompanyName())
                .RuleFor(o=> o.Instructor, f => f.Name.FullName(Name.Gender.Male))
                .RuleFor(o=> o.Students, f=> students.Generate(100).ToList());
        }

        /// <summary>
        /// All courses
        /// </summary>
        /// <returns></returns>
        [Route("courses")]
        [HttpGet]
        public List<Course> Get()
        {
            return _courses.Generate(15);
        }
    }
}
