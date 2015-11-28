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
using ZKit.ViewModels;

namespace ZKit.Views
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog : Window
    {
        //Project project;
        public NewProjectDialog()
        {
            InitializeComponent();
        }

        public NewProjectDialog(Project p)
        {
            InitializeComponent();
            this.DataContext = new vmNewProjectDialog(p);

            //project = p;
            //txt_name.Text = p.Name;
            txt_name.Focus();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Button b = (Button)sender;
            //if(b.Content.ToString() == "Okay")
            //{
            //    project.Name = txt_name.Text;
            //    this.Close();
            //}
            //if(b.Content.ToString() == "Cancel")
            //{
            //    this.Close();
            //}
        }
    }
}
