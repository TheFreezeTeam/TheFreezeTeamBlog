namespace TheFreezeTeamBlog.ShortCodes
{
  using Newtonsoft.Json;
  using Statiq.Common;
  using System;
  using System.Collections.Generic;
  using System.Net.Http;
  using System.Threading.Tasks;

  public class GitShortCode : SyncShortcode
  {
    private const string Owner = nameof(Owner);
    private const string Repo = nameof(Repo);
    private const string PathFileName = nameof(PathFileName);
    private const string RegionName = nameof(RegionName);
    private readonly HttpClient HttpClient = new HttpClient();

    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
    {
      IMetadataDictionary arguments = args.ToDictionary(Owner, Repo, PathFileName, RegionName);
      arguments.RequireKeys(Owner, Repo, PathFileName);
      content = GetContent(arguments.GetString(Owner), arguments.GetString(Repo), arguments.GetString(PathFileName), arguments.GetString(RegionName)).Result;
      return content;

    }
    public async Task<string> GetContent(string owner, string repo, string pathFileName, string regionName)
    {
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Get,
        RequestUri = new Uri(string.Format("https://api.github.com/repos/{0}/{1}/contents/{2}", owner, repo, pathFileName)),
      };
      HttpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0");
      using (HttpResponseMessage response = await HttpClient.SendAsync(request))
      {
        if (response.IsSuccessStatusCode)
        {
          string body = await response.Content.ReadAsStringAsync();
          GitContentModel? gitModel = JsonConvert.DeserializeObject<GitContentModel>(body);
          //Read Text from url
          HttpResponseMessage downloadStringResponse = await HttpClient.GetAsync(gitModel.Download_url);
          string responseString = await downloadStringResponse.Content.ReadAsStringAsync();
          string relativeUrl = "(" + gitModel.Html_url.RemoveEnd(pathFileName).Replace("blob", "raw");
          string adjustRelativePath = responseString.Replace("(.", relativeUrl);
          string textContent = adjustRelativePath;

          //Check if it has open and close front matter by check from first three words.
          if (textContent.Substring(0, 3) == "---")
          {
            string result = RemoveOpenAndCloseFrontMatter(textContent);
            return result;
            //Check if it has only end close front matter.
          } else if (textContent.Contains("---"))
          {
            string result = RemoveOnlyCloseFrontMatter(textContent);
            return result;
          } else if (!string.IsNullOrEmpty(regionName))
          {
            string result = GetContentInRegion(textContent, regionName);
            return result;
          }
          else
          {
            return textContent;
          }

        } else {
          return "#Oops, a parameter is invalid.";
        }
      }
    }

    public class GitContentModel
    {
      public string Name { get; set; }
      public string Download_url { get; set; }
      public string Html_url { get; set; }
      public GitContentModel(string aName, string aDownload_url, string aHtml_url)
      {
        Name = aName;
        Download_url = aDownload_url;
        Html_url = aHtml_url;
      }
    }

    public string RemoveOpenAndCloseFrontMatter(string content)
    {
      string firstThreeWords = content.Substring(0, 3);
      int startIndex = content.IndexOf(firstThreeWords) + "---".Length;
      int endIndex = content.LastIndexOf("---");
      string result = content.Remove(startIndex, endIndex - startIndex);
      return result;
    }

    public string RemoveOnlyCloseFrontMatter(string content)
    {
      string firstWord = content.Substring(0, 1);
      int startIndex = content.IndexOf(firstWord);
      int endIndex = content.LastIndexOf("---");
      string removeWords = content.Remove(startIndex, endIndex - startIndex);
      string result = removeWords.Replace("---", " ");
      return result;
    }

    public string GetContentInRegion(string content, string regionName)
    {
      string regionWithName = "#region " + regionName;
      string endregion = "#endregion";

      //get the index of given region name.
      //remove all words before the region name, this use to find index of endregion tag.
      string firstWord = content.Substring(0, 1);
      int startIndex = content.IndexOf(firstWord);
      int endIndex = content.IndexOf(regionWithName);
      string removeWords = content.Remove(startIndex, endIndex - startIndex);

      //select words between region name to endregion.
      int pFrom = removeWords.IndexOf(regionWithName) + regionWithName.Length;
      int pTo = removeWords.IndexOf(endregion);
      return removeWords.Substring(pFrom, pTo - pFrom);
    }
  }
}
