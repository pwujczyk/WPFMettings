using BusinessLogic.Meeting;
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
using WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Functions;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin
{
    /// <summary>
    /// Interaction logic for MeetingsMainWindow.xaml
    /// </summary>
    public partial class Meetings : UserControl, IPluginMainWindow
    {
        MetingsVM VM;
        public Meetings()
        {
            InitializeComponent();
            VM = new MetingsVM();
            this.DataContext = VM;
        }

        public List<IFunction> Functions
        {
            get
            {
                var list = new List<IFunction>();
                list.Add(new Save(this.VM));
                list.Add(new New(this.VM));
                list.Add(new Delete(this.VM));
                list.Add(new Bullet(this.VM));
                list.Add(new LoadContacts(typeof(Contacts)));
                return list;
            }
        }

        private void tvMeetings_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            VM.SelectedMeeting = (Meeting)e.NewValue;
        }

        bool IPluginMainWindow.Main 
        {
            get
            {
                return true;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(
       txtBeforeNotesXaml.Document.ContentStart,
       txtBeforeNotesXaml.Document.ContentEnd
   );
           var x= textRange.Text;


        }
    }
}
