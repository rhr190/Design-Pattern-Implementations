namespace DecoratorImplementation
{
    /// Decorator implementaion contains Components and Decorators

    // Component Interface
    public interface DataSource
    {
        void WriteData(string data);
        string ReadData();
    }

    // Concrete Component
    public class FileDataSource(string filePath) : DataSource
    {
        private string _filePath = filePath;
        private string _data;

        public void WriteData(string data)
        {
            // Simulate writing data to a file
            System.Console.WriteLine($"Writing data to {_filePath}: \"{data}\" using {nameof(FileDataSource)}");
            _data = data;
        }

        public string ReadData()
        {
            // Simulate reading data from a file
            System.Console.WriteLine($"Reading data from {_filePath}: \"{_data}\" using {nameof(FileDataSource)}");
            return _data;
        }
    }


    // Base Decorator
    public abstract class DataSourceDecorator(DataSource wrappee) : DataSource
    {
        protected DataSource _wrappee = wrappee;

        public virtual void WriteData(string data)
        {
            System.Console.WriteLine($"Writing data using {nameof(DataSource)}.");
            _wrappee.WriteData(data);
        }

        public virtual string ReadData()
        {
            System.Console.WriteLine($"Reading data using {nameof(DataSource)}.");
            return _wrappee.ReadData();
        }
    }

    // Concrete Decorator
    public class EncryptionDecorator(DataSource wrappee) : DataSourceDecorator(wrappee)
    {
        public override void WriteData(string data)
        {
            System.Console.WriteLine($"Encrypting data before writing using {nameof(EncryptionDecorator)}.");
            string encryptedData = Encrypt(data);
            base.WriteData(encryptedData);
        }

        public override string ReadData()
        {
            string data = base.ReadData();
            System.Console.WriteLine($"Decrypting data after reading using {nameof(EncryptionDecorator)}.");
            return Decrypt(data);
        }

        private string Encrypt(string data)
        {
            // Simple encryption logic (for demonstration purposes)
            char[] dataChars = data.ToCharArray();
            for (int i = 0; i < dataChars.Length; i++)
            {
                dataChars[i] = (char)(dataChars[i] + 1);
            }
            return new string(dataChars);
        }

        private string Decrypt(string data)
        {
            // Simple decryption logic (for demonstration purposes)
            char[] dataChars = data.ToCharArray();
            for (int i = 0; i < dataChars.Length; i++)
            {
                dataChars[i] = (char)(dataChars[i] - 1);
            }
            return new string(dataChars);
        }
    }
    
    // Another Concrete Decorator
    public class CompressionDecorator(DataSource wrappee) : DataSourceDecorator(wrappee)
    {
        public override void WriteData(string data)
        {
            System.Console.WriteLine($"Compressing data before writing using {nameof(CompressionDecorator)}.");
            string compressedData = Compress(data);
            base.WriteData(compressedData);
        }

        public override string ReadData()
        {
            string data = base.ReadData();
            System.Console.WriteLine($"Decompressing data before reading using {nameof(CompressionDecorator)}.");
            return Decompress(data);
        }

        private string Compress(string data)
        {
            // Simple compression logic (for demonstration purposes)
            return data.Replace(" ", "");
        }

        private string Decompress(string data)
        {
            // Simple decompression logic (for demonstration purposes)
            // Note: This is just a placeholder and does not restore spaces
            return data;
        }
    }
}