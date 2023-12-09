using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace HotelListing {
  public class Program {
    public static void Main(string[] args) {
      //creating a new logger object to use serilogger
      Log.Logger = new LoggerConfiguration()
         .WriteTo
         .File(
               path: "C:\\hotelListing\\log-.txt",
               outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
               rollingInterval: RollingInterval.Day,
               restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
              )
         .CreateLogger();
      Log.Information("Application is starting.");

      try {
        Log.Information("Application has started.");
        CreateHostBuilder(args).Build().Run();

      } catch (Exception ex) {
        Log.Fatal(ex,"Application has stopped due to an exception");
      } finally {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog() //adding serilog
            .ConfigureWebHostDefaults(webBuilder => {
              webBuilder.UseStartup<Startup>();
            });
  }
}