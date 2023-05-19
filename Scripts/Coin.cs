using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(50 * Time.deltaTime, 0, 0);
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.numberOfCoins += 1;
            Destroy(gameObject);
            Debug.Log("Coins:" + PlayerManager.numberOfCoins);
            FindObjectOfType<AudioManager>().PlaySound("coin sound");
            
        }
        
    }
   


}
