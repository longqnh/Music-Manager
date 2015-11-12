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
    /// Interaction logic for PlaylistView.xaml
    /// </summary>
    public partial class PlaylistView : UserControl
    {
        public PlaylistView()
        {
            InitializeComponent();
        }

        int i = 1;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayingSong song = new PlayingSong();
            song.Song_No.Text = i.ToString() + ".";
            song.Song_Title.Text += i.ToString();
            pnPlayList.Children.Add(song);
            i++;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Music starting......");
        }
    }
}
