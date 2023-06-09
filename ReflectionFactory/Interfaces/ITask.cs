namespace ReflectionFactory.Interfaces
{
    /// <summary>
    /// 任務
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// 任務代碼
        /// </summary>
        public string Code { get; }
        /// <summary>
        /// 有需求的執行任務
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ITaskResponse? Execute(ITaskRequest request);
        /// <summary>
        /// 沒需求的執行任務
        /// </summary>
        /// <returns></returns>
        public ITaskResponse? Execute();
    }
}
