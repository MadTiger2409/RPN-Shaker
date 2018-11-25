using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN_Shaker.ViewModels;
using Xamarin.Forms;

namespace RPN_Shaker.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var viewModel = new MainPageViewModel();
            BindingContext = viewModel;
        }
    }
}
