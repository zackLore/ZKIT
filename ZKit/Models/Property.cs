using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ZKit.Models
{
    public class Property : ModelBase
    {
        //=======================================================================================================
        // Properties
        //=======================================================================================================
        #region DefaultColor
        private string _defaultColor;
        public string DefaultColor
        {
            get
            {
                return _defaultColor;
            }
            set
            {
                if (value != _defaultColor)
                {
                    _defaultColor = value;
                    OnPropertyChanged("DefaultColor");
                }
            }
        }
        #endregion

        #region DefaultValue
        private string _defaultValue;
        public string DefaultValue
        {
            get
            {
                return _defaultValue;
            }
            set
            {
                if (value != _defaultValue)
                {
                    _defaultValue = value;
                    OnPropertyChanged("DefaultValue");
                }
            }
        }
        #endregion

        #region ParentRef
        private Obj _parentRef;
        public Obj ParentRef
        {
            get
            {
                return _parentRef;
            }
            set
            {
                if (value != _parentRef)
                {
                    _parentRef = value;
                    OnPropertyChanged("ParentRef");
                }
            }
        }
        #endregion

        #region PropertyId
        private string _propertyId;
        public string PropertyId
        {
            get
            {
                _propertyId = "txt_" + (!String.IsNullOrWhiteSpace(ParentRef.Name) ? ParentRef.Name + "_" : "") + Name;

                return _propertyId;
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

        #region PropertyType
        private PropType _propertyType;
        public PropType PropertyType
        {
            get
            {
                if (_propertyType == null)
                {
                    _propertyType = new PropType();
                }
                return _propertyType;
            }
            set
            {
                if (value != _propertyType)
                {
                    _propertyType = value;
                    OnPropertyChanged("PropertyType");
                }
            }
        }
        #endregion

        //=======================================================================================================
        // Constructors
        //=======================================================================================================
        //#region Property()
        //public Property()
        //{
        //    ParentObjName = "";
        //}
        //#endregion

        #region Property(string parentName)
        public Property(Obj parentRef)
        {
            ParentRef = parentRef;
        }
        #endregion

        //private string GetIdType()
        //{
        //    string type = "";
        //    switch (PropertyType.Name)
        //    {

        //    }


        //    return type;
        //}
    }
}
