using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace WPFMeetingsWorkplacePlugin
{
     public class PWXamlFormatter : ITextFormatter
    {
        public string GetText(System.Windows.Documents.FlowDocument document)
        {
            TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                tr.Save(ms, DataFormats.Xaml);
                var t = tr.Text;
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public void SetText(System.Windows.Documents.FlowDocument document, string text)
        {
            try
            {
                //if the text is null/empty clear the contents of the RTB. If you were to pass a null/empty string
                //to the TextRange.Load method an exception would occur.
                if (String.IsNullOrEmpty(text))
                {
                    document.Blocks.Clear();
                }
                else
                {
                    TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
                    var bytes = Encoding.UTF8.GetBytes(text);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        tr.Load(ms, DataFormats.Xaml);
                    }
                }
            }
            catch
            {
                throw new InvalidDataException("Data provided is not in the correct Xaml format.");
            }
        }
    }
}
