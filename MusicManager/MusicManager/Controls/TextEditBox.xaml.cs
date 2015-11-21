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

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for TextEditBox.xaml
    /// </summary>
    public partial class TextEditBox : UserControl
    {
        #region Constructor
        public TextEditBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private bool _isEditing = false;
        private TextBox _TextEditor;
        private string _FullText;
        #endregion

        #region Events
        private void btnEditText_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEditing)
            {
                _TextEditor = new TextBox();
                _TextEditor.HorizontalAlignment = HorizontalAlignment.Left;
                _TextEditor.Width = 161;
                _TextEditor.KeyDown += TextEditor_KeyDown;

                _TextEditor.Text = this._FullText; // copy current text to editor
                Editor.Children.Add(_TextEditor);    // show editor to edit     

                tbText.Visibility = Visibility.Hidden; //hide text
                _isEditing = true; //start editing
            }
            else
            {
                tbText.Text = _TextEditor.Text;   //copy edited text back to show
                this.CheckLength();

                Editor.Children.Remove(_TextEditor);
                
                _isEditing = false; // stop editing
                tbText.Visibility = Visibility.Visible; // show text
            }
        }
        void TextEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                tbText.Text = _TextEditor.Text;   //copy edited text back to show
                this.CheckLength();

                Editor.Children.Remove(_TextEditor);

                _isEditing = false; // stop editing
                tbText.Visibility = Visibility.Visible; // show text
            }
        }
        private void tbText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && e.ClickCount ==2) //double click to start editing
            {                
                _TextEditor = new TextBox();
                _TextEditor.HorizontalAlignment = HorizontalAlignment.Left;
                _TextEditor.Width = 161;
                _TextEditor.KeyDown += TextEditor_KeyDown;

                _TextEditor.Text = this._FullText; // copy current text to editor
                Editor.Children.Add(_TextEditor);    // show editor to edit  

                tbText.Visibility = Visibility.Hidden; //hide text
                _isEditing = true; //start editing
            }
        }
        #endregion

        #region Methods
        public void CheckLength()
        {
            _FullText = tbText.Text; // back up

            if (tbText.Text.Length > 15)
            {               
                tbText.Text = tbText.Text.Remove(15) + "..."; // set new title
                ToolTip aTooltip = new ToolTip();
                aTooltip.Content = _FullText;

                tbText.ToolTip = aTooltip;
            }
            else
            {
                tbText.ToolTip = null;
            }
        }
        #endregion
    }
}
