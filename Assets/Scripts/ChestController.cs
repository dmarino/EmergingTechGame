using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestController : MonoBehaviour
{

    [SerializeField]
    private string chestName;

    public void tryOpen(string word){

        if(word==chestName){

            GameController.instance.openChest();
             Destroy(gameObject);
        }
    }
}
