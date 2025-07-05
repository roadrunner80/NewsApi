using System;
public class GNewsSource
{
  public string Name { get; set; } = "";
  public string Url { get; set; } = "";
    
}

public class GNewsItem
{
  public string Title { get; set; } = "";
  public string Description { get; set; } = "";
  public string Content { get; set; } = "";
  public string Url { get; set; } = "";
  public string Image { get; set; } = "";
  public DateTime PublishedAt { get; set; }
  public GNewsSource Source { get; set; } = new GNewsSource();
}