using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ApiQueryLanguage.GraphV1
{
    internal class ClassGraphBuilder
    {
        private readonly Type _root;

        public ClassGraphBuilder(Type root)
        {
            _root = root;
        }

        public Graph<object> Build()
        {
            Graph<object> graph = new();
            BuildFrom(_root, graph);

            return graph;
        }

        private static void BuildFrom(object root, Graph<object> graph, string targetNodeId = null)
        {
            string nodeId = CreateNodeId(root);

            if (graph.Exists(nodeId))
            {
                return;
            }

            if (root is Type type)
            {
                graph.Add(nodeId, "class", type);

                foreach (var property in type.GetProperties())
                {
                    BuildFrom(property, graph, targetNodeId: nodeId);
                }
            }
            else if (root is PropertyInfo property)
            {
                if (IsCollection(property.PropertyType))
                {
                    var targetClass = GetClass(property.PropertyType);

                    graph.AddEdge(
                        nodeId,
                        CreateNodeId(targetClass),
                        createNodesIfMissing: false
                    );

                    BuildFrom(targetClass, graph);
                }
                else if (IsClass(property.PropertyType))
                {
                    graph.AddEdge(
                        nodeId,
                        CreateNodeId(property.PropertyType),
                        createNodesIfMissing: false
                    );

                    BuildFrom(property.PropertyType, graph);
                }

                graph.Add(nodeId, "property", property);
            }

            if (!string.IsNullOrEmpty(targetNodeId))
            {
                graph.AddEdge(nodeId, targetNodeId);
            }
        }

        public static string CreateNodeId(object? root)
        {
            if (root is null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (root is Type type)
            {
                return type.Name;
            }

            var property = (PropertyInfo)root;

            return $"{property.DeclaringType?.Name}.{property.Name.ToLowerFirstLetter()}";
        }

        public static PropertyInfo GetIdentityPropertyId(Type type)
        {
            string standardNamingForId = $"{type.Name}Id";
            var property = type.GetProperty(standardNamingForId);

            if (property != null)
            {
                return property;
            }

            var primaryKeyProperty = type.GetProperties()
                .FirstOrDefault(p => p.GetCustomAttribute<KeyAttribute>() != null);

            if (primaryKeyProperty != null)
            {
                return primaryKeyProperty;
            }

            throw new ArgumentNullException($"Could not find a identity property, add property with the following name {standardNamingForId} or add the KeyAttribute on the property thats should be used as an identity property");
        }

        public static PropertyInfo GetForeignPropertyId(
            Type forType,
            Type onType,
            PropertyInfo? through
        )
        {
            string standardNamingIdOnType = $"{forType.Name}Id";
            var property = onType.GetProperty(standardNamingIdOnType);

            if (property != null)
            {
                return property;
            }

            string standardNamingIdOnForType = $"{onType.Name}Id";
            property = forType.GetProperty(standardNamingIdOnForType);

            if (property != null)
            {
                return property;
            }

            if(through is null)
            {
                throw new ArgumentNullException(nameof(through));
            }

            var foreignKeyAttribute = through.GetCustomAttribute<ForeignKeyAttribute>()
                ?? throw new ArgumentNullException($"Could not find a foreign identity property, add a property with the following name {standardNamingIdOnType} on class {onType.Name} or add the ForeignKeyAttribute on the property {through.Name} on class {forType.Name}");
            var foreignKeyProperty = onType.GetProperty(foreignKeyAttribute.Name);

            if (foreignKeyProperty != null)
            {
                return foreignKeyProperty;
            }

            throw new ArgumentNullException($"ForeignKeyAttribute on property {through.Name} for class {forType.Name} using a property name that does not exists on class {onType.Name}");
        }

        public static Type GetClass(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericArguments()[0];
            }

            return type;
        }

        public static bool IsClass(Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            return type.IsClass && !type.Equals(typeof(string));
        }

        public static bool IsCollection(Type type)
        {
            return type.GetInterface(nameof(IEnumerable)) != null && !type.Equals(typeof(string));
        }
    }
}
