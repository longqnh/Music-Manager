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
    /// Interaction logic for Track.xaml
    /// </summary>
    public partial class Track : UserControl
    {
        public Track(MainWindow main)
        {
            InitializeComponent();
            this._Main = main;
        }

        #region Properties
        private MainWindow _Main;
        #endregion

        #region Events
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this._Main.TrackInfo.LoadInfo(this); // load trackinfo
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 2)
            {
                this._Main.PlayList.AddtoList(this); // add track to playlist
            }

        }
        #endregion
    }
}
