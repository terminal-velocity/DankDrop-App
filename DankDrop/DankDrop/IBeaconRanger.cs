using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DankDrop
{
    public interface IBeaconRanger
    {
        event EventHandler<IList<AbstractBeacon>> OnNearbyBeaconsChange;

        Task BeginRanging(string uuid, int? major, int? minor);
        Task EndRanging();
    }
}
