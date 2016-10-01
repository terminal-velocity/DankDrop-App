using System;
using DankDrop;
using EstimoteSdk;

namespace DankDrop.Droid
{
    public class AndroidBeacon : AbstractBeacon
    {
        public AndroidBeacon(Beacon b)
        {
            _beacon = b;
        }

        public override int Major => _beacon.Major;
        public override int Minor => _beacon.Minor;
        public override string UUID => _beacon.ProximityUUID.ToString();

        private readonly Beacon _beacon;
    }
}