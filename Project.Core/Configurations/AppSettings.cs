using Project.Core.Enums;

namespace Project.Core.Configurations
{
    public class AppSettings
    {
        public string HttpSchema { get; set; }

        public string Url { get; set; }

        public string Endpoint { get; set; }

        public Browsers Browser { get; set; }
    }
}
