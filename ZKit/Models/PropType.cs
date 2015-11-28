using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class PropType : ModelBase
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

        #region SQLEquivalent
        private string _sqlEquivalent;
        public string SQLEquivalent
        {
            get
            {
                _sqlEquivalent = SetSQLEquivalent();
                return _sqlEquivalent;
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region ObjType()
        public PropType()
        {
        }
        #endregion

        //================================================================================================================================================
        // Private Methods
        //================================================================================================================================================
        #region SetSQLEquivalent
        private string SetSQLEquivalent()
        {
            string s = "";

            switch (Name.ToLower())
            {
                case "string":
                case "str":
                    s = "varchar(255)";
                    break;
                case "int":
                    s = "int";
                    break;
                case "dbl":
                    s = "decimal(10, 4)";
                    break;
                case "boolean":
                case "bool":
                    s = "bit";
                    break;
            }

            return s;
        }
        #endregion

        #region GetDefaultValue
        public string GetDefaultValue()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                return "";
            }

            switch (Name.ToLower())
            {
                case "int":
                case "dbl":
                case "currency":
                case "percent":
                    return "0";
                default:
                    return "";
            }
        }
        #endregion
    }
}
