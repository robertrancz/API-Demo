using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnifiApiDemo.Models
{
    public class UnifiAPIViewModel
    {
        [Required(ErrorMessage = "Required Field")]
        [Url(ErrorMessage = "Please enter a valid url")]
        public string UnifiServerURI { get; } = "https://unifiapi.waters.com:50034/unifi/v1/";

        [Required(ErrorMessage = "Required Field")]
        public string EndpointAddress { get; } = "sampleresults";

        public string SelectedAnalyticalDataType { get; set; } = "MSe";

        public IEnumerable<SelectListItem> AnalyticalDataTypes { get; set; }

        public string MzmlPath { get; set; }
    }
}
