using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;

public class Menu : MonoBehaviour
{
	public GameObject bouttonLogin;
	public GameObject bouttonLike;
    public GameObject canvaLogin;
    public GameObject canvaUser;
    public GameObject CanvaCourrant;
    public GameObject nouveauCanva;
	public GameObject CanvaMap;
    public GameObject CanvaVaisseau;
    public GameObject CanvaVaisseau2;
	public GameObject CanvaJoueur;
	public GameObject canvaWatchPub;
    public Dropdown drop;
    public GamemodeManager ManagerMode;
	private string map="";
	private string vaisseau="";
	private string vaisseau2="";
	
	public TMP_InputField  identifiantFieldConnexion;
	public TMP_InputField  mdpFieldConnexion;
	public TMP_InputField  identifiantFieldRegister;
	public TMP_InputField  mdpFieldRegister;
	public GameObject errorMessage;
	
	public Toggle togglePremium;
	public GameObject PremiumMessage;
	
	private bool loginCoroutineRunning;
	
	public List<Image> spotPub;
	public Image pubIntertitiel;
	public List<GameObject> badgeList;
	
	public void Start()
	{
			CanvaCourrant.SetActive(true); 
		    nouveauCanva.SetActive(false);
			CanvaMap.SetActive(false);
			CanvaVaisseau.SetActive(false);
			CanvaVaisseau2.SetActive(false);
			CanvaJoueur.SetActive(false);
			canvaLogin.SetActive(false);
			errorMessage.SetActive(false);
			canvaUser.SetActive(false);
			canvaWatchPub.SetActive(false);
		    Screen.fullScreen = true; 
		   
	        drop.options.Clear();
	        drop.options.Add(new Dropdown.OptionData("Fullscreen"));
	        drop.options.Add(new Dropdown.OptionData("Fenêtre"));
	        drop.options.Add(new Dropdown.OptionData("1920x1080"));
	        drop.options.Add(new Dropdown.OptionData("2560x1440"));
	        drop.onValueChanged.AddListener(delegate
	        {
		        OnScreenModeChanged(drop.value);
	        });
	}
    
    public void Jouer(){
		CanvaJoueur.SetActive(true);
		CanvaCourrant.SetActive(false);
		ActiveBaniere();
    }

    public void Quitter(){
        Application.Quit();
    }
    public void Param(){
	   
        CanvaCourrant.SetActive(false); 
		nouveauCanva.SetActive(true);
		ActiveBaniere();

    }
	public void Solo(){
		ActiveBaniere();
		CanvaMap.SetActive(true);
		CanvaJoueur.SetActive(false);
		ManagerMode.CurrentMode = GameMode.Single;
	}
	public void Multi(){
		ActiveBaniere();
		CanvaMap.SetActive(true);
		CanvaJoueur.SetActive(false);
		ManagerMode.CurrentMode = GameMode.Multiplayer;
	}

	public void OnImageClick(){

		map = "Map1";
		CanvaMap.SetActive(false);
				CanvaVaisseau.SetActive(true);
		PlayerPrefs.SetString("map",map);
		Debug.Log(map);
	}

	public void OnPlayerOneChoice()
	{
		CanvaVaisseau.SetActive(false);
		CanvaVaisseau2.SetActive(true);
		
	}


	public void Chargement(UnityEngine.UI.Button button){
		
		string objectName = button.gameObject.name;
	    vaisseau = objectName;
		PlayerPrefs.SetString("vaisseau",vaisseau);

		if(ManagerMode.CurrentMode == GameMode.Single)
		{
			ManagerMode.SingleMode();
			
		}
		
		else if(ManagerMode.CurrentMode == GameMode.Multiplayer)
		{
			OnPlayerOneChoice();
			//ManagerMode.MultiplayerMode();
		}
	}

