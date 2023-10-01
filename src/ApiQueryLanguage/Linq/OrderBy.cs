using System.Linq.Expressions;
using System.Reflection;
using ApiQueryLanguage.LanguageV1;

namespace ApiQueryLanguage.Linq
{
    internal sealed class OrderBy<T>
    {
        private const string MethodNameForOrderBy = "OrderBy";
        private const string MethodNameForThenBy = "ThenBy";

        private readonly IEnumerable<OrderByProperty> _orderBy;
        private readonly ParameterExpression _parameter = Expression.Parameter(typeof(T), "x");

        public OrderBy(IEnumerable<OrderByProperty> orderBy)
        {
            _orderBy = orderBy;
        }

        public IQueryable<T> ApplyOn(IQueryable<T> queryable)
        {
            var queryExpression = queryable.Expression;
            bool firstIteration = true;

            foreach (var orderByProperty in _orderBy.Where(o => o.Direction != SortingDirections.None))
            {
                string orderByMethodName = firstIteration ? MethodNameForOrderBy : MethodNameForThenBy;
                firstIteration = false;

                if (orderByProperty.Direction == SortingDirections.Descending)
                {
                    orderByMethodName += "Descending";
                }

                var property = GetProperty(orderByProperty);
                var member = GetMember(orderByProperty);

                var func = typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType);
                var lambda = Expression.Lambda(func, member, _parameter);

                queryExpression = Expression.Call(
                    typeof(Queryable),
                    orderByMethodName,
                    new Type[] { typeof(T), property.PropertyType }, queryExpression, lambda
                );
            }

            return queryable.Provider.CreateQuery<T>(queryExpression);
        }

        private static PropertyInfo GetProperty(OrderByProperty orderByProperty)
        {
            string propertyId = orderByProperty.PropertyId;

            if (propertyId.Contains('.'))
            {
                propertyId = propertyId.Split('.')[1];
            }

            return typeof(T).GetProperties().First(p => p.Name.Equals(propertyId, StringComparison.InvariantCultureIgnoreCase));
        }

        private MemberExpression GetMember(OrderByProperty orderByProperty)
        {
            var property = GetProperty(orderByProperty);
            return Expression.Property(_parameter, property);
        }
    }
}
