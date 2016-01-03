﻿using System;
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
using MusicManager.Classes;
namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for AlbumView.xaml
    /// </summary>
    public partial class AlbumView : UserControl
    {
        #region Constructor
        public AlbumView()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        private int _Cur = 0;
        private List<Album> _AlbumList;
        private MainWindow _Main;
        #endregion

        #region Events
        private void imgPreAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _Cur--;
            if (_AlbumList[_Cur].cover == null)
                imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
            else imgCurAlbum.Source=_AlbumList[_Cur].cover; 

            this.CreateSongListofAlbum(_Cur);

            if (_AlbumList[_Cur + 1].cover == null)
                imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
            else
                imgNextAlbum.Source = _AlbumList[_Cur + 1].cover;

            tbAlbumName.Text = _AlbumList[_Cur].Name;
            tbArtist.Text = _AlbumList[_Cur].AlbumArtist;
            tbYear.Text = Convert.ToString(_AlbumList[_Cur].Year);

            if (_Cur - 1 < 0) // this current album is the last album
                imgPreAlbum.Source = null;
            else
                if (_AlbumList[_Cur - 1].cover == null)
                    imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur - 1].CoverPath, UriKind.Relative));
                else
                    imgPreAlbum.Source = _AlbumList[_Cur - 1].cover;

        }
        //private void imgNextAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if(_AlbumList[_Cur].cover==null) 
        //        imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
        //    else 
        //        imgPreAlbum.Source = _AlbumList[_Cur].cover;

        //    _Cur++;
        //    if (_AlbumList[_Cur].cover == null) 
        //        imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
        //    else 
        //        imgCurAlbum.Source=_AlbumList[_Cur].cover;

        //    tbAlbumName.Text = _AlbumList[_Cur].Name;
        //    tbArtist.Text = _AlbumList[_Cur].AlbumArtist;
        //    tbYear.Text = Convert.ToString(_AlbumList[_Cur].Year);

        //    this.CreateSongListofAlbum(_Cur);
        //    if (_Cur == _AlbumList.Count - 1) // this current album is the last album
        //        imgNextAlbum.Source = null;
        //    this.PreAlbum();
        //}
        private void imgNextAlbum_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.NextAlbum();
        }
        private void test_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                this.NextAlbum();
            else
                this.PreAlbum();
        }
        #endregion

        #region Methods
        public void ResetAlbumList()
        {
            if(this._AlbumList.Count>0) this._AlbumList.Clear();
            imgCurAlbum.Source = null;
            imgNextAlbum.Source = null;
            imgPreAlbum.Source = null;
        }
        public void ReceiveAlbumList(List<Album> list, MainWindow main)
        {           
            this._AlbumList = list;
            this._Main = main;
            this._Cur = 0;

            //cover path
            for (int i = 0; i < this._AlbumList.Count; i++)
                this._AlbumList[i].CoverPath = "/Albums/6.jpg";

            // set image on form
            if(_AlbumList[_Cur].cover==null)
                imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
           else  
                imgCurAlbum.Source = _AlbumList[_Cur].cover;

           if (this._AlbumList.Count > 1)
           {
               this.CreateSongListofAlbum(_Cur);
               if (_AlbumList[_Cur + 1].cover == null)
                   imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
               else
                   imgNextAlbum.Source = _AlbumList[_Cur + 1].cover;
           }
           CreateSongListofAlbum(0);
        }
        private void CreateSongListofAlbum(int index)
        {
            this.pnTrackList.Children.Clear(); // first, remove all the song contenting
            //then add songlist of _AlbumList[index]
            int i;
            for (i = 0; i < this._AlbumList[index].TrackList.Count; i++)
            {
                Song tmp = this._AlbumList[index].TrackList[i];

                Track track = new Track(_Main, (short)index, (short)i);
                track.tbNo.Text = i + ".";
             
                track.tbTitle.Text = tmp.Title;
                track.tbDur.Text = "0" + tmp.Dur.Minutes.ToString() + ":" + tmp.Dur.Seconds;
                this.pnTrackList.Children.Add(track);
            }

        }
        public Song SongX_OfAlbum(short index, short x) // return song x of album[index]
        {
            return this._AlbumList[index].TrackList[x];
        }
        private void NextAlbum()
        {
            if (_Cur + 1 < this._AlbumList.Count)
            {
                if (_AlbumList[_Cur].cover == null)
                    imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
                else
                    imgPreAlbum.Source = _AlbumList[_Cur].cover;

                _Cur++;

                if (_AlbumList[_Cur].cover == null)
                    imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
                else
                    imgCurAlbum.Source = _AlbumList[_Cur].cover;

                tbAlbumName.Text = _AlbumList[_Cur].Name;
                tbArtist.Text = _AlbumList[_Cur].AlbumArtist;
                tbYear.Text = Convert.ToString(_AlbumList[_Cur].Year);

                this.CreateSongListofAlbum(_Cur);
                if (_Cur == _AlbumList.Count - 1) // this current album is the last album
                    imgNextAlbum.Source = null;
                else
                {
                    if (_AlbumList[_Cur + 1].cover == null) imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
                    else imgNextAlbum.Source = _AlbumList[_Cur + 1].cover;
                }
            }
        }
        private void PreAlbum()
        {
            if (_Cur - 1 >= 0)
            {
                _Cur--;
                if (_AlbumList[_Cur].cover == null)
                    imgCurAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur].CoverPath, UriKind.Relative));
                else imgCurAlbum.Source = _AlbumList[_Cur].cover;

                this.CreateSongListofAlbum(_Cur);

                if (_AlbumList[_Cur + 1].cover == null)
                    imgNextAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur + 1].CoverPath, UriKind.Relative));
                else
                    imgNextAlbum.Source = _AlbumList[_Cur + 1].cover;

                tbAlbumName.Text = _AlbumList[_Cur].Name;
                tbArtist.Text = _AlbumList[_Cur].AlbumArtist;
                tbYear.Text = Convert.ToString(_AlbumList[_Cur].Year);

                if (_Cur - 1 < 0) // this current album is the last album
                    imgPreAlbum.Source = null;
                else
                    if (_AlbumList[_Cur - 1].cover == null)
                        imgPreAlbum.Source = new BitmapImage(new Uri(_AlbumList[_Cur - 1].CoverPath, UriKind.Relative));
                    else
                        imgPreAlbum.Source = _AlbumList[_Cur - 1].cover;
            }
        }
        #endregion
    }
}
