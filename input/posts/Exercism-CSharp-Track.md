Title: Exercism CSharp Track
Published: 05/31/2019
Tags: 
  - C# 
  - Kata 
Image: Exxxx.jpg
Description: I'm recording my progress working through the csharp track of Ecercism.io
Excerpt: I'm recording my progress working through the csharp track of Ecercism.io
---

#  Problem One "Hello, World!"

``` Csharp
return "Hello, World!";
```
#  Problem Two "Two-Fer"
`Two-fer` or `2-fer` is short for two for one. One for you and one for me.

Given a name, return a string with the message:

```text
One for X, one for me.
```

Where X is the given name.

However, if the name is missing, return the string:

```text
One for you, one for me.
```
## My Issues
Coming from a JS background, and not being *entirely* famliar with how dotnet works, my first thought was to add the potential String "Name" as an argument to the Speak method, that didn't  workout, so my next thought was to override the method, but turns out overriding doesn't quite work like that, as i understand it. What I've learned most during this process of getting to know csharp is that a great deal of my confusion stems from a basic misunderstanding of some very fundamental concepts in the language, this problem is a good example. 

## My Solution
```csharp
public class TwoFer
{
    public static string Speak(string name = "you")
    {
        return $"One for {name}, one for me.";
    }
}
```
Heavy inspiration from [this fella](https://exercism.io/tracks/csharp/exercises/two-fer/solutions/e85f6abd2eaf4156b71235c7df69b3c2).

For instance the concept of setting the variable's value in the () was new to me. The Idea to set it to "you" as default is one of those things that is so obvious it's painful to think that I hadn't thought of it before I saw it. 
Yay learning!

# Problem Three "Leap"

Given a year, report if it is a leap year.

The tricky thing here is that a leap year in the Gregorian calendar occurs:

```text
on every year that is evenly divisible by 4
  except every year that is evenly divisible by 100
    unless the year is also evenly divisible by 400
```

For example, 1997 is not a leap year, but 1996 is.  1900 is not a leap
year, but 2000 is.

## Notes

Though our exercise adopts some very simple rules, there is more to
learn!

For a delightful, four minute explanation of the whole leap year
phenomenon, go watch [this youtube video][video].

[video]: http://www.youtube.com/watch?v=xX96xng7sAE

The DateTime class in C# provides a built-in [IsLeapYear](https://msdn.microsoft.com/en-us/library/system.datetime.isleapyear(v=vs.110).aspx) method
which you should pretend doesn't exist for the purposes of implementing this exercise.

## My Thoughts
This brought me way back to learning to code the first time, back in '94 or so. Pretty straightforward, I'm confident my solution can be refined a bit, but good enough for me at the moment. 
## My Solution
```csharp
using System;

public static class Leap
{
    public static bool IsLeapYear(int year)
    {
        if(year % 100 == 0)
            {
                if(year % 400 == 0)
                {
                    return true;
                }
            return false;
            }          
        if(year % 4 == 0)
            {
                return true;
            }
        return false;
    }
}
```

# Problem Four: "Gigasecond"

Given a moment, determine the moment that would be after a gigasecond
has passed.

A gigasecond is 10^9 (1,000,000,000) seconds.

## My Thoughts
the `.AddSeconds()` method saves the day!
## My Solution
```csharp
using System;

public static class Gigasecond
{
    public static DateTime Add(DateTime moment)
    {
      return moment.AddSeconds(1000000000);
    }
}
```

# Problem Five: "Resistor Color"

Resistors have color coded bands, where each color maps to a number. The first 2 bands of a resistor have a simple encoding scheme: each color maps to a single number.

These colors are encoded as follows:

- Black: 0
- Brown: 1
- Red: 2
- Orange: 3
- Yellow: 4
- Green: 5
- Blue: 6
- Violet: 7
- Grey: 8
- White: 9

Mnemonics map the colors to the numbers, that, when stored as an array, happen to map to their index in the array: Better Be Right Or Your Great Big Values Go Wrong.

More information on the color encoding of resistors can be found in the [Electronic color code Wikipedia article](https://en.wikipedia.org/wiki/Electronic_color_code)

## My Thoughts
A good exercise in arrays. A bit different than JS, but nothing a quick Google search couldn't figure out. Again, room for optimizations but as a general rule I like to brute-force it, or write it out the long way so I can see it all first, then, go back to refactor for efficiency. 

## My Solution

```csharp
using System;

public static class ResistorColor
{
    public static int ColorCode(string color)
    {
        string[] colors = Colors();

       int index = Array.IndexOf(colors, color);
        return index;
    }

    public static string[] Colors()
    {
        string[] colors = new string[] { "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white"};  
        return colors;  
    }

```


