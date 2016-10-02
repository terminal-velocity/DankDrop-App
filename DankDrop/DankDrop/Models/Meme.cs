using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.IO;
using Plugin.Media;
using System.Threading.Tasks;

namespace DankDrop
{
    public class Meme
    {
        public Meme(string id, string dropId, double dankness, string caption)
        {
            MemeID = id;
            Rating = dankness;
            Name = caption;
        }

        [JsonProperty(PropertyName = "id")]
        public string MemeID { get; private set; }

        [JsonProperty(PropertyName = "dropPoint")]
        public string DropID { get; private set; }

        public ImageSource ImageUrl => 
            ImageSource.FromUri(new Uri($"{Util.API_ROOT}/meme/{MemeID}/img"));

        [JsonProperty(PropertyName = "computedScore")]
        public double Rating { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        public static async Task<Stream> GrabImage()
        {
            if (!await CrossMedia.Current.Initialize())
            {
                throw new InvalidOperationException("Media not available.");
            }
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                throw new InvalidOperationException("Photo picker unavailable.");
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            return file.GetStream();
        }
    }
}
