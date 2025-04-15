using System.Threading.Channels;

namespace BackgroundTaskHandlerAPI.Utils.Queue.Implementation;

public class BackgroundTaskQueue(int capacity = 100) : IBackgroundTaskQueue
{
    private readonly Channel<Func<CancellationToken, Task>> _queue = Channel
        .CreateBounded<Func<CancellationToken, Task>>(capacity);

    public ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, Task> workItem)
        => _queue.Writer.WriteAsync(workItem);

    public ValueTask<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        => _queue.Reader.ReadAsync(cancellationToken);
}