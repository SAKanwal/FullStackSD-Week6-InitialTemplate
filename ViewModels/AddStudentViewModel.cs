using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Week6_RESTFULAPI.Models;
using Week6_RESTFULAPI.Services;

namespace Week6_RESTFULAPI.ViewModels
{
    public class AddStudentViewModel : BaseViewModel
    {
        private readonly IStudentService _studentService;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _program;
        public string Program
        {
            get => _program;
            set => SetProperty(ref _program, value);
        }

        private int _year = 1;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        private double _gpa;
        public double Gpa
        {
            get => _gpa;
            set => SetProperty(ref _gpa, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddStudentViewModel(IStudentService studentService)
        {
            _studentService = studentService;

            SaveCommand = new Command(async () => await SaveStudentAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        private async Task SaveStudentAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var newStudent = new Student
                {
                    Name = Name,
                    Program = Program,
                    Year = Year,
                    Gpa = Gpa
                };

                var addedStudent = await _studentService.AddStudentAsync(newStudent);

                if (addedStudent != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Success",
                        "Student added successfully", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                        "Failed to add student", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    $"Failed to add student: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
