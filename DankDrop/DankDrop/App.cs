using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DankDrop
{
    public class App : Application
    {
        private IBeaconRanger _ranger;

        public App(IBeaconRanger ranger)
        {
            _ranger = ranger;
            MainPage = new HomePage(_ranger);
        }

        protected override void OnStart()
        {
            go();
        }

        protected async override void OnSleep()
        {
            await _ranger.EndRanging();
        }

        protected async override void OnResume()
        {
            go();
        }

        private async void go()
        {
            await _ranger.BeginRanging("4DF5966C-BB34-775C-A509-3A46726C5B7B", null, null);
        }
    }
}
