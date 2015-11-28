using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    class HTMLMenuItem : ModelBase
    {
        #region Child
        private object _child;
        public object Child
        {
            get
            {
                if (_child == null)
                {
                    _child = new object();
                }
                return _child;
            }
            set
            {
                if (value != _child)
                {
                    _child = value;
                    OnPropertyChanged("Child");
                }
            }
        }
        #endregion

        #region Info
        private string _info;
        public string Info
        {
            get
            {
                return _info;
            }
            set
            {
                if (value != _info)
                {
                    _info = value;
                    OnPropertyChanged("Info");
                }
            }
        }
        #endregion

        #region Link
        private string _link;
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                if (value != _link)
                {
                    _link = value;
                    OnPropertyChanged("Link");
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

        #region Parent
        private object _parent;
        public object Parent
        {
            get
            {
                if (_parent == null)
                {
                    _parent = new object();
                }
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

        public HTMLMenuItem()
        {
        }
    }
}
