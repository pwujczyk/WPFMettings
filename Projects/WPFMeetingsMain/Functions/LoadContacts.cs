using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Functions
{
    class LoadContacts : BaseFunction, IFunction
    {

        private const string NewName = "Load Contacts";
        private const string NewSection = "Panels";
        private const string NewTab = "Panels";

        IPluginMainWindow UserControl;

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

        public LoadContacts(IPluginMainWindow userControl)
        {
            UserControl = userControl;
        }

        void IFunction.Method()
        {

            this.LoadPlugin(UserControl);
            
        }
    }
}
