namespace TheFreezeTeam.Com
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class MetaDataKeys
  {
    public const string ImageUrl = nameof(ImageUrl);
    public const string SiteTitle = nameof(SiteTitle);

    /// <summary>
    /// A glob pattern configured in settings.yml
    /// </summary>
    public const string PostSources = nameof(PostSources);

    //public const string IsPost = nameof(IsPost);

    public const string Author = nameof(Author);
    //public const string PageTitle = nameof(PageTitle);
    //public const string ReadTime = nameof(ReadTime);
    //public const string SiteTitle = nameof(SiteTitle);
    public const string Tags = nameof(Tags);
  }
}
