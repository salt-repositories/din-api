namespace Din.Domain.Config.Abstractions
{
    public abstract class BaseClientConfig
    {
        public string Url { get; }
        public string Key { get; }

        protected BaseClientConfig(string url, string key)
        {
            Url = url;
            Key = key;
        }
    }
}