	public void MultiChargement(UnityEngine.UI.Button button)
	{
		
		string object2Name = button.gameObject.name;
		vaisseau2 = object2Name;
		PlayerPrefs.SetString("vaisseau2",vaisseau2);
		ManagerMode.MultiplayerMode();
	}

			public void OnScreenModeChanged(int value)
		    {
		        switch (value)
		        {
		            case 0:
		                Screen.fullScreen = true;
		                break;
		            case 1:
		                Screen.fullScreen = false;
		                break;
		            case 2:
		                Screen.SetResolution(1920, 1080, Screen.fullScreen);
		                break;
		            case 3:
		                Screen.SetResolution(2560, 1440, Screen.fullScreen);
		                break;
		            default:
		                Debug.LogError("Erreur");
		                break;
		        }
		    }
			public void Retour(GameObject canvaChildren)
			{
				CanvaCourrant.SetActive(true); 
				canvaChildren.SetActive(false);
			}
			
			public void Close()
			{
				canvaUser.SetActive(true); 
				canvaWatchPub.SetActive(false);
			}
			
			public void WatchPub()
			{
				Image buttonImage = bouttonLike.GetComponent<Image>();
				
				canvaWatchPub.SetActive(true); 
				canvaUser.SetActive(false);
				
				LoadPub pubLoader = pubIntertitiel.GetComponent<LoadPub>();
				ServerResponse.PubResponse pubResponse = pubLoader.GetPubResponse();

				if(pubResponse.like)
					buttonImage.sprite = Resources.Load<Sprite>("Images/hearth");
				else
					buttonImage.sprite = Resources.Load<Sprite>("Images/hearth_noFill");
						
				Sprite imageSprite = Resources.Load<Sprite>("PubImage/"+pubResponse.path);
				

				if (imageSprite != null)
				{
					pubIntertitiel.sprite = imageSprite;
				}
				else
				{
					Debug.Log("erreur Sprite : "+"PubImage/"+pubResponse.path);
				}
				
				StartCoroutine(WaitAndChangeCanvas(10f));
			}

			
			private IEnumerator WaitAndChangeCanvas(float waitTime)
			{
				yield return new WaitForSeconds(waitTime);
				StartCoroutine(UpdateNbPub());
				
				canvaWatchPub.SetActive(false);
				canvaUser.SetActive(true);
			}



			public void ActiveBaniere()
			{
				foreach (Image image in spotPub)
				{
					if (PlayerPrefs.GetInt("premium") == 0)
					{
						LoadPub pubLoader = image.GetComponent<LoadPub>();
						ServerResponse.PubResponse pubResponse = pubLoader.GetPubResponse();
						
						Sprite imageSprite = Resources.Load<Sprite>("PubImage/"+pubResponse.path);

						if (imageSprite != null)
						{
							image.sprite = imageSprite;
						}
						else
						{
							Debug.Log("erreur Sprite : "+"PubImage/"+pubResponse.path);
						}
						image.enabled = true;

					}
					else
					{
						image.enabled = false;
					}
				}
			}
	        
	        public void ToUserCanva()
	        {
		        if ( string.IsNullOrEmpty(bouttonLogin.GetComponentInChildren<Text>().text))
		        {
			        CanvaCourrant.SetActive(false);
			        canvaLogin.SetActive(true);
		        }
		        else
		        {
			        if (PlayerPrefs.GetInt("premium") == 1)
			        {
				        PremiumMessage.SetActive(false);
			        }
			        else
			        {
				        PremiumMessage.SetActive(true);
			        }

			        foreach (var badge in badgeList)
			        {
				        RawImage imageBadge = badge.GetComponentInChildren<RawImage>();
				        Color imageColor = imageBadge.color;
				        
				        if (PlayerPrefs.GetString(badge.name) == "")
					        imageColor.a = 0.1f;
				        else
							 imageColor.a = 1f;
				        imageBadge.color = imageColor;
			        }
			        
			        CanvaCourrant.SetActive(false);
			        canvaUser.SetActive(true);

			        
		        }
		        
	        }
	        
