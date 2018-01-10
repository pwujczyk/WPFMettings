using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFRibbonWorkplaceContracts;

namespace WPFMeetingsWorkplacePlugin.Plugins.ContactsPlugin.Functions
{
    class LoadMeetings : BaseFunction, IFunction
    {
        private const string NewName = "LoadMeetings";
        private const string NewSection = "Contact";
        private const string NewTab = "Contact";

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

        public LoadMeetings(Type userControl)
        {
            type = userControl;
        }

        void IFunction.Method()
        {
            var x = Activator.CreateInstance(type) as IPlugin;
            this.LoadPlugin(x);

        }
    }
}
