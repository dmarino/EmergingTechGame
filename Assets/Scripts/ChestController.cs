using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestController : MonoBehaviour
{

    [SerializeField]
    private string name;

    public void tryOpen(string word){

        Debug.Log(word);
        if(word==name){
             Destroy(gameObject);
        }
    }
}
