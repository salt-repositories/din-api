namespace Din.Service.Config.Abstractions
{
    public abstract class BaseClientConfig
    {
        public string Url { get; }
        public string Key { get; }

        public BaseClientConfig(string url, string key)
        {
            Url = url;
            Key = key;
        }
    }
}
