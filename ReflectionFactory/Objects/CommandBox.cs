using ReflectionFactory.Interfaces;
using ReflectionFactory.Objects.Attributes;
using System.Reflection;

namespace ReflectionFactory.Objects
{
    /// <summary>
    /// 使用該物件能執行不同的任務
    /// </summary>
    public class CommandBox : ICommandBox
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
                var factorys = assembly.GetTypes()
                                .Where(factory => factory.GetCustomAttributes<TaskFactoryAttribute>().Count() > 0);

                foreach (var factory in factorys)
                {
                    Type factoryType = assembly.GetType(factory.FullName);

                    ICommandFactory factoryObj = Activator.CreateInstance(factoryType) as ICommandFactory;

                    string taskCode = factoryType.GetCustomAttribute<TaskFactoryAttribute>().Code;

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
