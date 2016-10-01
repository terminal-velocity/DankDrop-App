using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DankDrop
{
    public abstract class AbstractBeacon
    {
        public abstract string UUID { get; }
        public abstract int Major { get; }
        public abstract int Minor { get; }

        public string MajorMinor => $"{Major}:{Minor}";
    }
}
