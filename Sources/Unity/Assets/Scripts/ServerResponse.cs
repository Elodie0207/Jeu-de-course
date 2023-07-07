using System.Collections.Generic;

[System.Serializable]
public class ServerResponse
{
    public class LoginResponse
    {
        public bool success;
        public string token;
        public string username;
        public bool prenium;
        public List<Badge> badge;
    }
    
    public class Badge
    {
        public string id;
        public string name;
        public string prix;
        public string titre;
        public string description;
    }
    
    public class RegisterResponse
    {
        public bool success;
        public string error;
    }
    
    public class LogoutResponse
    {
        public bool success;
    }
    
    public class PremiumResponse
    {
        public bool success;
        public int premium;
        public bool isFisrt;
    }
    
    public class PubResponse
    {
        public string id;
        public string themeID;
        public string websiteLink;
        public string path;
        public bool like;
    }
    
    public class LikeThemeResponse
    {
        public bool success;
        public bool like;
        public int nbLike;
        public bool isFisrt;
    }
    
    public class NbPubResponse
    {
        public bool success;
        public int nbPub;
    }
    
    public class BadgeResponse
    {
        public bool success;
        public string error;
    }
}
