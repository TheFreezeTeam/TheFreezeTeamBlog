---
  Title:  Building a DateTimeService to Generate Unique DateTime Values
  Published: 02/20/2023
  Tags:
    - dotnet
    - C#
    - DateTimeService
  Image: datetime-service.jpg
  Description: Generate unique DateTime values with the DateTimeService class that uses the Ticks property to ensure uniqueness.
  Excerpt: A DateTimeService which implement NextUtcNow which will generate unique DateTime values.
  Author: Steven T. Cramer
---

## Introduction

In the 'TimeWarp Architecture', we use a DateTimeService class to timestamp Domain Events, which is essential for maintaining the integrity of our application's data. Generating unique DateTime values is critical for applications that require consistent ordering. The DateTimeService generates unique DateTime values by incrementing the Ticks value until an unused tick is found. 

We will also demonstrate how to test this class to ensure that it works as expected.

>### Warning
> This approach only ensures uniqueness within a single machine.
> If your application needs to generate unique DateTime values across multiple machines,
> you may need a different solution.


## Building the DateTimeService Class

The `DateTimeService` class will implement the `IDateTimeService` interface, which defines two methods:
`UtcNow` and `NextUtcNow`.
The `UtcNow` method simply returns the current UTC time,
while the `NextUtcNow` method generates the next unique `DateTime` value
by incrementing the last value used until an unused tick is found.

Here is the implementation of the `DateTimeService` class:

```cs
<?! Git "TimeWarpEngineering" "timewarp-architecture" "Source/TimeWarp.Architecture.Template/templates/TimeWarp.Architecture/Source/Common/Common.Infrastructure/Services/DateTimeService.cs" /?>
```


In the `NextUtcNow` method, 
we use the `Interlocked.Increment` method to increment the `LastValueUsed` field and return the new value.
We then check if this value is greater than or equal to the current UTC time, and if so,
return a new `DateTime` value using the `result` ticks value.
Otherwise,
we update the `ticksNow` variable with the value of `LastValueUsed` and repeat the loop until an unused tick is found.

## Testing the DateTimeService Class

To test the `NextUtcNow` method of the `DateTimeService` class, we need to ensure that it generates unique `DateTime` values that are always increasing. We can achieve this by creating multiple threads and having each thread call the `NextUtcNow` method multiple times. We can then combine the generated `DateTime` values from all threads into a single array and check that there are no duplicates and that the values are in increasing order.

Here is the implementation of the test:

```cs
<?! Git "TimeWarpEngineering" "timewarp-architecture" "Source/TimeWarp.Architecture.Template/templates/TimeWarp.Architecture/Tests/Common/Common.Infrastructure.Tests/DateTimeService_Tests.cs" /?>
```

In this test, we create an instance of the `DateTimeService` class and use `Task.Run` to create 10 tasks that call the `NextUtcNow` method 100,000 times each. We then wait for a second to ensure that all tasks are running, and then allow them to generate `DateTime` values by calling the `Set` method of a `ManualResetEvent` object.

Once the tasks have finished generating `DateTime` values, we combine all of the values into a single array and check that there are no duplicates using LINQ's `GroupBy` method. We then use FluentAssertions to assert that the length of the array of `IGrouping<DateTime, DateTime>` where there is more than one instance of a particular `DateTime` is equal to zero.

## Credit
I want to give a big shoutout to my close friend, Peter Morris.
He has been an incredible help to me in both my personal and professional life.

In particular, I want to thank Peter for his contributions to the DateTimeService class featured in this post. He was the "driver" and wrote the vast majority of this while I was the "navigator." 

## Conclusion

In this post,
we have built a DateTimeService class that generates unique DateTime values by incrementing
the Ticks value until an unused tick is found.
We have also demonstrated how to test this class to ensure that it works as expected,
even when called concurrently by multiple threads.

By using the DateTimeService class in your software project,
you can be sure that each generated DateTime value is unique and that there are no duplicates.
This can be particularly useful in applications that require unique time-stamped values, such as financial transactions, event logging, or messaging.

By tapping into the superpower of pair programming, you and your coding partner can work together to improve the quality of your code and collaboration.
Consider harnessing the power of pair programming to supercharge your productivity and code quality in your next project!