using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafriSoftv1._3.Models.Data
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
        public decimal PackagePrice { get; set; }
    }
}