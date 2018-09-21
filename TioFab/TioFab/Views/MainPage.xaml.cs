using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TioFab.Model;
using Xamarin.Forms;

namespace TioFab.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ShowPlaylists();
        }

        private async void ShowPlaylists()
        {
            List<Playlist> playlists = await GetPlaylists();
            foreach(var playlist in playlists)
            {
                if (playlist.Name.ToLower().Contains("elet"))
                    continue;
                Image imgOpenPlaylist = new Image()
                {
                    Source = playlist.Thumb,
                    WidthRequest = 100,
                    HeightRequest = 100
                };
                Button btnOpenPlaylist = new Button()
                {
                    Text = playlist.Name,
                    AutomationId = playlist.Id                    
                };

                btnOpenPlaylist.Clicked += BtnOpenPlaylist_Clicked;

                StackLayoutPlaylists.Children.Add(imgOpenPlaylist);
                StackLayoutPlaylists.Children.Add(btnOpenPlaylist);
            }
        }

        private void BtnOpenPlaylist_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Navigation.PushAsync(new ListPlaylistVideosPage(button.AutomationId));
        }

        private async Task<List<Playlist>> GetPlaylists()
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDS4cJoieUrK9yLUo39hV9EAMSkwR1fvQw",
                ApplicationName = this.GetType().ToString()
            });
            var playlistService = youtubeService.Playlists.List("snippet,contentDetails");
            playlistService.ChannelId = "UCDKQmex_QOtjlZvWlRbjMew";
            var playListsResponse = await playlistService.ExecuteAsync();
            List<Playlist> playlists = new List<Playlist>();
            foreach(var playlist in playListsResponse.Items)
                playlists.Add(new Playlist(playlist.Id, playlist.Snippet.Title, playlist.Snippet.Thumbnails.Standard.Url));

            //var searchListRequest = youtubeService.Search.List("snippet");
            //searchListRequest.Q = "Google"; // Replace with your search term.
            //searchListRequest.MaxResults = 50;
            //// Call the search.list method to retrieve results matching the specified query term.
            //var searchListResponse = await searchListRequest.ExecuteAsync();

            //List<string> videos = new List<string>();
            //List<string> channels = new List<string>();
            //List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            //foreach (var searchResult in searchListResponse.Items)
            //{
            //    switch (searchResult.Id.Kind)
            //    {
            //        case "youtube#video":
            //            videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
            //            break;

            //        case "youtube#channel":
            //            channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
            //            break;

            //        case "youtube#playlist":
            //            playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
            //            break;
            //    }
            //}

            //LabelResult.Text = String.Format("Videos:\n{0}\n", string.Join("\n", videos));
            //LabelResult.Text += "\n" + String.Format("Channels:\n{0}\n", string.Join("\n", channels));
            //LabelResult.Text += String.Format("Playlists:\n{0}\n", string.Join("\n", playlists));

            return playlists;
        }
    }
}
