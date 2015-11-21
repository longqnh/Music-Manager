using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Classes
{
    class Album
    {
        #region Constructor
        public Album()
        {
            _TrackList = new List<Song>();
        }
        #endregion

        #region Properties
        private string _Name;
        private int _Year;
        private List<Song> _TrackList;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        #endregion

        #region Methods
        public void AddTrack(Song song)
        {
            this._TrackList.Add(song);
        }
        #endregion
    }
}
