using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    
    public Camera CamFirstPerson;
    public Camera CamThirdPerson;
	public Camera CamBack;
    public bool CamEtat;

    public void ShowThirdPersonView() {
        CamFirstPerson.enabled = false;
        CamThirdPerson.enabled = true;
        CamEtat = false;
    }
    public void ShowFirstPersonView() {
        CamFirstPerson.enabled = true;
        CamThirdPerson.enabled = false;
        CamEtat = true;
    }

	public void ShowBackView() {
        CamBack.enabled = true;
		CamFirstPerson.enabled = false;
        CamThirdPerson.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowThirdPersonView();
    }

    // Update is called once per frame
    void Update()
    {
 
		if (Input.GetKey(KeyCode.A))
		{
			ShowBackView();
        }else {
            if (CamEtat == false)
            {
                ShowThirdPersonView();
            }else
            {
                ShowFirstPersonView();
            }
		}

		if (Input.GetKeyDown(KeyCode.E))
        {
            if (CamEtat == true)
            {
                ShowThirdPersonView();
            }else
            {
                ShowFirstPersonView();
            }
        }
	}
}
