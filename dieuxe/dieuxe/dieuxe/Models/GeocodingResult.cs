using System;
using System.Collections.Generic;
using System.Text;

namespace dieuxe.Models
{
    public class GeocodingResult
    {
        public class Plus_code
        {
            public string compound_code { get; set; }
            public string global_code { get; set; }

        }
        public class Address_components
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public List<string> types { get; set; }

        }
        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }

        }
        public class Northeast
        {
            public double lat { get; set; }
            public double lng { get; set; }

        }
        public class Southwest
        {
            public double lat { get; set; }
            public double lng { get; set; }

        }
        public class Viewport
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }

        }
        public class Geometry
        {
            public Location location { get; set; }
            public string location_type { get; set; }
            public Viewport viewport { get; set; }

        }
        public class Results
        {
            public List<Address_components> address_components { get; set; }
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string place_id { get; set; }
            public List<string> types { get; set; }

        }
        public class ketqua
        {
            public Plus_code plus_code { get; set; }
            public List<Results> results { get; set; }
            public string status { get; set; }

        }
    }
}
