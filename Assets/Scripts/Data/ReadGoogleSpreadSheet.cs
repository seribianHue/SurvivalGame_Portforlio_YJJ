using UnityEngine.Networking;

public static class ReadGoogleSpreadSheet
{
    //CSV : Comma-Separated Values
    public static string GetCSVAddress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=csv&range={range}&gid={sheetID}";
    }

    public static string ReadData(string address, string range, long sheetID)
    {
        UnityWebRequest www = UnityWebRequest.Get(GetCSVAddress(address, range, sheetID));
        www.SendWebRequest();

        while(www.isDone == false)
        {
            continue;
        }

        return www.downloadHandler.text;
    }
}
