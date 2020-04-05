using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;

namespace Microsoft.AspNetCore.Components.Desktop
{
    public interface IDesktopApplicationBuilder :IApplicationBuilder
    {
        void AddComponent(Type componentType, string domElementSelector);
    }
    internal class DesktopApplicationBuilder: IDesktopApplicationBuilder
    {
        public DesktopApplicationBuilder(IServiceProvider services)
        {
            Services = services;
            Entries = new List<(Type componentType, string domElementSelector)>();
        }

        public List<(Type componentType, string domElementSelector)> Entries { get; }

        public IServiceProvider Services { get; }



        public void AddComponent(Type componentType, string domElementSelector)
        {
            if (componentType == null)
            {
                throw new ArgumentNullException(nameof(componentType));
            }

            if (domElementSelector == null)
            {
                throw new ArgumentNullException(nameof(domElementSelector));
            }

            Entries.Add((componentType, domElementSelector));
        }

        public IServiceProvider ApplicationServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IFeatureCollection ServerFeatures => throw new NotImplementedException();

        public IDictionary<string, object> Properties => throw new NotImplementedException();

      
        
        
        IServiceProvider IApplicationBuilder.ApplicationServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        IFeatureCollection IApplicationBuilder.ServerFeatures => throw new NotImplementedException();

        IDictionary<string, object> IApplicationBuilder.Properties => throw new NotImplementedException();

        RequestDelegate IApplicationBuilder.Build()
        {
            throw new NotImplementedException();
        }

        IApplicationBuilder IApplicationBuilder.New()
        {
            throw new NotImplementedException();
        }

        IApplicationBuilder IApplicationBuilder.Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            throw new NotImplementedException();
        }
    }
}
