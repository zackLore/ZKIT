using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZKit.Models;

namespace ZKit.Services
{
    public class HTMLGenerator : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        private readonly string DOCTYPE = "<!DOCTYPE html>";

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

        #region ScriptIncludes
        private List<string> _scriptIncludes;
        public List<string> ScriptIncludes
        {
            get
            {
                if (_scriptIncludes == null)
                {
                    _scriptIncludes = new List<string>();
                }
                return _scriptIncludes;
            }
            set
            {
                if (value != _scriptIncludes)
                {
                    _scriptIncludes = value;
                    OnPropertyChanged("ScriptIncludes");
                }
            }
        }
        #endregion

        #region Tags
        private List<HTMLTagBase> _tags;
        public List<HTMLTagBase> Tags
        {
            get
            {
                if (_tags == null)
                {
                    _tags = new List<HTMLTagBase>();
                }
                return _tags;
            }
            set
            {
                if (value != _tags)
                {
                    _tags = value;
                    OnPropertyChanged("Tags");
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
        #region HTMLGenerator
        public HTMLGenerator(Tool toolRef)
        {
            ToolRef = ToolRef;
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region GenerateDoc
        public string GenerateDoc()
        {
            StringBuilder html = new StringBuilder();
            html.Append(DOCTYPE).Append(Environment.NewLine);

            HTMLTagBase doc = StartDoc();

            HTMLTagBase head = new HTMLTagBase();
            head.TagName = "head";
            //*****add scripts and styles here
            //head.Children.Add(CreateStyle());
            //head.InnerHTML = CreateStyle();//TODO: Remove
            //head.InnerHTML += Environment.NewLine;
            //*****add javascript links here
            head.InnerHTML += CreateScriptLinks();

            doc.Children.Add(head);

            HTMLTagBase body = new HTMLTagBase();
                body.TagName = "body";
                body.Children.Add(CreateHeader());

            HTMLTagBase links = new HTMLTagBase();
            links.TagName = "div";
            links.Elements.Add("class", "links");
            links.Children.Add(CreateLinks());

            HTMLTagBase content = new HTMLTagBase();
                content.TagName = "div";
                content.Class = "content";//TODO: Remove
            content.Elements.Add("class", "content");
                content.Children.Add(CreateForm());

            body.Children.Add(links);
            body.Children.Add(content);

            doc.Children.Add(body);

            //html.Append(doc.OutputTag());//TODO: Remove
            html.Append(doc.GenerateTag());

            Tags.Add(doc);

            return html.ToString();
        }
        #endregion

        //TODO: rework this to handle multiple objects
        #region CreateForm()
        public HTMLTagBase CreateForm()
        {
            HTMLTagBase form = new HTMLTagBase();
            form.TagName = "form";

            foreach (var o in ToolRef.Objects)
            {
                HTMLTagBase lbl = new HTMLTagBase();
                lbl.TagName = "h2";
                lbl.InnerHTML = o.Name;

                HTMLTagBase br = new HTMLTagBase() { TagName = "br" };

                form.Children.Add(lbl);
                form.Children.Add(br);

                //output table here
                HTMLTagBase table = new HTMLTagBase();
                table.TagName = "table";
                foreach (var p in o.Properties)
                {
                    HTMLTagBase row = new HTMLTagBase();
                    row.TagName = "tr";

                    HTMLTagBase cell1 = new HTMLTagBase();
                        cell1.TagName = "td";
                        cell1.InnerHTML = p.Name;

                    HTMLTagBase cell2 = new HTMLTagBase();
                        cell2.TagName = "td";

                    HTMLTagBase txtBox = new HTMLTagBase();
                        txtBox.TagName = "input";
                        txtBox.TagType = "text";//TODO: Remove
                    txtBox.Elements.Add("type", "text");
                        txtBox.Id = p.PropertyId;//TODO: Remove
                    txtBox.Elements.Add("id", p.PropertyId);

                    cell2.Children.Add(txtBox);

                    row.Children.Add(cell1);
                    row.Children.Add(cell2);

                    table.Children.Add(row);
                }
                form.Children.Add(table);

                HTMLTagBase save = new HTMLTagBase();
                save.TagName = "input";
                save.TagType = "button";//TODO: Remove
                save.Elements.Add("type", "button");
                save.Value = "Save";//TODO: Remove
                save.Elements.Add("value", "Save");
                save.Events.Add(new Event() { Name = "click", Content = @"alert('Hi honey bunny!')" });//TODO: Remove
                save.Elements.Add("onclick", "save();");

                form.Children.Add(br);
                form.Children.Add(save);

            }
            return form;
        }
        #endregion

        #region CreateHeader()
        public HTMLTagBase CreateHeader()
        {
            HTMLTagBase iconTag = new HTMLTagBase();
                iconTag.TagName = "div";
            iconTag.Elements.Add("id", "icon");
            iconTag.Elements.Add("class", "icon");
                iconTag.Id = "icon";//TODO: Remove
                iconTag.Class = "icon";//TODO: Remove
            iconTag.InnerHTML = ProjectName;

            //add link functionality here

            HTMLTagBase header = new HTMLTagBase();
                header.TagName = "div";
            header.Elements.Add("id", "header");
            header.Elements.Add("class", "header");
                header.Id = "header";//TODO: Remove
            header.Class = "header";//TODO: Remove
            header.Children.Add(iconTag);

            return header;
        }
        #endregion

        #region CreateLinks()
        private HTMLTagBase CreateLinks()
        {
            HTMLTagBase links = new HTMLTagBase();
            links.TagName = "ul";

            foreach (var o in ToolRef.Objects)
            {
                HTMLTagBase listItem = new HTMLTagBase();
                listItem.TagName = "li";

                HTMLTagBase link = new HTMLTagBase();
                link.TagName = "a";
                link.Elements.Add("href", o.Name + ".html");
                link.InnerHTML = o.Name;

                listItem.Children.Add(link);
                links.Children.Add(listItem);
            }

            HTMLTagBase li = new HTMLTagBase();
            li.TagName = "li";

            HTMLTagBase l = new HTMLTagBase();
            l.TagName = "a";
            l.Elements.Add("href", "Reports.html");
            l.InnerHTML = "Reports";

            li.Children.Add(l);
            links.Children.Add(li);

            return links;
        }
        #endregion

        #region CreateReportPage()
        public HTMLTagBase CreateReportPage()//NOT FINISHED - Copied from GenerateDoc but not yet modified
        {
            StringBuilder html = new StringBuilder();
            html.Append(DOCTYPE).Append(Environment.NewLine);

            HTMLTagBase doc = StartDoc();

            HTMLTagBase head = new HTMLTagBase();
            head.TagName = "head";
            //*****add reference links here
            head.InnerHTML += CreateScriptLinks();

            doc.Children.Add(head);

            HTMLTagBase body = new HTMLTagBase();
            body.TagName = "body";
            body.Elements.Add("onload", "loadData();");
            body.Children.Add(CreateHeader());

            HTMLTagBase links = new HTMLTagBase();
            links.TagName = "div";
            links.Elements.Add("class", "links");
            links.Children.Add(CreateLinks());

            HTMLTagBase content = new HTMLTagBase();
            content.TagName = "div";
            content.Class = "content";//TODO: Remove
            content.Elements.Add("class", "content");

            HTMLTagBase mainContent = new HTMLTagBase();
            mainContent.TagName = "div";
            mainContent.Elements.Add("id", "content");

            content.Children.Add(mainContent);
            //content.Children.Add(CreateForm());

            body.Children.Add(links);
            body.Children.Add(content);

            doc.Children.Add(body);

            html.Append(doc.GenerateTag());

            //Tags.Add(doc);

            return doc;
        }
        #endregion

        //TODO: finish this
        #region GenerateReportPage()
        public string GenerateReportPage()//NOT FINISHED - Copied from GenerateDoc but not yet modified
        {
            StringBuilder html = new StringBuilder();
            html.Append(DOCTYPE).Append(Environment.NewLine);

            HTMLTagBase doc = StartDoc();

            HTMLTagBase head = new HTMLTagBase();
            head.TagName = "head";
            //*****add reference links here
            head.InnerHTML += CreateScriptLinks();

            doc.Children.Add(head);

            HTMLTagBase body = new HTMLTagBase();
            body.TagName = "body";
            body.Elements.Add("onload", "loadData();");
            body.Children.Add(CreateHeader());

            HTMLTagBase links = new HTMLTagBase();
            links.TagName = "div";
            links.Elements.Add("class", "links");
            links.Children.Add(CreateLinks());

            HTMLTagBase content = new HTMLTagBase();
            content.TagName = "div";
            content.Class = "content";//TODO: Remove
            content.Elements.Add("class", "content");

            HTMLTagBase mainContent = new HTMLTagBase();
            mainContent.TagName = "div";
            mainContent.Elements.Add("id", "content");

            content.Children.Add(mainContent);
            //content.Children.Add(CreateForm());

            body.Children.Add(links);
            body.Children.Add(content);

            doc.Children.Add(body);
            
            html.Append(doc.GenerateTag());

            //Tags.Add(doc);

            return html.ToString();
        }
        #endregion

        #region CreateScriptLinks()
        public string CreateScriptLinks()
        {
            StringBuilder html = new StringBuilder();
            string folder = "Scripts\\";

            html.Append(@"<link rel='stylesheet' type='text/css' href='Styles/").Append(ProjectName).Append("Styles.css'></link>").Append(Environment.NewLine);
            foreach (var s in ScriptIncludes)
            {
                html.Append(@"<script type='text/javascript' src='").Append(s.StartsWith("http") ? "" : folder).Append(s).Append("'></script>").Append(Environment.NewLine);
            }

            return html.ToString();
        }
        #endregion

        #region CreateStyle()
        public HTMLTagBase CreateStyle()
        {
            HTMLTagBase style = new HTMLTagBase();
            style.TagName = "style";
            StringBuilder styleText = new StringBuilder();
            styleText.Append(".icon").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("height:40px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("width:120px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("font-size:20px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("position:absolute;").Append(Environment.NewLine);
            styleText.Append("  ").Append("left:0px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("background:#AAAAAA;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            styleText.Append(".header").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("height:40px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("width:1028px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("margin:0 auto;").Append(Environment.NewLine);
            styleText.Append("  ").Append("background:#777777;").Append(Environment.NewLine);
            styleText.Append("  ").Append("position:relative;").Append(Environment.NewLine);
            styleText.Append("  ").Append("left:0px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("top:0px;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            styleText.Append(".content").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("width:1028px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("margin:0 auto;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            styleText.Append(".links").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("height:25px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("width:1028px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("margin:0 auto;").Append(Environment.NewLine);
            styleText.Append("  ").Append("background:#DDDDDD;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            styleText.Append(".links ul").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("list-style: none;").Append(Environment.NewLine);
            styleText.Append("  ").Append("padding: 2;").Append(Environment.NewLine);
            styleText.Append("  ").Append("margin:2;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            styleText.Append(".links li").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("display: inline;").Append(Environment.NewLine);
            styleText.Append("  ").Append("float: left;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            styleText.Append(".links a").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("text-decoration: none;").Append(Environment.NewLine);
            styleText.Append("  ").Append("display: block;").Append(Environment.NewLine);
            styleText.Append("  ").Append("color: #000000;").Append(Environment.NewLine);
            styleText.Append("  ").Append("background:#DDDDDD;").Append(Environment.NewLine);
            styleText.Append("  ").Append("width: 100px;").Append(Environment.NewLine);
            styleText.Append("  ").Append("font-family: arial;").Append(Environment.NewLine);
            styleText.Append("  ").Append("font-size: 1.1em;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            styleText.Append(".links a:hover").Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
            styleText.Append("  ").Append("background: #EEEEEE;").Append(Environment.NewLine);
            styleText.Append("  ").Append("color: #333333;").Append(Environment.NewLine);
            styleText.Append("}").Append(Environment.NewLine);

            style.InnerHTML = styleText.ToString();

            return style;
        }
        #endregion

        #region StartDoc()
        public HTMLTagBase StartDoc()
        {
            HTMLTagBase htmlTag = new HTMLTagBase();
            htmlTag.TagName = "html";
            
            return htmlTag;
        }
        #endregion

        #region ReOutputDoc()
        public string ReOutputDoc()
        {
            var doc = Tags.Where(x => x.TagName == "html").FirstOrDefault();
            if (doc != null)
            {
                return doc.GenerateTag();
            }
            return GenerateDoc();
        }
        #endregion
     }
}
