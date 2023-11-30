using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.Models
{
    public class DateCheck : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = Convert.ToDateTime(value);
            int year = date.Year;
            int currenetYear = DateTime.Now.Year;
            if (currenetYear - year >= 22)
            {
                return true;
            }
            else
            {
                return false;
            }
            //return base.IsValid(value);
        }
    }
}