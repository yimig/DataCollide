using Microsoft.Scripting.Utils;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataCollide.Data.Reader
{
    public class XLSXReader : IFileReader
    {
        private StorageFile File { get; set; }
        private List<List<List<string>>> Data { get; set; }

        private List<string> SheetNames { get; set; }

        public XLSXReader(StorageFile file)
        {
            File = file;
            Data = new List<List<List<string>>>();
        }

        public void ReadExcel()
        {
            SheetNames = new List<string>();
            var handler = File.CreateSafeFileHandle(FileAccess.Read, FileShare.Read, FileOptions.SequentialScan);
            using (var stream = new FileStream(handler, FileAccess.Read))
            {
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    foreach (var sheet in package.Workbook.Worksheets)
                    {
                        List<List<string>> sheetList = new List<List<string>>();
                        SheetNames.Add(sheet.Name);
                        if(sheet.Dimension is null)
                        {
                            sheetList.Add(new List<string>());
                            continue;
                        }
                        for(int col = 1; col <= sheet.Dimension.Columns; col++)
                        {
                            var colList = new List<string>();
                            for(int row = 1; row <= sheet.Dimension.Rows; row++)
                            {
                                try
                                {
                                    colList.Add(sheet.Cells[row, col].Value.ToString());
                                } catch (NullReferenceException)
                                {
                                    colList.Add("");
                                }
                                
                            }
                            sheetList.Add(colList);
                        }
                        Data.Add(sheetList);
                    }
                }
            }
        }

        public async Task<TableFile> GetTableFileAsync()
        {
            await Task.Run(() => ReadExcel());
            return new TableFile()
            {
                FilePath = File.Path,
                Name = File.Name,
                Format = File.Path.ToLower().EndsWith(".xlsx") ? TableFormat.XLSX: TableFormat.XLS,
                SheetList = SheetNames,
                Encoding = Encoding.UTF8,
                TableList = new List<Table>(Data.Select(i => new Table(i)))
            };
        }
    }
}
