using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class SQLGenerator : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region CurrentQuery
        private Query _currentQuery;
        public Query CurrentQuery
        {
            get
            {
                if (_currentQuery == null)
                {
                    _currentQuery = new Query();
                }
                return _currentQuery;
            }
            set
            {
                if (value != _currentQuery)
                {
                    _currentQuery = value;
                    OnPropertyChanged("CurrentQuery");
                }
            }
        }
        #endregion

        #region PrimaryTableName
        private string _primaryTableName;
        public string PrimaryTableName
        {
            get
            {
                return _primaryTableName;
            }
            set
            {
                if (value != _primaryTableName)
                {
                    _primaryTableName = value;
                    OnPropertyChanged("PrimaryTableName");
                }
            }
        }
        #endregion

        #region ToolRef
        private Tool _toolRef;
        public Tool ToolRef
        {
            get
            {
                return _toolRef;
            }
            set
            {
                if (value != _toolRef)
                {
                    _toolRef = value;
                    OnPropertyChanged("ToolRef");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region SQLGenerator(Tool toolRef)
        public SQLGenerator(Tool toolRef)
        {
            _toolRef = toolRef;
        }
        #endregion

        //================================================================================================================================================
        // Private Methods
        //================================================================================================================================================
        #region CreateTable()
        private string CreateTable()
        {
            if (!String.IsNullOrWhiteSpace(PrimaryTableName))
            {
                return null;
            }
            StringBuilder sql = new StringBuilder();

            //Check Settings for what type of database to generate code for

            foreach (var o in ToolRef.Objects)
            {
                //mysql db
                sql.Append("CREATE TABLE IF NOT EXISTS tbl").Append(o.Name).Append("(").Append(Environment.NewLine);
                sql.Append(o.Name).Append("Id").Append(" INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY, ").Append(Environment.NewLine);

                for (int i = 0; i < o.Properties.Count; i++)
                {
                    Property p = o.Properties[i];
                    sql.Append(p.Name).Append(" ").Append(p.PropertyType.SQLEquivalent != "" ? p.PropertyType.SQLEquivalent : "varchar(255)").Append(" NULL");
                    if (i < o.Properties.Count - 1)
                    {
                        sql.Append(",").Append(Environment.NewLine);
                    }
                    else
                    {
                        sql.Append(Environment.NewLine);
                    }
                }
                                
                sql.Append(");").Append(Environment.NewLine);
            }

            return sql.ToString();
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region GenerateDoc()
        public string GenerateDoc()
        {
            string doc = "";

            doc += CreateTable();

            return doc;
        }
        #endregion
    }
}
