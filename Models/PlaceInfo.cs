using System.Collections.Generic;
using System.Windows;

namespace CV19.Models
{
    internal class PlaceInfo
    {
        public string Name { get; set; }

        public Point Locatiom { get; set; }

        public IEnumerable<ConfirmedCount> Counts { get; set; }
    }
}
