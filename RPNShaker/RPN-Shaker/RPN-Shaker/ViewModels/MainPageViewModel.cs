using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RPN_Shaker.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Equation { get; set; }
    }
}
