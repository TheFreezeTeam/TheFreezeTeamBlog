namespace TheFreezeTeamBlog.ModuleDelegate
{
  using Statiq.Common;

  public class ReadTimeCalculator
  {
    public static string CalculateReadingTime(IDocument doc)
    {
      string? content = doc.GetContentStringAsync().Result;
      int numberOfWordsInContent = content.Split(' ').Length;
      //According to wiki, average reading time for one person is 150 words in 1 minute.
      int wordsPerMinute = numberOfWordsInContent / 150;
      string displayReadingTime = wordsPerMinute.ToString() + " MIN READ";
      if (wordsPerMinute == 0)
      {
        return "QUICK READ";
      } else
      {
        return displayReadingTime;
      }

    }
  }
}
