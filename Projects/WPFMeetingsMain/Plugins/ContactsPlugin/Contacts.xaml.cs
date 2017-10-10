using MeetingsDTO;
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
using WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin;
using WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin.Functions;
using WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin
{
    /// <summary>
    /// Interaction logic for Contacts.xaml
    /// </summary>
    public partial class Contacts : UserControl, IPlugin//, IPluginMainWindow
    {
        ContactsVM VM;
        public Contacts()
        {
            InitializeComponent();
            VM = new ContactsVM();
            this.DataContext = VM;
        }

        public List<IFunction> Functions
        {
            get
            {
                var list = new List<IFunction>();
                list.Add(new New(this.VM));
                list.Add(new Save(this.VM));
                list.Add(new LoadMeetings(typeof(Meetings)));
                return list;
            }
        }

        public bool Main
        {
            get
            {
                return false;
            }
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.VM.SelectedContact = (Contact)e.NewValue;
        }
    }
}
