using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Musique : MonoBehaviour
{
  public Slider music; 
    
  void Start()
  {
      if (!PlayerPrefs.HasKey("Volume"))
      {
          PlayerPrefs.SetFloat("Volume", 1);
          Load();
      }
      else
      {
          Load();
      }

      DontDestroyOnLoad(gameObject);
  }

    public void SetVolume()
    {
        AudioListener.volume = music.value;
        Save();
    }

    public void Load()
    {
        music.value = PlayerPrefs.GetFloat("Volume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", music.value);
    }
}
