using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Course
{
    class MyFavoriteList
    {
        public List<Music> mylist;
        public MyFavoriteList()
        {
            mylist = new List<Music>();
        }
        public void AddToMyFavorite(string name, JsonSave List, AllList Major, string ArtistName, string AlbumName, string MusicName, int Year, bool first = false)
        {
            name = "PlayList/" + name + ".json";
            if (!first)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].AlbumName == AlbumName && List[i].ArtistName == ArtistName && List[i].MusicName == MusicName && List[i].Year == Year)
                    {

                        return;
                    }
                }

                List.list.Add(new JSONElement(ArtistName, AlbumName, MusicName, Year));
                File.WriteAllText(name, JsonConvert.SerializeObject(List));
            }
            else
            {

                for (int i = 0; i < Major.Count; i++)
                {
                    if (AllList.SearchForAdd(Major.artistlist[i].name, ArtistName))
                    {
                        for (int j = 0; j < Major.artistlist[i].Count; j++)
                        {
                            if (AllList.SearchForAdd(Major.artistlist[i][j].name, AlbumName) && Major.artistlist[i][j].years == Year)
                            {
                                for (int g = 0; g < Major.artistlist[i][j].Count; g++)
                                {
                                    if (AllList.SearchForAdd(Major.artistlist[i][j][g].name, MusicName))
                                    {
                                        if (!mylist.Contains(Major.artistlist[i][j][g]))
                                            mylist.Add(Major.artistlist[i][j][g]);
                                        return;
                                    }
                                }
                                return;
                            }
                        }
                        return;
                    }
                }
            }
        }
        public void RemoveAtMyFavorite(string name, JsonSave List, string ArtistName, string AlbumName, string MusicName, int Year)
        {
            name = "PlayList/" + name + ".json";
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].AlbumName == AlbumName && List[i].ArtistName == ArtistName && List[i].MusicName == MusicName && List[i].Year == Year)
                {
                    List.list.RemoveAt(i);
                    File.WriteAllText(name, JsonConvert.SerializeObject(List));
                    break;
                }
            }
            for (int i = 0; i < Count; i++)
            {
                if (mylist[i].name == MusicName && mylist[i].album.name == AlbumName && mylist[i].artist.name == ArtistName && mylist[i].years == Year)
                {
                    mylist.RemoveAt(i);
                }
            }
        }
        public void Load(ref JsonSave FavoriteMusic,string name, AllList Major)
        {
            try
            {
                FavoriteMusic = JsonConvert.DeserializeObject<JsonSave>(File.ReadAllText("PlayList/" + name + ".json"));

                for (int i = 0; i < FavoriteMusic.Count; i++)
                {
                    this.AddToMyFavorite(name, FavoriteMusic, Major, FavoriteMusic[i].ArtistName, FavoriteMusic[i].AlbumName, FavoriteMusic[i].MusicName, FavoriteMusic[i].Year, true);
                }
            }
            catch
            {
                
            }
            
                
            
        }
        public int Count
        {
            get { return mylist.Count; }
        }
        public Music this[int i]
        {
            get { return mylist[i]; }
        }

    }
}
