using System;
using System.IO;
using System.Reflection;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using Windows.Media.ContentRestrictions;

namespace Microsoft.AspNetCore.Components.Desktop
{
    internal class ContentRootResolver : IUriToStreamResolver
    {
        public ContentRootResolver(Func<Uri, Stream> customResolution = null) {
            CustomResolution = customResolution;
        }
        public Stream UriToStream(Uri uri)
        {
            if (uri.Scheme != "ms-local-stream")
            {
                throw new ArgumentException($"{nameof(ContentRootResolver)} should only be used for local stream URIs");
            }

            Console.WriteLine($"Resolving local stream URI {uri}");

            var localPath = uri.LocalPath;
            var frameworkPrefix = "/_framework/";
           
            if (localPath.StartsWith(frameworkPrefix))
            {
                localPath = localPath.Substring(frameworkPrefix.Length);
                switch (localPath)
                {
                    case "blazor.desktop.js":
                        return typeof(ContentRootResolver).Assembly.GetManifestResourceStream("Microsoft.AspNetCore.Components.Desktop.blazor.desktop.js");
                    default:
                        return CustomResolution?.Invoke(uri) ?? throw new ArgumentException($"Unknown framework file: {uri}");
                }
            }
            else if (localPath.StartsWith('/'))
            {
                localPath = localPath.Replace('/', Path.DirectorySeparatorChar).Substring(1);

                var filePath = Path.Combine(ContentRoot.Value, localPath);

                if (File.Exists(filePath))
                {
                    return File.OpenRead(filePath);
                }
                else
                {
                  return  CustomResolution?.Invoke(uri) ?? throw new FileNotFoundException($"Local stream URI '{uri}' does not correspond to an existing file on disk", filePath);
                }
            }
            else
            {
                return CustomResolution?.Invoke(uri) ?? throw new ArgumentException($"Expected local path to start with '/', but received value '{uri.LocalPath}'");
            }
        }


       

        private Lazy<string> ContentRoot = new Lazy<string>(() =>
        {
            var startDir = Path.GetDirectoryName(typeof(ContentRootResolver).Assembly.Location);
            
            var dir = startDir;
            while (!string.IsNullOrEmpty(dir))
            {
                var candidate = Path.Combine(dir, "wwwroot");
                if (Directory.Exists(candidate))
                {
                    return candidate;
                }

                dir = Path.GetDirectoryName(dir);
            }

            throw new DirectoryNotFoundException($"Could not find wwwroot in '{startDir}' or any ancestor directory");
        });

        public DirectoryInfo LocalRoot { get; }
        public Func<Uri, Stream> CustomResolution { get; }
    }
}
