using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataCollide.Data.Reader
{
    public static class ReaderFactory
    {
        public static IFileReader ReadFile(StorageFile file)
        {
            return file.FileType switch
            {
                ".xlsx" => new XLSXReader(file),
                ".xls" => new XLSXReader(file),
                ".csv" => new CSVReader(file),
                _ => throw new FormatException("格式不支持"),
            };
        }
    }
}
