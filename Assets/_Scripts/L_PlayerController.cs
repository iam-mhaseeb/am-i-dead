using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_PlayerController : MonoBehaviour {

    [Header("Level#01 Vars")]

    [SerializeField]
    private string[] dialogues;

    [SerializeField]
    private float TotalTimeToReachTarget;

    [SerializeField]
    private Vector3 DestinationRotation;

    [SerializeField]
    private L_DialogueManager dialogueManagerObj;

    private Vector3 StartRotation;

    private float movementSpeed;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public void LevelOneControls()
	{
        StartRotation = this.transform.rotation.eulerAngles;
        InvokeRepeating("RotateToRight", 0, 0.1f);
	}

	private void RotateToRight()
	{
        movementSpeed += Time.deltaTime / TotalTimeToReachTarget;
        Vector3 tempRot = Vector3.Lerp(StartRotation, DestinationRotation, movementSpeed);
        this.transform.rotation = Quaternion.Euler(tempRot);

        if(this.transform.rotation.eulerAngles == DestinationRotation)
        {
            CancelInvoke("RotateToRight");
            dialogueManagerObj.ResumeDialogues();
        }
    }
}