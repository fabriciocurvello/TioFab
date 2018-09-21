using System;
using System.Collections.Generic;
using System.Text;

namespace TioFab.Model
{
    public class Item
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Thumb { get; set; }


        public string VideoId { get; set; }

        public Item(string id, string title, string thumb, string videoId)
        {
            Id = id;
            Title = title;
            Thumb = thumb;
            VideoId = videoId;
        }

    }
}
