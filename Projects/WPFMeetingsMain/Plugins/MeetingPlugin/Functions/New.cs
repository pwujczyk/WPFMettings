using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Meetings;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Functions
{
    class New : BaseFunction, IFunction
    {

        private const string NewName = "New";
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

        public Type ButtonType
        {
            get
            {
                return typeof(Button);
            }
        }

        public New(MetingsVM vm)
        {
            VM = vm;
        }

        void IFunction.Method()
        {

            VM.AddNew();
            VM.ClearFields();
        }
    }
}
