using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public class DiaryItem : IDiaryItem
    {
        public DateTime dateTimeEnd { get; private set; }

        public DateTime dateTimeStart { get; private set; }

        public string description { get; private set; }

        public Guid guid { get; private set; }

        public string[] labels { get; private set; }

        public string title { get; private set; }

        DiaryItem(Guid guid, string title, string description, 
            DateTime dateTimeStart, DateTime dateTimeEnd, string[] labels)
        {
            this.guid = guid;
            this.title = title;
            this.description = description;
            this.dateTimeStart = dateTimeStart;
            this.dateTimeEnd = dateTimeEnd;
            this.labels = labels;
        }
    }
}
