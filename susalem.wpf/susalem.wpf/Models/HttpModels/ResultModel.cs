using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Models.HttpModels
{
    public class ResultModel
    {
        public bool Success { get; set; } = true;
        public int State { get; set; } = 200;
        public string ExceptionMessage { get; set; }
        public object Data { get; set; }
    }

    public class ResultModel<T>
    {
        public bool Success { get; set; } = true;
        public int State { get; set; } = 200;
        public string ExceptionMessage { get; set; }
        public T Data { get; set; }
    }
}
