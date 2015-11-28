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

using ZKit.Models;
using ZKit.Views;
using ZKit.ViewModels;

namespace ZKit.Controls
{
    /// <summary>
    /// Interaction logic for ObjectCreat.xaml
    /// </summary>
    public partial class ObjectCreator : UserControl
    {
        MainWindow main_ref;
        //Obj CurrentObject;
        public ObjectCreator()
        {
            InitializeComponent();
        }

        public ObjectCreator(Tool tool)
        {
            InitializeComponent();
            DataContext = new vmObjectCreator(this, tool);
        }

        public ObjectCreator(MainWindow main, Obj obj)
        {
            InitializeComponent();
            main_ref = main;
            //CurrentObject = obj;
        }
    }
}
