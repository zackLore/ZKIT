using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ZKit.Controls;
using ZKit.Models;
using ZKit.Services;

namespace ZKit.ViewModels
{
    public class vmObjectCreator : ViewModelBase
    {
        //=======================================================================================================
        // Properties
        //=======================================================================================================
        ObjectCreator viewRef;

        #region CurrentObject
        private Obj _currentObject;
        public Obj CurrentObject
        {
            get
            {
                if (_currentObject == null)
                {
                    _currentObject = new Obj();
                }
                return _currentObject;
            }
            set
            {
                if (value != _currentObject)
                {
                    _currentObject = value;
                    OnPropertyChanged("CurrentObject");
                }
            }
        }
        #endregion

        #region CurrentProps
        private ObservableCollection<PropertyCreator> _currentProps;
        public ObservableCollection<PropertyCreator> CurrentProps
        {
            get
            {
                if (_currentProps == null)
                {
                    _currentProps = new ObservableCollection<PropertyCreator>();
                }
                return _currentProps;
            }
            set
            {
                if (value != _currentProps)
                {
                    _currentProps = value;
                    OnPropertyChanged("CurrentProps");
                }
            }
        }
        #endregion

        #region CurrentTool
        private Tool _currentTool;
        public Tool CurrentTool
        {
            get
            {
                return _currentTool;
            }
            set
            {
                if (value != _currentTool)
                {
                    _currentTool = value;
                    OnPropertyChanged("CurrentTool");
                }
            }
        }
        #endregion

        //=======================================================================================================
        // Constructors
        //=======================================================================================================
        #region vmObjectCreator(Tool tool)
        public vmObjectCreator(ObjectCreator viewRef, Tool tool)
        {
            this.viewRef = viewRef;
            CurrentTool = tool;
            if (tool.Objects.Count > 0)
            {
                CurrentObject = tool.Objects.ElementAt(0);
            }
            else
            {
                CurrentObject = tool.AddNewObject();
            }
            AddNewProperty();
        }
        #endregion

        //=======================================================================================================
        // Commands
        //=======================================================================================================
        #region AddProperty
        private DelegateCommand<object> _addProperty;
        public DelegateCommand<object> AddProperty
        {
            get
            {
                if (_addProperty == null)
                {
                    _addProperty = new DelegateCommand<object>(OnAddProperty);
                }
                return _addProperty;
            }
        }
        #endregion

        #region GenerateCode
        private DelegateCommand<object> _generateCode;
        public DelegateCommand<object> GenerateCode
        {
            get
            {
                if (_generateCode == null)
                {
                    _generateCode = new DelegateCommand<object>(OnGenerateCode);
                }
                return _generateCode;
            }
        }
        #endregion

        //=======================================================================================================
        // Delegate Command Methods
        //=======================================================================================================
        #region OnAddProperty(object parameter)
        protected void OnAddProperty(object parameter)
        {
            AddNewProperty();
        }
        #endregion

        //=======================================================================================================
        // Private Methods
        //=======================================================================================================
        #region AddNewProperty()
        private void AddNewProperty()
        {
            if (CurrentObject != null)
            {
                Property prop = new Property(CurrentObject);
                CurrentObject.AddNewProperty(prop);
                CurrentProps.Add(new PropertyCreator(prop));
                OnPropertyChanged("CurrentProps");
            }
        }
        #endregion

