

using UnityEngine;

public class ColisionTest : MonoBehaviour
{
  
    private void OnCollisionEnter(Collision collision)
    {
         if(collision.gameObject.CompareTag("Obstacle")){

             Debug.Log("Collision with " + collision.gameObject.tag);

         }


    }
}
 
