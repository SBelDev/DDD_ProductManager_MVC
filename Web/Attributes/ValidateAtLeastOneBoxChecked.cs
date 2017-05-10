using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using Web.ViewModels;

namespace Web.Attributes
{
    public class ValidateAtLeastOneBoxChecked : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if ((value as IEnumerable<SelectedTag>).Any(l => l.Selected)) return true;
            }
            return false;
        }
    }
}
