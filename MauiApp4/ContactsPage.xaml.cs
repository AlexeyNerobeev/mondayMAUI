using MauiApp4.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp4
{
    public partial class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            InitializeComponent();
            
            var viewModel = MauiProgram.CreateMauiApp().Services.GetService<ContactsViewModel>();
            BindingContext = viewModel;
        }
    }
}