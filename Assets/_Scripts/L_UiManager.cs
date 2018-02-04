using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L_UiManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TwinDeadButton()
    {
        string choice = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(choice);
    }

    public void RunButton()
    {
        string choice = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(choice);
        if(choice == "Run")
        {

        }
        else
        {

        }
    }
}
