using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZKit.Models;

namespace ZKit.Services
{
    public class CSSGenerator : ModelBase//***** NOT FINISHED
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region Styles
        private Dictionary<string, Dictionary<string, string>> _styles;
        public Dictionary<string, Dictionary<string, string>> Styles
        {
            get
            {
                if (_styles == null)
                {
                    _styles = new Dictionary<string, Dictionary<string, string>>();
                }
                return _styles;
            }
            set
            {
                if (value != _styles)
                {
                    _styles = value;
                    OnPropertyChanged("Styles");
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
        #region CSSGenerator
        public CSSGenerator(Tool toolRef)
        {
            ToolRef = toolRef;
            CreateStyles();
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region AddStyleContent(string name, string content)
        public void AddStyleContent(string styleName, string contentName, string content)
        {
            int index = GetStyleIndexByName(styleName);
            if (index >= 0)
            {
                Dictionary<string, string> temp = Styles.ElementAt(index).Value;
                temp.Add(contentName, content);
            }
        }
        #endregion

        #region CreateStyles()
        public void CreateStyles()
        {
            Styles.Clear();

            Dictionary<string, string> icon = new Dictionary<string, string>();
            icon.Add("height", "40px");
            icon.Add("width", "120px");
            icon.Add("font-size", "20px");
            icon.Add("position", "absolute");
            icon.Add("left", "0px");
            icon.Add("background", "#AAAAAA");

            Styles.Add(".icon", icon);

            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("height", "40px");
            header.Add("width", "1028px");
            header.Add("margin", "0 auto");
            header.Add("background", "#777777");
            header.Add("position", "relative");
            header.Add("left", "0px");
            header.Add("top", "0px");

            Styles.Add(".header", header);

            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("width", "1028px");
            content.Add("margin", "0 auto");

            Styles.Add(".content", content);

            Dictionary<string, string> links = new Dictionary<string, string>();
            links.Add("height", "25px");
            links.Add("width", "1028px");
            links.Add("margin", "0 auto");
            links.Add("background", "#DDDDDD");

            Styles.Add(".links", links);

            Dictionary<string, string> links_ul = new Dictionary<string, string>();
            links_ul.Add("list-style", "none");
            links_ul.Add("padding", "2");
            links_ul.Add("margin", "2");

            Styles.Add(".links ul", links_ul);

            Dictionary<string, string> links_li = new Dictionary<string, string>();
            links_li.Add("display", "inline");
            links_li.Add("float", "left");

            Styles.Add(".links li", links_li);

            Dictionary<string, string> links_a = new Dictionary<string, string>();
            links_a.Add("text-decoration", "none");
            links_a.Add("display", "block");
            links_a.Add("color", "#000000");
            links_a.Add("background", "#DDDDDD");
            links_a.Add("width", "120px");
            links_a.Add("font-family", "arial");
            links_a.Add("font-size", "1.1em");

            Styles.Add(".links a", links_a);

            Dictionary<string, string> links_aHover = new Dictionary<string, string>();
            links_aHover.Add("background", "#EEEEEE");
            links_aHover.Add("color", "#333333");

            Styles.Add(".links a:hover", links_aHover);
            
        }
        #endregion

        #region GenerateDoc()
        public string GenerateDoc()
        {
            StringBuilder css = new StringBuilder();

            foreach (KeyValuePair<string, Dictionary<string, string>> style in Styles)
            {
                css.Append(style.Key).Append(Environment.NewLine).Append("{").Append(Environment.NewLine);
                foreach (KeyValuePair<string, string> content in style.Value)
                {
                    css.Append("    ").Append(content.Key).Append(" : ").Append(content.Value).Append(";").Append(Environment.NewLine);
                }
                css.Append("}").Append(Environment.NewLine).Append(Environment.NewLine);
            }

            return css.ToString();
        }
        #endregion

        #region GetStyleIdByName(string name)
        public int GetStyleIndexByName(string name)
        {
            if (Styles.Keys.Contains(name))
            {
                for (int i = 0; i < Styles.Count; i++)
                {
                    if (Styles.ElementAt(i).Key == name)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        #endregion
    }
}
