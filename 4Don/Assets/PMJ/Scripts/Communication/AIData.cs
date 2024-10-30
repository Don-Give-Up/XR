  using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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
    [JsonConverter(typeof(Texture2DConverter))]
    public Texture2D image;
}

[System.Serializable]
public class ArticlesData
{
    public List<Article> articles;  // articles 배열
}



public class Texture2DConverter : JsonConverter
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
}
