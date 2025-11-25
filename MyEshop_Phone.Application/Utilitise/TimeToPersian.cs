using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Utilitise
{
    public static class TimeToPersian
    {
        public static string ToShamsi(this DateTime time)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(time) + "/" + pc.GetMonth(time).ToString("00") + "/" + pc.GetDayOfMonth(time).ToString("00");
        }
    }
}
