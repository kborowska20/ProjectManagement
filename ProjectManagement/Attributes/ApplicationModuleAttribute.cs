using ProjectManagement.Enums;

namespace ProjectManagement.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ApplicationModuleAttribute : Attribute
    {
        public Module BelongingModule { get; }

        public ApplicationModuleAttribute(Module belongingModule)
        {
            BelongingModule = belongingModule;
        }
    }
}
