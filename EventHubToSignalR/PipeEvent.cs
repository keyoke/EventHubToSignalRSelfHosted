using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.EventHubs;
using System.Text;

namespace EventHubToSignalR
{
    public static class PipeEvent
    {
        //[FunctionName("PipeEvent")]
        //public static async Task Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    var connection = new HubConnectionBuilder()
        //        .WithUrl("http://localhost:29557/EventsHub")
        //        .Build();
        //    await connection.StartAsync();
        //    await connection.InvokeAsync("SendEvent","Event");
        //}

        [FunctionName("PipeEvent")]
        public static async Task Run(
            [EventHubTrigger("samples-workitems", Connection = "EventHubConnectionAppSetting")] EventData myEventHubMessage,
            DateTime enqueuedTimeUtc,
            Int64 sequenceNumber,
            string offset,
            ILogger log)
        {
            // Improve this, either re-use or even better use the official output binding
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:29557/EventsHub")
                .Build();
            await connection.StartAsync();
            await connection.InvokeAsync("SendEvent", Encoding.UTF8.GetString(myEventHubMessage.Body));
        }
    }
}
