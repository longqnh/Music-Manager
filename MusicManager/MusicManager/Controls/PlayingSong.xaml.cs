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
using MusicManager.Classes;

namespace MusicManager.Controls
{
    /// <summary>
    /// Interaction logic for PlayingSong.xaml
    /// </summary>
    public partial class PlayingSong : UserControl
    {
        #region Constructor
        public PlayingSong(MainWindow main, short no)
        {
            InitializeComponent();
            this._Main = main;
            this._No = no;
        }
        #endregion

        #region Properties
        private MainWindow _Main;
        private short _No;

        private string _FullText; //store the full title if it's too long
        #endregion

        #region Events
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //select song from play list
            this._Main.PlayList.SongX_Selected(_No);
        }
        #endregion

        #region Methods
        public void Checklength()
        {
            if (tbTitle.Text.Length > 13)
            {
                _FullText = tbTitle.Text; // back up

                tbTitle.Text = tbTitle.Text.Remove(10) + "..."; // set new title
                ToolTip aTooltip = new ToolTip();
                aTooltip.Content = _FullText;
                //set the tooltip to title
                tbTitle.ToolTip = aTooltip;
            }
        }
        public void Selected()
        {
            Color coSelected = Color.FromRgb(162, 24, 102);
            tbNo.Foreground = new SolidColorBrush(coSelected);
            tbTitle.Foreground = new SolidColorBrush(coSelected);
            tbArtist.Foreground = new SolidColorBrush(coSelected);
            tbAlbum.Foreground = new SolidColorBrush(coSelected);
            tbDur.Foreground = new SolidColorBrush(coSelected);
        }
        public void DeSelected()
        {
            Color coSelected = Colors.White;
            tbNo.Foreground = new SolidColorBrush(coSelected);
            tbTitle.Foreground = new SolidColorBrush(coSelected);
            tbArtist.Foreground = new SolidColorBrush(coSelected);
            tbAlbum.Foreground = new SolidColorBrush(coSelected);
            tbDur.Foreground = new SolidColorBrush(coSelected);
        }
        #endregion
    }
}
