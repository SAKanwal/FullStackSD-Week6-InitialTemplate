namespace Week6_RESTFULAPI.Views;

using Week6_RESTFULAPI.ViewModels;

public partial class AddStudentPage : ContentPage
{
    public AddStudentPage(AddStudentViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}