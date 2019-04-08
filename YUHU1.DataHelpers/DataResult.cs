using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUHU1.DataHelpers
{
    /// <summary>
    /// A generic class that holds a value of certain operation and status result class
    /// which can be used to detect whether certain operation succeeded or not
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public class DataResult<T>
    {
        public DataResult(T data, StatusResult status, int Id = 0)
        {
            Data = data;
            Status = status;
            ID = Id;
        }
        /// <summary>
        /// Main value passed as a result
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Status of the operation
        /// </summary>
        public StatusResult Status { get; private set; }

        public int ID { get; set; }

    }
}
