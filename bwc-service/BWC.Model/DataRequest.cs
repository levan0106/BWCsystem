using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWC.Model
{
    public class DataRequest<T>:DataRequest<T,T>
    {
    }
    public class DataRequest<T, T1>:DataRequest<T,T1,T1>
    {
    }
    public class DataRequest<T, T1, T2>
    {
        public T Value { get; set; }
        public T1 Value1 { get; set; }
        public T2 Value2 { get; set; }
    }
}
