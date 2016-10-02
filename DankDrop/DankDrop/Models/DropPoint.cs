using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DankDrop
{
    public class DropPoint
    {
        [JsonProperty(PropertyName = "_id")]
        public string ID { get; private set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }
        [JsonProperty(PropertyName = "uuid")]
        public string CombinedBeaconID { get; private set; }

        public async Task<IList<Meme>> GetMemesHere()
        {
            var url = Util.API_ROOT + "/memes?dropPoint=" + ID;

            var text = await Util.HttpGetText(url, Encoding.UTF8);
            var memes = JsonConvert.DeserializeObject<List<Meme>>(text);

            return memes;
        }
        

        public DropPoint(string id, string name, string beacon)
        {
            ID = id; Name = name; CombinedBeaconID = beacon;
        }

        public static async Task<DropPoint> GetDropPointByBeaconInfo(string uuid, int major, int minor)
        {
            var requiredBeaconString = Util.GetCombinedBeaconID(uuid, major, minor);
            var allDropPointsJson = await Util.HttpGetText(Util.API_ROOT + "/dropPoints", Encoding.UTF8);
            var allDropPoints = JsonConvert.DeserializeObject<List<DropPoint>>(allDropPointsJson);

            var theOneTrueDropPoint = allDropPoints.Where(
                (point) => point.CombinedBeaconID.Equals(requiredBeaconString, StringComparison.OrdinalIgnoreCase)
            ).FirstOrDefault();
            if (theOneTrueDropPoint == null) throw new ArgumentException("The specified beacon does not exist on the server");

            return theOneTrueDropPoint;
        }
    }
}
