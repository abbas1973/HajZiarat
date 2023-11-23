using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities
{


    /// <summary>
    /// مدل برای تبدیل enum به کلاس
    /// </summary>
    public class EnumVM
    {
        public int? Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
    }
}
