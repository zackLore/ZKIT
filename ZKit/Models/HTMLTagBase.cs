using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKit.Models
{
    public class HTMLTagBase
    {
        //================================================================================================================================================
        // Property
        //================================================================================================================================================
        #region Child
        private HTMLTagBase _child;
        public HTMLTagBase Child
        {
            get
            {
                if (Children.Count > 0)
                {
                    Children.ElementAt(0);
                }
                return _child;
            }
        }
        #endregion

        #region Children
        private ObservableCollection<HTMLTagBase> _children;
        public ObservableCollection<HTMLTagBase> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new ObservableCollection<HTMLTagBase>();
                }
                return _children;
            }
            set
            {
                if (value != _children)
                {
                    _children = value;
                    OnPropertyChanged("Children");
                }
            }
        }
        #endregion

        #region Class
        private string _class;
        public string Class
        {
            get
            {
                return _class;
            }
            set
            {
                if (value != _class)
                {
                    _class = value;
                    OnPropertyChanged("Class");
                }
            }
        }
        #endregion

        #region CloseTag
        private string _closeTag;
        public string CloseTag
        {
            get
            {
                _closeTag = GenerateCloseTag();
                return _closeTag;
            }
            set
            {
                if (value != _closeTag)
                {
                    _closeTag = value;
                    OnPropertyChanged("CloseTag");
                }
            }
        }
        #endregion

        #region Elements
        private Dictionary<string, string> _elements;
        public Dictionary<string, string> Elements
        {
            get
            {
                if (_elements == null)
                {
                    _elements = new Dictionary<string, string>();
                }
                return _elements;
            }
            set
            {
                if (value != _elements)
                {
                    _elements = value;
                    OnPropertyChanged("Elements");
                }
            }
        }
        #endregion

        #region Events
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new ObservableCollection<Event>();
                }
                return _events;
            }
            set
            {
                if (value != _events)
                {
                    _events = value;
                    OnPropertyChanged("Events");
                }
            }
        }
        #endregion

        #region Id
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        #endregion

        #region InnerHTML
        private string _innerHTML;
        public string InnerHTML
        {
            get
            {
                return _innerHTML;
            }
            set
            {
                if (value != _innerHTML)
                {
                    _innerHTML = value;
                    OnPropertyChanged("InnerHTML");
                }
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

        #region OpenTag
        private string _openTag;
        public string OpenTag
        {
            get
            {
                _openTag = GenerateOpenTag();
                return _openTag;
            }
            set
            {
                if (value != _openTag)
                {
                    _openTag = value;
                    OnPropertyChanged("OpenTag");
                }
            }
        }
        #endregion

        #region Parent
        private HTMLTagBase _parent;
        public HTMLTagBase Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (value != _parent)
                {
                    _parent = value;
                    OnPropertyChanged("Parent");
                }
            }
        }
        #endregion

        #region TagName
        private string _tagName;
        public string TagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                if (value != _tagName)
                {
                    _tagName = value;
                    OnPropertyChanged("TagName");
                }
            }
        }
        #endregion

        #region TagType
        private string _tagType;
        public string TagType
        {
            get
            {
                return _tagType;
            }
            set
            {
                if (value != _tagType)
                {
                    _tagType = value;
                    OnPropertyChanged("TagType");
                }
            }
        }
        #endregion

        #region Value
        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region HTMLTagBase()
        public HTMLTagBase()
        {
            Children.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(
                delegate (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                    {
                        if (sender.GetType() == typeof(HTMLTagBase))
                        {
                            HTMLTagBase temp = (HTMLTagBase)sender;
                            temp.Parent = this;
                        }
                    }
                }
            );
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region OutputTag()
        public string OutputTag()
        {
            StringBuilder html = new StringBuilder();

            if (TagName == "br")
            {
                return "<br />";
            }

            if (TagName == "input")
            {
                html.Append(OpenTag.Replace(">", "/>"));
                return html.ToString();
            }
            else
            {
                html.Append(OpenTag);
            }

            if (Children != null)
            {
                foreach (var child in Children)
                {
                    html.Append(child.GenerateTag());
                }
            }
            html.Append(InnerHTML);            
            html.Append(CloseTag).Append(Environment.NewLine);

            return html.ToString();
        }
        #endregion

        #region GenerateCloseTag()
        public string GenerateCloseTag()
        {
            StringBuilder html = new StringBuilder();

            html.Append("</").Append(TagName).Append(">").Append(Environment.NewLine);

            return html.ToString();
        }
        #endregion

        #region GenerateOpenTag()
        public string GenerateOpenTag()
        {
            StringBuilder html = new StringBuilder();

            html.Append("<").Append(TagName);
            if (!String.IsNullOrWhiteSpace(Id))
            {
                html.Append(" id='").Append(Id).Append("'");
            }
            if (!String.IsNullOrWhiteSpace(Name))
            {
                html.Append(" name='").Append(Name).Append("'");
            }
            if (!String.IsNullOrWhiteSpace(TagType))
            {
                html.Append(" type='").Append(TagType).Append("'");
            }
            if (!String.IsNullOrWhiteSpace(Class))
            {
                html.Append(" class='").Append(Class).Append("'");
            }
            if (!String.IsNullOrWhiteSpace(Value))
            {
                html.Append(" value='").Append(Value).Append("'");
            }
            //TODO: Fix this functionality
            //if (Events != null && Events.Count > 0)
            //{
            //    foreach (var e in Events)
            //    {
            //        html.Append(" on").Append(e.Name).Append("=\"").Append(e.Content).Append(";\"");
            //    }
            //}

            html.Append(">").Append(Environment.NewLine);

            return html.ToString();
        }
        #endregion

        #region GenerateTag()
        public string GenerateTag()
        {
            StringBuilder html = new StringBuilder();

            if (TagName == "br")
            {
                return "<br />";
            }
            
            html.Append("<").Append(TagName);
            foreach (var e in Elements)
            {
                html.Append(" ").Append(e.Key).Append("=\"").Append(e.Value).Append("\"");
            }

            if (TagName == "input")
            {
                html.Append("/>");
                return html.ToString();
            }

            html.Append(">").Append(Environment.NewLine);

            if (Children != null)
            {
                foreach (var child in Children)
                {
                    html.Append(child.GenerateTag());
                }
            }

            html.Append(Environment.NewLine).Append(InnerHTML);
            html.Append(CloseTag).Append(Environment.NewLine);

            return html.ToString();
        }
        #endregion

        //================================================================================================================================================
        // PropertyChanged Implementation
        //================================================================================================================================================
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
