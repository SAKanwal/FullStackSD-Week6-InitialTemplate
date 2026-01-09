namespace Week6_RESTFULAPI.Views;
using Week6_RESTFULAPI.ViewModels;

public partial class StudentPage : ContentPage
{
    public StudentPage(StudentsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}