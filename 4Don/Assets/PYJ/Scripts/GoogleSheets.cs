using UnityEngine;

public class GoogleSheets : MonoBehaviour
{
    public string googleSpreadsheetId = "your-spreadsheet-id";
    public string credentialsNameInStreamingAssets = "your-credentials.json";

    private struct SampleData
    {
        public int ID;
        public string Name;
        public string Description { get; set; }

        public SampleData(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }
    }
    
    // A1:E29
}
