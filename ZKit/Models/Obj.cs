using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class Obj : ModelBase
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

        #region Properties
        private ObservableCollection<Property> _properties;
        public ObservableCollection<Property> Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new ObservableCollection<Property>();
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

        //#region PropertyType
        //private PropType _propertyType;
        //public PropType PropertyType
        //{
        //    get
        //    {
        //        return _propertyType;
        //    }
        //    set
        //    {
        //        //check for types here
        //        _propertyType = value;
        //        OnPropertyChanged("PropertyType");
        //    }
        //}
        //#endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region Obj()
        public Obj()
        {
            Name = "Object";
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region AddNewProperty()
        public void AddNewProperty(Property prop)
        {
            Properties.Add(prop);
        }
        #endregion

    }
}
