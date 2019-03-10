using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Diary
{
    class DiaryRepository : IDiaryRepository
    {
        private Dictionary<Guid, IDiaryItem> repository;

        public DiaryRepository(string XmlFilePath)
        {
            this.Load(XmlFilePath);
        }

        public bool Load(string XmlFilePath)
        {
            if (!System.IO.File.Exists(XmlFilePath))
                throw new ArgumentException("Argument XmlFilePath must contain a valid path to a XML file.");
            XDocument xmlRepo;

            xmlRepo = XDocument.Load(XmlFilePath);

            Dictionary<Guid, IDiaryItem> tmpRepo = new Dictionary<Guid, IDiaryItem>();
            foreach (XElement item in xmlRepo.Root.Elements("item"))
            {
                Dictionary<string, string[]> itemDataDict = new Dictionary<string, string[]>();
                foreach (XElement itemProperty in item.Elements("property"))
                {
                    List<string> values = new List<string>();
                    foreach (XElement propertyValue in itemProperty.Elements("value"))
                    {
                        values.Add(propertyValue.Value);
                    }
                    itemDataDict.Add(itemProperty.Attribute("name").Value, values.ToArray());
                }
                IDiaryItem diaryItem = Global.classFactory.Create<IDiaryItem>(itemDataDict);
                tmpRepo.Add(diaryItem.guid, diaryItem);
            }
            this.repository = tmpRepo;
            return true;

        }
        public bool Save(string XmlFilePath)
        {
            XDocument document = new XDocument();
            XElement root = new XElement("root");
            foreach (KeyValuePair<Guid, IDiaryItem> item in this.repository)
            {
                XElement itemNode = new XElement("item");

                XElement guid = new XElement("property");
                guid.SetAttributeValue("name", "guid");
                XElement guidValue = new XElement("value");
                guidValue.Value = item.Value.guid.ToString();
                guid.Add(guidValue);

                XElement title = new XElement("property");
                title.SetAttributeValue("name", "title");
                XElement titleValue = new XElement("value");
                titleValue.Value = item.Value.title;
                title.Add(titleValue);

                XElement description = new XElement("property");
                description.SetAttributeValue("name", "description");
                XElement descriptionValue = new XElement("value");
                descriptionValue.Value = item.Value.description;
                description.Add(descriptionValue);

                XElement dateTimeStart = new XElement("property");
                dateTimeStart.SetAttributeValue("name", "dateTimeStart");
                XElement dateTimeStartValue = new XElement("value");
                dateTimeStartValue.Value =
                    item.Value.dateTimeStart.ToShortDateString() + " " +
                    item.Value.dateTimeStart.ToShortTimeString();
                dateTimeStart.Add(dateTimeStartValue);

                XElement dateTimeEnd = new XElement("property");
                dateTimeEnd.SetAttributeValue("name", "dateTimeEnd");
                XElement dateTimeEndValue = new XElement("value");
                dateTimeEndValue.Value =
                    item.Value.dateTimeEnd.ToShortDateString() + " " +
                    item.Value.dateTimeEnd.ToShortTimeString();
                dateTimeEnd.Add(dateTimeEndValue);

                XElement labels = new XElement("property");
                labels.SetAttributeValue("name", "labels");
                foreach (string label in item.Value.labels)
                {
                    XElement labelsValue = new XElement("value");
                    labelsValue.Value = label;
                    labels.Add(labelsValue);
                }

                itemNode.Add(guid);
                itemNode.Add(title);
                itemNode.Add(description);
                itemNode.Add(dateTimeStart);
                itemNode.Add(dateTimeEnd);
                itemNode.Add(labels);

                root.Add(itemNode);
            }
            document.Add(root);

            document.Save(XmlFilePath);
            return true;
        }
        public IDiaryItem Create(IDiaryItem item)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is null.");
            }
            if (!repository.ContainsKey(item.guid))
            {
                repository.Add(item.guid, (IDiaryItem)item.Clone());
            }
            return (IDiaryItem)repository[item.guid].Clone();
        }

        public bool Delete(IDiaryItem item)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is null.");
            }
            if (repository.ContainsKey(item.guid))
            {
                repository.Remove(item.guid);
            }
            return true;
        }

        public Dictionary<Guid, IDiaryItem> Read()
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is null.");
            }
            Dictionary<Guid, IDiaryItem> re = new Dictionary<Guid, IDiaryItem>(this.repository.Count);
            foreach (KeyValuePair<Guid, IDiaryItem> item in this.repository)
            {
                re.Add(item.Key, (IDiaryItem)item.Value.Clone());
            }
            return re;
        }

        public IDiaryItem Update(IDiaryItem item)
        {
            if (repository == null)
            {
                throw new InvalidOperationException("Repository is null.");
            }
            if (repository.ContainsKey(item.guid))
            {
                repository[item.guid] = (IDiaryItem)item.Clone();
            }
            var re = Global.classFactory.Create<IDiaryItem>(repository[item.guid]);
            return re;
        }
        public bool isLoaded()
        {
            if (repository == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
