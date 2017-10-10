using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Functions
{
    class Delete : BaseFunction, IFunction
    {
        private const string NewName = "Delete";
        private const string NewSection = "Meeting";
        private const string NewTab = "Meeting";

        MetingsVM VM;

        string IFunction.FunctionName
        {
            get
            {
                return NewName;
            }
        }

        string IFunction.SectionName
        {
            get
            {
                return NewSection;
            }
        }

        string IFunction.TabName
        {
            get
            {
                return NewTab;
            }
        }

        public Delete(MetingsVM vm)
        {
            VM = vm;
        }

        void IFunction.Method()
        {
            MessageBoxResult result =MessageBox.Show("For sure?","Confirmation", MessageBoxButton.YesNo);
            if (result.HasFlag(MessageBoxResult.Yes))
            {
              
            
            }
        }
    }
}
