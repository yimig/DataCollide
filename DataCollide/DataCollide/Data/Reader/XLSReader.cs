using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataCollide.Data.Reader
{
    public class XLSReader : IFileReader
    {
        public XLSReader(StorageFile file)
        {

        }

        public async Task<TableFile> GetTableFileAsync()
        {
            throw new NotImplementedException();
        }
    }
}
