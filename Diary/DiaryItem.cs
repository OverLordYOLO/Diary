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

        public DiaryItem(Dictionary<string, string[]> args)
        {
            if (args.ContainsKey("guid"))
            {
                this.guid = new Guid(args["guid"][0]);
            }
            else
            {
                this.guid = new Guid();
            }
            if (args.ContainsKey("title"))
            {
                this.title = args["title"][0];
            }
            else
            {
                this.title = "";
            }
            if (args.ContainsKey("description"))
            {
                this.description = args["description"][0];
            }
            else
            {
                this.description = "";
            }
            if (args.ContainsKey("dateTimeStart"))
            {
                this.dateTimeStart = DateTime.Parse(args["dateTimeStart"][0]);
            }
            else
            {
                throw new ArgumentException("Property dateTimeStart must be passed.", "args");
            }
            if (args.ContainsKey("dateTimeEnd"))
            {
                this.dateTimeEnd = DateTime.Parse(args["dateTimeEnd"][0]);
            }
            else
            {
                throw new ArgumentException("Property dateTimeEnd must be passed.", "args");
            }
            if (args.ContainsKey("labels"))
            {
                this.labels = args["labels"];
            }
            else
            {
                this.labels = new string[] { "GeneralItem" };
            }

        }
        public DiaryItem(IDiaryItem diaryItem)
        {
            this.guid = diaryItem.guid;
            this.title = diaryItem.title;
            this.description = diaryItem.description;
            this.dateTimeStart = diaryItem.dateTimeStart;
            this.dateTimeEnd = diaryItem.dateTimeEnd;
            this.labels = diaryItem.labels;
        }

        public object Clone()
        {
            return new DiaryItem(this);
        }
    }
}
