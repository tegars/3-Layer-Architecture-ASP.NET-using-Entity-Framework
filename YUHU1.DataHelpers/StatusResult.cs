using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUHU1.DataHelpers
{
    /// <summary>
    /// Status Result class that contains a flag and list of errors which indicate whether certain
    /// operation succeeded or not
    /// </summary>
    public class StatusResult
    {
        public StatusResult(bool isSuccess, List<string> errors, int Id = 0)
        {
            IsSuccess = isSuccess;
            Errors = errors;
            ID = Id;
        }
        /// <summary>
        /// A flag that indicates certain operation succeeded or not
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// List of errors
        /// </summary>
        public List<string> Errors { get; set; }
        public int ID { get; set; }
    }
}
