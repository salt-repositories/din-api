using System;
using System.Text;

namespace Din.Service.Clients.Abstractions
{
    public abstract class BaseClient
    {
        protected string BuildUrl(params string[] p)
        {
            var sb = new StringBuilder();

            foreach (var i in p)
            {
                sb.Append(i);
            }

            return sb.ToString();
        }

        protected string GetCalendarTimeSpan()
        {
            return $"&start={DateTime.Today.AddDays(-14):yyyy-MM-dd}&end={DateTime.Today.AddMonths(1):yyyy-MM-dd}";
        }
    }
}
