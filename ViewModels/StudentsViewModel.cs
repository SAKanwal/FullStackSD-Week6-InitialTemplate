using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Week6_RESTFULAPI.Models;
using Week6_RESTFULAPI.Services;

using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Week6_RESTFULAPI.ViewModels
{
    public class StudentsViewModel : BaseViewModel
        {
            private readonly IStudentService _studentService;

            public ObservableCollection<Student> Students { get; set; }

            private string _searchText;
            public string SearchText
            {
                get => _searchText;
                set => SetProperty(ref _searchText, value);
            }

            private Student _selectedStudent;
            public Student SelectedStudent
            {
                get => _selectedStudent;
                set => SetProperty(ref _selectedStudent, value);
            }

        private bool isRefreshing;
        private bool isRefreshing1;

        public bool IsRefreshing
            { get => isRefreshing;
              set => SetProperty(ref isRefreshing, value);
            }
        public ICommand LoadStudentsCommand { get; }
            public ICommand SearchCommand { get; }
            public ICommand DeleteCommand { get; }
            public ICommand AddStudentCommand { get; }
            public ICommand RefreshCommand { get; }


            public StudentsViewModel(IStudentService studentService)
            {
                _studentService = studentService;
                Students = new ObservableCollection<Student>();

                LoadStudentsCommand = new Command(async () => await LoadStudentsAsync());
                SearchCommand = new Command(async () => await SearchStudentsAsync());
                DeleteCommand = new Command<Student>(async (student) => await DeleteStudentAsync(student));
                AddStudentCommand = new Command(async () => await NavigateToAddStudentAsync());
                RefreshCommand = new Command(async () => await RefreshStudentsAsync());

            }

            public async Task LoadStudentsAsync()
            {
                if (IsBusy)
                    return;

                try
                {
                    IsBusy = true;
                    Students.Clear();

                    var students = await _studentService.GetAllStudentsAsync();

                    foreach (var student in students)
                    {
                        Students.Add(student);
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                        $"Unable to load students: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                    IsRefreshing = false;
                }
            }

            private async Task SearchStudentsAsync()
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    await LoadStudentsAsync();
                    return;
                }

                if (IsBusy)
                    return;

                try
                {
                    IsBusy = true;
                    Students.Clear();

                    // Try to parse as ID first
                    if (int.TryParse(SearchText, out int id))
                    {
                        var student = await _studentService.GetStudentByIdAsync(id);
                        if (student != null)
                        {
                            Students.Add(student);
                        }
                    }
                    else
                    {
                        // Search by name
                        var student = await _studentService.GetStudentByNameAsync(SearchText);
                        if (student != null)
                        {
                            Students.Add(student);
                        }
                    }

                    if (Students.Count == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Not Found",
                            "No student found with that ID or name", "OK");
                        await LoadStudentsAsync();
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                        $"Search failed: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }

            private async Task DeleteStudentAsync(Student student)
            {
                if (student == null)
                    return;

                bool answer = await Application.Current.MainPage.DisplayAlert(
                    "Delete Student",
                    $"Are you sure you want to delete {student.Name}?",
                    "Yes", "No");

                if (!answer)
                    return;

                try
                {
                    IsBusy = true;
                    bool success = await _studentService.DeleteStudentAsync(student.Id);

                    if (success)
                    {
                        Students.Remove(student);
                        await Application.Current.MainPage.DisplayAlert("Success",
                            "Student deleted successfully", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error",
                            "Failed to delete student", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                        $"Delete failed: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }

            private async Task NavigateToAddStudentAsync()
            {
                await Shell.Current.GoToAsync("AddStudentPage");
            }

            public async Task RefreshStudentsAsync()
            {
            Console.WriteLine("The value of is refreshing is",isRefreshing);
                await LoadStudentsAsync();
            }
        
    }
}
