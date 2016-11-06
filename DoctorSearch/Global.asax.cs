using System.Web;
using System.Web.Http;
using Amazon.CloudWatchLogs;
using Amazon.Runtime;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;

namespace DoctorSearch
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start(IHostingEnvironment env)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var logGroupName = "Logger/" + env.EnvironmentName;

            // customer renderer (optional, defaults to a simple rendered message of Serilog's LogEvent
            var renderer = new CustomRenderer();

            // options for the sink, specifying the log group name
            var options = new CloudWatchSinkOptions { LogGroupName = logGroupName, LogEventRenderer = renderer };

            // setup AWS CloudWatch client
            var credentials = new BasicAWSCredentials(myAwsAccessKey, myAwsSecretKey);
            var client = new AmazonCloudWatchLogsClient(credentials, myAwsRegion);

            // Attach the sink to the logger configuration
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
              .WriteTo.AmazonCloudWatch(options, client)
              .CreateLogger();
        }
    }
}
