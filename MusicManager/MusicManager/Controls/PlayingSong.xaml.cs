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
    /// Interaction logic for PlayingSong.xaml
    /// </summary>
    public partial class PlayingSong : UserControl
    {
        public PlayingSong(MainWindow main)
        {
            InitializeComponent();
            this._Main = main;
        }

        #region Properties
        private MainWindow _Main;
        private string _FullText; //store the full title if it's too long
        #endregion

        #region fortesting
       public PlayingSong()
        { }
        #endregion

        #region Events
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._Main.TrackInfo.LoadInfo(this);
        }
        #endregion

        #region Methods
        public void Checklength()
        {
            if (tbTitle.Text.Length > 10)
            {
                _FullText = tbTitle.Text; // back up

                tbTitle.Text = tbTitle.Text.Remove(10) + "..."; // set new title
                ToolTip aTooltip = new ToolTip();
                aTooltip.Content = _FullText;

                tbTitle.ToolTip = aTooltip;
            }
        }
        #endregion
    }
}
