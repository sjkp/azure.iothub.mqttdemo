using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace Azure.IoTHubMQTTDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //How to connect to azure iot Hub using mqtt https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-mqtt-support#using-the-mqtt-protocol-directly
            var client = new MqttClient("dghub.azure-devices.net", 8883, true, MqttSslProtocols.TLSv1_2, null,null);


            var deviceId = "test";

            //Generate the shared access signature using the device explorer https://github.com/Azure/azure-iot-sdk-csharp/releases              

            //Only the following part of the shared access signature is used as password:
            //SharedAccessSignature sr={your hub name}.azure-devices.net%2Fdevices%2FMyDevice01%2Fapi-version%3D2016-11-14&sig=vSgHBMUG.....Ntg%3d&se=1456481802
            var res = client.Connect(deviceId, $"dghub.azure-devices.net/{deviceId}/api-version=2016-11-14", "SharedAccessSignature sr={your hub name}.azure-devices.net%2Fdevices%2FMyDevice01%2Fapi-version%3D2016-11-14&sig=vSgHBMUG.....Ntg%3d&se=1456481802");

            if (res!=0)
            {
                Console.WriteLine($"Unable to connect code: {res}, see http://docs.oasis-open.org/mqtt/mqtt/v3.1.1/csd02/mqtt-v3.1.1-csd02.html#_Toc385349257");
            }

            //To send an event to the hub.
            client.Publish($"devices/{deviceId}/messages/events/", Encoding.UTF8.GetBytes("{\"temp\":25}"), 1, true);
            


            Console.ReadKey();
        }
    }
}
    