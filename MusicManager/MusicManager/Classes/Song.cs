using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Classes
{
    class Duration
    {
        #region Constructor
        public Duration()
        {
            _Minute = _Second = 0;
        }
        #endregion

        #region Properties
        private int _Minute;
        private int _Second;

        public int Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }
        public int Second
        {
            get { return _Second; }
            set { _Second = value; }
        }
        #endregion

        #region methods
        public override string ToString()
        {
            string data = String.Format("{0}:{1}", this._Minute, this._Second);
            return data;
        }
        #endregion
    }
    class Song
    {
        #region Constructor
        public Song()
        {

        }
        #endregion

        #region Properties
        private string _Title;
        private string _Album;
        private string _Artist;
        private Duration _Dur;
        private int _Bitrate;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string Album
        {
            get { return _Album; }
            set { _Album = value; }
        }
        public string Artist
        {
            get { return _Artist; }
            set { _Artist = value; }
        }
        public Duration Dur
        {
            get { return _Dur; }
            set { _Dur = value; }
        }
        public int Bitrate
        {
            get { return _Bitrate; }
            set { _Bitrate = value; }
        }
        #endregion

        #region Methods
        #endregion
    }
}
