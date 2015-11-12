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
        #endregion

        #region Events
        private void btn_EditText_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                isEditing = true; //start editing

                TextEditor = new TextBox();
                TextEditor.HorizontalAlignment = HorizontalAlignment.Left;
                TextEditor.Width = 141;
                TextEditor.KeyDown += TextEditor_KeyDown;
                TextEditor.Text = tb_Presenter.Text; // copy current text to editor
                Editor.Children.Add(TextEditor);    // show editor to edit      
            }
            else
            {
                tb_Presenter.Text = TextEditor.Text;   //copy edited text back to show
                TextEditor.Visibility = Visibility.Hidden; // hide editor

                isEditing = false; // stop editing
            }
        }
        void TextEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                tb_Presenter.Text = TextEditor.Text;   //copy edited text back to show
                TextEditor.Visibility = Visibility.Hidden; // hide editor
                isEditing = false; // stop editing
            }
        }
        private void tb_Presenter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && e.ClickCount ==2)
            {
                isEditing = true; //start editing

                TextEditor = new TextBox();
                TextEditor.HorizontalAlignment = HorizontalAlignment.Left;
                TextEditor.Width = 141;
                TextEditor.KeyDown += TextEditor_KeyDown;
                TextEditor.Text = tb_Presenter.Text; // copy current text to editor
                Editor.Children.Add(TextEditor);    // show editor to edit  
            }
        }
        #endregion
    }
}
