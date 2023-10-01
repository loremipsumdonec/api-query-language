namespace ApiQueryLanguage.LanguageV1
{
    public class RequestBuilder
    {
        public static RequestBuilder Create()
        {
            return new RequestBuilder();
        }

        private RequestBuilder()
        {
        }

        public RequestBuilder UseFilter(IFilter? filter)
        {
            return this;
        }

        public RequestBuilder UseProperties(IEnumerable<IProperty>? properties)
        {
            return this;
        }

        public RequestBuilder UseOrderBy(IEnumerable<IOrderByProperty>? orderBy)
        {
            return this;
        }

        public IRequest Build()
        {
            return new Request();
        }
    }

    public static class RequestExtensions
    {
        public static IQueryable<T> ApplyOn<T>(this IRequest request, IQueryable<T> queryable)
            where T: class
        {
            return queryable;
        }
    }
}
