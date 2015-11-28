using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class JSProperty : ModelBase
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
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        #endregion

        #region Value
        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region JSProperty
        public JSProperty()
        {
        }
        #endregion
    }
}
