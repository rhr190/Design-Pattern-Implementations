
namespace FactoryImplementation
{

    // Product interface
    public interface IDelivery
    {
        void Deliver();
    }


    // Concrete product classes
    public class RoadwayDelivery : IDelivery
    {
        public void Deliver()
        {
            // Implementation for roadway delivery
            System.Console.WriteLine("Delivered by road.");
        }
    }


    public class AirwayDelivery : IDelivery
    {
        public void Deliver()
        {
            // Implementation for airway delivery
            System.Console.WriteLine("Delivered by air.");
        }
    }

    public class WaterwayDelivery : IDelivery
    {
        public void Deliver()
        {
            // Implementation for waterway delivery
            System.Console.WriteLine("Delivered by water.");
        }
    }

    // Creator base class
    public abstract class TransportCreator
    {
        public abstract IDelivery CreateDeliveryMode();

        public void CoreBusinessLogic()
        {
            System.Console.WriteLine("Implementing required business logic before delivery.");
        }
    }

    // Concrete creator classes
    public class RoadwayTransportCreator(string vehicleName) : TransportCreator
    {
        public override IDelivery CreateDeliveryMode()
        {
            System.Console.WriteLine($"Creating roadway delivery with vehicle: {vehicleName}");
            return new RoadwayDelivery();
        }
    }

    public class AirwayTransportCreator(string vehicleName) : TransportCreator
    {
        public override IDelivery CreateDeliveryMode()
        {
            System.Console.WriteLine($"Creating airway delivery with vehicle: {vehicleName}");
            return new AirwayDelivery();
        }
    }

    public class WaterwayTransportCreator(string vehicleName) : TransportCreator
    {
        public override IDelivery CreateDeliveryMode()
        {
            System.Console.WriteLine($"Creating waterway delivery with vehicle: {vehicleName}");
            return new WaterwayDelivery();
        }
    }
}