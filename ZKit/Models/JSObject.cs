using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZKit.Models;

namespace ZKit.Models
{
    public class JSObject : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region Content
        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value != _content)
                {
                    _content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
        #endregion

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

        #region Properties
        private List<JSProperty> _properties;
        public List<JSProperty> Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new List<JSProperty>();
                }
                return _properties;
            }
            set
            {
                if (value != _properties)
                {
                    _properties = value;
                    OnPropertyChanged("Properties");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region JSObject()
        public JSObject()
        {
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================

        public string CreateObjectCode()
        {
            StringBuilder js = new StringBuilder();

            js.Append("var ").Append(Name).Append(" = function() {").Append(Environment.NewLine);
            js.Append(Content).Append(Environment.NewLine);
            js.Append("}").Append(Environment.NewLine);

            return js.ToString();
        }

    }
}
