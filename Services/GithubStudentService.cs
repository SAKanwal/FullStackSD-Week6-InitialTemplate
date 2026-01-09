using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Week6_RESTFULAPI.Models;
using Week6_RESTFULAPI.Services;

namespace Week6_RESTFULAPI.Services
{
   
        public class GitHubStudentService : IStudentService
        {
            private readonly HttpClient _httpClient;
            private const string GitHubUrl = "https://raw.githubusercontent.com/SafiaK/MAUI-DataSources/refs/heads/main/students.json";
            // Example: "https://raw.githubusercontent.com/username/repo/main/students.json"

            private List<Student> _cachedStudents;

            public GitHubStudentService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<Student>> GetAllStudentsAsync()
            {
                try
                {
                    if (_cachedStudents == null)
                    {
                        _cachedStudents = await _httpClient.GetFromJsonAsync<List<Student>>(GitHubUrl);
                    }
                    return _cachedStudents ?? new List<Student>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching students: {ex.Message}");
                    return new List<Student>();
                }
            }

            public async Task<Student> GetStudentByIdAsync(int id)
            {
                var students = await GetAllStudentsAsync();
                return students.FirstOrDefault(s => s.Id == id);
            }

            public async Task<Student> GetStudentByNameAsync(string name)
            {
                var students = await GetAllStudentsAsync();
                return students.FirstOrDefault(s =>
                    s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            public async Task<Student> AddStudentAsync(Student student)
            {
                // For GitHub JSON, we simulate adding (in-memory only)
                var students = await GetAllStudentsAsync();
                student.Id = students.Any() ? students.Max(s => s.Id) + 1 : 1;
                _cachedStudents.Add(student);
                return student;
            }

            public async Task<bool> DeleteStudentAsync(int id)
            {
                // For GitHub JSON, we simulate deletion (in-memory only)
                var students = await GetAllStudentsAsync();
                var student = students.FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    _cachedStudents.Remove(student);
                    return true;
                }
                return false;
            }
        }
    }

