using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionFactory.Interfaces
{
    /// <summary>
    /// 任務回復
    /// </summary>
    public interface ITaskResponse
    {
        /// <summary>
        /// 任務代碼
        /// </summary>
        public string Code { get; }
    }
}
