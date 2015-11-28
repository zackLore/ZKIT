using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using ZKit.Controls;
using ZKit.Models;
using ZKit.Services;
using ZKit.Views;

namespace ZKit.ViewModels
{
    public class vmMainWindow : ViewModelBase
    {
        //================================================================================================================================================
        // Properties
        //================================================================================================================================================
        #region CurrentItem
        private object _currentItem;
        public object CurrentItem
        {
            get
            {
                return _currentItem;
            }
            set
            {
                _currentItem = value;
                UpdateCurrentItem();
                OnPropertyChanged("CurrentItem");
            }
        }
        #endregion

        #region Items
        private ObservableCollection<object> _items;
        public ObservableCollection<object> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<object>();
                }
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }
        #endregion

        #region Projects
        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get
            {
                if (_projects == null)
                {
                    _projects = new ObservableCollection<Project>();
                }
                return _projects;
            }
            set
            {
                _projects = value;
                OnPropertyChanged("Projects");
            }
        }
        #endregion

        #region Tools
        private ObservableCollection<Tool> _tools;
        public ObservableCollection<Tool> Tools
        {
            get
            {
                if (_tools == null)
                {
                    _tools = new ObservableCollection<Tool>();
                }
                return _tools;
            }
            set
            {
                _tools = value;
                OnPropertyChanged("Tools");
            }
        }
        #endregion

        #region TreeViewItems
        private ObservableCollection<TreeViewItem> _treeViewItems;
        public ObservableCollection<TreeViewItem> TreeViewItems
        {
            get
            {
                if (_treeViewItems == null)
                {
                    _treeViewItems = new ObservableCollection<TreeViewItem>();
                }
                return _treeViewItems;
            }
            set
            {
                if (value != _treeViewItems)
                {
                    _treeViewItems = value;
                    OnPropertyChanged("TreeViewItems");
                }
            }
        }
        #endregion

        //private List<Project> Projects = new List<Project>();
        //public List<Tool> Tools = new List<Tool>();
        //public Project CurrentProject;
        //public Tool CurrentTool;
        //public object CurrentObject;

        //================================================================================================================================================
        // Constructors
        //================================================================================================================================================
        #region vmMainWindow()
        public vmMainWindow(MainWindow main)
        {
            mainRef = main;
        }
        #endregion

        //================================================================================================================================================
        // Commands
        //================================================================================================================================================
        #region AddProject()
        private DelegateCommand _addProject;
        public DelegateCommand AddProject
        {
            get
            {
                if (_addProject == null)
                {
                    _addProject = new DelegateCommand(OnAddProject);
                }
                return _addProject;
            }
        }
        #endregion

        #region AddTool
        private DelegateCommand<object> _addTool;
        public DelegateCommand<object> AddTool
        {
            get
            {
                if (_addTool == null)
                {
                    _addTool = new DelegateCommand<object>(OnAddTool);
                }
                return _addTool;
            }
        }
        #endregion

        #region LoadProject
        private DelegateCommand<object> _loadProject;
        public DelegateCommand<object> LoadProject
        {
            get
            {
                if (_loadProject == null)
                {
                    _loadProject = new DelegateCommand<object>(OnLoadProject);
                }
                return _loadProject;
            }
        }
        #endregion

        #region SaveProject
        private DelegateCommand<object> _saveProject;
        public DelegateCommand<object> SaveProject
        {
            get
            {
                if (_saveProject == null)
                {
                    _saveProject = new DelegateCommand<object>(OnSaveProject);
                }
                return _saveProject;
            }
        }
        #endregion

        //================================================================================================================================================
        // Delegate Command Methods
        //================================================================================================================================================
        #region OnAddProject(object parameter)
        protected void OnAddProject(object parameter)
        {
            Project p = new Project();
            Projects.Add(p);
            //Items.Add(p);
            NewProjectDialog npd = new NewProjectDialog(p);
            npd.ShowDialog();
            TreeViewItem tvi = new TreeViewItem();
            tvi.Header = p.Name;
            tvi.DataContext = p;
            TreeViewItems.Add(tvi);

            CurrentItem = tvi;
        }
        #endregion
        
        #region OnAddTool(object parameter)
        protected void OnAddTool(object parameter)
        {
            if (CurrentItem != null)
            {
                if (CurrentItem.GetType() == typeof(TreeViewItem))
                {
                    TreeViewItem tvItem = (TreeViewItem)CurrentItem;
                    Tool tool;
                    Project tempProject = null;
                    if (tvItem.DataContext.GetType() == typeof(Project))
                    {
                        tempProject = (Project)tvItem.DataContext;
                        tool = new Tool(tempProject);
                        tempProject.Tools.Add(tool);
                    }
                    else
                    {
                        tool = new Tool(null);
                    }
                    
                    NewToolDialog ntd = new NewToolDialog(tool);
                    ntd.ShowDialog();
                    UpdateCurrentItem();
                    OnPropertyChanged("CurrentItem");
                    
                    TreeViewItem tvi = new TreeViewItem();
                    tvi.Header = tool.Name;
                    tvi.DataContext = tool;

                    TreeViewItem tvi_ = null;
                    if (tempProject != null)
                    {
                        tvi_ = TreeViewItems.Where(x => x.Header.ToString() == tempProject.Name).FirstOrDefault();
                    }

                    if (tvi_ != null)
                    {
                        tvi_.Items.Add(tvi);
                    }
                    else
                    {
                        TreeViewItems.Add(tvi);
                    }

                    Tools.Add(tool);
                    CurrentItem = tvi;
                }
            }
            else
            {
                Tool tool = new Tool();
                //object tool = new object();
                NewToolDialog ntd = new NewToolDialog(tool);
                ntd.ShowDialog();
                UpdateCurrentItem();
                OnPropertyChanged("CurrentItem");

                TreeViewItem tvi = new TreeViewItem();
                tvi.Header = tool.Name;
                tvi.DataContext = tool;

                //Items.Add(tool);
                TreeViewItems.Add(tvi);
                Tools.Add(tool);
                CurrentItem = tvi;
            }

        }
        #endregion

        #region OnLoadProject(object parameter)
        protected void OnLoadProject(object parameter)
        {

        }
        #endregion

        #region OnSaveProject(object parameter)
        protected void OnSaveProject(object parameter)
        {
            if (Projects.Count <= 0)
            {
                MessageBox.Show("No Project Found");
                return;
            }
            TreeViewItem tvi = (TreeViewItem)CurrentItem;
            if (tvi.DataContext.GetType() == typeof(Project))
            {
                Project p = (Project)tvi.DataContext;
                MessageBox.Show(DocumentCreator.SaveProject(p));

                //Testing
                //XMLGenerator x = new XMLGenerator(p);
                //x.GenDoc(p);
            }
        }
        #endregion

        //================================================================================================================================================
        // Public Methods
        //================================================================================================================================================
        #region UpdateCurrentItem
        public void UpdateCurrentItem()
        {
            if (CurrentItem == null)
            {
                return;
            }
            if (CurrentItem.GetType() == typeof(TreeViewItem))
            {
                TreeViewItem tvi = (TreeViewItem)CurrentItem;
                if (tvi.DataContext.GetType() == typeof(Tool))
                {
                    Tool tool = (Tool)tvi.DataContext;
                    switch (tool.Language.ToLower())
                    {
                        case "javascript":
                            ObjectCreator oc = new ObjectCreator(tool);
                            mainRef.main_content.Content = oc;
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
