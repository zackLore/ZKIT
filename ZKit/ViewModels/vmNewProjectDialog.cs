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
    public class vmNewProjectDialog : ViewModelBase
    {
        //=======================================================================================================
        // Properties
        //=======================================================================================================
        #region CurrentProject
        private Project _currentProject;
        public Project CurrentProject
        {
            get
            {
                return _currentProject;
            }
            set
            {
                if (value != _currentProject)
                {
                    _currentProject = value;
                    OnPropertyChanged("CurrentProject");
                }
            }
        }
        #endregion

        //=======================================================================================================
        // Constructors
        //=======================================================================================================
        #region vmNewProjectDialog()
        public vmNewProjectDialog()
        {
        }
        #endregion

        #region vmNewProjectDialog(Project p)
        public vmNewProjectDialog(Project p)
        {
            CurrentProject = p;
        }
        #endregion
    }
}