using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using ZKit.Models;

namespace ZKit.Services
{
    public class XMLGenerator : ModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        private readonly string DOCTYPE = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        #region DocumentName
        private string _documentName;
        public string DocumentName
        {
            get
            {
                return _documentName.ToLower().Replace(" ", "_");
            }
            set
            {
                if (value != _documentName)
                {
                    _documentName = value;
                    OnPropertyChanged("DocumentName");
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

        #region ProjectRef
        private Project _projectRef;
        public Project ProjectRef
        {
            get
            {
                return _projectRef;
            }
            set
            {
                if (value != _projectRef)
                {
                    _projectRef = value;
                    OnPropertyChanged("ProjectRef");
                }
            }
        }
        #endregion

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region XMLGenerator
        public XMLGenerator(Project projRef)
        {
            ProjectRef = projRef;
            DocumentName = ProjectRef.Name;
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region GenerateDoc
        public string GenerateDoc()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append(DOCTYPE).Append(Environment.NewLine);

            //Project Tag
            HTMLTagBase proj = new HTMLTagBase();
            proj.TagName = ProjectRef.GetType().Name.ToString().ToLower();
            proj.Elements.Add("name", DocumentName);

            foreach (var t in ProjectRef.Tools)
            {
                HTMLTagBase tool = new HTMLTagBase();
                tool.TagName = t.GetType().Name.ToString().ToLower();
                tool.Elements.Add("name", t.Name);
                
                foreach (var o in t.Objects)
                {
                    HTMLTagBase ob = new HTMLTagBase();
                    ob.TagName = o.GetType().Name.ToString().ToLower();
                    ob.Elements.Add("name", o.Name);

                    foreach (var p in o.Properties)
                    {
                        HTMLTagBase prop = new HTMLTagBase();
                        prop.TagName = p.GetType().Name.ToString().ToLower();
                        prop.Elements.Add("name", p.Name);
                        prop.Elements.Add("type", p.PropertyType.Name.ToString());//May need to change to handle the propery object
                        if (p.DefaultValue != null)
                        {
                            prop.Elements.Add("default_value", p.DefaultValue.ToString());
                        }
                        if (p.DefaultColor != null)
                        {
                            prop.Elements.Add("default_color", p.DefaultColor.ToString());
                        }

                        ob.Children.Add(prop);
                    }
                    tool.Children.Add(ob);
                }
                proj.Children.Add(tool);
            }

            //Create the xml content
            xml.Append(proj.GenerateTag());

            return xml.ToString();
        }
        #endregion

        //DOES NOT WORK
        public HTMLTagBase GenDoc(object thing)
        {
            HTMLTagBase tag = new HTMLTagBase();

            Type myType = thing.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (PropertyInfo pInfo in props)
            {
                object propValue = pInfo.GetValue(thing, null);

                string propString = pInfo.ToString();
                Type propType = pInfo.PropertyType;
                string propName = propString.Split(' ')[1];

                if (propName == "Name")
                {
                    tag.TagName = propValue.ToString();
                    continue;
                }

                if(propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    Type t = propType.GetElementType();
                    //foreach (var item in propValue)
                    //{

                    //}
                }

                if (propType.IsArray)
                {
                    Array a = (Array)propValue;
                    foreach (var item in a)
                    {
                        tag.Children.Add(GenDoc(pInfo));
                    }
                }

                if (!propType.IsPrimitive)
                {
                    tag.Children.Add(GenDoc(pInfo));
                }
                else
                {
                    tag.InnerHTML += propValue;
                }
             }

            return tag;
        }

    }
}
