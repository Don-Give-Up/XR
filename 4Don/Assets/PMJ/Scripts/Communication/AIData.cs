using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Article
{
    public string field;
    public string cleaned_title;
    public string cleaned_body;
    public string summary;
    public Image image;
}

[System.Serializable]
public class ArticlesData
{
    public List<Article> articles;  // articles 배열
}