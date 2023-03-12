using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafriSoftv1._3.Models.Data
{
    public class Software
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}