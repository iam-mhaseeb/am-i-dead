using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	[Header("List of Prefabs for Activity")]
	public GameObject [] activityPrfabs;
	[Header("Transition Screen")]
	public GameObject transitionScreen;
	private int activityIterator;


	//Static Variables
	public static bool isPaused = false;
	public static bool isPlay = false;
	public static bool isCurrActivityDone=false;
	public static bool isGameWon = false;
	public static bool isGameLoose = false;

	static GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = this;
		activityIterator = 0;
		Time.timeScale = 1.0f;
		transitionScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static void PauseGame(){
		isPaused = true;
		isPlay=false;
		Time.timeScale = 0.0f;//It does not stop everything. Use game level pause state
	}
	public static void ResumeGame(){
		isPaused=false;
		isPlay=true;
		Time.timeScale = 1.0f;//Again use scene level states to resume the game
	}
	public static void CurrActivityDone(){
		isCurrActivityDone = true;
		gameManager.InstantiateNextActivity();//Trigger Next Activity
	}
	private void InstantiateNextActivity(){
		/*TODO: Combinations of Prefabs is yet to be decided yet */
		Destroy(activityPrfabs[activityIterator]);//Destory Current Activity
		IncrementIterator();//Increment Iterator
		StartCoroutine(ShowTransitionScreen());//Show Transition Screen
		Instantiate(activityPrfabs[activityIterator]);//Instantiate Next Activity

	}
	private void IncrementIterator(){
		if(activityIterator<activityPrfabs.Length){
			activityIterator++;
		}else{
			 GameWon();//Completed all levels
		}
	}

	public static void GameWon(){
		if(!isGameWon){
			isGameWon = true;
		}
	}

	public static void GameLoose(){
		if(!isGameLoose){
			isGameLoose = true;
		}
	}


	IEnumerator  ShowTransitionScreen(){
		transitionScreen.SetActive(true);
		yield return new WaitForSeconds(2.0f);
		transitionScreen.SetActive(false);
	}




	
}
