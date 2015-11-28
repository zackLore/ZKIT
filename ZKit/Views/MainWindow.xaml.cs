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
using System.Windows.Navigation;
using System.Windows.Shapes;

using ZKit.Controls;
using ZKit.Models;
using ZKit.ViewModels;

namespace ZKit.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Project> Projects = new List<Project>();
        public List<Tool> Tools = new List<Tool>();
        public Project CurrentProject;
        public Tool CurrentTool;
        public object CurrentObject;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new vmMainWindow(this);
        }

        private void LoadTool()
        {
            if (CurrentTool != null)
            {
                StackPanel sp = new StackPanel();
                foreach (Obj o in CurrentTool.Objects)
                {
                    sp.Children.Add(new ObjectCreator(this, o));
                }
                this.main_content.Content = sp;
            }
        }

        private void LoadProjects()
        {
            Project js = new Project();
            js.Tools.Add(new JavascriptTool());

            Projects.Add(js);

            CurrentProject = Projects.ElementAt(0);
        }

        private void AddNewProject()
        {
            //Projects.Add(new Project());
            //CurrentProject = Projects.ElementAt(Projects.Count - 1);
            //NewProjectDialog pd = new NewProjectDialog(this);
            //pd.ShowDialog();
            //if (CurrentProject.Name == "")
            //{
            //    Projects.Remove(CurrentProject);
            //    CurrentProject = null;
            //}
        }

        public void AddNewTool()
        {
            NewToolDialog ntd = new NewToolDialog(this);
            ntd.ShowDialog();
            ToolData.ToolTypes tt = ntd.CurrentToolType;

            Tool temp;
            if (CurrentProject != null)
            {
                temp = CurrentProject.Tools.Where(x => x.Language.ToLower() == tt.ToString().ToLower()).FirstOrDefault();
            }
            else
            {
                temp = Tools.Where(x => x.Language.ToLower() == tt.ToString().ToLower()).FirstOrDefault();
            }

            if (temp == null)
            {
                switch (tt)
                {
                    case ToolData.ToolTypes.Javascript:
                        JavascriptTool jt = new JavascriptTool();
                        if (CurrentProject != null)
                        {
                            CurrentProject.Tools.Add(jt);
                        }
                        else
                        {
                            Tools.Add(jt);
                        }
                        break;
                }
            }
        }

        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            //if (CurrentProject != null)
            //{
            //    LoadTreeView();
            //}
        }

        public void LoadTreeView()
        {
            tv_current_project.Items.Clear();
            if (CurrentProject != null)
            {
                TreeViewItem tv = new TreeViewItem();
                tv.Header = CurrentProject.Name;
                tv.MouseRightButtonUp += TreeViewItem_MouseRightButtonUp;

                foreach (Tool t in CurrentProject.Tools)
                {
                    TreeViewItem tool = new TreeViewItem();
                    tool.Header = t.Language;
                    tool.MouseRightButtonUp += TreeViewItem_MouseRightButtonUp;

                    foreach (Obj o in t.Objects)
                    {
                        TreeViewItem tvi = new TreeViewItem();
                        tvi.Header = o.Name;
                        tvi.MouseRightButtonUp += TreeViewItem_MouseRightButtonUp;

                        tool.Items.Add(tvi);
                    }
                    tv.Items.Add(tool);
                }
                this.tv_current_project.Items.Add(tv);
            }

            foreach (Tool t in Tools)
            {
                foreach (Obj o in t.Objects)
                {
                    TreeViewItem tvi = new TreeViewItem();
                    tvi.Header = o.Name;

                    this.tv_current_project.Items.Add(tvi);
                }
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string item = "";
            if (sender.GetType() == typeof(MenuItem))
            {
                MenuItem mi = (MenuItem)sender;
                item = mi.Header.ToString();
            }

            switch (item)
            {
                case "New Project":
                    AddNewProject();
                    //LoadTreeView();
                    break;
                case "New Tool":
                    //For now just Javascript tool.  TODO: Add form to select specific language.
                    AddNewTool();
                    //LoadTreeView();
                    break;
            }
        }

        private void TreeViewItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            string item = "";
            if (sender.GetType() == typeof(TreeViewItem))
            {
                TreeViewItem mi = (TreeViewItem)sender;
                item = mi.Header.ToString();
            }

            switch (item)
            {
                case "New Project":
                    break;
                case "New Tool":
                    break;
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            vmMainWindow temp = (vmMainWindow)this.DataContext;
            if (e.NewValue != null && e.NewValue.GetType() == typeof(TreeViewItem))
            {
                TreeViewItem tvi = (TreeViewItem)e.NewValue;
                temp.CurrentItem = tvi;
            }
            temp.UpdateCurrentItem();
            temp.OnPropertyChanged("CurrentItem");
            //if (sender.GetType() == typeof(TreeView))
            //{
            //    TreeView tv = (TreeView)sender;
            //    TreeViewItem tvi = (TreeViewItem)e.NewValue;
            //    if (tvi != null)
            //    {
            //        if (CurrentProject != null)
            //        {
            //            CurrentTool = CurrentProject.Tools.Where(x => x.Language.ToLower() == tvi.Header.ToString().ToLower()).FirstOrDefault();
            //        }
            //        else
            //        {
            //            CurrentTool = Tools.Where(x => x.Language.ToLower() == tvi.Header.ToString().ToLower()).FirstOrDefault();
            //        }
            //        LoadTool();
            //    }
            //}
        }
        
    }
}
