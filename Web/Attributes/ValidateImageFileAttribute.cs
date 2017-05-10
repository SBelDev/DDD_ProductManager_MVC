using Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace Web.Attributes
{
    public class ValidateImageFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }

            if (file.ContentLength > 1 * 1024 * 1024)
            {
                return false;
            }

            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; 

            foreach (var item in formats)
            {
                if (file.FileName.EndsWith(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}