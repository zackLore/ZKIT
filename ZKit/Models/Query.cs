using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class Query : ModelBase
    {
        public enum QueryTypes
        {
            DELETE,
            INSERT,
            UPDATE,
            SELECT
        }

        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region Fields
        private List<Field> _fields;
        public List<Field> Fields
        {
            get
            {
                if (_fields == null)
                {
                    _fields = new List<Field>();
                }
                return _fields;
            }
            set
            {
                if (value != _fields)
                {
                    _fields = value;
                    OnPropertyChanged("Fields");
                }
            }
        }
        #endregion

        #region Froms
        private List<string> _froms;
        public List<string> Froms
        {
            get
            {
                if (_froms == null)
                {
                    _froms = new List<string>();
                }
                return _froms;
            }
            set
            {
                if (value != _froms)
                {
                    _froms = value;
                    OnPropertyChanged("Froms");
                }
            }
        }
        #endregion

        #region QueryType
        private QueryTypes _queryType;
        public QueryTypes QueryType
        {
            get
            {
                return _queryType;
            }
            set
            {
                if (value != _queryType)
                {
                    _queryType = value;
                    OnPropertyChanged("QueryType");
                }
            }
        }
        #endregion

        #region Wheres
        private List<string> _wheres;
        public List<string> Wheres
        {
            get
            {
                if (_wheres == null)
                {
                    _wheres = new List<string>();
                }
                return _wheres;
            }
            set
            {
                if (value != _wheres)
                {
                    _wheres = value;
                    OnPropertyChanged("Wheres");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region Query()
        public Query()
        {
        }
        #endregion

    }
}
