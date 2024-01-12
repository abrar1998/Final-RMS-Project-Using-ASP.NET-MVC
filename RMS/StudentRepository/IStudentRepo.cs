using RMS.Models;

namespace RMS.AccountRepository
{
    public interface IStudentRepo
    {
       /* Task RegisterCourse(Course cor);

        List<Course> GetCourseList();*/

        Task RegisterStudent(Student stud);



        Task<List<Student>> GetAllStudents();

        Task<IEnumerable<Student>> GetStudentandCourseJoin();

        Task<IEnumerable<Student>> GetStudentandCourseJoinById(int id);

        //get student by id to pass the model to delete view
        Task<Student> GetStudentCourseJoinModelById(int id);


        //This method will receive a model from controller to delete database
        Task DeleteStudent(Student deleteStudent);

        //convert student model to studentview model so that we can send it to the edit view
        Task<StudentViewEdit> GetStudentViewEdit(int id);

        //This method will receive a model of student type then it will be updated in database
        Task UpdateStudent(Student updateOldStudentt);

       
    }
}
