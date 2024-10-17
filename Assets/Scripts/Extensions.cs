namespace CulTA
{
    public static class Extensions
    {
        public static T ThrowIfNull<T>(this T obj, string message = null)
        {
            if (obj == null)
            {
                throw new System.NullReferenceException(message);
            }
            return obj;
        }
    }
}