using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using UnityEngine;

public static partial class GoogleSheets
{
    /// <summary>
    /// 범위에 해당하는 셀의 데이터를 가져옵니다.
    /// </summary>
    /// <example>
    /// <c>await Get("A1:C10");</c><br/>
    /// <c>await Get("시트2!A1:C10");</c>
    /// </example>
    /// <param name="range">데이터를 가져올 시트 및 셀의 범위</param>
    public static async UniTask<IList<IList<object>>> Get(string range) // 주어진 범위의 데이터를 스프레드시트에서 가져옴
     {
        var request = _sheetsService.Spreadsheets.Values.Get(_spreadsheetId, range); // 스프레드시트의 특정 범위를 요청
        var response = await request.ExecuteAsync(); // 요청실행 
        return response.Values; // 가져온 데이터 반환 
    }
    
    /// <summary>
    /// 범위에 해당하는 셀에 데이터를 씁니다.
    /// </summary>
    /// <example>
    /// <code>
    /// var values = new List&lt;IList&lt;object&gt;&gt;
    /// {
    ///     new List&lt;object&gt; { "1", "2", "3" },
    ///     new List&lt;object&gt; { "a", "b", "c" }
    /// };
    /// await Write("D21:F22", values);
    /// </code>
    /// </example>
    /// <param name="range">데이터를 쓸 시트 및 셀의 범위</param>
    /// <param name="values">데이터 목록</param>
    public static async UniTask Write(string range, List<IList<object>> values)  // 데이터 쓰기 
    {
        var valueRange = new ValueRange { Values = values }; // 쓸 데이터를 valueRange 형식으로 설정 

        var updateRequest = _sheetsService.Spreadsheets.Values.Update(valueRange, _spreadsheetId, range); // 데이터를 업데이트할 요청을 생성 
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        await updateRequest.ExecuteAsync(); // 비동기 방식으로 요청을 실행 
    }

    /// <summary>
    /// 범위에 해당하는 Column에 데이터를 씁니다.
    /// </summary>
    /// <example>
    /// <code>
    /// var newRow = new List&lt;object&gt;
    ///     { "New", "Row", "Data", "1", "2" };
    /// await WriteRow("A15:E15", newRow);
    /// </code>
    /// </example>
    /// <param name="range">데이터를 쓸 시트 및 셀의 범위</param>
    /// <param name="rowData">데이터 목록</param>
    public static async UniTask WriteRow(string range, IList<object> rowData)
    {
        var valueRange = new ValueRange { Values = new List<IList<object>> { rowData } };

        var updateRequest = _sheetsService.Spreadsheets.Values.Update(valueRange, _spreadsheetId, range);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        await updateRequest.ExecuteAsync();
    }

    /// <summary>
    /// 범위에 해당하는 Column에 데이터를 씁니다.
    /// </summary>
    /// <example>
    /// <code>
    /// var newCol = new List&lt;object&gt;
    ///     { "New", "Col", "Data", "1", "2" };
    /// await WriteColumn("A16:A20", newCol);
    /// </code>
    /// </example>
    /// <param name="range">데이터를 쓸 시트 및 셀의 범위</param>
    /// <param name="colData">데이터 목록</param>
    public static async UniTask WriteColumn(string range, IList<object> colData)
    {
        // row -> col
        var valueRange = new ValueRange
        {
            Values = colData.Select(value => new List<object> { value }).ToList<IList<object>>()
        };

        var updateRequest = _sheetsService.Spreadsheets.Values.Update(valueRange, _spreadsheetId, range);
        updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
        await updateRequest.ExecuteAsync();
    }

    /// <summary>
    /// 시트의 가장 아래에 row를 추가합니다.
    /// </summary>
    /// <example>
    /// <code>
    /// var appendRow = new List&lt;object&gt;
    ///     { "Append", "Data", "1", "2" };
    /// await AppendRow("A:A", appendRow);
    /// </code>
    /// </example>
    /// <param name="rowData">데이터 목록</param>
    public static async UniTask AppendRow(IList<object> rowData)
    {
        var valueRange = new ValueRange { Values = new List<IList<object>> { rowData } };
        var appendRequest = _sheetsService.Spreadsheets.Values.Append(valueRange, _spreadsheetId, "A:A");
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;
        await appendRequest.ExecuteAsync();
    }

