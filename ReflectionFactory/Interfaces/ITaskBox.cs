using System.Reflection;

namespace ReflectionFactory.Interfaces
{
    /// <summary>
    /// 處理不同任務的集合
    /// </summary>
    public interface ITaskBox
    {
        public ITaskResponse? DoTask(ITaskRequest request);
        public ITaskResponse? DoTask(string code);
        public void AddTasks(Assembly assembly);
        public void RemoveTask(string code);

    }
}
