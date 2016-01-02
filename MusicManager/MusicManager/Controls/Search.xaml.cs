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
using System.Data;
using System.Data.SQLite;

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        public Search()
        {
            InitializeComponent();
        }

        public SearchWindow searchwindow {get ; set;}

        private void cmb_Loaded(object sender, RoutedEventArgs e)
        {
            cmb.Text = "Tên Bài Hát";
        }

        SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source = music.db");
        SQLiteCommand sqlite_cmd;


        private void txtSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearchBox.Text != null) sqlite_conn.Open();
                else datagrid.ItemsSource = null;
            sqlite_cmd = sqlite_conn.CreateCommand();
            if (cmb.Text == "Tên Bài Hát")
            {
                sqlite_cmd.CommandText = "select Title, ArtistName, AlbumName from Song,Artist,Album where (Song.Title like '%" + txtSearchBox.Text + "%') and (Song.AlbumID=Album.AlbumID) and (Song.ArtistID=Artist.ArtistID)";
            }
            else
            {
                if (cmb.Text == "Tên Ca Sĩ")
                {
                    sqlite_cmd.CommandText = "select Title, ArtistName, AlbumName from Song,Artist,Album where (Artist.ArtistName like '%" + txtSearchBox.Text + "%') and (Song.AlbumID=Album.AlbumID) and (Song.ArtistID=Artist.ArtistID)";
                }
                else
                {
                    sqlite_cmd.CommandText = "select Title, ArtistName, AlbumName from Song,Artist,Album where (Album.AlbumName like '%" + txtSearchBox.Text + "%') and (Song.AlbumID=Album.AlbumID) and (Song.ArtistID=Artist.ArtistID)";
                }
            }

            DataSet dataSet = new DataSet();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlite_cmd.CommandText, sqlite_conn);
            dataAdapter.Fill(dataSet);

            datagrid.ItemsSource = dataSet.Tables[0].DefaultView;
            datagrid.CanUserAddRows = false;
            datagrid.CanUserDeleteRows = false;
            sqlite_conn.Close();
        }


        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearchBox.Text != null) sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            if (cmb.Text == "Tên Bài Hát")
            {
                sqlite_cmd.CommandText = "select Title, ArtistName, AlbumName from Song,Artist,Album where (Song.Title='" + txtSearchBox.Text + "') and (Song.AlbumID=Album.AlbumID) and (Song.ArtistID=Artist.ArtistID)";
            }
            else
            {
                if (cmb.Text == "Tên Ca Sĩ")
                {
                    sqlite_cmd.CommandText = "select Title, ArtistName, AlbumName from Song,Artist,Album where (Artist.ArtistName='" + txtSearchBox.Text + "') and (Song.AlbumID=Album.AlbumID) and (Song.ArtistID=Artist.ArtistID)";
                }
                else
                {
                    sqlite_cmd.CommandText = "select Title, ArtistName, AlbumName from Song,Artist,Album where (Album.AlbumName='" + txtSearchBox.Text + "') and (Song.AlbumID=Album.AlbumID) and (Song.ArtistID=Artist.ArtistID)";
                }
            }
            
            DataSet dataSet = new DataSet();
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(sqlite_cmd.CommandText, sqlite_conn);
            dataAdapter.Fill(dataSet);

            datagrid.ItemsSource = dataSet.Tables[0].DefaultView;
            datagrid.CanUserAddRows = false;
            datagrid.CanUserDeleteRows = false;
            sqlite_conn.Close();
        }
    }
}
