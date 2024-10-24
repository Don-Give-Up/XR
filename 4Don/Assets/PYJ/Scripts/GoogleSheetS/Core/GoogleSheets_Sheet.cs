using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

public static partial class GoogleSheets
{
    /// <summary>
    /// 새로운 시트를 만듭니다.
    /// 이미 같은 이름의 시트가 있을 경우 실패합니다.
    /// </summary>
    /// <example>
    /// <c>await CreateSheet("New Sheet3");</c>
    /// </example>
    /// <param name="sheetName">새 시트의 이름</param>
    public static async UniTask<int?> CreateSheet(string sheetName) // 새로운 시트를 생성, 이미 같은 이름의 시트가 존재하면 실패
    {
        var sheetId = await GetSheetId(sheetName);
        if (sheetId != null)
        {
            Debug.Log($"Failed CreateSheet. Sheet '{sheetName}' exists.");
            return null;
        }
        
        var requestBody = new BatchUpdateSpreadsheetRequest // 새로운 시트를 추가하기 위한 요청 생성
        {
            Requests = new List<Request>
            {
                new Request
                {
                    AddSheet = new AddSheetRequest
                    {
                        Properties = new SheetProperties
                        { 
                            Title = sheetName // 시트의 이름은 title 속성에 설정 
                        }
                    }
                }
            }
        };

        var request = _sheetsService.Spreadsheets.BatchUpdate(requestBody, _spreadsheetId); 
        var response = await request.ExecuteAsync(); // google sheet api 를 통해 시트를 생성하는 요청을 보냄. 비동기 방식, 완료될 때까지 기다림 

        Debug.Log($"Sheet '{sheetName}' created.");

        return response.Replies[0].AddSheet.Properties.SheetId; // 성공적으로 시트가 생성되면 로그를 남기고, 생성된 시트의 id 반환
    }
    
    /// <summary>
    /// 시트를 삭제합니다.
    /// sheetName에 해당하는 시트가 없을 경우 실패합니다.
    /// </summary>
    /// <example>
    /// <c>await DeleteSheet("New Sheet2");</c>
    /// </example>
    /// <param name="sheetName">삭제하려는 시트의 이름</param>
    public static async UniTask DeleteSheet(string sheetName) 
    {
        var sheetId = await GetSheetId(sheetName);
        if (sheetId == null)
        {
            Debug.Log($"Failed DeleteSheet. Sheet '{sheetName}' not found.");
            return;
        }

        var deleteRequest = _sheetsService.Spreadsheets.BatchUpdate(new BatchUpdateSpreadsheetRequest
        {
            Requests = new List<Request>
            {
                new Request()
                {
                    DeleteSheet = new DeleteSheetRequest
                    {
                        SheetId = sheetId
                    }
                }
            }
        }, _spreadsheetId);

        await deleteRequest.ExecuteAsync();
        Debug.Log($"Sheet '{sheetName}' deleted.");
    }

    /// <summary>
    /// 모든 시트의 목록을 얻어옵니다.
    /// </summary>
    public static async UniTask<IList<Sheet>> GetAllSheets()
    {
        var request = _sheetsService.Spreadsheets.Get(_spreadsheetId);
        var response = await request.ExecuteAsync();
        return response.Sheets;
    }
    
    /// <summary>
    /// sheetName에 해당하는 시트의 ID를 얻어옵니다.
    /// </summary>
    /// <example>
    /// <c>await GetSheetId(sheetName);</c>
    /// </example>
    /// <param name="sheetName">ID를 얻어올 시트의 이름</param>
    public static async UniTask<int?> GetSheetId(string sheetName)
    {
        var sheets = await GetAllSheets();
        var sheet = sheets.ToList().Find(s => s.Properties.Title == sheetName);
        if (sheet == null)
        {
            Debug.Log($"Failed GetSheetId. Sheet '{sheetName}' not found.");
            return null;
        }

        return sheet.Properties.SheetId;
    }
}