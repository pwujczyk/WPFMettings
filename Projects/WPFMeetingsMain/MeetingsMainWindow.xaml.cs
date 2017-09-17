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
using WPFMeetingsWorkplacePlugin.Functions;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin
{
    /// <summary>
    /// Interaction logic for MeetingsMainWindow.xaml
    /// </summary>
    public partial class MeetingsMainWindow : UserControl, IPluginMainWindow
    {
        MetingsMainWindowVM VM;
        public MeetingsMainWindow()
        {
            InitializeComponent();
            VM = new WPFMeetingsWorkplacePlugin.MetingsMainWindowVM();
            this.DataContext = VM;
        }

        public List<IFunction> Functions
        {
            get
            {
                var list = new List<IFunction>();
                list.Add(new Save(this.VM));
                list.Add(new New(this.VM));
                return list;
            }
        }

        private void tvMeetings_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            VM.SelectedMeeting = (Meeting)e.NewValue;
        }
    }
}
