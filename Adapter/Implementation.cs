using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    #region Object adapter pattern implementation
    #region Adaptees
    public class  RedisCache
    {
        private Dictionary<string, object> _cache;
        public RedisCache() {
            _cache = [];
        }

        public void SetValue(string key, object value)
        {
            _cache[key] = value;
        }

        public object? GetValue(string key)
        {
            return _cache.TryGetValue(key, out object? value) ? value : null;
        }

    }

    public class InMemoryCache
    {
        private Dictionary<string, object> _cache;
        public InMemoryCache() {
            _cache = [];
        }
        public void Store(string key, object value)
        {
            _cache[key] = value;
        }
        public object? Retrieve(string key)
        {
            return _cache.TryGetValue(key, out object? value) ? value : null;
        }
    }

    public class MemcachedCache
    {
        private Dictionary<string, object> _cache;
        public MemcachedCache() {
            _cache = [];
        }
        public void Add(string key, object value)
        {
            _cache[key] = value;
        }
        public object? Fetch(string key)
        {
            return _cache.TryGetValue(key, out object? value) ? value : null;
        }
    }
    #endregion Adaptees

    #region Target Interface for the Adapter
    public interface ICache
    {
        void Set(string key, object value);
        object? Get(string key);
    }
    #endregion Target Interface for the Adapter

    #region Adapters
    public class RedisCacheAdapter : ICache
    {
        private readonly RedisCache _redisCache;
        public RedisCacheAdapter(RedisCache redisCache)
        {
            _redisCache = redisCache;
        }
        public void Set(string key, object value)
        {
            _redisCache.SetValue(key, value);
        }
        public object? Get(string key)
        {
            return _redisCache.GetValue(key);
        }
    }

    public class InMemoryCacheAdapter : ICache
    {
        private readonly InMemoryCache _inMemoryCache;
        public InMemoryCacheAdapter(InMemoryCache inMemoryCache)
        {
            _inMemoryCache = inMemoryCache;
        }
        public void Set(string key, object value)
        {
            _inMemoryCache.Store(key, value);
        }
        public object? Get(string key)
        {
            return _inMemoryCache.Retrieve(key);
        }
    }

    public class MemcachedCacheAdapter : ICache
    {
        private readonly MemcachedCache _memcachedCache;
        public MemcachedCacheAdapter(MemcachedCache memcachedCache)
        {
            _memcachedCache = memcachedCache;
        }
        public void Set(string key, object value)
        {
            _memcachedCache.Add(key, value);
        }
        public object? Get(string key)
        {
            return _memcachedCache.Fetch(key);
        }
    }
    #endregion Adapters
    #endregion Object adapter pattern implementation

    #region Class adapter pattern implementation
    // Note: C# does not support multiple inheritance, so class adapter pattern is less common.

    #region Legacy class to be adapted, Adaptee
    public class BankPayment
    {
        // other properties and methods

        public void PayWithBank(string accountNumber, decimal amount)
        {
            Console.WriteLine($"Paid {amount} using bank account {accountNumber}");
        }
    }
    #endregion Legacy class to be adapted, Adaptee

    #region new payment systems to integrate, also Adaptees
    public class PayPalPayment
    {
        public void PayWithPayPal(string email, decimal amount)
        {
            Console.WriteLine($"Paid {amount} using PayPal account {email}");
        }
    }

    public class CreditCardPayment
    {
        public void PayWithCreditCard(string cardNumber, decimal amount)
        {
            Console.WriteLine($"Paid {amount} using credit card {cardNumber}");
        }
    }

    public class GooglePayPayment
    {
        public void PayWithGooglePay(string googleAccount, decimal amount)
        {
            Console.WriteLine($"Paid {amount} using Google Pay account {googleAccount}");
        }
    }
    #endregion new payment systems to integrate, also Adaptees


    #region Target interface
    public interface IPayment
    {
        void Pay(string accountIdentifier, decimal amount);
        Tuple<string, decimal> GetPaymentDetailsFromBank();

    }
    #endregion Target interface

    #region Class adapters
    public class PayPalPaymentAdapter : BankPayment, IPayment
    {
        private readonly PayPalPayment _payPalPayment = new PayPalPayment();

        public Tuple<string, decimal> GetPaymentDetailsFromBank()
        {
            // Example method to demonstrate additional functionality
            // use base class methods to get bank payment details
            // extract and return relevant details
            return new Tuple<string, decimal>("uniquePayPalID", 100.00m);
        }
        public void Pay(string accountIdentifier, decimal amount)
        {
            (accountIdentifier, amount) = GetPaymentDetailsFromBank();
            _payPalPayment.PayWithPayPal(accountIdentifier, amount);
        }
    }

    public class CreditCardPaymentAdapter : BankPayment, IPayment
    {
        private readonly CreditCardPayment _creditCardPayment = new CreditCardPayment();

        public Tuple<string, decimal> GetPaymentDetailsFromBank()
        {
            // Example method to demonstrate additional functionality
            // use base class methods to get bank payment details
            // extract and return relevant details
            return new Tuple<string, decimal>("uniqueCreditCardNumber", 100.00m);
        }
        public void Pay(string accountIdentifier, decimal amount)
        {
            (accountIdentifier, amount) = GetPaymentDetailsFromBank();
            _creditCardPayment.PayWithCreditCard(accountIdentifier, amount);
        }
    }

    public class GooglePayPaymentAdapter : BankPayment, IPayment
    {
        private readonly GooglePayPayment _googlePayPayment = new GooglePayPayment();

        public Tuple<string, decimal> GetPaymentDetailsFromBank()
        {
            // Example method to demonstrate additional functionality
            // use base class methods to get bank payment details
            // extract and return relevant details
            return new Tuple<string, decimal>("uniqueGooglePayID", 100.00m);
        }
        public void Pay(string accountIdentifier, decimal amount)
        {
            (accountIdentifier, amount) = GetPaymentDetailsFromBank();
            _googlePayPayment.PayWithGooglePay(accountIdentifier, amount);
        }
    }
    #endregion Class adapters


    #endregion Class adapter pattern implementation

}
