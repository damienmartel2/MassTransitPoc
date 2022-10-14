namespace MassTransitLibrary.Contracts.MyTask
{
    public class TaskTypeAttribute : Attribute
    {
        public string Type { get; }

        public TaskTypeAttribute(string type)
        {
            Type = type;
        }
    }
}
