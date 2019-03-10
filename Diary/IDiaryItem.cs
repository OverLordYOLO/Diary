using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public interface IDiaryItem : ICloneable
    {
        Guid guid { get; }
        string title { get; }
        string description { get; }
        DateTime dateTimeStart { get; }
        DateTime dateTimeEnd { get; }
        string[] labels { get; }
    }
}
