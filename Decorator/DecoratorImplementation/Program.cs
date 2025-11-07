// See https://aka.ms/new-console-template for more information
using DecoratorImplementation;

namespace DecoratorDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSource fileDataSource = new FileDataSource("data.txt");

            DataSource compressedDataSource = new CompressionDecorator(fileDataSource);

            compressedDataSource.WriteData("Hello, Rafsan!");

            string result = compressedDataSource.ReadData();

            Console.WriteLine("Decompressed Data: " + result);

            Console.WriteLine("--------------------------------------------------");

            DataSource augmentedDataSource = new EncryptionDecorator(compressedDataSource);

            // Write data using the decorated data source
            augmentedDataSource.WriteData("Hello, World!");

            // Read data using the decorated data source
            result = augmentedDataSource.ReadData();

            Console.WriteLine("Decrypted Data: " + result);
        }
    }
}
