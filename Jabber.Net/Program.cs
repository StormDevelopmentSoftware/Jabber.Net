using System;
using System.ServiceProcess;
using Jabber.Net.Config;
using Microsoft.Practices.Unity.Configuration;

namespace Jabber.Net.Server
{
    class Program : ServiceBase
    {
        private readonly JabberNetServer jabberServer = new JabberNetServer();


        static void Main(string[] args)
        {
            var service = new Program();

            if (Environment.UserInteractive)
            {
                try
                {
                    service.OnStart(args);
                    Console.WriteLine("Press Ctrl+C key to stop...");

					Console.CancelKeyPress += (s, e) =>
					{
						e.Cancel = true;
						service.OnStop();
					};
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Run(service);
            }
        }

        protected override void OnStart(string[] args)
        {
            jabberServer.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            jabberServer.Start();
        }

        protected override void OnStop()
        {
            jabberServer.Stop();
        }
    }
}
