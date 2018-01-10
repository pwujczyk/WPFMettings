using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.MeetingPlugin.Functions
{
    class LoadContacts : BaseFunction, IFunction
    {

        private const string NewName = "Load Contacts";
        private const string NewSection = "Panels";
        private const string NewTab = "Panels";

      
        Type type;

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

        //public LoadContacts(IPlugin userControl)
        //{
        //    UserControl = userControl;
        //}

        public LoadContacts(Type userControl)
        {
            type = userControl;
        }

        void IFunction.Method()
        {
            var x=Activator.CreateInstance(type) as IPlugin;
            this.LoadPlugin(x);
            
        }
    }
}
