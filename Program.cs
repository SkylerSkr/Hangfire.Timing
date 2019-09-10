using System;
using Hangfire.Topshelf.Core;
using Serilog;
using Topshelf;

namespace Hangfire.Topshelf
{
    class Program
    {
        static int Main(string[] args)
        {

            try
            {
                return (int)HostFactory.Run(x =>
                {
                    x.RunAsLocalSystem();

                    x.SetServiceName(HangfireSettings.Instance.ServiceName);
                    x.SetDisplayName(HangfireSettings.Instance.ServiceDisplayName);
                    x.SetDescription(HangfireSettings.Instance.ServiceDescription);

                    x.UseOwin(baseAddress: HangfireSettings.Instance.ServiceAddress);



                    x.SetStartTimeout(TimeSpan.FromMinutes(5));

                    x.SetStopTimeout(TimeSpan.FromMinutes(35));

                    x.EnableServiceRecovery(r => { r.RestartService(1); });
                });
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return 0;
            }
        }
    }
}
