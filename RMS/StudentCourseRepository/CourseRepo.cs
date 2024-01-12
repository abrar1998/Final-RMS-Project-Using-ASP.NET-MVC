using Microsoft.EntityFrameworkCore;
using RMS.DatabaseContext;
using RMS.Models;

namespace RMS.StudentCourseRepository
{
    public class CourseRepo:ICourseRepo
    {
        private readonly AccountContext context;

        public CourseRepo(AccountContext context)
        {
            this.context = context;
        }


        public async Task RegisterCourse(Course cor)
        {
            if (cor != null)
            {
                await context.courses.AddAsync(cor);
                await context.SaveChangesAsync();
            }
        }


        public List<Course> GetCourseList()
        {
            var data = context.courses.ToList();
            return data;
        }

        public async Task<Course> GetCourseById(int id)
        {
            var course = await context.courses.FindAsync(id);
            return course;
        }

        public Course GetCourseStudentJoinById(int id)
        {
            var courseTodelete =  context.courses.Include(c => c.Students).Where(c => c.CourseId == id).FirstOrDefault();
            return courseTodelete;
        }

        public  void DeleteCourse(Course deletecourse)
        {

            context.courses.Remove(deletecourse);
            context.SaveChanges();
        }

        public async Task<CourseViewEdit> GetCourseViewEditById(int id)
        {
            var courseview = await context.courses.Where(x => x.CourseId == id).Select(s => new CourseViewEdit
            {
                CourseId = s.CourseId,
                CourseName = s.CourseName,
                CourseFee = s.CourseFee,
                CoursePdf = s.CoursePdf,
            }).FirstOrDefaultAsync();

            return courseview;
        }

        public void UpdateCourse(Course updatecourse)
        {
            context.courses.Update(updatecourse);
            context.SaveChanges();
        }
    }
}
