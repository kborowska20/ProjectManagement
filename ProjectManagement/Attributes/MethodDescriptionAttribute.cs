namespace ProjectManagement.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MethodDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public MethodDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
