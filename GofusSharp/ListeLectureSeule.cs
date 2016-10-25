using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GofusSharp
{
    public class ListeLectureSeule<T> : System.Collections.ObjectModel.ReadOnlyCollection<T>
    {
        public ListeLectureSeule(IList<T> list) : base(list)
        {
        }
    }
}
