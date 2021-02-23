namespace MKraken.Domain.Routing
{
    public interface IRouteManager
    {
        string GetRouteTo(string internalRoute);

        string GetRouteTo(string internalRoute, string internalReturnRoute);

        void NavigateTo(string internalRoute);
    }
}