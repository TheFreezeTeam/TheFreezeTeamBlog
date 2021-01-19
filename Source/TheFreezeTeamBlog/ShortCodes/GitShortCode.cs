namespace TheFreezeTeamBlog.ShortCodes
{
  using Newtonsoft.Json;
  using Statiq.Common;
  using System;
  using System.Collections.Generic;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;

  public class GitShortCode : SyncShortcode
  {
    private const string Owner = nameof(Owner);
    private const string Repo = nameof(Repo);
    private const string FileName = nameof(FileName);

    public override ShortcodeResult Execute(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
    {
      var GitShortCode = new GitShortCode();
      IMetadataDictionary arguments = args.ToDictionary(Owner, Repo, FileName);
      arguments.RequireKeys(Owner, Repo, FileName);
      content = GitShortCode.GetContent(arguments.GetString(Owner), arguments.GetString(Repo), arguments.GetString(FileName)).Result;
      return content;

    }
    public async Task<string> GetContent(string owner, string repo, string fileName)
    {
      string plainText;
      var httpClient = new HttpClient();
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Get,
        RequestUri = new Uri(string.Format("https://api.github.com/repos/{0}/{1}/contents/{2}", owner, repo, fileName)),
      };
      httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10; Win64; x64; rv:60.0) Gecko/20100101 Firefox/60.0");
      using (HttpResponseMessage response = await httpClient.SendAsync(request))
      {
        if (response.IsSuccessStatusCode)
        {
          string body = await response.Content.ReadAsStringAsync();
          var gitModel = new GitContentModel();
          gitModel = JsonConvert.DeserializeObject<GitContentModel>(body);

          //Read Text from url
          using (var webClient = new WebClient())
          {
            plainText = webClient.DownloadString(gitModel.Download_url);
          }
          return plainText;
        } else {
          return "#Ops, markdown is invalid.";
        }
      }
    }

    public class GitContentModel
    {
      public string Name { get; set; }
      public string Download_url { get; set; }
    }
  }
}
