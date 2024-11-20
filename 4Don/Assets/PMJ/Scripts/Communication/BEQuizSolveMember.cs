using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BEQuizSolveMember : MonoBehaviour
{
    public QuizSloveMember[] quizSloveMembers; // 데이터를 저장할 배열
    public int member = 1;
    private async void Start()
    {
        if (!GoogleSheetManager.Instance.IsLoaded)
            await UniTask.WaitUntil(() => GoogleSheetManager.Instance.IsLoaded);

        FetchquizsolveMember();
    }

    private void FetchquizsolveMember()
    {
        // URL 데이터를 GoogleSheetManager에서 가져옴
        var urlData = GoogleSheetManager.Instance.UrldataGet("퀴즈풀이기록멤버");

        if (string.IsNullOrEmpty(urlData.Server))
        {
            Debug.LogError("퀴즈풀이기록멤버 URL의 서버 주소가 비어있습니다.");
            return;
        }

        // 특정 숫자를 URL 뒤에 추가
        string appendedUrl = AppendNumberToUrl(urlData.Server, member); // 예: 숫자 123을 추가
        Debug.Log("최종 URL: " + appendedUrl);

        GetSolveMemberFromServer(appendedUrl).Forget();
    }

    private string AppendNumberToUrl(string baseUrl, int number)
    {
        // URL에 숫자를 추가 (URL 끝에 "/"가 없으면 추가한 후 숫자를 붙임)
        if (!baseUrl.EndsWith("/"))
        {
            baseUrl += "/";
        }

        return baseUrl + number;
    }

    private async UniTask GetSolveMemberFromServer(string url)
    {
        using var request = UnityWebRequest.Get(url);

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", LoginCommunicator.Value);
        //request.SetRequestHeader("Authorization",
            //"Bearer eyJkYXRlIjoxNzMxMTM4NzQxOTcwLCJ0eXBlIjoiand0IiwiYWxnIjoiSFMyNTYifQ.eyJzdWIiOiJ0b2tlbiA6IDYiLCJtZW1iZXJTY2hvb2wiOiLsi6DssL3spJEiLCJtZW1iZXJHcmFkZSI6MSwibWVtYmVyTmFtZSI6IuyGoe2YuOynhCIsIm1lbWJlck5pY2tuYW1lIjoi7Iah7Zi47KeEIiwiZXhwIjoxNzYyNjc0NzQxLCJtZW1iZXJSb2xlIjoiVEVBQ0hFUiIsIm1lbWJlckNsYXNzIjoyLCJtZW1iZXJJZCI6NiwibWVtYmVyRW1haWwiOiIwOTE4c3lqQGcuY29tIn0.pH30ziFfTxYDLMqSAfBvKUvfIdEXlRCexcO6zg5ig8k");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            Debug.Log("서버 응답 수신 성공: " + jsonResponse);

            // JSON 데이터를 List<StockRecordMemberGet>로 파싱
            List<QuizSloveMember> quizSloveMember = JsonConvert.DeserializeObject<List<QuizSloveMember>>(jsonResponse);
            UseStocksData(quizSloveMember);
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }


    private void UseStocksData(List<QuizSloveMember> quizSloveMember)
    {
        // 데이터를 QuizSloveMember 배열로 변환하여 저장
        quizSloveMembers = quizSloveMember.ToArray();

        // 변환된 데이터를 출력
        foreach (var quiz in quizSloveMembers)
        {
            Debug.Log($"Quiz Solve Record ID: {quiz.quizSolveRecordId}, " +
                      $"Member ID: {quiz.gameMemberId}, " +
                      $"Quiz ID: {quiz.quizId}, " +
                      $"Created At: {quiz.createdAt}, " +
                      $"Correct: {quiz.correct}, " +
                      $"Correct Money: {quiz.quizCorrectMoney}");
        }
    }

}
