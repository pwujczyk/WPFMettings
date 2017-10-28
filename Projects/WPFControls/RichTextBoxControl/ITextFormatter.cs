using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFControls
{
    public interface ITextFormatter
    {
        string GetText(FlowDocument document);
        void SetText(FlowDocument document, string text);
    }
}
