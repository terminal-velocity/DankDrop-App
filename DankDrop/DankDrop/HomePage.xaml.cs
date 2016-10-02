using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin;
using Xamarin.Forms;

namespace DankDrop
{
    public partial class HomePage : ContentPage
    {
        public event EventHandler<DropPoint> OnBeaconChosen;

        public HomePage(IBeaconRanger ranger)
        {
            InitializeComponent();

            ranger.OnNearbyBeaconsChange += async (sender, beacons) =>
            {
                var futureDropPoints = beacons
                    .Select(beacon => DropPoint.GetDropPointByBeaconInfo(beacon.UUID, beacon.Major, beacon.Minor))
                    .Select(async dropPoint =>
                    {
                        try
                        {
                            return await dropPoint;
                        }
                        catch (ArgumentException)
                        {
                            return null;
                        }
                    })
                    .ToArray();
                var dropPoints = await Task.WhenAll(futureDropPoints);
                list.ItemsSource = dropPoints.ToArray();
            };

            list.ItemTapped += (sender, args) =>
            {
                OnBeaconChosen?.Invoke(this, (DropPoint)args.Item);
            };
        }
    }
}
