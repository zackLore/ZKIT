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
using ZKit.ViewModels;

namespace ZKit.Controls
{
    /// <summary>
    /// Interaction logic for PropertyCreator.xaml
    /// </summary>
    public partial class PropertyCreator : UserControl
    {
        public PropertyCreator(Property prop)
        {
            InitializeComponent();
            this.DataContext = new vmPropertyCreator(prop);
        }
    }
}