	        private IEnumerator Login()
	        {
		        string login = identifiantFieldConnexion.text;
		        string mdp = mdpFieldConnexion.text;

		        if (!string.IsNullOrEmpty(login.Trim()) && !string.IsNullOrEmpty(mdp.Trim()))
		        {
			        if (Application.internetReachability != NetworkReachability.NotReachable)
			        {
				        StringBuilder jsonBuilder = new StringBuilder();
				        jsonBuilder.Append("{");
				        jsonBuilder.Append("\"username\": \"" + login + "\",");
				        jsonBuilder.Append("\"password\": \"" + mdp + "\"");
				        jsonBuilder.Append("}");

				        byte[] postData = Encoding.UTF8.GetBytes(jsonBuilder.ToString());

				        UnityWebRequest webRequest = new UnityWebRequest("http://localhost/srv_unity/login", UnityWebRequest.kHttpVerbPOST);
				        webRequest.downloadHandler = new DownloadHandlerBuffer();
				        webRequest.uploadHandler = new UploadHandlerRaw(postData);
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
					        
					        ServerResponse.LoginResponse response = JsonConvert.DeserializeObject<ServerResponse.LoginResponse>(jsonResponse);

					        bool success = response.success;

					        if (success == false)
					        {
						        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
						        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
						        localizedText.SetTranslationKey("identifiantIncorrect");
						        errorMessage.SetActive(true);
						        
						        webRequest.Dispose();
					        }
					        else
					        {
						        errorMessage.SetActive(false);
						        string token = response.token;
						        string username = response.username;
						        bool premium = response.prenium;
						        
						        if (response.badge != null && response.badge.Count > 0)
						        {
							        foreach (var badge in response.badge)
							        {
								        string badgeJson = JsonUtility.ToJson(badge);
								        PlayerPrefs.SetString(badge.name, badgeJson);
							        }
						        }
						        else
						        {
							        Debug.Log("Pas de badge disponible");
						        }
						        
						        PlayerPrefs.SetString("token",token);
						        PlayerPrefs.SetInt("premium",premium ? 1 : 0);
						        PlayerPrefs.SetString("username",username);
						        
						        togglePremium.isOn = premium;

						        bouttonLogin.GetComponentInChildren<Text>().text = username;
						        webRequest.Dispose();
						        Retour(canvaLogin); 
					        }
					        webRequest.Dispose();
				        }
			        }
			        else
			        {
				        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
				        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
				        localizedText.SetTranslationKey("internetErreur");
				        errorMessage.SetActive(true);
			        }
		        }
		        else
		        {
			        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
			        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
			        localizedText.SetTranslationKey("inputVide");
			        errorMessage.SetActive(true);
		        }
	        }
	        
	        private IEnumerator Register()
	        {
		        string login = identifiantFieldRegister.text;
		        string mdp = mdpFieldRegister.text;

		        if (!string.IsNullOrEmpty(login.Trim()) && !string.IsNullOrEmpty(mdp.Trim()))
		        {
			        if (Application.internetReachability != NetworkReachability.NotReachable)
			        {
				        StringBuilder jsonBuilder = new StringBuilder();
				        jsonBuilder.Append("{");
				        jsonBuilder.Append("\"username\": \"" + login + "\",");
				        jsonBuilder.Append("\"password\": \"" + mdp + "\"");
				        jsonBuilder.Append("}");

				        byte[] postData = Encoding.UTF8.GetBytes(jsonBuilder.ToString());

				        UnityWebRequest webRequest = new UnityWebRequest("http://localhost/srv_unity/users", UnityWebRequest.kHttpVerbPOST);
				        webRequest.downloadHandler = new DownloadHandlerBuffer();
				        webRequest.uploadHandler = new UploadHandlerRaw(postData);
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
					        
					        ServerResponse.RegisterResponse response = JsonUtility.FromJson<ServerResponse.RegisterResponse>(jsonResponse);

					        bool success = response.success;

					        if (success == false)
					        {
						        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
						        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
						        if (response.error == "taken")
							        localizedText.SetTranslationKey("usernameTaken");
						        else
							        localizedText.SetTranslationKey("erreurInscription");
					        }
					        else
					        {
						        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
						        errorMessage.GetComponent<Text>().color = new Color(0.54f, 0.9f, 0.39f, 0.255f);
						        localizedText.SetTranslationKey("successInscription");
						        
					        }
					        errorMessage.SetActive(true);
					        webRequest.Dispose();
				        }
			        }
			        else
			        {
				        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
				        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
				        localizedText.SetTranslationKey("internetErreur");
				        errorMessage.SetActive(true);
			        }
		        }
		        else
		        {
			        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
			        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
			        localizedText.SetTranslationKey("inputVide");
			        errorMessage.SetActive(true);
		        }
	        }
	        
	        private IEnumerator Logout()
	        {
		        if (Application.internetReachability != NetworkReachability.NotReachable)
		        {
						string token = PlayerPrefs.GetString("token");
				        
				        StringBuilder jsonBuilder = new StringBuilder();
				        jsonBuilder.Append("{");
				        jsonBuilder.Append("\"token\": \"" + token + "\"");
				        jsonBuilder.Append("}");

				        byte[] postData = Encoding.UTF8.GetBytes(jsonBuilder.ToString());

				        UnityWebRequest webRequest = new UnityWebRequest("http://localhost/srv_unity/logout", UnityWebRequest.kHttpVerbPOST);
				        webRequest.downloadHandler = new DownloadHandlerBuffer();
				        webRequest.uploadHandler = new UploadHandlerRaw(postData);
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
					        
					        ServerResponse.LogoutResponse response = JsonUtility.FromJson<ServerResponse.LogoutResponse>(jsonResponse);

					        bool success = response.success;

					        if (success == false)
					        {
						        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
						        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
						        localizedText.SetTranslationKey("erreurRequest");
						        errorMessage.SetActive(true);
						        
						        webRequest.Dispose();
					        }
					        else
					        {
						        errorMessage.SetActive(false);
						        
						        PlayerPrefs.SetString("token","");
						        PlayerPrefs.SetString("username","");
						        PlayerPrefs.SetInt("premium",0);
						        
						        foreach (var badge in badgeList)
						        {
							        PlayerPrefs.SetString(badge.name, "");
						        }

						        bouttonLogin.GetComponentInChildren<Text>().text = "";
						        webRequest.Dispose();
						        Retour(canvaUser); 
					        }
					        webRequest.Dispose();
				        }
		        }
		        else
		        {
			        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
			        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
			        localizedText.SetTranslationKey("internetErreur");
			        errorMessage.SetActive(true);
		        }
	        }
	        
	        private IEnumerator Premium(int premium)
	        {
		        if (Application.internetReachability != NetworkReachability.NotReachable)
		        {
						string token = PlayerPrefs.GetString("token");
						
				        StringBuilder jsonBuilder = new StringBuilder();
				        jsonBuilder.Append("{");
				        jsonBuilder.Append("\"premium\": \"" + premium + "\",");
				        jsonBuilder.Append("\"token\": \"" + token + "\"");
				        jsonBuilder.Append("}");

				        byte[] postData = Encoding.UTF8.GetBytes(jsonBuilder.ToString());

				        UnityWebRequest webRequest = new UnityWebRequest("http://localhost/srv_unity/premium", UnityWebRequest.kHttpVerbPOST);
				        webRequest.downloadHandler = new DownloadHandlerBuffer();
				        webRequest.uploadHandler = new UploadHandlerRaw(postData);
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
					        
					        ServerResponse.PremiumResponse response = JsonUtility.FromJson<ServerResponse.PremiumResponse>(jsonResponse);

					        bool success = response.success;

					        if (success == false)
					        {
						        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
						        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
						        localizedText.SetTranslationKey("errorRequest");
						        errorMessage.SetActive(true);
						        
						        webRequest.Dispose();
					        }
					        else
					        {
						        errorMessage.SetActive(false);
						        
						        PlayerPrefs.SetInt("premium",response.premium);
						        
						        if (response.isFisrt)
						        {
							        StartCoroutine(updateBadge("badge_premium"));
						        }
						        
						        if (PlayerPrefs.GetInt("premium") == 1)
						        {
							        PremiumMessage.SetActive(false);
						        }
						        else
						        {
							        PremiumMessage.SetActive(true);
						        }
						        webRequest.Dispose();
					        }
					        webRequest.Dispose();
				        }
		        }
		        else
		        {
			        errorMessage.GetComponent<Text>().color = new Color(0.89f, 0.09f, 0.09f, 1f);
			        LocalizedText localizedText = errorMessage.GetComponent<LocalizedText>();
			        localizedText.SetTranslationKey("internetErreur");
			        errorMessage.SetActive(true);
		        }
	        }
	        
	        private IEnumerator LikePub()
	        {
		        if (Application.internetReachability != NetworkReachability.NotReachable)
		        {
			        string token = PlayerPrefs.GetString("token");
			        LoadPub pubLoader = pubIntertitiel.GetComponent<LoadPub>();
			        ServerResponse.PubResponse pubResponse = pubLoader.GetPubResponse();
			        
			        StringBuilder jsonBuilder = new StringBuilder();
			        jsonBuilder.Append("{");
			        jsonBuilder.Append("\"themeID\": \"" + pubResponse.themeID + "\"");
			        jsonBuilder.Append("}");

			        byte[] postData = Encoding.UTF8.GetBytes(jsonBuilder.ToString());

			        UnityWebRequest webRequest = new UnityWebRequest("http://localhost/srv_unity/like", UnityWebRequest.kHttpVerbPOST);
			        webRequest.downloadHandler = new DownloadHandlerBuffer();
			        webRequest.uploadHandler = new UploadHandlerRaw(postData);
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
				        ServerResponse.LikeThemeResponse likePubRep = JsonUtility.FromJson<ServerResponse.LikeThemeResponse>(jsonResponse);

				        Image buttonImage = bouttonLike.GetComponent<Image>();
				        if (likePubRep.like)
				        {
					        buttonImage.sprite = Resources.Load<Sprite>("Images/hearth");
					        
					        if (PlayerPrefs.GetString("badge_like") == "")
					        {
						        GameObject badge = badgeList.FirstOrDefault(b => b.name == "badge_like");

						        if (badge != null)
						        {
							        RawImage imageBadge = badge.GetComponentInChildren<RawImage>();
							        Color imageColor = imageBadge.color;
							        imageColor.a = 1f;
							        imageBadge.color = imageColor;
							        PlayerPrefs.SetString("badge_like","badge_like");
						        }
					        }
				        }
				        else
					        buttonImage.sprite = Resources.Load<Sprite>("Images/hearth_noFill");
				        
				        webRequest.Dispose();
			        }
		        }
	        }
	        
	        private IEnumerator UpdateNbPub()
	        {
		        if (Application.internetReachability != NetworkReachability.NotReachable)
		        {
			        string token = PlayerPrefs.GetString("token");

			        UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost/srv_unity/updateNbPub");
			        webRequest.downloadHandler = new DownloadHandlerBuffer();
			        webRequest.SetRequestHeader("Content-Type", "application/json");
			        webRequest.SetRequestHeader("token", token);

			        yield return webRequest.SendWebRequest();

			        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
			        {
				        Debug.Log("Erreur lors de la requête : " + webRequest.error);
				        
			        }else
			        {
				        string jsonResponse = webRequest.downloadHandler.text;
					        
				        ServerResponse.NbPubResponse nbPubResponse = JsonUtility.FromJson<ServerResponse.NbPubResponse>(jsonResponse);

				        switch (nbPubResponse.nbPub)
				        {
					        case 1:
						        if (PlayerPrefs.GetString("badge_1er_pub") == "")
						        {
							        StartCoroutine(updateBadge("badge_1er_pub"));
						        }
						        break;
					        case 5:
						        if (PlayerPrefs.GetString("badge_5eme_pub") == "")
						        {
							        StartCoroutine(updateBadge("badge_5eme_pub"));
						        }
						        break;
					        case 10:
						        if (PlayerPrefs.GetString("badge_10eme_pub") == "")
						        {
							        StartCoroutine(updateBadge("badge_10eme_pub"));
						        }
						        break;
				        }
				        
				        webRequest.Dispose();
			        }
		        }
	        }
	        
	        private IEnumerator updateBadge(string nameBadge)
	        {
		        if (Application.internetReachability != NetworkReachability.NotReachable)
		        {
			        string token = PlayerPrefs.GetString("token");
			        
			        StringBuilder jsonBuilder = new StringBuilder();
			        jsonBuilder.Append("{");
			        jsonBuilder.Append("\"nameBadge\": \"" + nameBadge + "\"");
			        jsonBuilder.Append("}");

			        byte[] postData = Encoding.UTF8.GetBytes(jsonBuilder.ToString());

			        UnityWebRequest webRequest = new UnityWebRequest("http://localhost/srv_unity/updateBadge", UnityWebRequest.kHttpVerbPOST);
			        webRequest.downloadHandler = new DownloadHandlerBuffer();
			        webRequest.uploadHandler = new UploadHandlerRaw(postData);
			        webRequest.SetRequestHeader("Content-Type", "application/json");
			        webRequest.SetRequestHeader("token", token);

			        yield return webRequest.SendWebRequest();

			        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
			        {
				        Debug.Log("Erreur lors de la requête : " + webRequest.error);
				        
			        }else
			        {
				        string jsonResponse = webRequest.downloadHandler.text;
					        
				        ServerResponse.BadgeResponse nbPubResponse = JsonUtility.FromJson<ServerResponse.BadgeResponse>(jsonResponse);
				        Debug.Log("repBadge : "+nbPubResponse.success);

				        GameObject badge = badgeList.FirstOrDefault(b => b.name == nameBadge);

				        if (badge != null)
				        {
					        RawImage imageBadge = badge.GetComponentInChildren<RawImage>();
					        Color imageColor = imageBadge.color;
					        imageColor.a = 1f;
					        imageBadge.color = imageColor;
					        PlayerPrefs.SetString(nameBadge,nameBadge);
				        }
				        else
				        {
					        Debug.LogError("Badge not found: " + nameBadge);
				        }
				        

				        webRequest.Dispose();
			        }
		        }
	        }
	        public void CallRequest(int request)
	        {
		        if (loginCoroutineRunning)
		        {
			        Debug.Log("La coroutine Login est déjà en cours d'exécution.");
			        return;
		        }

		        switch (request)
		        {
			        case 1:
				        StartCoroutine(Login());
				        break;
			        case 2:
				        StartCoroutine(Register());
				        break;
			        
			        case 3:
				        StartCoroutine(Logout());
				        break;
			        
			        case 4:
				        int toggleValue = togglePremium.isOn ? 1 : 0;
				        StartCoroutine(Premium(toggleValue));
				        break;
			        
			        case 5:
				        StartCoroutine(LikePub());
				        break;
		        }
	        }
	        
	        void OnApplicationQuit()
	        {
		        PlayerPrefs.SetString("token","");
		        foreach (var badge in badgeList)
		        {
			        PlayerPrefs.SetString(badge.name, "");
		        }
	        }
	}




