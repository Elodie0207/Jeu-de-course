using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject bullet;
    
    public  float launchSpeed = 100f;

    private int munition = 3;
    
    public void Shoot()
    {

        while (munition != 0)
        {


            if (Input.GetKeyDown((KeyCode.Space)))
            {

                GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

                //newBullet.GetComponent<Rigidbody>().v= 

                munition -= 1;




            }

        }

    }
}