using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DankDrop
{
    public class App : Application
    {
        private const string ESTIMOTE_DEFAULT_UUID = "B9407F30-F5F8-466E-AFF9-25556B57FE6D";

        private IBeaconRanger _ranger;

        public App(IBeaconRanger ranger)
        {
            _ranger = ranger;
            var home = new HomePage(_ranger);
            var nav = new NavigationPage(home);
            home.OnBeaconChosen += (sender, dropPoint) =>
            {
                nav.PushAsync(new MemePage(dropPoint));
            };

            nav.Icon = "icon.png";
            nav.Title = "Dank Drop";
            MainPage = nav;
        }

        protected override void OnStart()
        {
            go();
        }

        protected async override void OnSleep()
        {
            await _ranger.EndRanging();
        }

        protected override void OnResume()
        {
            go();
        }

        private async void go()
        {
            await _ranger.BeginRanging(ESTIMOTE_DEFAULT_UUID, null, null);
        }
    }
}
