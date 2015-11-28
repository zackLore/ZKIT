using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ZKit.Models;

namespace ZKit.Views
{
    /// <summary>
    /// Interaction logic for NewToolDialog.xaml
    /// </summary>
    public partial class NewToolDialog : Window
    {
        Tool tool;
        MainWindow mainRef;
        public ToolData.ToolTypes CurrentToolType;
        public NewToolDialog()
        {
            InitializeComponent();
        }

        public NewToolDialog(Tool t)
        {
            InitializeComponent();
            tool = t;
            LoadTools();
        }

        public NewToolDialog(MainWindow main)
        {
            InitializeComponent();
            mainRef = main;
            LoadTools();
        }

        private void LoadTools()
        {
            this.drp_tool.Items.Clear();
            foreach (var tt in Enum.GetValues(typeof(ToolData.ToolTypes)))
            {
                this.drp_tool.Items.Add(tt);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (b.Content.ToString() == "Okay")
            {
                //CurrentToolType = ToolData.ToolTypes.Javascript;
                switch (CurrentToolType)
                {
                    case ToolData.ToolTypes.Javascript:
                        tool.Language = "javascript";
                        tool.Name = CurrentToolType.ToString();
                        break;
                }

                this.Close();
            }
            if (b.Content.ToString() == "Cancel")
            {
                CurrentToolType = ToolData.ToolTypes.None;
                this.Close();
            }
        }
    }
}
