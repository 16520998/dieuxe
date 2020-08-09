using dieuxe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dieuxe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPwPage : ContentPage
    {
        public ForgotPwPage()
        {
            InitializeComponent();
            this.BindingContext = new ForgotPasswordViewModel();
        }
    }
}