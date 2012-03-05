using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DotnetMvcBoilerplate.Tests.Unit.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace DotnetMvcBoilerplate.Tests.Unit.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}