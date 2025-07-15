namespace SemanticRelease.DotNet
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}