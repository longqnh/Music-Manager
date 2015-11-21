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
using System.Windows.Forms;
using System.IO;

namespace MusicManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SongList.Main = PlayList.Main = TrackInfo.Main = this;
        }

        #region Properties
        private string _Media;
        private List<string> _MusicList; //list of files that are music file
        #endregion

        #region Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _MusicList = new List<string>();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                _Media = fbd.SelectedPath;
                DirectoryInfo dir = new DirectoryInfo(_Media);
                foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (file.Extension == ".wmv" || file.Extension == ".mp3" || file.Extension == ".mp4"
                        || file.Extension == ".flac" || file.Extension == ".wma"
                        || file.Extension == ".m4a" || file.Extension == ".wav")
                        _MusicList.Add(file.FullName);
                }
            }
        }
        #endregion
    }
}
