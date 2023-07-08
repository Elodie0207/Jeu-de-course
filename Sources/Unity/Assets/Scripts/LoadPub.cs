using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

//type de la pub
public enum PubType
{
    Banniere,
    Interstitiel
}

public class LoadPub : MonoBehaviour
{
    public PubType type;
    private ServerResponse.PubResponse pubResponse = new ServerResponse.PubResponse();
    
    //lancer coroutine
    void OnEnable()
    {
        SwitchPub();
    }
    
    //coroutine pour load une bannierre avec token
    private IEnumerator LoadBannierre()
    {
        //verif si on a internet
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string token = PlayerPrefs.GetString("token");

            //crea de la requete 
            UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/get_1_banniere");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("token", token);

            //envoie requete
            yield return webRequest.SendWebRequest();

            //verifie les erreurs
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                webRequest.Dispose();
            }
            else
            {
                //telecharge la reponse du srv
                string jsonResponse = webRequest.downloadHandler.text;        
                pubResponse = JsonUtility.FromJson<ServerResponse.PubResponse>(jsonResponse);

                webRequest.Dispose();
            }
        }
		        
    }
    
    //coroutine pour load une bannierre sans token
    private IEnumerator LoadBannierreWithoutToken()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //crea de la requete
            UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/get_1_banniere");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            //envoie requete
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError )
            {
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

    //load pub intertitiel avec token
    private IEnumerator Load1Pub()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string token = PlayerPrefs.GetString("token");
            //crea de la requete
            UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/get_1_pub");
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("token", token);

            //envoie requete
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
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
    
    //load pub intertitiel sans token
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
    
    //recup la reponse pour les autres script
    public ServerResponse.PubResponse GetPubResponse()
    {
        return pubResponse;
    }

    //load coroutine selon si l'usr est connecté ou si le type de la pub
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

