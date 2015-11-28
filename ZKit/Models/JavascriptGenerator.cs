using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZKit.Models;

namespace ZKit.Models
{
    public class JavascriptGenerator : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region Objects
        private List<JSObject> _objects;
        public List<JSObject> Objects
        {
            get
            {
                if (_objects == null)
                {
                    _objects = new List<JSObject>();
                }
                return _objects;
            }
            set
            {
                if (value != _objects)
                {
                    _objects = value;
                    OnPropertyChanged("Objects");
                }
            }
        }
        #endregion

        #region ProjectName
        private string _projectName;
        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (value != _projectName)
                {
                    _projectName = value;
                    OnPropertyChanged("ProjectName");
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
        #region JavascriptGenerator(Tool tool)
        public JavascriptGenerator(Tool tool)
        {
            ToolRef = tool;
        }
        #endregion

        //================================================================================================================================================
        // Private Methods
        //================================================================================================================================================
        #region Tab(int num)
        private string Tab(int num)
        {
            StringBuilder tabs = new StringBuilder();
            string tab = "    ";
            for (int i = 0; i < num; i++)
            {
                tabs.Append(tab);
            }
            return tabs.ToString();
        }
        #endregion
        
        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region CreateJSObjects()
        public void CreateJSObjects()
        {
            foreach (var o in ToolRef.Objects)
            {
                JSObject jso = new JSObject();
                jso.Name = o.Name;
                StringBuilder c = new StringBuilder();

                foreach (var p in o.Properties)
                {
                    c.Append(Environment.NewLine).Append("this.").Append(p.Name).Append(" = new Obj('").Append(p.PropertyId).Append("');").Append(Environment.NewLine);
                    c.Append("this.").Append(p.Name).Append(".valType = '").Append(p.PropertyType.Name).Append("';").Append(Environment.NewLine);
                    if (!String.IsNullOrWhiteSpace(p.DefaultValue))
                    {
                        c.Append("this.").Append(p.Name).Append(".setDefaultVal('").Append(p.DefaultValue).Append("');").Append(Environment.NewLine);
                    }
                    else
                    {
                        c.Append("this.").Append(p.Name).Append(".setDefaultVal('").Append(p.PropertyType.GetDefaultValue()).Append("');").Append(Environment.NewLine);
                    }
                    if (!String.IsNullOrWhiteSpace(p.DefaultColor))
                    {
                        c.Append("this.").Append(p.Name).Append(".setDefaultColor('").Append(p.DefaultColor).Append("');").Append(Environment.NewLine);
                    }
                    else
                    {
                        c.Append("this.").Append(p.Name).Append(".setDefaultColor('").Append(p.PropertyType.GetDefaultValue()).Append("');").Append(Environment.NewLine);
                    }
                    if (p.PropertyType.Name.ToLower() == "bool" || p.PropertyType.Name.ToLower() == "boolean")//Set Active Color to Green - may enable custom colors later
                    {
                        c.Append("this.").Append(p.Name).Append(".setActiveColor('").Append("#CCFF99").Append("');").Append(Environment.NewLine);
                    }

                }

                c.Append(Environment.NewLine).Append("this.collectValues = function() {").Append(Environment.NewLine);
                c.Append("var returnObject = {").Append(Environment.NewLine);

                for(int i=0; i<o.Properties.Count; i++)
                {
                    Property p = o.Properties.ElementAt(i);
                    c.Append(p.Name).Append(" : this.").Append(p.Name).Append(".val");
                    if (i < o.Properties.Count - 1)
                    {
                        c.Append(" ,").Append(Environment.NewLine);
                    }
                }

                c.Append(Environment.NewLine).Append("};").Append(Environment.NewLine);//end returnObject

                c.Append(Environment.NewLine).Append("return returnObject;").Append(Environment.NewLine).Append(Environment.NewLine);
                c.Append("};").Append(Environment.NewLine);//end collectValues

                jso.Content = c.ToString();

                Objects.Add(jso);
            }
        }
        #endregion

        #region GenerateFunctionsDoc()
        public string GenerateFunctionsDoc()
        {
            StringBuilder js = new StringBuilder();

            js.Append("var request;").Append(Environment.NewLine);
            js.Append("var requests = [];").Append(Environment.NewLine);

            js.Append(GenerateSaveFunction());
            js.Append(GenerateLoadFunction());

            return js.ToString();
        }
        #endregion

        #region GenerateDoc()
        public string GenerateDoc()
        {
            StringBuilder js = new StringBuilder();
            CreateJSObjects();
            foreach (var o in Objects)
            {
                js.Append(o.CreateObjectCode()).Append(Environment.NewLine);
            }

            return js.ToString();
        }
        #endregion

        //================================================================================================================================================
        // Static Methods
        //================================================================================================================================================
        #region GenerateInitFunction()
        public static string GenerateInitFunction(List<string> objectNames)
        {
            StringBuilder js = new StringBuilder();

            foreach (string name in objectNames)
            {
                js.Append("var ").Append(name[0] + name.Substring(1)).Append("Obj;").Append(Environment.NewLine);
            }

            js.Append(Environment.NewLine).Append("function initObjs()").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            
            foreach (string name in objectNames)
            {
                js.Append("    ").Append(name[0] + name.Substring(1)).Append("Obj").Append(" = new ").Append(name).Append("();").Append(Environment.NewLine);
            }

            js.Append("}").Append(Environment.NewLine);

            return js.ToString();
        }
        #endregion

        #region GenerateReportInitFunction()
        public static string GenerateReportInitFunction(List<string> objectNames, Obj obj)
        {
            StringBuilder js = new StringBuilder();

            //declare the obj(s)
            foreach (string name in objectNames)
            {
                js.Append("var ").Append(name[0] + name.Substring(1)).Append("Obj;").Append(Environment.NewLine);
            }

            //create init function
            js.Append(Environment.NewLine).Append("function initObjs()").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);

            foreach (string name in objectNames)
            {
                string capsName = name[0] + name.Substring(1);
                //initialize the object
                js.Append("    ").Append(capsName).Append("Obj").Append(" = new ").Append(name).Append("();").Append(Environment.NewLine);

                //add dropdown values for report filters
                js.Append("    ").Append("var actions = [];").Append(Environment.NewLine);
                js.Append("    ").Append("actions.push({ text: '', value: ''});").Append(Environment.NewLine);
                js.Append("    ").Append("actions.push({ text: 'total', value: 'SUM'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions.push({ text: 'count', value: 'COUNT'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions.push({ text: 'average', value: 'AVG'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions.push({ text: 'maximum value', value: 'MAX'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions.push({ text: 'minimum value', value: 'MIN'});").Append(Environment.NewLine);

                js.Append("    ").Append(capsName).Append("._Action.setItemList(actions, true);").Append(Environment.NewLine);

                js.Append("    ").Append("var actions2 = [];").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: '', value: ''});").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: 'greater than', value: 'greater than'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: 'greater than or equal to', value: 'greater than or equal to'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: 'less than', value: 'less than'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: 'less than or equal to', value: 'less than or equal to'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: 'equal to', value: 'equal to'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: 'like', value: 'like'});").Append(Environment.NewLine);
                js.Append("    ").Append("actions2.push({ text: 'not equal to', value: 'does not equal'});").Append(Environment.NewLine);

                js.Append("    ").Append(capsName).Append("._Action2.setItemList(actions2, true);").Append(Environment.NewLine);
                
                js.Append("    ").Append("var fields = [];").Append(Environment.NewLine);

                foreach (Property p in obj.Properties)
                {
                    js.Append("    ").Append("fields.push({ text: '").Append(p.Name).Append("', value: '").Append(p.Name).Append("'});").Append(Environment.NewLine);
                }

                js.Append("    ").Append(capsName).Append("Obj").Append("._Field.setItemList(fields, true);").Append(Environment.NewLine);
                js.Append("    ").Append(capsName).Append("Obj").Append("._Field2.setItemList(fields, true);").Append(Environment.NewLine);
            }

            js.Append("}").Append(Environment.NewLine);

            return js.ToString();
        }
        #endregion

        //http://stackoverflow.com/questions/5004233/jquery-ajax-post-example-with-php
        #region GenerateLoadFunction()
        public string GenerateLoadFunction()//TODO: add filters for search
        {
            StringBuilder js = new StringBuilder();
            int tabCount = 0;
            string url = "load_" + (ToolRef.Parent != null ? ((Project)ToolRef.Parent).Name : "Project") + ".php";

            //create initiate request function
            js.Append("function loadData()").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);

            js.Append(Tab(++tabCount)).Append("var d = document.getElementById('content');").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("request = ").Append("$.ajax({").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("url: 'PHP/").Append(url).Append("',").Append(Environment.NewLine);
            js.Append(Tab(tabCount)).Append("type: 'post',").Append(Environment.NewLine);
            js.Append(Tab(tabCount)).Append("datatype: 'json',").Append(Environment.NewLine);

            js.Append(Tab(tabCount)).Append("success: function(responseText, textStatus, jqXHR)").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("{").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("var objects = JSON.parse(responseText);").Append(Environment.NewLine);
            js.Append(Tab(tabCount)).Append("for (var i = 0; i < objects.length; i++)").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("{").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("var o = objects[i];").Append(Environment.NewLine);
            js.Append(Tab(tabCount)).Append("d.innerHTML += 'ProductID: ' + o.ProductId + ' Name: ' + o.Name + ' Price: ' + o.Price + ' < br /> ';").Append(Environment.NewLine);
            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//end for
            js.Append(Tab(--tabCount)).Append("}, ").Append(Environment.NewLine);//end success

            js.Append(Tab(tabCount)).Append("fail: function(jqXHR, textStatus, errorThrown)").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("{").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("alert('An error has occured while attempting to save: ' + errorThrown);").Append(Environment.NewLine);
            js.Append(Tab(--tabCount)).Append("} ").Append(Environment.NewLine);//end fail

            js.Append(Tab(--tabCount)).Append("} );").Append(Environment.NewLine);

            //          var d = document.getElementById('content');
            //          request = $.ajax({
            //          url: 'PHP/load_Project.php',
            //      type: 'post',
            //datatype: 'json',
            //      success:
            //              function(responseText, textStatus, jqXHR)
            //                       {
            //                  var objects = JSON.parse(responseText);
            //                  console.log(objects);
            //                  console.log(responseText.length);
            //                  for (var i = 0; i < objects.length; i++)
            //                  {
            //                      var o = objects[i];
            //                      d.innerHTML += "ProductID: " + o.ProductId + " Name: " + o.Name + " Price: " + o.Price + "<br />";
            //                  }
            //                  //d.innerHTML = response;
            //              },
            //fail:
            //              function(jqXHR, textStatus, errorThrown)
            //                    {
            //                  alert('An error has occured while attempting to save: ' + errorThrown);
            //              }
            //          } );

            //          console.log(request);

            //js.Append(Tab(++tabCount)).Append("request = ").Append("$.ajax({").Append(Environment.NewLine).Append(Tab(++tabCount)).Append("url: 'PHP/").Append(url).Append("',").Append(Environment.NewLine);
            //js.Append(Tab(tabCount)).Append("type: 'post',").Append(Environment.NewLine).Append(Tab(tabCount)).Append("data: {} }");
            //js.Append(Tab(--tabCount)).Append(Environment.NewLine).Append(");").Append(Environment.NewLine);
            //js.Append(Tab(++tabCount)).Append("console.log(request);").Append(Environment.NewLine);

            //js.Append(Tab(tabCount)).Append("request.done = function(response, textStatus, jqXHR) {").Append(Environment.NewLine);

            //js.Append(Tab(tabCount)).Append("var data = document.getElementById('content');").Append(Environment.NewLine);
            //js.Append(Tab(tabCount)).Append("data.innerHTML = response;").Append(Environment.NewLine);

            //js.Append(Tab(--tabCount)).Append("};").Append(Environment.NewLine);//end request.done

            //js.Append(Tab(tabCount)).Append("request.fail = function(jqXHR, textStatus, errorThrown) {").Append(Environment.NewLine);

            //js.Append(Tab(++tabCount)).Append("alert('An error has occured while attempting to save: ' + errorThrown);").Append(Environment.NewLine);

            //js.Append(Tab(--tabCount)).Append("};").Append(Environment.NewLine);//end request.done
            
            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//close function

            return js.ToString();
        }
        #endregion

        //http://stackoverflow.com/questions/5004233/jquery-ajax-post-example-with-php
        #region GenerateSaveFunction()
        public string GenerateSaveFunction()
        {
            StringBuilder js = new StringBuilder();
            int tabCount = 0;
            string url = "save_" + (ToolRef.Parent != null ? ((Project)ToolRef.Parent).Name : "Project") + ".php";//TODO: URL for the PHP script needed here

            //create initiate request function
            js.Append("function initiateRequest()").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);

            js.Append(Tab(++tabCount)).Append("if(requests.length > 0)").Append(Environment.NewLine);
            js.Append(Tab(tabCount)).Append("{").Append(Environment.NewLine);//open if
            js.Append(Tab(++tabCount)).Append("request = requests.pop();").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("console.log(request);").Append(Environment.NewLine);
            
            js.Append(Tab(tabCount)).Append("request.done = function(response, textStatus, jqXHR) {").Append(Environment.NewLine);

            js.Append(Tab(++tabCount)).Append("if(requests.length > 0)").Append(Environment.NewLine);
            js.Append(Tab(tabCount)).Append("{").Append(Environment.NewLine);//open if
            js.Append(Tab(++tabCount)).Append("initiateRequest();").Append(Environment.NewLine);
            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//close if

            js.Append(Tab(--tabCount)).Append("};").Append(Environment.NewLine);//end request.done

            js.Append(Tab(tabCount)).Append("request.fail = function(jqXHR, textStatus, errorThrown) {").Append(Environment.NewLine);

            js.Append(Tab(++tabCount)).Append("alert('An error has occured while attempting to save: ' + errorThrown);").Append(Environment.NewLine);

            js.Append(Tab(--tabCount)).Append("};").Append(Environment.NewLine);//end request.done
            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//close if
            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//close function

            //create save function
            js.Append("function save()").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            tabCount++;

            foreach (var o in ToolRef.Objects)//add each object save to request array
            {
                js.Append(Tab(tabCount)).Append("var ").Append(o.Name.ToLower()).Append(" = ").Append(o.Name).Append("Obj.").Append("collectValues();").Append(Environment.NewLine);                
                js.Append(Tab(tabCount)).Append("requests.push(").Append("$.ajax({").Append(Environment.NewLine).Append(Tab(++tabCount)).Append("url: 'PHP/").Append(url).Append("',").Append(Environment.NewLine);
                js.Append(Tab(tabCount)).Append("type: 'post',").Append(Environment.NewLine).Append(Tab(tabCount)).Append("data: {").Append(o.Name).Append(" : JSON.stringify(").Append(o.Name.ToLower()).Append(")} })");
                js.Append(Tab(--tabCount)).Append(Environment.NewLine).Append(");").Append(Environment.NewLine);
            }

            js.Append(Tab(--tabCount));

            js.Append(Tab(++tabCount)).Append("if(requests.length > 0)").Append(Environment.NewLine);
            js.Append(Tab(tabCount)).Append("{").Append(Environment.NewLine);//open if
            js.Append(Tab(++tabCount));

            js.Append(Tab(tabCount)).Append("initiateRequest();").Append(Environment.NewLine);

            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//close if
            js.Append(Tab(tabCount)).Append("else").Append(Environment.NewLine);
            js.Append(Tab(++tabCount)).Append("{").Append(Environment.NewLine);
                        
            js.Append(Tab(++tabCount)).Append("alert('No Requests Sent.');").Append(Environment.NewLine);
            --tabCount;

            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//close else

            js.Append(Tab(--tabCount)).Append("}").Append(Environment.NewLine);//close function

            return js.ToString();
        }
        #endregion
    }
}
