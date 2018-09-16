namespace WebApiTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Bogus.DataSets;
    using WebApiTests.Models;

    /// <summary>
    ///
    /// </summary>
    public class CourseRepository
    {
        private static IList<Course> _courses;
        /// <summary>
        /// ctor
        /// </summary>
        public CourseRepository()
        {
            _courses = GenerateCourses();
        }
        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        public IList<Course> GetAll()
        {
            return _courses;
        }

        /// <summary>
        /// get by
        /// </summary>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        public IEnumerable<Course> GetBy(Func<Course, bool> searchBy)
        {
            return _courses.Where(searchBy);
        }

        private static IList<Course> GenerateCourses()
        {
            int id = 0;
            return new Faker<Course>()
                .RuleFor(o => o.Id, f => ++id)
                .RuleFor(o => o.Title, f => f.Company.CompanyName())
                .RuleFor(o => o.Instructor, f => f.Name.FullName(Name.Gender.Male))
                .Generate(100);
        }
    }
}