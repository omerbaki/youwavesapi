using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastAnalysisModel
{
    public class StorageAttribute : Attribute
    {
        public string Name { get; set; }

        public StorageAttribute(string name)
        {
            this.Name = name;
        }
    }
}
