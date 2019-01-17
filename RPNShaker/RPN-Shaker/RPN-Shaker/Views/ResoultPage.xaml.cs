using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPNShaker.Logic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RPN_Shaker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResoultPage : ContentPage
	{
		public ResoultPage ()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            resoultLabel.Text = GetResoult();
            base.OnAppearing();
        }

        private void BackButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private string GetResoult()
        {
            if (!RPNConverter.input.Any())
            {
                return "Input can't be empty!";
            }
            else if (RPNConverter.input.Count(x => x == "(") != RPNConverter.input.Count(x => x == ")"))
            {
                return "Each open parenthesis must be closed!";
            }
            else
            {
                return RPNConverter.NormalToRPN();
            }
        }
    }
}