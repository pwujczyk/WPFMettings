using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace WPFControls
{
    public class RichTextBox : System.Windows.Controls.RichTextBox
    {
        #region Private Members

        private bool _preventDocumentUpdate;
        private bool _preventTextUpdate;

        #endregion //Private Members

        #region Constructors

        public RichTextBox()
        {
        }

        public RichTextBox(System.Windows.Documents.FlowDocument document)
          : base(document)
        {

        }

        #endregion //Constructors

        #region Properties

        public static readonly DependencyProperty SelectedTextProperty= DependencyProperty.Register("SelectedText", typeof(string), typeof(RichTextBox), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTextPropertyChanged, CoerceSelectedTextProperty, true, UpdateSourceTrigger.Explicit));

        private static object CoerceSelectedTextProperty(DependencyObject d, object value)
        {
            var rtb = (d as RichTextBox);
            var selection = (d as RichTextBox).Selection;
            if (!selection.IsEmpty)
            {
                EditingCommands.ToggleBullets.Execute(null, rtb);
            }
            return null;
        }

        private static void OnSelectedTextPropertyChanged(DependencyObject d,  DependencyPropertyChangedEventArgs e)
        {
           //return null
        }

        public string SelectedText
        {
            get
            {
                return (string)GetValue(SelectedTextProperty);
            }
            set
            {
                SetValue(SelectedTextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(RichTextBox), new FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextPropertyChanged, CoerceTextProperty, true, UpdateSourceTrigger.LostFocus));
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RichTextBox)d).UpdateDocumentFromText();
        }

        private static object CoerceTextProperty(DependencyObject d, object value)
        {
            return value ?? "";
        }

        
        #region TextFormatter

        public static readonly DependencyProperty TextFormatterProperty = DependencyProperty.Register("TextFormatter", typeof(ITextFormatter), typeof(RichTextBox), new FrameworkPropertyMetadata(new PWXamlFormatter(), OnTextFormatterPropertyChanged));
        public ITextFormatter TextFormatter
        {
            get
            {
                return (ITextFormatter)GetValue(TextFormatterProperty);
            }
            set
            {
                SetValue(TextFormatterProperty, value);
            }
        }

        private static void OnTextFormatterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RichTextBox richTextBox = d as RichTextBox;
            if (richTextBox != null)
                richTextBox.OnTextFormatterPropertyChanged((ITextFormatter)e.OldValue, (ITextFormatter)e.NewValue);
        }

        protected virtual void OnTextFormatterPropertyChanged(ITextFormatter oldValue, ITextFormatter newValue)
        {
            this.UpdateTextFromDocument();
        }

        #endregion //TextFormatter

        #endregion //Properties

        #region Methods

        protected override void OnTextChanged(System.Windows.Controls.TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            UpdateTextFromDocument();
        }

        private void UpdateTextFromDocument()
        {
            if (_preventTextUpdate)
                return;

            _preventDocumentUpdate = true;
            Text = TextFormatter.GetText(Document);
            _preventDocumentUpdate = false;
        }

        private void UpdateDocumentFromText()
        {
            if (_preventDocumentUpdate)
                return;

            _preventTextUpdate = true;
            TextFormatter.SetText(Document, Text);
            _preventTextUpdate = false;
        }

        /// <summary>
        /// Clears the content of the RichTextBox.
        /// </summary>
        public void Clear()
        {
            Document.Blocks.Clear();
        }

        public override void BeginInit()
        {
            base.BeginInit();
            // Do not update anything while initializing. See EndInit
            _preventTextUpdate = true;
            _preventDocumentUpdate = true;
        }

        public override void EndInit()
        {
            base.EndInit();
            _preventTextUpdate = false;
            _preventDocumentUpdate = false;
            // Possible conflict here if the user specifies 
            // the document AND the text at the same time 
            // in XAML. Text has priority.
            if (!string.IsNullOrEmpty(Text))
                UpdateDocumentFromText();
            else
                UpdateTextFromDocument();
        }

        #endregion //Methods
    }
}
