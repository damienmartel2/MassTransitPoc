using System.Reflection;
using MassTransit;

namespace MassTransitLibrary.Contracts.MyTask
{
    public class TaskTypeFilter<TConsumer> :
        IFilter<ConsumerConsumeContext<TConsumer, MyTask>>
        where TConsumer : class
    {
        readonly string _taskType;

        public TaskTypeFilter()
        {
            var attribute = typeof(TConsumer).GetCustomAttribute<TaskTypeAttribute>();

            if(attribute == null)
            {
                throw new ArgumentException("Message does not have the attribute required");
            }

            _taskType = attribute.Type;
        }


        public async Task Send(ConsumerConsumeContext<TConsumer, MyTask> context, IPipe<ConsumerConsumeContext<TConsumer, MyTask>> next)
        {
            if (context.Message.Type.Equals(_taskType))
            {
                await next.Send(context);
            }
        }

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateFilterScope("taskType");
            scope.Add("type", _taskType);
        }
    }
}
