using ReflectionFactory.Interfaces;
using ReflectionFactory.Objects.Attributes;
using System.Reflection;

namespace ReflectionFactory.Objects
{
    /// <summary>
    /// 使用該物件能執行不同的任務
    /// </summary>
    public class TaskBox : ITaskBox
    {
        private readonly Dictionary<string,ITask> _taskers = new Dictionary<string,ITask>();
        
        public void AddTasks(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new Exception("command box need to load a assembly");
            }

            try
            {
                // 挑選有該屬性的工廠
                //IEnumerable<Type> factorys = assembly.GetTypes()
                //                .Where(factory => factory.GetCustomAttributes<TaskFactoryAttribute>().Count() > 0);

                IEnumerable<Type> factories = assembly.GetTypes()
                        .Where(factory => typeof(ITaskFactory).IsAssignableFrom(factory));

                foreach (var factory in factories)
                {
                    Type factoryType = assembly.GetType(factory.FullName);

                    ITaskFactory factoryObj = Activator.CreateInstance(factoryType) as ITaskFactory;

                    string taskCode = factoryObj.Code;

                    // 生產任務
                    ITask task = factoryObj.GetTask();

                    this._taskers.Add(taskCode, task);
                }
            }
            catch
            {
                throw;
            }
        }

        public void RemoveTask(string key)
        {
            this._taskers.Remove(key);
        }

        public ITaskResponse? DoTask(string code)
        {
            try
            {
                ITask task;

                bool haveTask = this._taskers.TryGetValue(code, out task);

                if (!haveTask)
                {
                    throw new Exception($"no task in box code is {code}");
                }

                ITaskResponse response = task.Execute();

                return response;
            }
            catch
            {
                throw;
            }

        }
        public ITaskResponse? DoTask(ITaskRequest request)
        {
            try
            {
                ITask task;

                string code = request.Code;

                bool haveTask = this._taskers.TryGetValue(code, out task);

                if (!haveTask)
                {
                    throw new Exception($"no task in box code is {code}");
                }

                ITaskResponse response = task.Execute(request);

                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
