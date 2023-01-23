using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private float speed = 5.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void Shoot()
    {
        
        if (Input.GetKeyDown((KeyCode.Space)))
        {

            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            
            
        }
        
    }
}
