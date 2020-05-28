using System.Collections;
using UnityEngine;

public class GameController: MonoBehaviour
{

     //singleton of the gameManager
     public static GameController instance;


     //manages the number of chest in the level and the current score
     [SerializeField]
     private int numberOfChest=1;

     private int score=0;


     //the current status of the game
     private Status status;


     //manages the time of the level
     [SerializeField]
     private int minutesToPlay=1;
     [SerializeField]
     private int secondsToPlay=30;


     //maganes the ui 
     [SerializeField] GameObject lostScreen;
     [SerializeField] GameObject winScreen;

    private void Awake ()
    {
        instance = this;
    }
     private void Start()
     {
          StartCoroutine(CountDown());
          status = Status.playing;

     }

     private void Update()
     {
          Debug.Log(score);
          if(score == numberOfChest){
               Debug.Log("aaaaa");
               status = Status.over;
               winScreen.SetActive(true);
          }         
     }


     public void openChest(){
          score++;
     }


     IEnumerator CountDown(){

          int totalSeconds= minutesToPlay*60 + secondsToPlay;

          for(int i= totalSeconds; i>0 && status!= Status.over; i--){
               
               yield return new WaitForSeconds (1);
          }

          if(status!= Status.over){

               status = Status.over;
               lostScreen.SetActive(true);
          }

     }
}
