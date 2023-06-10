using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionFactory.Interfaces
{
    /// <summary>
    /// 生產類別的工廠，必須要有無參數的建構子
    /// </summary>
    public interface ITaskFactory
    {
        public string Code { get; }
        public ITask GetTask();
    }
}
