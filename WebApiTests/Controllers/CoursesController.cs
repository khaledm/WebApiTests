﻿using FluentValidation;
using WebApiTests.Filters;

namespace WebApiTests.Controllers
{
    using System.Web.Http;
    using Microsoft.Web.Http;
    using System.Collections.Generic;
    using System.Linq;
    using WebApiTests.Models;

    /// <inheritdoc />
    /// <summary>
    /// Courses
    /// </summary>
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/catalogue")]
    public class CoursesController : ApiController
    {
        private readonly AbstractValidator<PurchaseOrderType> _validator;
        private readonly CourseRepository _courses;

        /// <inheritdoc />
        /// <summary>
        /// Courses
        /// </summary>
        public CoursesController(CourseRepository courseRepository)
        {
            //_validator = validator;
            _courses = courseRepository;
        }

        /// <summary>
        /// All courses
        /// </summary>
        /// <returns></returns>
        [Route("courses")]
        [HttpGet]
        [ValidationFilter]
        public List<Course> Get()
        {
            return _courses.GetAll().ToList();
        }
    }
}
