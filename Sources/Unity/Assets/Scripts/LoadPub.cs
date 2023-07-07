using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public enum PubType
{
    Banniere,
    Interstitiel
}

public class LoadPub : MonoBehaviour
{
    public PubType type;
    private ServerResponse.PubResponse pubResponse = new ServerResponse.PubResponse();
    
    void OnEnable()
    {
        SwitchPub();
    }
    
    
    private IEnumerator LoadBannierre()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string token = PlayerPrefs.GetString("token");

            UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/get_1_banniere");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("token", token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Erreur lors de la requête : " + webRequest.error);
                webRequest.Dispose();
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;        
                pubResponse = JsonUtility.FromJson<ServerResponse.PubResponse>(jsonResponse);

                webRequest.Dispose();
            }
        }
		        
    }
    
    private IEnumerator LoadBannierreWithoutToken()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {

            UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/get_1_banniere");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError )
            {
                Debug.Log("Erreur lors de la requête : " + webRequest.error);
                webRequest.Dispose();
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
					        
                pubResponse = JsonUtility.FromJson<ServerResponse.PubResponse>(jsonResponse);

                webRequest.Dispose();
            }
        }
		        
    }

    private IEnumerator Load1Pub()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string token = PlayerPrefs.GetString("token");

            UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/get_1_pub");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("token", token);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Erreur lors de la requête : " + webRequest.error);
                webRequest.Dispose();
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;        
                pubResponse = JsonUtility.FromJson<ServerResponse.PubResponse>(jsonResponse);

                webRequest.Dispose();
            }
        }
		        
    }
    
    private IEnumerator LoadPubWithoutToken()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {

            UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/get_1_pub");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError )
            {
                Debug.Log("Erreur lors de la requête : " + webRequest.error);
                webRequest.Dispose();
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
					        
                pubResponse = JsonUtility.FromJson<ServerResponse.PubResponse>(jsonResponse);
                Debug.Log("infos : "+jsonResponse);
					        
                webRequest.Dispose();
            }
        }
		        
    }
    public ServerResponse.PubResponse GetPubResponse()
    {
        return pubResponse;
    }

    public void SwitchPub()
    {
        if (type == PubType.Banniere)
        {
            if (PlayerPrefs.GetString("token") != "")
            {
                StartCoroutine(LoadBannierre());
            }
            else
            {
                StartCoroutine(LoadBannierreWithoutToken());
            }
        }
        else if (type == PubType.Interstitiel)
        {
            if (PlayerPrefs.GetString("token") != "")
            {
                StartCoroutine(Load1Pub());
            }
            else
            {
                StartCoroutine(LoadPubWithoutToken());
            }
        }
    }
    
}

