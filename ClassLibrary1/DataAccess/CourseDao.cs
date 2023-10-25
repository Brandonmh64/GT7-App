using GranTurismoFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranTurismoFramework.DataAccess
{
    public class CourseDao
    {
        public List<Course> GetCourses()
        {
            var courses = new List<Course>();

            using (var context = new GranTurismoDb())
            {
                courses = context.Courses.ToList();
            }

            return courses;
        }
    }
}
