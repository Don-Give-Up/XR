  using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

using System;

[System.Serializable]
public class NewsBigData
{
    public NewsBig economy;
    public NewsBig finance;
}

[System.Serializable]
public class NewsBig
{
    public string title;
    public string summary;
}


[System.Serializable]
public class CompanyData
{

    // public List<CompanyInfo> Company;
    public CompanyInfo minjeong;
    public CompanyInfo hojin;
    public CompanyInfo meta;
    public CompanyInfo boyeong;
    public CompanyInfo yeowon;
    public CompanyInfo yujin;
    public CompanyInfo chaeho;
    public CompanyInfo minju;

}

[System.Serializable]
public class CompanyInfo
{
    //public string company_name;
    public string hint_title_1;      
    public string summary_100;       
    public string hint_title_2;    // 이거는 돈 주고 샀을떄 
    public string summary_2_lines; // 나오는 겁니당.
}

  /*
[System.Serializable]
public class Article

{
    public string field;
    public string title;
    public string cleaned_body;
    public string summary_2_lines;
    public string summary;
    //[JsonConverter(typeof(Texture2DConverter))]
    //public Texture2D image;
}
*/

/*
[System.Serializable]
public class ArticlesData
{
    public List<Article> articles;  // articles 배열
}
*/
  

/*public class Texture2DConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Texture2D).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var v = reader.Value?.ToString();
        if (!string.IsNullOrEmpty(v))
            return ConvertBase64ToTexture(v);
        
        return new JsonSerializer().Deserialize(reader, objectType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
    }
    
    private Texture2D ConvertBase64ToTexture(string base64Image)
    {
        // Base64 문자열을 byte 배열로 변환
        byte[] imageBytes = System.Convert.FromBase64String(base64Image);

        // Texture2D 생성 및 이미지 데이터 로드
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        return texture;
    }
}*/
