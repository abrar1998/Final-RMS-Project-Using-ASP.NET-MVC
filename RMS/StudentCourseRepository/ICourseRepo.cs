using RMS.Models;

namespace RMS.StudentCourseRepository
{
    public interface ICourseRepo
    {
        Task RegisterCourse(Course cor);
        List<Course> GetCourseList();

        Task<Course> GetCourseById(int id);

        //get course and include student table with it
        Course GetCourseStudentJoinById(int id);

        void DeleteCourse(Course deletecourse);

        Task<CourseViewEdit> GetCourseViewEditById(int id);

        void UpdateCourse(Course updatecourse);
       
    }
}
