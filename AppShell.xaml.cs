using Week6_RESTFULAPI.Views;

namespace Week6_RESTFULAPI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddStudentPage), typeof(AddStudentPage));

        }
    }
}
