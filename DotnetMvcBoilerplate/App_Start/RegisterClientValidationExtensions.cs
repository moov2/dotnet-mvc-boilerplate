using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DotnetMvcBoilerplate.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace DotnetMvcBoilerplate.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}