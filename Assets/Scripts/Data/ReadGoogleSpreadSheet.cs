using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class ReadGoogleSpreadSheet
{
/*    static ReadGoogleSpreadSheet instance;
    public static ReadGoogleSpreadSheet Instance {  get { return instance; } }

    private void Awake()
    {
        instance = this;
    }*/

/*    public static IEnumerator CRT_ReadData(string address, string range, long sheetID)
    {
        UnityWebRequest www = UnityWebRequest.Get(GetCSVAddress(address, range, sheetID));
        yield return www.SendWebRequest();

        if(www.isDone)
        {
            yield return www.downloadHandler.text;
        }

    }*/

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
