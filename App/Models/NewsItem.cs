using System;

public class NewsSource
{
  public string Name { get; set; } = "";
  public string Url { get; set; } = "";
    
}
public class NewsItem
{
  public string Title { get; set; } = "";
  public string Description { get; set; } = "";
  public string Content { get; set; } = "";
  public string Url { get; set; } = "";
  public string Image { get; set; } = "";
  public DateTime PublishedAt { get; set; }
  public NewsSource Source { get; set; } = new NewsSource();
}