using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollide.Data
{
    public class TableFile
    {
        public String FilePath { get; set; }
        public String Name { get; set; }
        public TableFormat Format { get; set; }
        public List<string> SheetList { get; set; }
        public List<Table> TableList { get; set; }
        public Encoding Encoding { get; set; }

    }

    public enum TableFormat
    {
        XLSX,XLS,CSV
    }
}
