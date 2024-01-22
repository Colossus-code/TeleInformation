using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    /// <summary>
    /// Model request for the api, in this case it would be null to get all information
    /// </summary>
    public class InputModel
    {
        public int MobileID { get; set; }
        public string MobileName {  get; set; }
        public string MobileFabricator {  get; set; }

    }
}