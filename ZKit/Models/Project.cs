using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class Project : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region Name
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region Tools
        private List<Tool> _tools;
        public List<Tool> Tools
        {
            get
            {
                return _tools;
            }
            set
            {
                _tools = value;
                OnPropertyChanged("Tools");
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region Project()
        public Project()
        {
            Name = "[New Project]";
            Tools = new List<Tool>();
        }
        #endregion
    }
}
