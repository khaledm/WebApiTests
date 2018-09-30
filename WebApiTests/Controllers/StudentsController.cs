using System.Collections.Generic;
using System.Linq;
using FluentValidation;
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
        private readonly AbstractValidator<PurchaseOrderType> _validator;
        private readonly StudentRespository _students;

        /// <summary>
        /// ctor
        /// </summary>
        public StudentsController(AbstractValidator<PurchaseOrderType> validator)
        {
            _validator = validator;
            _students = new StudentRespository();
        }

        /// <summary>
        /// Get all students
        /// </summary>
        [Route("students")]
        [HttpGet]
        public List<Student> Get()
        {
            return _students.GetAll().ToList();
        }

        // <summary>
        /// Get all students
        /// </summary>
        [Route("students/{firstName}")]
        [HttpGet]
        public List<Student> GetBy(string firstName)
        {
            return _students.GetBy(student => student.FirstName.Contains(firstName)).ToList();
        }
    }
}
