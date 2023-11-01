using GranTurismoFramework;
using GranTurismoFramework.DataTransfer.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class CourseDao
    {
        public List<CourseDto> GetCourses()
        {
            var courses = new List<CourseDto>();

            using (var context = new GranTurismoDb())
            {
                var courseList = context.Courses.ToList();
                courses = FwMapper.MapList<Course, CourseDto>(courseList);
            }

            return courses;
        }
    }
}
