Title: Better Error Messages with FluentValidation
Published: 02/04/2019
Tags: 
  - FluentValidation 
  - C#
Author: Steven T. Cramer
Image: better-error.webp
Description: Create better error messages in dotnet core with FluentValidation.
Excerpt: Create better error messages in dotnet core with FluentValidation.
---

## I know whats broken, but I ain't telling!
<br>

Console: "Invalid File name!" 
Me: "Why?"
Me: "Hey Cortana Why do I have an invalid file name?"  
Cortana: "I lost the thread there. Can you phrase it another way?"  
Me: "Alexa why do I have an invalid file name?"  
Alexa: "Sorry I don't know that"  
Me: "Yea me either!"  

> We used to talk to ourselves but now we have bots!

As a developer I use [Fluent Validation](https://fluentvalidation.net/) to validate my objects. I am working on a console app that should save a sound file to disk. I want to validate the `FileName` property.

My first attempt:

```csharp
internal class GetSoundFileValidator : AbstractValidator<GetSoundFileRequest>
  {
    public GetSoundFileValidator()
    {
      RuleFor(aGetSoundFileRequest => aGetSoundFileRequest.FileName)
        .Must(ValidFileName);
    }

    private bool ValidFileName(string aFileName) =>
      aFileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
  }
```

This will validate that no invalid characters are in the `FileName`, 
but the error message is:

> **"The specified condition was not met for 'File Name'."**

Which begs the question "What specified condition?"

So we do our user/future self, a favor and improve the error to give a more specific message:

```csharp
    public GetSoundFileValidator()
    {
      RuleFor(aGetSoundFileRequest => aGetSoundFileRequest.FileName).NotEmpty();
      When(aGetSoundFileRequest => aGetSoundFileRequest.FileName != null, () =>
      {
        RuleFor(aGetSoundFileRequest => aGetSoundFileRequest.FileName)
        .Must(IsValidFileName)
        .WithMessage("Invalid Character '{InvalidChar}' in FileName at index {Index}");
      });
    }

    private bool ValidFileName(
      GetSoundFileRequest aGetSoundFileRequest,
      string aFileName,
      PropertyValidatorContext aPropertyValidatorContext)
    {
      int index = aFileName.IndexOfAny(Path.GetInvalidFileNameChars());
      if (index >= 0)
      {
        aPropertyValidatorContext.MessageFormatter.AppendArgument("InvalidChar", aFileName[index]);
        aPropertyValidatorContext.MessageFormatter.AppendArgument("Index", index);
      }
      return index < 0;
    }
```
The new error message:

> **Invalid Character '<' in FileName at index 8**

Now that's better.

#### Real life example
Erorr Message: "[19-01-20 09:31:36.589]Error:Referenced TOC file C:\git\github\blazor-state\Documentation\Process\Backlog\toc.yml does not exist."
Me: "Great, Referenced from where?"


#### References:
[Fluent Validation](https://fluentvalidation.net/)

