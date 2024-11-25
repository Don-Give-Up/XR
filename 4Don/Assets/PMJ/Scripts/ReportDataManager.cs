using System;
using Newtonsoft.Json;
using UnityEngine;

public class ReportDataManager : MonoBehaviour
{
    private RootData parsedData; // 파싱된 데이터를 저장할 객체

    private void Start()
    {
        LoadJsonData();
    }

    public void LoadJsonData()
    {
        // Resources 폴더에서 JSON 파일 로드 (파일명: "Data.json")
        TextAsset jsonFile = Resources.Load<TextAsset>("ReportData");

        if (jsonFile != null)
        {
            try
            {
                // JSON 문자열을 RootData 객체로 역직렬화(Deserialize)
                parsedData = JsonConvert.DeserializeObject<RootData>(jsonFile.text);

                if (parsedData != null)
                {
                    Debug.Log("플레이어 ID: " + parsedData.raw_data.player_id);
                    Debug.Log("총 자산: " + parsedData.raw_data.assets.total);
                    Debug.Log("현금 비율: " + parsedData.raw_data.ratios.cash + "%");
                    Debug.Log("자산 요약: " + parsedData.analysis.AssetStatusSummary);
                }
                else
                {
                    Debug.LogError("JSON 데이터를 파싱하는 데 실패했습니다.");
                }
            }
            catch (JsonException ex)
            {
                Debug.LogError("JSON 파싱 중 오류 발생: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("JSON 파일을 찾을 수 없습니다! Resources 폴더에 Data.json 파일이 있는지 확인하세요.");
        }
    }
}