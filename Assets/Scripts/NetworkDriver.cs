using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Web;


public class NetworkDriver : MonoBehaviour
{
    static private readonly string db = "https://kvdb.io/88jaQDc2YDgKHgQfMwv3zz/";

    static public IEnumerator Upload(string key,string data)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(db + key, data))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }


    static public IEnumerator Get(string key,System.Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(db+key))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            PlayerPrefs.SetFloat("SOM_KEY",1.2f);
            float myFloat = PlayerPrefs.GetFloat("SOM_KEY");

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(key + ": Error: " + webRequest.error);
                    callback("");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(key + ": HTTP Error: " + webRequest.error);
                    callback("");
                    break;
                case UnityWebRequest.Result.Success:
                    callback(UnityWebRequest.UnEscapeURL(UnityWebRequest.UnEscapeURL(webRequest.downloadHandler.text.ToString())));
                    break;
            }
        }
    }
}
