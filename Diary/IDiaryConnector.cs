using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    interface IDiaryConnector
    {
        IDiaryItem Create(IDiaryItem item);

        Dictionary<Guid, IDiaryItem> Read(IDiaryItem item);

        IDiaryItem Update(IDiaryItem item);

        bool Delete(IDiaryItem item);
    }
}
