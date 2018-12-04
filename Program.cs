using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace maqta.identityserver {
    public class Program {
        public static void Main (string[] args) {
            Console.Title = "Fleet Management Server";

            var seed = args.Contains ("/seed");
            if (seed) {
                args = args.Except (new [] { "/seed" }).ToArray ();
            }

            var host = BuildWebHost (args);

            if (seed) {
                SeedData.EnsureSeedData (host.Services);
            }

            host.Run ();
        }

        public static IWebHost BuildWebHost (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .Build ();
    }
}