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
using MusicManager.Classes;
using HundredMilesSoftware.UltraID3Lib;
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
        private List<FileInfo> _MusicList; //list of files that are music file
        #endregion

        #region Events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _MusicList = new List<FileInfo>();
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
                        _MusicList.Add(file);                    
                }
                this.CreateAlbumList(_MusicList);
            }
        }
        #endregion

        #region Methods
        private void CreateAlbumList(List<FileInfo> MusicList) // find all album of music list
        {
            List<Album> AlbumList = new List<Album>();
            foreach(FileInfo musicfile in MusicList)
            {
                UltraID3 Tag = new UltraID3();
                Tag.Read(musicfile.FullName);
                string albumName = Tag.Album; // current musicfile's albumname
                int i;
                for (i = 0; i < AlbumList.Count; i++) 
                {
                    if (albumName == AlbumList[i].Name) // found it's album in the albumlist
                    {
                        //create a song
                        Song aSong = new Song(Tag.Title, Tag.Artist, Tag.Album, Tag.Year, Tag.Genre, 
                            Tag.TrackNum, Tag.GetMPEGTrackInfo().AverageBitRate, Tag.Duration);
                        aSong.Path = musicfile.FullName;
                        AlbumList[i].TrackList.Add(aSong);
                        break;
                    }                   
                }
                if (i == AlbumList.Count) // current musicfile's albumname not in the album list
                {
                    //create new album
                    Album NewAlbum = new Album();
                    NewAlbum.Name = albumName;
                    //NewAlbum.CoverPath =???;
                    AlbumList.Add(NewAlbum);

                    //create a song
                    Song aSong = new Song(Tag.Title, Tag.Artist, Tag.Album, Tag.Year, Tag.Genre, 
                            Tag.TrackNum, Tag.GetMPEGTrackInfo().AverageBitRate, Tag.Duration);
                    aSong.Path = musicfile.FullName;
                    AlbumList[i].TrackList.Add(aSong);

                    //add song to album
                    NewAlbum.TrackList.Add(aSong);
                }
            }
            this.SongList.ctAlbumView.ReceiveAlbumList(AlbumList, this);
        }
        #endregion
    }
}
