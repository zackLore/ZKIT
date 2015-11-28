using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZKit.Services;

namespace ZKit.Models
{
    public class Tool : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region HTML
        private HTMLGenerator _html;
        public HTMLGenerator HTML
        {
            get
            {
                return _html;
            }
            set
            {
                if (value != _html)
                {
                    _html = value;
                    OnPropertyChanged("HTML");
                }
            }
        }
        #endregion

        #region Language
        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
                OnPropertyChanged("Language");
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
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        #endregion

        #region Objects
        private List<Obj> _objects;
        public List<Obj> Objects
        {
            get
            {
                if(_objects == null)
                {
                    _objects = new List<Obj>();
                }
                return _objects;
            }
            set
            {
                _objects = value;
                OnPropertyChanged("Objects");
            }
        }
        #endregion

        #region Parent
        private object _parent;
        public object Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (value != _parent)
                {
                    _parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region Tool()
        public Tool()
        {
            Name = "[New Tool]";           
            HTML = new HTMLGenerator(this);
        }
        #endregion

        #region Tool(object parent)
        public Tool(object parent)
        {
            Parent = parent;
            Name = "[New Tool]";
            HTML = new HTMLGenerator(this);
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region AddNewObject()
        public Obj AddNewObject()
        {
            Obj obj = new Obj();
            Objects.Add(obj);
            return obj;
        }
        #endregion
    }
}
