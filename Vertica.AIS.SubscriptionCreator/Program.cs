using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vertica.AIS.SubscriptionCreator
{
    class Program
    {
        static string ReadKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        static void Main(string[] args)
        {
            try
            {
                string connectionString = ReadKey("connectionString");
                string topicPath = ReadKey("topicPath");

                string subscriptionName = args[0];
                string filter = args[1];
                var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

                SubscriptionDescription subscriptionDescription =
                    new SubscriptionDescription(topicPath, subscriptionName)
                    {
                      
                    };
                namespaceManager.CreateSubscription(subscriptionDescription, new SqlFilter(filter));
                Console.WriteLine($"Subscription: {subscriptionName}({filter}) created in topic: {topicPath}");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
