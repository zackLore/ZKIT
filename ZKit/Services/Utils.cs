using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using ZKit.Views;
using ZKit.Models;

namespace ZKit.Services
{
    public static class Utils
    {
        /// <summary>
        /// Takes a tree view item and returns the specified item.
        /// </summary>
        /// <param name="tv">The TreeView to search.</param>
        /// <param name="parentName">Name of the parent item in the tree node.  Blank or null if there is no parent.</param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static TreeViewItem GetTreeViewItem(TreeView tv, string parentName, string itemName)
        {
            foreach (TreeViewItem tvi in tv.Items)
            {
                if (!String.IsNullOrWhiteSpace(parentName))
                {
                    if (tvi.Header.ToString() == parentName)
                    {
                        foreach (TreeViewItem tvi_ in tvi.Items)
                        {
                            if (tvi_.Header.ToString() == itemName)
                            {
                                return tvi_;
                            }
                        }
                    }
                }
                else
                {
                    if (tvi.Header.ToString() == itemName)
                    {
                        return tvi;
                    }
                }
            }
            return null;
        }

        public static JavascriptTool ToJavascriptTool(object t)
        {
            try
            {
                JavascriptTool jt = (JavascriptTool)t;
                return jt;
            }
            catch (Exception ex)
            {
                //TODO: error handling
                return null;
            }
        }

        public static Tool ToTool(object t)
        {
            string type = t.GetType().ToString();
            switch (type.ToLower())
            {
                case "javascript":
                    return ToJavascriptTool(t);
            }
            return null;
        }

    }
}
