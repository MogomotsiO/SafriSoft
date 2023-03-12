using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafriSoftv1._3.Models.Data
{
    public class PackageFeature
    {
        [Key]
        public int Id { get; set; }
        public string FeatureName { get; set; }
        public string FeatureDescription { get; set; }
        public int PackageId { get; set; } 
    }
}