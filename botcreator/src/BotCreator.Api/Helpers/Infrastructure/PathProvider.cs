using BotCreator.Core.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace BotCreator.Api.Helpers.Infrastructure
{
    public class PathProvider : IPathProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public PathProvider(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public string ExecutableRootPath
        {
            get
            {
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(location);

                return directory;
            }
        }
    }
}
