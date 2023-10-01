namespace ApiQueryLanguage.GraphV1
{
    public static class GraphExtensions
    {
        public static GraphQuery<T> Where<T>(
            this Graph<T> graph,
            Func<Node<T>, bool> selector)
        {
            return new GraphQuery<T>(graph).Where(selector);
        }
    }
}
