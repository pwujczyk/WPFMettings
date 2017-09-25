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
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : UserControl, IPluginMainWindow
    {
        public Contacts()
        {
            InitializeComponent();
        }

        public List<IFunction> Functions
        {
            get
            {
                return new List<IFunction>();
            }
        }

        public bool Main
        {
            get
            {
                return false;
            }
        }
    }
}
