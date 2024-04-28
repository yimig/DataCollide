using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollide.Data
{
    public class Table
    {
        private List<List<string>> Data { get; set; }

        private int HeaderRow { get; set; }

        public List<string> Headers 
        {
            get => GetRow(HeaderRow);
        }

        public int MaxRows
        { 
            get => Data.Max(i => i.Count - HeaderRow); 
        }

        public int Columns
        {
            get => Data.Count;
        }

        public string this[int column, int row]
        {
            get => Data[column][HeaderRow + row + 1];
        }

        public Table(List<List<string>> data)
        {
            HeaderRow = 0;
            Data = data;
        }

        public void ResetHeaderToNextRow()
        {
            if (HeaderRow < Data.Max(i => i.Count))
            {
                HeaderRow++;
            }
            else
            {
                throw new InvalidOperationException("当前已是最大行");
            }
            
        }

        public void ResetHeaderToPreviousRow()
        {
            if (HeaderRow > 0)
            {
                HeaderRow--;
            }
            else
            {
                throw new InvalidOperationException("当前已是最小行");
            }
        }

        public List<string> GetColumn(int columnIndex)
        {
            return Data[columnIndex].SkipWhile((_, index) => index <= HeaderRow).ToList();
        }

        public List<string> GetRow(int rowIndex)
        {
            var result = new List<string>();
            foreach(var column in Data)
            {
                var value = column.ElementAtOrDefault(rowIndex);
                result.Add(String.IsNullOrEmpty(value) ? "": value);
            }
            return result;
        }

        public List<string> ColumnGlance(int columnIndex, int maxRow)
        {
            return Data[columnIndex].SkipWhile((_, index) => index <= HeaderRow || index > maxRow).ToList();
        }
    }
}
