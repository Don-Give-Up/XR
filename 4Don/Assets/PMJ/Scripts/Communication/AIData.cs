using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Article
{
    public string field;
    public string cleaned_title;
    public string cleaned_body;
    public string summary_2_lines;
    public string summary_50;
    public string image;
    public Texture2D texture;
}

[System.Serializable]
public class ArticlesData
{
    public List<Article> articles;  // articles 배열
}


