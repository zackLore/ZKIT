using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZKit.Views;

namespace ZKit.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public MainWindow mainRef;
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        //public void UpdateProperty(string propertyName)
        //{
        //    OnPropertyChanged(propertyName);
        //}
    }
}
