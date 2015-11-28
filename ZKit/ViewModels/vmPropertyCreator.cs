using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ZKit.Models;

namespace ZKit.ViewModels
{
    public class vmPropertyCreator : ViewModelBase
    {
        //=======================================================================================================
        // Properties
        //=======================================================================================================
        #region CurrentProperty
        private Property _currentProperty;
        public Property CurrentProperty
        {
            get
            {
                return _currentProperty;
            }
            set
            {
                if (value != _currentProperty)
                {
                    _currentProperty = value;
                    OnPropertyChanged("CurrentProperty");
                }
            }
        }
        #endregion

        //=======================================================================================================
        // Constructors
        //=======================================================================================================
        #region vmPropertyCreator()
        public vmPropertyCreator(Property prop)
        {
            CurrentProperty = prop;
        }
        #endregion
    }
}
