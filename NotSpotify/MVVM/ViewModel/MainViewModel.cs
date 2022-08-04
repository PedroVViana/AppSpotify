using Newtonsoft.Json;
using NotSpotify.MVVM.Model;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotSpotify.MVVM.ViewModel
{
    
    internal class MainViewModel
    {

        public ObservableCollection<Item> Songs { get; set; }
        public MainViewModel()
        {
            Songs = new ObservableCollection<Item>();
            PupolateCollection();
        }

        void PupolateCollection()
        {
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQCe3bZYpNatxMJ8PMvvzXaTC9aeZOUHRbAaXgBDTSdgam0eM0poV3EVX3OUVIiP7BO7p4HplVHA6W1MJviGFheBz8lGHRhRl-RIYqkl8udp7nEbq4UZToSJ9XZNedITgncuHh8-vkVCbRh5oS9ARj8zxi4uxpV06U4lSgru-gKzjWodzds4_8_66b2T2zt_XVs", "Bearer");

            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");


            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for (int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
               track.Duration = "2:32";
               Songs.Add(track);
                
            }
        }
    }
}
