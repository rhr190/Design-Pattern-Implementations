// See https://aka.ms/new-console-template for more information
using FactoryImplementation;

namespace FactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a transport factory
            TransportCreator factoryHost = new WaterwayTransportCreator("Cargo Ship");

            // Use the factory's create method to create a delivery mode
            IDelivery delivery = factoryHost.CreateDeliveryMode();

            // Use the delivery mode
            delivery.Deliver();
        }
    }
}