using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ZKit.Models;

namespace ZKit.Services
{
    public class PHPGenerator : ModelBase
    {
        //=======================================================================================================
        // Properties
        //=======================================================================================================
        #region DB
        private string _dB;
        public string DB
        {
            get
            {
                return _dB;
            }
            set
            {
                if (value != _dB)
                {
                    _dB = value;
                    OnPropertyChanged("DB");
                }
            }
        }
        #endregion

        #region Pass
        private string _pass;
        public string Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                if (value != _pass)
                {
                    _pass = value;
                    OnPropertyChanged("Pass");
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
                if (_toolRef == null)
                {
                    _toolRef = new Tool();
                }
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

        #region Server
        private string _server;
        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                if (value != _server)
                {
                    _server = value;
                    OnPropertyChanged("Server");
                }
            }
        }
        #endregion

        #region User
        private string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                if (value != _user)
                {
                    _user = value;
                    OnPropertyChanged("User");
                }
            }
        }
        #endregion

        //=======================================================================================================
        // Constructors
        //=======================================================================================================
        #region PHPGenerator(Tool toolRef)
        public PHPGenerator(Tool toolRef)
        {
            ToolRef = toolRef;
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region GenerateLoadDoc
        public string GenerateLoadDoc()//Not Finished.  Need to create SQL to save for each object.
        {
            StringBuilder php = new StringBuilder();

            php.Append("<?php include 'connect.php';").Append(Environment.NewLine).Append(Environment.NewLine);

            php.Append("try").Append(Environment.NewLine);
            php.Append("{").Append(Environment.NewLine);
            
            php.Append("$con = new PDO(\"mysql:host=$server;dbname=$db\", $user, $pass);").Append(Environment.NewLine);
            php.Append("$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);").Append(Environment.NewLine).Append(Environment.NewLine);

            php.Append("$sel = 'SELECT * FROM tbl").Append(ToolRef.Objects.ElementAt(0).Name).Append("';").Append(Environment.NewLine);
            php.Append("$query = $con->prepare($sel);").Append(Environment.NewLine);
            php.Append("$query->execute();").Append(Environment.NewLine).Append(Environment.NewLine);

            php.Append("$result = $query->fetchAll();").Append(Environment.NewLine);
            php.Append("echo(json_encode($result));").Append(Environment.NewLine);

            php.Append("}").Append(Environment.NewLine);
            php.Append("catch(Exception $ex)").Append(Environment.NewLine);
            php.Append("{").Append(Environment.NewLine);

            php.Append("echo('An error occured: '.").Append("$ex->Message);").Append(Environment.NewLine);

            php.Append("}").Append(Environment.NewLine).Append(Environment.NewLine);

            php.Append("?>").Append(Environment.NewLine);

            return php.ToString();
        }
        #endregion

        //http://stackoverflow.com/questions/13168120/mysql-pdo-connection-to-database
        #region GenerateSaveDoc
        public string GenerateSaveDoc()//Not Finished.  Need to create SQL to save for each object.
        {
            StringBuilder php = new StringBuilder();

            php.Append("<?php include 'connect.php';").Append(Environment.NewLine).Append(Environment.NewLine);

            php.Append("try").Append(Environment.NewLine);
            php.Append("{").Append(Environment.NewLine);

            php.Append("$totalRows = 0;").Append(Environment.NewLine);
            php.Append("$inserts = array();").Append(Environment.NewLine);
            php.Append("$con = new PDO(\"mysql:host=$server;dbname=$db\", $user, $pass);").Append(Environment.NewLine);
            php.Append("$con->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);").Append(Environment.NewLine).Append(Environment.NewLine);

            //declare variables
            foreach (var o in ToolRef.Objects)
            {
                php.Append("$").Append(o.Name).Append(" = null;").Append(Environment.NewLine);
            }

            php.Append(Environment.NewLine);

            //check to see if they are set
            foreach (var o in ToolRef.Objects)
            {
                php.Append("if(isset($_POST['").Append(o.Name).Append("']))").Append(Environment.NewLine);
                php.Append("{").Append(Environment.NewLine);

                php.Append("$").Append(o.Name).Append(" = json_decode(").Append("$_POST['").Append(o.Name).Append("'], true);").Append(Environment.NewLine);

                #region Insert
                php.Append("$tempSql = $con->prepare('INSERT INTO tbl").Append(o.Name).Append(" (");

                for (int i = 0; i < o.Properties.Count; i++)
                {
                    Property p = o.Properties[i];
                    php.Append("`").Append(p.Name).Append("`");
                    if (i < o.Properties.Count - 1)
                    {
                        php.Append(", ");
                    }
                 }

                php.Append(") VALUES (");

                for (int i = 0; i < o.Properties.Count; i++)
                {
                    Property p = o.Properties[i];
                    php.Append(":").Append(p.Name);
                    if (i < o.Properties.Count - 1)
                    {
                        php.Append(", ");
                    }
                }

                php.Append(")' );").Append(Environment.NewLine).Append(Environment.NewLine);

                //Generate binding code
                for (int i = 0; i < o.Properties.Count; i++)
                {
                    Property p = o.Properties[i];
                    php.Append("$tempSql->bindParam(':").Append(p.Name).Append("', $").Append(o.Name).Append("['").Append(p.Name).Append("']");
                    string temp = GetPDOType(p.PropertyType.Name);
                    if (!String.IsNullOrWhiteSpace(temp))
                    {
                        php.Append(", PDO::").Append(temp);
                    }

                    php.Append(");").Append(Environment.NewLine);

                }

                php.Append("$inserts[] = $tempSql;").Append(Environment.NewLine);

                #endregion

                php.Append(Environment.NewLine).Append("}").Append(Environment.NewLine).Append(Environment.NewLine);
            }

            php.Append("foreach($inserts as $insert)").Append(Environment.NewLine);
            php.Append("{").Append(Environment.NewLine);

            php.Append("$insert->execute();").Append(Environment.NewLine);
            php.Append("$totalRows = $totalRows + $insert->rowCount();").Append(Environment.NewLine);

            php.Append("}").Append(Environment.NewLine).Append(Environment.NewLine);

            php.Append("echo($totalRows);").Append(Environment.NewLine);

            php.Append("}").Append(Environment.NewLine);
            php.Append("catch(Exception $ex)").Append(Environment.NewLine);
            php.Append("{").Append(Environment.NewLine);

            php.Append("echo('An error occured: '.").Append("$ex->Message);").Append(Environment.NewLine);

            php.Append("}").Append(Environment.NewLine).Append(Environment.NewLine);

            php.Append("?>").Append(Environment.NewLine);

            return php.ToString();
        }
        #endregion

        #region GenerateConnectDoc
        public string GenerateConnectDoc()
        {
            StringBuilder php = new StringBuilder();

            php.Append("<?php").Append(Environment.NewLine);

            php.Append("$user = '").Append(User).Append("';").Append(Environment.NewLine);
            php.Append("$pass = '").Append(Pass).Append("';").Append(Environment.NewLine);
            php.Append("$db = '").Append(DB).Append("';").Append(Environment.NewLine);
            php.Append("$server = '").Append(Server).Append("';").Append(Environment.NewLine);

            php.Append("?>").Append(Environment.NewLine);

            return php.ToString();
        }
        #endregion

        public string GetPDOType(string type)
        {
            switch (type)
            {
                case "str":
                    return "PARAM_STR";
                case "int":
                    return "PARAM_INT";
                case "bool":
                    return "PARAM_BOOL";
                default:
                    return "";
            }
        }
    }
}
