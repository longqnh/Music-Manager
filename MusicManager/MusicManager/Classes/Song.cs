using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager.Classes
{
    public class Song
    {
        #region Constructor
        public Song(string title, string artist, string album, short? year, string genre, short? track, short bitrate, TimeSpan dur, string path)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Year = year;
            Genre = genre;
            Track = track;
            Bitrate = bitrate;
            Dur = dur;
            this.Path = path;
        }
        public Song ()
        {

        }
        #endregion

        #region Properties
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public short? Year { get; set; }
        public string Genre { get; set; }
        public short? Track { get; set; }
        public int Bitrate { get; set; }
        public TimeSpan Dur { get; set; }
        public string Path { get; set; }
        public string filetype { get; set; }
        #endregion
    }
}
