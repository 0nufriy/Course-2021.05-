using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Course
{
    class AllList
    {
        public List<Artist> artistlist;
        public int Count
        {
            get { return artistlist.Count; }
        }
        public int CountMusic
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < Count; i++)
                {
                    for (int j = 0; j < artistlist[i].Count; j++)
                    {
                        sum += artistlist[i][j].Count;
                    }
                }
                return sum;
            }
        }
        public AllList()
        {
            artistlist = new List<Artist>();
        }
        public static bool SearchForAdd(string l1, string l2)
        {
            l1 = l1.ToLowerInvariant();
            l2 = l2.ToLowerInvariant();
            if (l1 == l2) return true;
            return false;
        }
        private static bool SearchIn(string l1, string l2)
        {
            l1 = l1.ToLowerInvariant();
            l2 = l2.ToLowerInvariant();
            for (int i = 0; i < l1.Length - l2.Length + 1; i++)
            {
                int k = 0;
                for (int j = 0; j < l2.Length; j++)
                {
                    if (l1[i + j] == l2[j])
                    {
                        k++;
                    }
                }
                if (k == l2.Length) return true;
            }
            return false;
        }
        public List<Music> SearchByMusicName(string ind)
        {
            List<Music> result = new List<Music>();
            if (ind.Length == 0) return result;
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < artistlist[i].Count; j++)
                {
                    for (int g = 0; g < artistlist[i][j].Count; g++)
                    {
                        if (SearchIn(artistlist[i][j][g].name, ind))
                        {

                            result.Add(artistlist[i][j][g]);
                        }
                    }
                }
            }
            return result;
        }
        public List<Album> SearchByAlbumName(string ind)
        {
            List<Album> result = new List<Album>();
            if (ind.Length == 0) return result;
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < artistlist[i].Count; j++)
                {
                    if (SearchIn(artistlist[i][j].name, ind))
                    {
                        result.Add(artistlist[i][j]);
                    }
                }
            }
            return result;
        }
        public List<Artist> SearchByArtistName(string ind)
        {
            List<Artist> result = new List<Artist>();
            if (ind.Length == 0) return result;
            for (int i = 0; i < Count; i++)
            {
                if (SearchIn(artistlist[i].name, ind))
                {
                    result.Add(artistlist[i]);
                }
            }
            return result;
        }
        public bool AlbumExistTwice(string n)
        {
            
            for(int i = 0; i<Count; i++)
            {
                int ind = 0;
                for (int j = 0; j< artistlist[i].Count;j++)
                {
                    if (artistlist[i][j].name == n) ind++;
                    if (ind == 2) return true;
                }
            }
            return false;
        }
        public void Load(ref JsonSave AllMusic)
        {
            if (!File.Exists("AllList.json"))
            {
                File.WriteAllText("AllList.json", JsonConvert.SerializeObject(AllMusic));
            }
            else
            {
                AllMusic = JsonConvert.DeserializeObject<JsonSave>(File.ReadAllText("AllList.json"));
                for (int i = 0; i < AllMusic.Count; i++)
                {
                    this.AddToList(AllMusic, AllMusic[i].ArtistName, AllMusic[i].AlbumName, AllMusic[i].MusicName, AllMusic[i].Year, true);
                }
            }
        }
        public void AddToList(JsonSave List, string ArtistName, string AlbumName, string MusicName, int Year, bool first = false)
        {

            for (int i = 0; i < Count; i++)
            {
                if (SearchForAdd(artistlist[i].name, ArtistName))
                {
                    for (int j = 0; j < artistlist[i].Count; j++)
                    {
                        if (SearchForAdd(artistlist[i][j].name, AlbumName) && artistlist[i][j].years == Year)
                        {
                            for (int g = 0; g < artistlist[i][j].Count; g++)
                            {
                                if (SearchForAdd(artistlist[i][j][g].name, MusicName))
                                {
                                    return;
                                }
                            }
                            if (!first)
                            {
                                List.list.Add(new JSONElement(ArtistName, AlbumName, MusicName, Year));
                                File.WriteAllText("AllList.json", JsonConvert.SerializeObject(List));
                            }
                            else artistlist[i][j].musiclist.Add(new Music(artistlist[i][j], artistlist[i], MusicName));
                                
                            return;
                        }
                    }
                    if (!first)
                    {
                        List.list.Add(new JSONElement(ArtistName, AlbumName, MusicName, Year));
                        File.WriteAllText("AllList.json", JsonConvert.SerializeObject(List));
                    }
                    else
                    {
                        artistlist[i].albumlist.Add(new Album(Year, AlbumName, artistlist[i], new List<Music>()));
                        artistlist[i][artistlist[i].Count - 1].musiclist.Add(new Music(artistlist[i][artistlist[i].Count - 1], artistlist[i], MusicName));

                    }
                    return;
                }
            }
            if (!first)
            {
                List.list.Add(new JSONElement(ArtistName, AlbumName, MusicName, Year));
                File.WriteAllText("AllList.json", JsonConvert.SerializeObject(List));
            }
            else
            {
                artistlist.Add(new Artist(ArtistName, new List<Album>()));
                artistlist[Count - 1].albumlist.Add(new Album(Year, AlbumName, artistlist[Count - 1], new List<Music>()));
                artistlist[Count - 1][artistlist[Count - 1].Count - 1].musiclist.Add(new Music(artistlist[Count - 1][artistlist[Count - 1].Count - 1], artistlist[Count - 1], MusicName));

            }


        }

        public void RomoveAtList(JsonSave List, string ArtistName, string AlbumName, string MusicName, int Year)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].AlbumName == AlbumName && List[i].ArtistName == ArtistName && List[i].MusicName == MusicName && List[i].Year == Year)
                {
                    List.list.RemoveAt(i);
                    File.WriteAllText("AllList.json", JsonConvert.SerializeObject(List));
                    break;
                }
            }
            for (int i = 0; i < Count; i++)
            {
                if (SearchForAdd(artistlist[i].name, ArtistName))
                {
                    for (int j = 0; j < artistlist[i].Count; j++)
                    {
                        if (SearchForAdd(artistlist[i][j].name, AlbumName) && artistlist[i][j].years == Year)
                        {
                            for (int g = 0; g < artistlist[i][j].Count; g++)
                            {
                                if (SearchForAdd(artistlist[i][j][g].name, MusicName))
                                {
                                    artistlist[i][j].musiclist.RemoveAt(g);
                                    if (artistlist[i][j].Count == 0)
                                    {
                                        string n = artistlist[i][j].name;
                                        

                                        if (File.Exists(artistlist[i][j].Cover) && !AlbumExistTwice(n)) File.Delete(artistlist[i][j].Cover);
                                        artistlist[i].albumlist.RemoveAt(j);
                                        if (artistlist[i].Count == 0)
                                        {
                                            artistlist.RemoveAt(i);
                                        }
                                    }
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
        public static bool ChangeCover(string file, string ArtistName, string AlbumName)
        {
            if (!Directory.Exists("Covers/"))
            {
                Directory.CreateDirectory("Covers/");
            }
            if (File.Exists("Covers/" + ArtistName + '.' + AlbumName + ".png"))
            {
                File.Delete("Covers/" + ArtistName + '.' + AlbumName + ".png");
            }
            try
            {
                File.Copy(file, "Covers/" + ArtistName + '.' + AlbumName + ".png");
                return true;
            }
            catch
            {
                return false;
                
            }
        }
    }
}
