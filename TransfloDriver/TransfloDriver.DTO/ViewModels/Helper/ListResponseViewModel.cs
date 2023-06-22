using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransflowDriver.DTO.ViewModels.Helper
{
    public class ListResponseViewModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int ItemCount { get; set; }
    }
}
