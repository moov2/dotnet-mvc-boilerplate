using System;
using NUnit.Framework;
using System.ServiceProcess;
using DotnetMvcBoilerplate.Core.IO;

namespace DotnetMvcBoilerplate.Tests.Integration.Core.IO
{
    public class WindowsServiceTests
    {
        private static int Timeout = 3000;

        private static string ServiceThatDoesntExist = "Windows Service That Doesnt Exist";

        /// <summary>
        /// Flag indicating whether the test windows service
        /// is running before.
        /// </summary>
        private bool _runningBeforeTest;

        [SetUp]
        public void Setup()
        {
            _runningBeforeTest = IsTestServiceRunning(WindowsService.MongoDB);
        }

        [TearDown]
        public void TearDown()
        {
            if (_runningBeforeTest)
                StartService(WindowsService.MongoDB);
            else
                StopService(WindowsService.MongoDB);
        }

        /// <summary>
        /// Tests that a stopped windows service is started by calling
        /// StartIfNotRunning.
        /// </summary>
        [Test]
        public void StartIfNotRunning_StartsTheWindowsService()
        {
            StopService(WindowsService.MongoDB);

            WindowsService.StartIfNotRunning(WindowsService.MongoDB);
            Assert.That(IsTestServiceRunning(WindowsService.MongoDB), Is.True);
        }

        /// <summary>
        /// Tests that a starting a windows service that isn't running returns true
        /// upon successfully starting the service.
        /// </summary>
        [Test]
        public void StartIfNotRunning_StartsTheWindowsService_ReturnsTrue()
        {
            StopService(WindowsService.MongoDB);
            Assert.IsTrue(WindowsService.StartIfNotRunning(WindowsService.MongoDB));
        }

        /// <summary>
        /// Tests that a starting a windows service that isn't running returns true
        /// even though the windows service is already running.
        /// </summary>
        [Test]
        public void StartIfNotRunning_ServiceAlreadyRunning_ReturnsTrue()
        {
            StartService(WindowsService.MongoDB);
            Assert.IsTrue(WindowsService.StartIfNotRunning(WindowsService.MongoDB));
        }

        /// <summary>
        /// Tests that trying to start a windows service that doesn't exist
        /// returns false.
        /// </summary>
        [Test]
        public void StartIfNotRunning_ServiceDoesntExist_ReturnsFalse()
        {
            Assert.IsFalse(WindowsService.StartIfNotRunning(ServiceThatDoesntExist));
        }

        /// <summary>
        /// Returns flag notifying if the windows service is running.
        /// </summary>
        /// <param name="serviceName">Windows service being tested.</param>
        /// <returns>True if running, otherwise false.</returns>
        private bool IsTestServiceRunning(string serviceName)
        {
            var service = new ServiceController(serviceName);
            var running = (service.Status == ServiceControllerStatus.Running);
            service.Dispose();
            return running;
        }

        /// <summary>
        /// Starts the window service back up again.
        /// </summary>
        /// <param name="serviceName">Name of the windows service.</param>
        private void StartService(string serviceName)
        {
            var service = new ServiceController(serviceName);

            if (service.Status == ServiceControllerStatus.Running)
            {
                service.Dispose();
                return;
            }

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
        /// Stops the windows service.
        /// </summary>
        /// <param name="serviceName">Name of the windows service.</param>
        private void StopService(string serviceName)
        {
            var service = new ServiceController(serviceName);

            if (service.Status == ServiceControllerStatus.Stopped)
            {
                service.Dispose();
                return;
            }

            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(Timeout);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch { }

            service.Dispose();
        }
    }
}
