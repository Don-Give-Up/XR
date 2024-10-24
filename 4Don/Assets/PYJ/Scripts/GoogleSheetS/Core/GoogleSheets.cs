using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

public static partial class GoogleSheets
{
    private static string _spreadsheetId;
    private static SheetsService _sheetsService;

    /// <summary>
    /// 구글 시트의 데이터를 읽고 쓰기 위해 필요한 초기화를 진행합니다.
    /// </summary>
    /// <param name="spreadsheetId">시트의 URL에서 얻어온 스프레드시트 ID를 입력합니다. https://docs.google.com/spreadsheets/d/{spreadsheetId}</param>
    /// <param name="credentialsPath">구글 클라우드 콘솔에서 받아온 인증 파일의 경로를 입력합니다.</param>
    public static void Initialize(string spreadsheetId, string credentialsPath)
    {
        _spreadsheetId = spreadsheetId;
        
        var credential = GoogleCredential.FromFile(credentialsPath)  // 인증파일을 사용하여 google Api에 접근하기 위한 자격 증명을 생성함 //파일 위치나 이름 문제라는데 왜 안 됨? 
            .CreateScoped(SheetsService.Scope.Spreadsheets); // sheet API에 대한 권한을 설정

        _sheetsService = new SheetsService(new BaseClientService.Initializer // SheetService 객체를 초기화, 이 객체를 통해 google sheet api 와 상호작용
        {
            HttpClientInitializer = credential, // 생성한 인증 정보가 포함되고
            ApplicationName = Application.productName // guswp unity 애플리케이션의 이름으로 설정 
        });
    }
}