    /// <summary>
    /// 구조체의 모든 필드를 시트의 가장 아래에 row로 추가합니다.
    /// private이거나, 프로퍼티는 무시합니다.
    /// </summary>
    /// <param name="structData">row로 추가할 구조체 인스턴스</param>
    public static async UniTask AppendStructToRow<T>(T structData) where T : struct
    {
        var rowData = new List<object>();

        var fields = typeof(T).GetFields();
        foreach (var field in fields)
        {
            var value = field.GetValue(structData);
            var v = value?.ToString() ?? "";
            rowData.Add(v);
        }

        await AppendRow(rowData);
    }
    
    /// <summary>
    /// 범위에 해당하는 영역의 데이터를 지웁니다.
    /// <example>
    /// <c>await Delete("H12:H15");</c><br/>
    /// <c>await Delete("G7:G9");</c><br/>
    /// <c>await Delete("시트2!G9:I10");</c>
    /// </example>
    /// </summary>
    /// <param name="range">데이터를 지울 시트 및 셀의 범위</param>
    public static async UniTask Delete(string range)
    {
        var request = new ClearValuesRequest();
        var clearRequest = _sheetsService.Spreadsheets.Values.Clear(request, _spreadsheetId, range);
        await clearRequest.ExecuteAsync();
    }
    
    /// <summary>
    /// 해당 인덱스의 해당하는 row의 데이터를 포함하여 row 자체를 삭제합니다.
    /// </summary>
    /// <example>
    /// <c>await DeleteRows("시트1", 3, 5);</c>
    /// </example>
    /// <param name="sheetName">삭제할 시트의 이름</param>
    /// <param name="startIndex">시작 인덱스</param>
    /// <param name="endIndex">종료 인덱스</param>
    public static async UniTask DeleteRows(string sheetName, int startIndex, int endIndex)
    {
        var sheetId = await GetSheetId(sheetName);
        if (sheetId == null)
        {
            Debug.Log($"Failed DeleteRows. Sheet '{sheetName}' not found.");
            return;
        }

        var deleteRequest = _sheetsService.Spreadsheets.BatchUpdate(new BatchUpdateSpreadsheetRequest()
        {
            Requests = new List<Request>()
            {
                new Request()
                {
                    DeleteDimension = new DeleteDimensionRequest()
                    {
                        Range = new DimensionRange()
                        {
                            SheetId = sheetId,
                            Dimension = "ROWS",
                            StartIndex = startIndex - 1,
                            EndIndex = endIndex
                        }
                    }
                }
            }
        }, _spreadsheetId);

        await deleteRequest.ExecuteAsync();
        Debug.Log($"Rows {startIndex} to {endIndex} deleted from sheet '{sheetName}'.");
    }
    
    /// <summary>
    /// 해당 인덱스의 해당하는 column의 데이터를 포함하여 column 자체를 삭제합니다.
    /// </summary>
    /// <example>
    /// <c>await DeleteColumns("시트1", 2, 3);</c>
    /// </example>
    /// <param name="sheetName">삭제할 시트의 이름</param>
    /// <param name="startIndex">시작 인덱스</param>
    /// <param name="endIndex">종료 인덱스</param>
    public static async UniTask DeleteColumns(string sheetName, int startIndex, int endIndex)
    {
        var sheetId = await GetSheetId(sheetName);
        if (sheetId == null)
        {
            Debug.Log($"Failed DeleteColumns. Sheet '{sheetName}' not found.");
            return;
        }

        var deleteRequest = _sheetsService.Spreadsheets.BatchUpdate(new BatchUpdateSpreadsheetRequest()
        {
            Requests = new List<Request>()
            {
                new Request()
                {
                    DeleteDimension = new DeleteDimensionRequest()
                    {
                        Range = new DimensionRange()
                        {
                            SheetId = sheetId,
                            Dimension = "COLUMNS",
                            StartIndex = startIndex - 1,
                            EndIndex = endIndex
                        }
                    }
                }
            }
        }, _spreadsheetId);

        await deleteRequest.ExecuteAsync();
        Debug.Log($"Columns {startIndex} to {endIndex} deleted from sheet '{sheetName}'.");
    }
}