using System;
using Newtonsoft.Json;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    private void Start()
    {
        //폴더에서 json파일 찾아
        TextAsset jsonData = Resources.Load<TextAsset>("Quiz");
        
        //QuizlistData loadedData = JsonConvert.DeserializeObject<QuizlistData>();
    }
}
