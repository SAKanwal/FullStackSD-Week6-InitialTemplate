using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week6_RESTFULAPI.Models;

namespace Week6_RESTFULAPI.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> GetStudentByNameAsync(string name);
        Task<Student> AddStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
    }
}
