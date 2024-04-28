using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataCollide.Data.Reader
{
    public class CSVReader : IFileReader
    {
        private StorageFile File { get; set; }
        private List<List<string>> Data { get; set; }

        public CSVReader(StorageFile file)
        {
            File = file;
            Data = new List<List<string>>();
        }

        private void ReadByEncoding(Encoding encoding)
        {
            var handler = File.CreateSafeFileHandle(FileAccess.Read, FileShare.Read, FileOptions.SequentialScan);
            using (var stream = new FileStream(handler, FileAccess.Read))
            {
                Data.Clear();
                TextReader reader = new StreamReader(stream, encoding);
                CsvReader csv = new CsvReader(reader, CultureInfo.CurrentCulture);

                while (csv.Read())
                {
                    for (var i = 0; i < csv.ColumnCount; i++)
                    {
                        if (i > Data.Count - 1)
                        {
                            Data.Add(new List<string>());
                        }
                        Data[i].Add(csv.GetField(i));
                    }
                }

            }

        }

        public async Task<TableFile> GetTableFileAsync()
        {
            await Task.Run(() => ReadByEncoding(Encoding.UTF8));
            return new TableFile()
            {
                FilePath = File.Path,
                Name = File.Name,
                Format = TableFormat.CSV,
                SheetList = new List<string>(),
                Encoding = Encoding.UTF8,
                TableList = new List<Table>() { new Table(Data) }
            };
        }

        public async Task<TableFile> ResetEncodingAsync(Encoding encoding)
        {
            await Task.Run(() => ReadByEncoding(encoding));
            return new TableFile()
            {
                FilePath = File.Path,
                Name = File.Name,
                Format = TableFormat.CSV,
                SheetList = new List<string>(),
                Encoding = Encoding.UTF8,
                TableList = new List<Table>() { new Table(Data) }
            };
        }
    }
}
