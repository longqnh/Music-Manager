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
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView : UserControl
    {
        public AlbumView()
        {
            InitializeComponent();
            
        }

        #region Properties
        private string[] _CoverPath;
        private int _AlbumCount;
        private int _Current = 0;
        #endregion

        #region Events
        private void imgPreAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _Current--;
            imgCurAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current], UriKind.Relative));
            imgNextAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current + 1], UriKind.Relative));
            if (_Current - 1 < 0) // this current album is the last album
                imgPreAlbum.Source = null;
            else
                imgPreAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current - 1], UriKind.Relative));
        }

        private void imgNextAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgPreAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current], UriKind.Relative));
            _Current++;
            imgCurAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current], UriKind.Relative));
            if (_Current == _AlbumCount - 1) // this current album is the last album
                imgNextAlbum.Source = null;
            else
                imgNextAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current + 1], UriKind.Relative));
        }
        #endregion

        #region Methods
        public void RecCoverPath(string[] Passer)
        {
            _CoverPath = (string[])Passer.Clone();
            _AlbumCount = _CoverPath.Length;
            imgCurAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current], UriKind.Relative));
            imgNextAlbum.Source = new BitmapImage(new Uri(_CoverPath[_Current + 1], UriKind.Relative));
        }
        #endregion
    }
}
