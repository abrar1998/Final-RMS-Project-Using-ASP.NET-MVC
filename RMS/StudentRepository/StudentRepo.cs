
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.DatabaseContext;
using RMS.Models;

namespace RMS.AccountRepository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly AccountContext context;

        public StudentRepo(AccountContext context)
        {
            this.context = context;
        }

      
       /* public async Task RegisterCourse(Course cor)
        {
            if(cor !=null)
            {
                await context.courses.AddAsync(cor);
                await context.SaveChangesAsync();
            }
        }


        public List<Course> GetCourseList()
        {
            var data =  context.courses.ToList();
            return data;
        }*/


        public async Task RegisterStudent(Student stud)
        {
            await context.Students.AddAsync(stud); 
            await context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var data = await context.Students.Include(c => c.CourseList).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Student>> GetStudentandCourseJoin()
        {
            var data = await context.Students.Include(c => c.CourseList).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Student>> GetStudentandCourseJoinById(int id)
        {

            var data = await context.Students.Include(c => c.CourseList).Where(x => x.StudentId == id).ToListAsync();
            return data;
        }


        //here we create this function for delete view because delete view accepts only model so here we return student model
        public async Task<Student> GetStudentCourseJoinModelById(int id)
        {
            var data = await context.Students.Include(c => c.CourseList).Where(x => x.StudentId == id).FirstOrDefaultAsync();
            return data;

        }


        //This method will receive a model from controller to delete database
        public async Task DeleteStudent(Student deleteStudent)
        {
            context.Students.Remove(deleteStudent);
            await context.SaveChangesAsync();
        }

        public async Task<StudentViewEdit> GetStudentViewEdit(int id)
        {
            StudentViewEdit data = await context.Students
                         .Include(c => c.CourseList)
                         .Where(s => s.StudentId == id)
                         .Select(s => new StudentViewEdit
                         {
                             StudentId = s.StudentId,
                             StudentName = s.StudentName,
                             Parentage = s.Parentage,
                             StudentAge = s.StudentAge,
                             StudentEmail = s.StudentEmail,
                             StudentPhone = s.StudentPhone,
                             Adhaar = s.Adhaar,
                             DOB = s.DOB,
                             RegistrationDate = s.RegistrationDate,
                             Course = s.Course,
                             StudentPhoto = s.StudentPhoto,

                         })
                        .FirstOrDefaultAsync();
            return data;


            
        }

        public async Task UpdateStudent(Student updateOldStudent)
        {
            context.Students.Update(updateOldStudent);
            await context.SaveChangesAsync();
        }
    }
}
