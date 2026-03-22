using ProjectManagement.Enums;

namespace ProjectManagement.Attributes
{
    [AttributeUsage(System.AttributeTargets.Interface | System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = true)]
    public class ApplicationModuleAttribute : Attribute
    {
        public Module BelongingModule { get; }

        public ApplicationModuleAttribute(Module belongingModule)
        {
            BelongingModule = belongingModule;
        }
    }
}
