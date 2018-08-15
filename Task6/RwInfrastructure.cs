using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    interface LibraryInfrastructure<T>
    {
        IEnumerable<T> Read();
        void Write(T item);
        void Flush();
    }
}
