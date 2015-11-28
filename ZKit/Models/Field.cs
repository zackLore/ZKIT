using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class Field : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region AllowNulls
        private bool _allowNulls;
        public bool AllowNulls
        {
            get
            {
                return _allowNulls;
            }
            set
            {
                if (value != _allowNulls)
                {
                    _allowNulls = value;
                    OnPropertyChanged("AllowNulls");
                }
            }
        }
        #endregion

        #region FieldName
        private string _fieldName;
        public string FieldName
        {
            get
            {
                return _fieldName;
            }
            set
            {
                if (value != _fieldName)
                {
                    _fieldName = value;
                    OnPropertyChanged("FieldName");
                }
            }
        }
        #endregion

        #region FieldType
        private string _fieldType;
        public string FieldType
        {
            get
            {
                return _fieldType;
            }
            set
            {
                if (value != _fieldType)
                {
                    _fieldType = value;
                    OnPropertyChanged("FieldType");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region Field()
        public Field()
        {
        }
        #endregion
    }
}