        #region OnGenerateCode(object parameter)
        protected void OnGenerateCode(object parameter)
        {
            switch (CurrentTool.Language)
            {
                case "javascript":
                    //Check for source files
                    string fileStatus = DocumentCreator.AddFile(@"D:\Zkit\SourceFiles", @"Output\Scripts", "CommonObjects.js");

                    //Create CSS
                    CSSGenerator css = new CSSGenerator(CurrentTool);
                    string cssStatus = DocumentCreator.CreateDocument(@"Output\Styles", (CurrentTool.Parent != null ? ((Project)CurrentTool.Parent).Name : "Project") + "Styles", "css", css.GenerateDoc(), true);

                    PHPGenerator php = new PHPGenerator(CurrentTool);
                    //set php info here
                    php.DB = "zkit";
                    php.Pass = "1234";
                    php.Server = "localhost";
                    php.User = "zkit";

                    //Create PHP Scripts
                    string phpStatus = DocumentCreator.CreateDocument(@"Output\PHP", "connect", "php", php.GenerateConnectDoc(), true);
                    if (!String.IsNullOrWhiteSpace(phpStatus))
                    {
                        MessageBox.Show(phpStatus);
                    }
                    phpStatus = DocumentCreator.CreateDocument(@"Output\PHP", "save_" + (CurrentTool.Parent != null ? ((Project)CurrentTool.Parent).Name : "Project"), "php", php.GenerateSaveDoc(), true);
                    if (!String.IsNullOrWhiteSpace(phpStatus))
                    {
                        MessageBox.Show(phpStatus);
                    }
                    phpStatus = DocumentCreator.CreateDocument(@"Output\PHP", "load_" + (CurrentTool.Parent != null ? ((Project)CurrentTool.Parent).Name : "Project"), "php", php.GenerateLoadDoc(), true);
                    if (!String.IsNullOrWhiteSpace(phpStatus))
                    {
                        MessageBox.Show(phpStatus);
                    }

                    //Create Javascript files
                    JavascriptGenerator js = new JavascriptGenerator(CurrentTool);
                    js.ProjectName = CurrentTool.Parent != null ? ((Project)CurrentTool.Parent).Name : "Project";//Needs to be the name of the objecst
                    string jsStatus = DocumentCreator.CreateDocument(@"Output\Scripts", js.ProjectName, "js", js.GenerateDoc(), true);                    
                    if (!String.IsNullOrWhiteSpace(jsStatus))
                    {
                        MessageBox.Show(jsStatus);
                    }
                    jsStatus = DocumentCreator.CreateDocument(@"Output\Scripts", js.ProjectName + "_functions", "js", js.GenerateFunctionsDoc(), true);
                    if (!String.IsNullOrWhiteSpace(jsStatus))
                    {
                        MessageBox.Show(jsStatus);
                    }

                    HTMLGenerator g = CurrentTool.HTML;
                    g.ProjectName = CurrentTool.Parent != null ? ((Project)CurrentTool.Parent).Name : "Project";
                    g.ToolRef = CurrentTool;
                    g.ScriptIncludes.Add("CommonObjects.js");
                    g.ScriptIncludes.Add(js.ProjectName + ".js");
                    g.ScriptIncludes.Add("https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js");
                    g.ScriptIncludes.Add(g.ProjectName + "_functions.js");
                    g.GenerateDoc();//Creates begining point of doc

                    //Load js objects
                    var htmlTag = g.Tags.Where(x => x.TagName == "html").FirstOrDefault(); 
                    if (htmlTag != null && htmlTag.GetType() == typeof(HTMLTagBase))
                    {
                        var headTag = ((HTMLTagBase)htmlTag).Children.Where(x => x.TagName == "head").FirstOrDefault();
                        if (headTag != null)
                        {
                            var scriptTag = ((HTMLTagBase)headTag).Children.Where(x => x.TagName == "script").FirstOrDefault();
                            HTMLTagBase script = null;
                            if (scriptTag != null)
                            {
                                script = (HTMLTagBase)scriptTag;
                            }
                            else//create a script tag
                            {
                                script = new HTMLTagBase();
                                script.TagName = "script";
                                headTag.Children.Add(script);
                            }
                            List<string> names = new List<string>();
                            foreach (var o in CurrentTool.Objects)
                            {
                                names.Add(o.Name);
                            }
                            script.InnerHTML = JavascriptGenerator.GenerateInitFunction(names);
                        }

                        var bodyTag = ((HTMLTagBase)htmlTag).Children.Where(x => x.TagName == "body").FirstOrDefault();
                        if (bodyTag != null)
                        {
                            bodyTag.Elements.Add("onload", "initObjs();");
                        }
                    }
                    else//no html tag found.  Something is wrong.
                    {
                    }

                    string htmlDoc = g.ReOutputDoc();

                    string status = DocumentCreator.CreateDocument("Output", CurrentTool.Parent != null ? ((Project)CurrentTool.Parent).Name : "Project", "html", htmlDoc, true);
                    if (!String.IsNullOrWhiteSpace(status))
                    {
                        MessageBox.Show(status);
                    }

                    //TODO: Finish adding all report elements - tags in reports.html, additional scripts
                    //***** Create Report Document
                    HTMLTagBase reportDoc = g.CreateReportPage();
                    var reportHeadTag = ((HTMLTagBase)reportDoc).Children.Where(x => x.TagName == "head").FirstOrDefault();
                    if (reportHeadTag != null)
                    {
                        var scriptTag = ((HTMLTagBase)reportHeadTag).Children.Where(x => x.TagName == "script").FirstOrDefault();
                        HTMLTagBase script = null;
                        if (scriptTag != null)
                        {
                            script = (HTMLTagBase)scriptTag;
                        }
                        else//create a script tag
                        {
                            script = new HTMLTagBase();
                            script.TagName = "script";
                            reportHeadTag.Children.Add(script);
                        }
                        List<string> names = new List<string>();
                        foreach (var o in CurrentTool.Objects)
                        {
                            names.Add(o.Name);
                        }
                        script.InnerHTML = JavascriptGenerator.GenerateReportInitFunction(names, CurrentTool.Objects.ElementAt(0));
                    }

                    var reportBodyTag = ((HTMLTagBase)reportDoc).Children.Where(x => x.TagName == "body").FirstOrDefault();
                    if (reportBodyTag != null)
                    {
                        reportBodyTag.Elements["onload"] += "initObjs();";
                    }

                    status = DocumentCreator.CreateDocument("Output", "Reports", "html", reportDoc.GenerateTag(), true);
                    if (!String.IsNullOrWhiteSpace(status))
                    {
                        MessageBox.Show(status);
                    }

                    //***** Create SQL Scripts
                    SQLGenerator sql = new SQLGenerator(CurrentTool);
                    string sqlStatus = DocumentCreator.CreateDocument("Output", "CREATE_TABLES", "sql", sql.GenerateDoc(), true);

                    MessageBox.Show("Files Created!");
                    break;
            }
        }
        #endregion
    }
}
