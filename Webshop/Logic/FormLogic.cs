using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Logic
{
    public class FormLogic
    {
        public bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; //Add more if needed.
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
    

}