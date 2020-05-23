using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Spy.Entities
{
    public class Log : EventArgs,  ILogger
    {
        public Log()
        {
        }

        public void Gravar()
        {
            this.LogTrace("Alguem criou um arquivo!");
        }


        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = String.Empty;
            var parameters = (state as IEnumerable<KeyValuePair<string, object>>)?.ToDictionary(i => i.Key, i => i.Value);
            if (formatter != null)
            {
                message += formatter(state, exception);
            }

            using (StreamWriter sr = new StreamWriter($"{Guid.NewGuid()}.txt"))
                sr.WriteLine(JsonConvert.SerializeObject(new { Date = DateTime.UtcNow.ToString("MM/dd/yyyy H:mm"), Message = message}));

        }
    }
}
