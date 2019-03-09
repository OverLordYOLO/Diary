using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    interface IClassFactory
    {
        T Create<T>() where T : class;

        T Create<T>(params object[] args) where T : class;
    }
}
