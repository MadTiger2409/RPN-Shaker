using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPN_Shaker.Logic.Commands;
using RPNShaker.Logic;

namespace RPN_Shaker.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Equation { get; set; }
        public RelayCommand<object> AddCharacterCommand { get; set; }
        public RelayCommand RemoveCharacterCommand { get; set; }
        public RelayCommand ClearAllCommand { get; set; }

        public MainPageViewModel()
        {
            Equation = "";

            RPNCoverter.ClearAll();
            AddCharacterCommand = new RelayCommand<object>(x =>
            {
                Equation += x as string;
                RPNCoverter.AddToInputList(x as string);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Equation)));
            });

            RemoveCharacterCommand = new RelayCommand(() =>
            {
                RemoveCharacterFromEquation();
                RPNCoverter.RemoveFromInputList();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Equation)));
            });

            ClearAllCommand = new RelayCommand(() =>
            {
                Equation = "";
                RPNCoverter.ClearAll();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Equation)));
            });
        }

        private void RemoveCharacterFromEquation()
        {
            if (Equation != null && Equation.Length > 0)
            {
                Equation = Equation.Remove(Equation.Length - 1);
            }
        }
    }
}
