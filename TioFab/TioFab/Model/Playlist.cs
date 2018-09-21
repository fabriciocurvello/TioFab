using System;
using System.Collections.Generic;
using System.Text;

namespace TioFab.Model
{
    public class Playlist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Thumb { get; set; }

        public Playlist(string id, string name, string thumb)
        {
            Id = id;
            Name = name;
            Thumb = thumb;
        }
    }
}
