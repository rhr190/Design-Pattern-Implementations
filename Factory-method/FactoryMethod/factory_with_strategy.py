from abc import ABC, abstractmethod
from typing import List
import time

# ============= STRATEGY PATTERN =============
# Different retry strategies (the behaviors)

class RetryStrategy(ABC):
    """Strategy interface for retry logic"""
    
    @abstractmethod
    def should_retry(self, attempt: int, max_attempts: int) -> bool:
        pass
    
    @abstractmethod
    def get_delay(self, attempt: int) -> float:
        pass


class ExponentialBackoffStrategy(RetryStrategy):
    """Doubles the delay with each retry"""
    
    def should_retry(self, attempt: int, max_attempts: int) -> bool:
        return attempt < max_attempts
    
    def get_delay(self, attempt: int) -> float:
        return 2 ** attempt  # 1s, 2s, 4s, 8s...


class LinearBackoffStrategy(RetryStrategy):
    """Increases delay linearly"""
    
    def should_retry(self, attempt: int, max_attempts: int) -> bool:
        return attempt < max_attempts
    
    def get_delay(self, attempt: int) -> float:
        return attempt * 2  # 2s, 4s, 6s, 8s...


class NoRetryStrategy(RetryStrategy):
    """Never retries"""
    
    def should_retry(self, attempt: int, max_attempts: int) -> bool:
        return False
    
    def get_delay(self, attempt: int) -> float:
        return 0


# ============= FACTORY METHOD PATTERN =============
# Creates appropriate retry strategies for different notification channels

class NotificationChannel(ABC):
    """Creator class with factory method"""
    
    def __init__(self):
        # The strategy is created by the factory method
        self.retry_strategy = self.create_retry_strategy()
    
    @abstractmethod
    def create_retry_strategy(self) -> RetryStrategy:
        """Factory method - subclasses decide which strategy to create"""
        pass
    
    @abstractmethod
    def send(self, message: str) -> bool:
        """Template method using the strategy"""
        pass
    
    def send_with_retry(self, message: str, max_attempts: int = 3) -> bool:
        """Uses the retry strategy created by factory method"""
        attempt = 0
        
        while True:
            print(f"  Attempt {attempt + 1}: Sending via {self.__class__.__name__}")
            success = self.send(message)
            
            if success:
                print(f"  ✓ Success!")
                return True
            
            if not self.retry_strategy.should_retry(attempt, max_attempts):
                print(f"  ✗ Failed after {attempt + 1} attempts")
                return False
            
            delay = self.retry_strategy.get_delay(attempt)
            print(f"  Retrying in {delay}s...")
            time.sleep(delay)
            attempt += 1


# Concrete notification channels (Factory Method implementations)

class EmailChannel(NotificationChannel):
    """Email needs exponential backoff due to rate limits"""
    
    def create_retry_strategy(self) -> RetryStrategy:
        return ExponentialBackoffStrategy()
    
    def send(self, message: str) -> bool:
        # Simulate sending (50% success rate for demo)
        import random
        return random.random() > 0.5


class SMSChannel(NotificationChannel):
    """SMS needs linear backoff - simpler retry logic"""
    
    def create_retry_strategy(self) -> RetryStrategy:
        return LinearBackoffStrategy()
    
    def send(self, message: str) -> bool:
        import random
        return random.random() > 0.5


class PushNotificationChannel(NotificationChannel):
    """Push notifications don't retry - either work or fail immediately"""
    
    def create_retry_strategy(self) -> RetryStrategy:
        return NoRetryStrategy()
    
    def send(self, message: str) -> bool:
        import random
        return random.random() > 0.5


# ============= DEMO =============

def main():
    channels: List[NotificationChannel] = [
        EmailChannel(),
        SMSChannel(),
        PushNotificationChannel()
    ]
    
    message = "Your order has shipped!"
    
    for channel in channels:
        print(f"\n{'='*50}")
        print(f"Sending via {channel.__class__.__name__}")
        print(f"Using {channel.retry_strategy.__class__.__name__}")
        print('='*50)
        channel.send_with_retry(message)


if __name__ == "__main__":
    main()