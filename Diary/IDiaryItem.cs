using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    interface IDiaryItem
    {
        Guid guid { get; set; }
        string title { get; set; }
        string description { get; set; }
        DateTime dateTimeStart { get; set; }
        DateTime dateTimeEnd { get; set; }
        string[] labels { get; set; }
    }
}
