namespace TimeWarp.Statiq.ShortCodes;

using Newtonsoft.Json;

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

    content =
      GetContent
      (
        arguments.GetString(Owner),
        arguments.GetString(Repo),
        arguments.GetString(PathFileName),
        arguments.GetString(RegionName)
      ).Result;

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

        Console.WriteLine("body" + body);
        GitContentModel? gitModel = JsonConvert.DeserializeObject<GitContentModel>(body);
        //Read Text from url
        Console.WriteLine("Download Url" + gitModel?.Html_url);
        Console.WriteLine("Download Url" + gitModel?.Name);
        Console.WriteLine("Download Url" + gitModel?.Download_url);
        HttpResponseMessage downloadStringResponse = await HttpClient.GetAsync(gitModel?.Download_url);
        string responseString = await downloadStringResponse.Content.ReadAsStringAsync();
        string relativeUrl = "(" + gitModel?.Html_url.RemoveEnd(pathFileName).Replace("blob", "raw");
        string adjustRelativePath = responseString.Replace("(.", relativeUrl);
        string textContent = adjustRelativePath;

        //Check if it has open and close front matter by check from first three words.
        if (textContent.Substring(0, 3) == "---")
        {
          string result = RemoveOpenAndCloseFrontMatter(textContent);
          return result;
        }
        // Remove this problematic else-if that's causing issues
        // GitHub API returns raw content without frontmatter, so --- in content
        // should not be treated as frontmatter delimiters
        else if (!string.IsNullOrEmpty(regionName))
        {
          string result = GetContentInRegion(textContent, regionName);
          return result;
        }
        else
        {
          return textContent;
        }
      }
      else
      {
        string errorMessage = 
          $"#Error: GitHub API request failed with status {(int)response.StatusCode} ({response.StatusCode}). " +
          $"Parameters provided: Owner='{owner}', Repo='{repo}', PathFileName='{pathFileName}'" +
          (string.IsNullOrEmpty(regionName) ? "" : $", RegionName='{regionName}'");
          
        return errorMessage;
      }
    }
  }

  public class GitContentModel
  {
    [JsonProperty("name")]
    public string Name { get; set; } = String.Empty;

    [JsonProperty("download_url")]
    public string Download_url { get; set; } = String.Empty;

    [JsonProperty("html_url")]
    public string Html_url { get; set; } = String.Empty;
  }

  public string RemoveOpenAndCloseFrontMatter(string content)
  {
    // Find the second occurrence of "---" which marks the end of frontmatter
    int firstDashIndex = content.IndexOf("---");
    if (firstDashIndex != 0)
    {
      // Content doesn't start with frontmatter
      return content;
    }
    
    int secondDashIndex = content.IndexOf("---", firstDashIndex + 3);
    if (secondDashIndex == -1)
    {
      // No closing frontmatter delimiter found
      return content;
    }
    
    // Skip past the closing "---" and any following newline
    int contentStart = secondDashIndex + 3;
    if (contentStart < content.Length && content[contentStart] == '\r')
      contentStart++;
    if (contentStart < content.Length && content[contentStart] == '\n')
      contentStart++;
    
    return content.Substring(contentStart);
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
