using Microsoft.AspNetCore.Components;
using System;

namespace MKraken.Domain.Routing
{
    public class RouteManager : IRouteManager
    {
        public RouteManager(NavigationManager navigationManager)
        {
            this.NavigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public NavigationManager NavigationManager { get; }

        public string GetRouteTo(string internalRoute)
        {
            return $"{this.NavigationManager.BaseUri}{internalRoute.TrimStart('/')}";
        }

        public string GetRouteTo(string internalRoute, string internalReturnRoute)
        {
            return $"{this.GetRouteTo(internalRoute)}?ReturnUrl={internalReturnRoute}";
        }

        public void NavigateTo(string internalRoute)
        {
            if (internalRoute.StartsWith(this.NavigationManager.BaseUri, StringComparison.InvariantCultureIgnoreCase))
            {
                this.NavigationManager.NavigateTo(this.GetRouteTo(internalRoute.Remove(0, this.NavigationManager.BaseUri.Length - 1)));
            }
            else
            {
                this.NavigationManager.NavigateTo(this.GetRouteTo(internalRoute));
            }
        }
    }
}