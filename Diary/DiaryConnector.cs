using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public class DiaryConnector : IDiaryConnector
    {
        public IDiaryItem Create(IDiaryItem item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IDiaryItem item)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Guid, IDiaryItem> Read(IDiaryItem item)
        {
            throw new NotImplementedException();
        }

        public IDiaryItem Update(IDiaryItem item)
        {
            throw new NotImplementedException();
        }
    }
}
