using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIHelper : MonoBehaviour
{
    public static APIHelper Instance = null;
    public GameObject loadingScreen;

    public string BASE_URL = "https://crudcrud.com/api/";
    public string KEY = "3bc1af367fd64d59a215f12ed68b0d5a";
    public string ID = "63f8b28b1d8b0e03e8142d13";

    [SerializeField]
    private Room room;

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

        string url = BASE_URL + KEY + "/data/" + ID;
        using UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                string response = request.downloadHandler.text;
                room = JsonUtility.FromJson<Room>(response);
                Debug.Log(response);
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log(request.error);
                break;
            case UnityWebRequest.Result.InProgress:
                Debug.Log("Posting Data...");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log(request.error);
                break;
        }

        loadingScreen.SetActive(false);
    }

    public void PutData(string json)
    {
        StartCoroutine(PutRequest(json));
    }

    IEnumerator PutRequest(string json)
    {
        loadingScreen.SetActive(true);

        string url = BASE_URL + KEY + "/data/" + ID;
        byte[] bodyData = Encoding.UTF8.GetBytes(json);

        using UnityWebRequest request = UnityWebRequest.Put(url, bodyData);
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyData);

        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                string response = request.downloadHandler.text;
                Debug.Log("Put Successful.");
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log(request.error);
                break;
            case UnityWebRequest.Result.InProgress:
                Debug.Log("Putting Data...");
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.Log(request.error);
                break;
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

        string url = BASE_URL + KEY + "/data";
        using UnityWebRequest request = UnityWebRequest.Post(url, json, "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        byte[] rawData = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(rawData);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                string response = request.downloadHandler.text;
                Debug.Log(response);
                break;
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log(request.error); 
                break;
            case UnityWebRequest.Result.InProgress:
                Debug.Log("Posting Data...");
                break;
            case UnityWebRequest.Result.ProtocolError: 
                Debug.Log(request.error);
                break;
        }

        loadingScreen.SetActive(false);
    }
}
