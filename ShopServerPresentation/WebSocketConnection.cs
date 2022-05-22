using System;
using System.Threading.Tasks;

namespace ShopServerPresentation
{
    public abstract class WebSocketConnection
    {
        protected abstract Task SendTask(string message);

        public abstract Task Disconnect();

        public async Task SendAsync(string message)
        {
            await SendTask(message);
        }

        public virtual Action<string> onMessage { set; protected get; } = x => { };
        public virtual Action onClose { set; protected get; } = () => { };
        public virtual Action onError { set; protected get; } = () => { };
    }
}