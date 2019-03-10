using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public interface IDiaryRepository
    {
        IDiaryItem Create(IDiaryItem item);

        Dictionary<Guid, IDiaryItem> Read();

        IDiaryItem Update(IDiaryItem item);

        bool Delete(IDiaryItem item);
    }
}
