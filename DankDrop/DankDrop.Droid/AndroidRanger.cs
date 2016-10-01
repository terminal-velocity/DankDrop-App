using System;
using EstimoteSdk;
using DankDrop;
using System.Linq;
using Android;
using Android.Content;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DankDrop.Droid
{
    public class AndroidRanger : IBeaconRanger
    {
        public event EventHandler<IList<AbstractBeacon>> OnNearbyBeaconsChange;

        private Region _region = null;
        public Task BeginRanging(string uuid, int? major, int? minor)
        {
            if (major != null)
            {
                if (minor != null)
                {
                    _region = new Region($"{uuid}:{major}:{minor}", uuid, major.Value, minor.Value);
                }
                else
                {
                    _region = new Region($"{uuid}:{major}", uuid, major.Value);
                }
            }
            else
            {
                _region = new Region(uuid, uuid);
            }

            var t = new TaskCompletionSource<object>();

            _manager.Connect(new MyServiceReadyCallback(() =>
            {
                _manager.StartRanging(_region);

                t.TrySetResult(null);
            }));

            return t.Task;
        }

        public Task EndRanging()
        {
            _manager.StopRanging(_region);
            _region = null;

            return Task.CompletedTask;
        }

        public AndroidRanger(Context c)
        {
            _manager = new BeaconManager(c);
            _manager.SetRangingListener(new MyRangingListener((Region region, IList<Beacon> beacons) =>
            {
                OnNearbyBeaconsChange?.Invoke(this, beacons.Select(b => (AbstractBeacon) new AndroidBeacon(b)).ToList());
            }));
        }

        private readonly BeaconManager _manager;

        protected class MyServiceReadyCallback : Java.Lang.Object, BeaconManager.IServiceReadyCallback
        {
            private Action _action;
            public MyServiceReadyCallback(Action a)
            {
                _action = a;
            }
            public void OnServiceReady()
            {
                _action();
            }
        }

        protected class MyRangingListener : Java.Lang.Object, BeaconManager.IRangingListener
        {
            public delegate void MyDelegate(Region r, IList<Beacon> l);
            private MyDelegate func;
            public MyRangingListener(MyDelegate f) { func = f; }

            public void OnBeaconsDiscovered(Region region, IList<Beacon> beacons)
            {
                func(region, beacons);
            }
        }
    }
}