using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIHelper : MonoBehaviour
{
    public static APIHelper Instance = null;
    public GameObject loadingScreen;
    public string URL = "https://crudcrud.com/api/4cd75df6ba2e45bbb729ee2ad587e8ab/data";

    private void Start()
    {
        Instance = this;
    }

    public void GetData()
    {
        StartCoroutine(GetRequest());
    }

    IEnumerator GetRequest()
    {
        loadingScreen.SetActive(true);

        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

        loadingScreen.SetActive(false);
    }

    public void PostData(string json)
    {
        StartCoroutine(PostRequest(json));
    }

    IEnumerator PostRequest(string json)
    {
        loadingScreen.SetActive(true);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, json, "application/json"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else if(www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Form upload complete! " + www.downloadHandler.text);
            }
        }

        loadingScreen.SetActive(false);
    }
}
