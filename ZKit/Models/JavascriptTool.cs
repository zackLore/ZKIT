using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZKit.Services;

namespace ZKit.Models
{
    public class JavascriptTool : Tool
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region JavascriptTool()
        public JavascriptTool()
        {
            Language = "JavaScript";
            LoadObjects();
        }
        #endregion

        private void LoadObjects()
        {
            Obj o = new Obj();
            this.Objects.Add(o);
        }
    }
}
