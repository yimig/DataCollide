using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataCollide.Data.Reader
{
    public interface IFileReader
    {
        public Task<TableFile> GetTableFileAsync();
    }


}
