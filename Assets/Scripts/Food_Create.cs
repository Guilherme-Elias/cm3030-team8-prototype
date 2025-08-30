using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Create : MonoBehaviour // create food
{
    public GameObject Food1, Food2;
    public GameObject[] Foodpos;
    void Start() // start the game
    {
        InvokeRepeating("Acreate", 5, 5);
    }
    public void Acreate() // create food
    {
        if (GameObject.FindGameObjectsWithTag("Food").Length < 2)
        {
            int index = Random.Range(0, 10); // random index
            if (index < 5)
            {
              
                for (int i = 0; i < Foodpos.Length; i++) // loop through the food positions
                {
                    if (Foodpos[i].transform.childCount < 1)
                    {
                        GameObject obj = Instantiate(Food1); // instantiate the food
                        obj.transform.SetParent(Foodpos[i].transform);
                        obj.SetActive(true);
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Foodpos.Length; i++) // loop through the food positions
                {
                    if (Foodpos[i].transform.childCount < 1)
                    {
                        GameObject obj = Instantiate(Food2); // instantiate the food
                        obj.transform.SetParent(Foodpos[i].transform);
                        obj.SetActive(true);
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    }
                }
            }
          
        }
    }
    void Update() // update the game
    {
        
    }
}
