using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int levelNumber = 0;
    
	private static GameManager thisInstance;

    [SerializeField]
    private GameObject loadingScreen;

    [SerializeField]
    private GameObject[] levelsArray;

    private int levelsIndex = 0;

    public static bool done = false;

	// Use this for initialization
	void Start ()
    {
        thisInstance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void GoToNextLevel(int val)
    {
        //if(!done)
        {
            Debug.Log("Next LEvel");
            thisInstance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            thisInstance.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            thisInstance.levelsArray[thisInstance.levelsIndex].SetActive(false);
            thisInstance.levelsIndex = val;
            thisInstance.loadingScreen.SetActive(true);
            thisInstance.SceneChangeTransition();
        }
    }

    void SceneChangeTransition()
    {
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        levelsArray[levelsIndex].SetActive(true);
        loadingScreen.SetActive(false);

        thisInstance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        thisInstance.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        //levelNumber++;
        //done = true;
    }
}