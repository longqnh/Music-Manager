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
using System.Data.SQLite;
using AudioPlayerSample;
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
            try
            {
                CreateAlbumList();
                CreateArtistList();

                
            }
            catch
            {

            }
            mWorker.DoWork += mWorker_DoWork;
            mWorker.RunWorkerCompleted += mWorker_RunWorkerCompleted;
            _Timer.Tick += _Timer_Tick;
            _Timer.Interval = TimeSpan.FromMilliseconds(100);
            Loadbutt.IsEnabled = false;
            
           
        }

        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source = music.db");
        SQLiteCommand sqlite_cmd;

        #region Properties
        private string _Media;
        private List<FileInfo> _MusicList; //list of files that are music file
        int ID_Album = 0;
        int ID_Artist = 0;
        int ID_Song = 0;
        bool loaded = false;

        private List<FileInfo> FileList;
        private System.ComponentModel.BackgroundWorker mWorker = new System.ComponentModel.BackgroundWorker();
        private System.Windows.Threading.DispatcherTimer _Timer = new System.Windows.Threading.DispatcherTimer();
        int Count = 1;
        int Total = 0;
        #endregion

        #region Thread
        public class DialogState
        {
            public DialogResult result;
            public FolderBrowserDialog dialog;


            public void ThreadProcShowDialog()
            {
                result = dialog.ShowDialog();
            }
        }
        private DialogResult STAShowDialog(FolderBrowserDialog dialog)
        {
            DialogState state = new DialogState();
            state.dialog = dialog;
            System.Threading.Thread t = new
            System.Threading.Thread(state.ThreadProcShowDialog);
            t.SetApartmentState(System.Threading.ApartmentState.STA);
            t.Start();
            t.Join();
            return state.result;
        }
        #endregion

        #region Events
        private void Button_Click(object sender, RoutedEventArgs e) //browse
        {
            _MusicList = new List<FileInfo>();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            System.Windows.Forms.DialogResult _result = STAShowDialog(fbd);
            if (_result == System.Windows.Forms.DialogResult.OK)
            {
                _Media = fbd.SelectedPath;
                Loadbutt.IsEnabled = true;
            }
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //load
        {
            if (Convert.ToString(sqlite_conn.State) == "Closed") sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "delete from Song";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "delete from Album";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "delete from Artist";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
            if (_Media != null)
            {
                DirectoryInfo dir = new DirectoryInfo(_Media);
                foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (file.Extension == ".wmv" || file.Extension == ".mp3" || file.Extension == ".mp4"
                        || file.Extension == ".flac" || file.Extension == ".wma"
                        || file.Extension == ".m4a" || file.Extension == ".wav")
                        _MusicList.Add(file);
                }
                //this.CreateAlbumList(_MusicList);
                loaded = true;
                if (_MusicList != null) Loadbutt.IsEnabled = true;
                this.AddListToDB(_MusicList);
                //CreateAlbumList();
                //CreateArtistList();
                
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Không thể load !");
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) //search
        {
            SearchWindow _searchwindow = new SearchWindow();
            _searchwindow.Show();
        }
        private void mWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            #region AddListtoDtb
            Count = 0;
            _Timer.Start();
            TagLib.File song;
            //Open connection 
            if (Convert.ToString(sqlite_conn.State) == "Closed") sqlite_conn.Open();
            else
            {
                sqlite_conn.Close();
                sqlite_conn.Open();
            }
            //create command
            sqlite_cmd = sqlite_conn.CreateCommand();
            int tmp_ID_Album, tmp_ID_Artist;
            sqlite_cmd.CommandText = "insert into Album(AlbumID, AlbumName) values ('0','Unknow')";
            sqlite_cmd.ExecuteNonQuery();
            ID_Album++;
            sqlite_cmd.CommandText = "insert into Artist(ArtistID, ArtistName) values ('0','Unknow')";
            sqlite_cmd.ExecuteNonQuery();
            ID_Artist++;

            this.Total = FileList.Count;
            foreach (FileInfo musicfile in FileList)
            {
                string albumname, artistname,genre,path;
                int temp;
                song = TagLib.File.Create((string)(musicfile.FullName)); // with a path of file we create a file tag
                if (song.Tag.Album != null)
                {
                    albumname = song.Tag.Album;

                    if (albumname.IndexOf("'") > 0)
                    {
                        temp = albumname.IndexOf("'");
                        albumname = albumname.Insert(temp, "'");
                    };
                }
                else albumname = "Unknow";
                if (song.Tag.Artists.Count()>0)
                {
                    artistname = song.Tag.Artists[0];
                    if (artistname.IndexOf("'") > 0)
                    {
                        temp = artistname.IndexOf("'");
                        artistname = artistname.Insert(temp, "'");
                    }
                }
                else
                {
                    artistname = "Unknow";
                };
                try
                {
                    sqlite_cmd.CommandText = "insert into Album(AlbumID, AlbumName) values (' " + ID_Album + " ','" + albumname + "')";
                    sqlite_cmd.ExecuteNonQuery();
                    ID_Album++;
                }
                catch
                {

                };
                try
                {
                    sqlite_cmd.CommandText = "insert into Artist(ArtistID, ArtistName) values (' " + ID_Artist + " ','" + artistname + "')";
                    sqlite_cmd.ExecuteNonQuery();
                    ID_Artist++;
                }
                catch
                {

                };
                sqlite_cmd.CommandText = "select AlbumID from Album where AlbumName = '" + albumname + "';";
                tmp_ID_Album = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                sqlite_cmd.CommandText = "select ArtistID from Artist where ArtistName = '" + artistname + "';";
                tmp_ID_Artist = Convert.ToInt32(sqlite_cmd.ExecuteScalar());
                string titletemp;
                // Tag
                if (song.Tag.Title == null)
                {
                    titletemp = musicfile.Name;
                }
                else
                {
                    titletemp = song.Tag.Title;

                    if (song.Tag.Title.IndexOf("'") > 0)
                    {
                        int a = song.Tag.Title.IndexOf("'");
                        titletemp = titletemp.Insert(a, "'");
                    };
                };
                // Genres 
                if (song.Tag.Genres.Count() > 0) genre = song.Tag.Genres[0];
                else
                    if (song.Tag.FirstGenre != null) genre = song.Tag.FirstGenre;
                    else genre = "unknow";
                //----- path
                path = musicfile.FullName;
                if (path.IndexOf("'") > 0)
                    {
                        temp = path.IndexOf("'");
                        path = path.Insert(temp, "'");
                    }
                
                
                

                sqlite_cmd.CommandText = "INSERT INTO Song(Songid,Title,Dur,Year,Path,Bitrate,Genre,AlbumID,ArtistId) VALUES ('" + ID_Song + "','" + titletemp + "','" + song.Properties.Duration + "','" + song.Tag.Year + "','" + path+ "'," + song.Properties.AudioBitrate + ",'" + genre+ "','" + tmp_ID_Album + "','" + tmp_ID_Artist + "');";
                sqlite_cmd.ExecuteNonQuery();
                ID_Song++;
                Count++;
            };
            _Timer.Stop();
            sqlite_conn.Close();
            #endregion
            this.Title = "MusicManager - Load Completed";
            
        }
        private void mWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Title = "MusicManager - Load Completed";
            this.SongList.ctAlbumView.ResetAlbumList();
            this.CreateAlbumList();
            this.SongList.ctArtistView.ResetArtistList();
            this.CreateArtistList();
        }
        void _Timer_Tick(object sender, EventArgs e)
        {
            this.Title = "MusicManager - Loading: " + this.Count.ToString() + " of " + this.Total.ToString();
        }
        #endregion

        #region Methods
 
        //--- Hàm thêm list<fileinfo> vào database 
        private void AddListToDB(List<FileInfo> List)
        {
            FileList = List;
            mWorker.RunWorkerAsync();
        }
        private void CreateAlbumList()
        {
            if (Convert.ToString(sqlite_conn.State) == "Closed") sqlite_conn.Open();
            else
            {
                sqlite_conn.Close();
                sqlite_conn.Open();
            }
            sqlite_cmd = sqlite_conn.CreateCommand();
            List<Album> Albums = new List<Album>();
            sqlite_cmd.CommandText = "select * from Album";
            SQLiteDataReader Dtreader;
            Dtreader = sqlite_cmd.ExecuteReader();
            int i=0;
            while (Dtreader.Read())
            {
                Album temp = new Album();
                temp.ID = Dtreader.GetInt32(0);
                temp.Name = Dtreader.GetString(1);
                Albums.Add(temp);
            }// Create List AlbumName With an Album ADD song;
            Dtreader.Close();
            foreach (Album album in Albums)
            {
                sqlite_cmd.CommandText = "SELECT  * FROM Album,Song Where Song.AlbumId = Album.AlbumId and Album.AlbumID=" + album.ID + ";";
                Dtreader = sqlite_cmd.ExecuteReader();
                while (Dtreader.Read())
                {
                    // Song song = new Song(Dtreader.GetString(3), "test", album.Name, (short?)(Dtreader.GetInt32(5)), Dtreader.GetString(8), 3, (short)Dtreader.GetInt32(7), , Dtreader.GetString(6));
                    Song asong = new Song();
                    asong.Path = Dtreader.GetString(6);
                    asong.Genre = Dtreader.GetString(8);
                    asong.Year = (short?)Dtreader.GetInt32(5);
                    asong.Bitrate = Dtreader.GetInt32(7);
                    string time = Dtreader.GetString(4);
                    asong.Dur = TimeSpan.Parse(time);
                    asong.Album = album.Name;
                    TagLib.File tlfile2 = TagLib.File.Create(@asong.Path);
                    string extension;
                    extension = System.IO.Path.GetExtension(asong.Path);
                    asong.filetype = Convert.ToString(extension);
                    asong.Title = Dtreader.GetString(3);
                    asong.Track = (short?)tlfile2.Tag.TrackCount;
                    if (!album.havecover)
                    {
                        try
                        {
                            TagLib.File tlfile = TagLib.File.Create(@asong.Path);
                            TagLib.IPicture pic;
                            if (tlfile.Tag.Pictures.Length > 0)
                            {
                                pic = tlfile.Tag.Pictures[0];
                                MemoryStream ms = new MemoryStream(pic.Data.Data);
                                ms.Seek(0, SeekOrigin.Begin);

                                // ImageSource for System.Windows.Controls.Image
                                BitmapImage bitmap = new BitmapImage();
                                bitmap.BeginInit();
                                bitmap.StreamSource = ms;
                                bitmap.EndInit();
                                // Create a System.Windows.Controls.Image control
                                System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                                album.cover = bitmap;
                            };
                            album.havecover = true;
                            album.AlbumArtist = tlfile.Tag.FirstArtist;
                            asong.Artist = tlfile.Tag.FirstArtist;
                        }
                        catch
                        {

                        }
                    }
                    album.TrackList.Add(asong);
                }
                Dtreader.Close();
            }
            this.SongList.ctAlbumView.ReceiveAlbumList(Albums, this);
            sqlite_conn.Close();
        }       
        private void CreateArtistList()
        {
            if (Convert.ToString(sqlite_conn.State) == "Closed") sqlite_conn.Open();
            else
            {
                sqlite_conn.Close();
                sqlite_conn.Open();
            }
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "select * from Artist";
            SQLiteDataReader Dtreader;
            Dtreader = sqlite_cmd.ExecuteReader();

            //int i=0;
            List<Artist> AL = new List<Artist>();
            while (Dtreader.Read())
            {
                Artist temp = new Artist();
                temp.ID = Dtreader.GetInt32(0);
                temp.Name = Dtreader.GetString(1);
                AL.Add(temp);

            }// Create List AlbumName With an Album ADD song;
            Dtreader.Close();
            foreach (Artist art in AL)
            {   
                art.Songlist= new List<Song>();
                sqlite_cmd.CommandText = "SELECT  * FROM Artist,Song Where Song.ArtistID = Artist.ArtistID and artist.ArtistID=" + art.ID + ";";
                Dtreader = sqlite_cmd.ExecuteReader();
                while (Dtreader.Read())
                {
                    Song asong = new Song();
                    asong.Path = Dtreader.GetString(6);
                    asong.Genre = Dtreader.GetString(8);
                    asong.Year = (short?)Dtreader.GetInt32(5);
                    asong.Bitrate = Dtreader.GetInt32(7);
                    string time = Dtreader.GetString(4);
                    asong.Dur = TimeSpan.Parse(time);
                    asong.Artist= art.Name;
                    TagLib.File tlfile2 = TagLib.File.Create(@asong.Path);
                    string extension;
                    extension = System.IO.Path.GetExtension(asong.Path);
                    asong.filetype = Convert.ToString(extension);
                    asong.Title = Dtreader.GetString(3);
                    asong.Track = (short?)tlfile2.Tag.TrackCount;
                    asong.Album = tlfile2.Tag.Album;
                    art.Songlist.Add(asong);
                }
                Dtreader.Close();
            }
            sqlite_conn.Close();
           this.SongList.ctArtistView.ReceiveArtistList(AL, this);
        }
        private void SongList_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        //-- kết thúc
        #endregion
    }
}
