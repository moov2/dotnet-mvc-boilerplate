using System;
using System.ServiceProcess;

namespace DotnetMvcBoilerplate.Core.IO
{
    public class WindowsService
    {
        public const string MongoDB = "Mongo DB";
        private static int Timeout = 3000;

        /// <summary>
        /// Returns a flag that highlights whether the
        /// window service with a particular name is 
        /// running.
        /// </summary>
        /// <param name="name">Name of the windows service
        /// to check.</param>
        /// <returns>True if running, otherwise false.</returns>
        private static bool Running(string serviceName)
        {
            var service = new ServiceController(serviceName);
            var result = (service.Status == ServiceControllerStatus.Running);
            service.Dispose();
            return result;
        }

        /// <summary>
        /// Starts a windows service.
        /// </summary>
        /// <param name="name">Name of the windows service.</param>
        private static void Start(string serviceName)
        {
            var service = new ServiceController(serviceName);

            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(Timeout);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            finally
            {
                service.Dispose();
            }
        }

        /// <summary>
        /// Starts a windows service if it isn't running.
        /// </summary>
        /// <param name="name">Name of the service to run.</param>
        public static bool StartIfNotRunning(string serviceName)
        {
            try {
                if (!Running(serviceName))
                    Start(serviceName);
            }
            catch {
                return false;
            }

            return true;
        }
    }
}