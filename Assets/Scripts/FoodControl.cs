using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodControl : MonoBehaviour // control the food
{
    public float index;
    void Start() // start the game
    {
        
    }
    private void OnTriggerEnter(Collider other) // on trigger enter
    {
        if (other.tag == "Player")
        {
            GameObject.Find("PlayerManager").GetComponent<PlayerAction>().RecoverHealth(index); // recover health
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update() // update the game    
    {
        
    }
}
