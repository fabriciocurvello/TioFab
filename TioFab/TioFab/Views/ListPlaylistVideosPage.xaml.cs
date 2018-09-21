using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TioFab.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TioFab.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListPlaylistVideosPage : ContentPage
	{
		public ListPlaylistVideosPage (string id)
		{
			InitializeComponent ();
            ShowPlaylist(id);
		}

        private async void ShowPlaylist(string id)
        {
            List<Item> items = await GetPlaylistItems(id);

            foreach (var item in items)
            {
                if (item.Title.ToLower().Contains("elet"))
                    continue;
                Image imgOpenPlaylist = new Image()
                {
                    Source = item.Thumb,
                    WidthRequest = 100,
                    HeightRequest = 100
                };
                Button btnOpenPlaylist = new Button()
                {
                    Text = item.Title,
                    //AutomationId = playlist.Id
                };

                btnOpenPlaylist.Clicked += BtnOpenPlaylist_Clicked; ;

                StackLayoutPlaylistItems.Children.Add(imgOpenPlaylist);
                StackLayoutPlaylistItems.Children.Add(btnOpenPlaylist);
            }
        }

        private void BtnOpenPlaylist_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VideoPlayerPage());
        }

        private async Task<List<Item>> GetPlaylistItems(string id)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDS4cJoieUrK9yLUo39hV9EAMSkwR1fvQw",
                ApplicationName = this.GetType().ToString()
            });

            var playlistService = youtubeService.PlaylistItems.List("snippet,contentDetails");
            playlistService.PlaylistId = id;

            var playListsResponse = await playlistService.ExecuteAsync();

            List<Item> items = new List<Item>();

            foreach (var item in playListsResponse.Items)
                items.Add(new Item(item.Id, item.Snippet.Title, item.Snippet.Thumbnails.Standard.Url, item.ContentDetails.VideoId));

            return items;
        }
    }
}