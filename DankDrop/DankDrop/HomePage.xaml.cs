using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DankDrop
{
    public partial class HomePage : ContentPage
    {
        public HomePage(IBeaconRanger ranger)
        {
            InitializeComponent();

            ranger.OnNearbyBeaconsChange += (sender, beacons) =>
            {
                list.ItemsSource = beacons;
            };
        }
    }
}
