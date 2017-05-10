using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.Attributes;

namespace Web.ViewModels
{
    public class SelectedTag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }
        public bool Selected { get; set; }
    }
}