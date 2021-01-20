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
    private readonly HttpClient HttpClient = new HttpClient();

    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
    {
      IMetadataDictionary arguments = args.ToDictionary(Owner, Repo, PathFileName);
      arguments.RequireKeys(Owner, Repo, PathFileName);
      content = GetContent(arguments.GetString(Owner), arguments.GetString(Repo), arguments.GetString(PathFileName)).Result;
      return content;

    }
    public async Task<string> GetContent(string owner, string repo, string pathFileName)
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

          //Check if it has open and close front matter by check from first three words.
          if (responseString.Substring(0, 3) == "---")
          {
            string result = RemoveOpenAndCloseFrontMatter(responseString);
            return result;
            //Check if it has only end close front matter.
          } else if (responseString.Contains("---")) 
          {
            string result = RemoveOnlyCloseFrontMatter(responseString);
            return result;
          }
          else
          {
            return responseString;
          }

        } else {
          return "#Ops, parameter is invalid.";
        }
      }
    }

    public class GitContentModel
    {
      public string Name { get; set; }
      public string Download_url { get; set; }
      public GitContentModel(string aName, string aDownload_url)
      {
        Name = aName;
        Download_url = aDownload_url;
      }
    }

    public string RemoveOpenAndCloseFrontMatter(string content)
    {
      string firstThreeWords = content.Substring(0, 3);
      int startIndex = content.IndexOf(firstThreeWords) + "---".Length;
      int endIndex = content.LastIndexOf("---");
      string processedResult = content.Remove(startIndex, endIndex - startIndex);
      return processedResult;
    }

    public string RemoveOnlyCloseFrontMatter(string content)
    {
      string firstWord = content.Substring(0, 1);
      int startIndex = content.IndexOf(firstWord);
      int endIndex = content.LastIndexOf("---");
      string removeWords = content.Remove(startIndex, endIndex - startIndex);
      string prosseedResult = removeWords.Replace("---", " ");
      return prosseedResult;
    }
  }
}
