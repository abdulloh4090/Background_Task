using System.Threading.Channels;

namespace Project.Servcies
{
    public class QueueService<T>
    {
        private readonly Channel<T> _channel = Channel.CreateUnbounded<T>();

        #region Methods region

        #region Public methods region
        public async Task EnqueueAsync(T item)
        => await _channel.Writer.WriteAsync(item);

        public async Task<T> DequeueAsync(CancellationToken cancellationToken)
        => await _channel.Reader.ReadAsync(cancellationToken);

        #endregion

        #endregion
    }
}
