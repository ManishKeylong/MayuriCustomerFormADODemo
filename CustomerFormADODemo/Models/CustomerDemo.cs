using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerFormADODemo.Models
{
    public class CustomerDemo
    {
        public int Id { get; set; }
        [Required]
        public string Cust_Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Mobile_No { get; set; }
    }
}