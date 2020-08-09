using System;
using System.Collections.Generic;
using System.Text;

namespace dieuxe.Models
{
    public class placesearchdetail
    {

        public partial class Temperatures
        {
            public List<prediction> predictions { get; set; }
            public string status { get; set; }
        }

        public class prediction//trả về kq địa điểm
        {
            public string description { get; set; }
            public string id { get; set; }
            public List<matchedSubstring> matched_substrings { get; set; }
            public string place_id { get; set; }
            public string reference { get; set; }
            public structured_formatting structured_formatting { get; set; }

            public List<Term> terms { get; set; }
            public List<string> types { get; set; }
        }

        public class matchedSubstring
        {
            public int length { get; set; }
            public int offset { get; set; }
        }

        public class structured_formatting
        {
            public string main_text { get; set; }

            public List<matchedSubstring> main_text_matched_substrings { get; set; }
            public string secondary_text { get; set; }
        }

        public class Term
        {
            public long offset { get; set; }
            public string value { get; set; }
        }
        public class AddressInfo
        {

            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string ZipCode { get; set; }

            public double Longitude { get; set; }

            public double Latitude { get; set; }
        }
        public class Addressdi
        {

            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string ZipCode { get; set; }

            public double Longitude { get; set; }

            public double Latitude { get; set; }
        }
        public class Addressdden
        {

            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string ZipCode { get; set; }

            public double Longitude { get; set; }

            public double Latitude { get; set; }
        }
    }
}
