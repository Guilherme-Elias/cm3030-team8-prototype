using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Create : MonoBehaviour
{
    public GameObject Food1, Food2;
    public GameObject[] Foodpos;
    void Start()
    {
        InvokeRepeating("Acreate", 5, 5);
    }
    public void Acreate()
    {
        if (GameObject.FindGameObjectsWithTag("Food").Length < 2)
        {
            int index = Random.Range(0, 10);
            if (index < 5)
            {
              
                for (int i = 0; i < Foodpos.Length; i++)
                {
                    if (Foodpos[i].transform.childCount < 1)
                    {
                        GameObject obj = Instantiate(Food1);
                        obj.transform.SetParent(Foodpos[i].transform);
                        obj.SetActive(true);
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Foodpos.Length; i++)
                {
                    if (Foodpos[i].transform.childCount < 1)
                    {
                        GameObject obj = Instantiate(Food2);
                        obj.transform.SetParent(Foodpos[i].transform);
                        obj.SetActive(true);
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        break;
                    }
                }
            }
          
        }
    }
    void Update()
    {
        
    }
}
