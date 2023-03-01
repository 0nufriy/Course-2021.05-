using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course
{
    class JSONElement
    {
        public string ArtistName;
        public string AlbumName;
        public string MusicName;
        public int Year;
        public JSONElement(string artistName, string albumName, string musicName, int year)
        {
            ArtistName = artistName;
            AlbumName = albumName;
            MusicName = musicName;
            Year = year;
        }
    }
}
