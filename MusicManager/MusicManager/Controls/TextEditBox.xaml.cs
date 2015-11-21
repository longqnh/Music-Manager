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
        public TextEditBox()
        {
            InitializeComponent();
        }
        #region Properties
        private bool isEditing = false;
        private TextBox TextEditor;
        private string _FullText;
        #endregion

        #region Events
        private void btnEditText_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                TextEditor = new TextBox();
                TextEditor.HorizontalAlignment = HorizontalAlignment.Left;
                TextEditor.Width = 141;
                TextEditor.KeyDown += TextEditor_KeyDown;

                TextEditor.Text = tbText.Text; // copy current text to editor
                Editor.Children.Add(TextEditor);    // show editor to edit     

                tbText.Visibility = Visibility.Hidden; //hide text
                isEditing = true; //start editing
            }
            else
            {
                tbText.Text = TextEditor.Text;   //copy edited text back to show
                Editor.Children.Remove(TextEditor);
                //TextEditor.Visibility = Visibility.Hidden; // hide editor
                
                isEditing = false; // stop editing
                tbText.Visibility = Visibility.Visible; // show text
            }
        }
        void TextEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                tbText.Text = TextEditor.Text;   //copy edited text back to show
                Editor.Children.Remove(TextEditor);
                //TextEditor.Visibility = Visibility.Hidden; // hide editor

                isEditing = false; // stop editing
                tbText.Visibility = Visibility.Visible; // show text
            }
        }
        private void tbText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && e.ClickCount ==2) //double click to start editing
            {                
                TextEditor = new TextBox();
                TextEditor.HorizontalAlignment = HorizontalAlignment.Left;
                TextEditor.Width = 141;
                TextEditor.KeyDown += TextEditor_KeyDown;

                TextEditor.Text = tbText.Text; // copy current text to editor
                Editor.Children.Add(TextEditor);    // show editor to edit  

                tbText.Visibility = Visibility.Hidden;
                isEditing = true; //start editing
            }
        }
        #endregion

        #region Methods
        public void CheckLength()
        {
            if (tbText.Text.Length > 23)
            {
                _FullText = tbText.Text; // back up

                tbText.Text = tbText.Text.Remove(23) + "..."; // set new title
                ToolTip aTooltip = new ToolTip();
                aTooltip.Content = _FullText;

                tbText.ToolTip = aTooltip;
            }
        }
        #endregion
    }
}
