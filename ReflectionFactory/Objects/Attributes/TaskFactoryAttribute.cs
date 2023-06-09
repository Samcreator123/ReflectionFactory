using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionFactory.Objects.Attributes
{
    public class TaskFactoryAttribute : Attribute
    {
        private readonly string _code;
        public string Code => this._code;

        public TaskFactoryAttribute(string code)
        {
            _code = code;
        }
    }
}
