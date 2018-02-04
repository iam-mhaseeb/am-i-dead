using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_PlayerController : MonoBehaviour {

    [SerializeField]
    private L_DialogueManager dialogueManagerObj;

    private Vector3 StartRotation;

    private float movementSpeed = 50;

    private bool isRotating = false;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isRotating)
        {
            RotateToRight();
        }
	}

	public void LevelOneControls()
	{
        StartRotation = new Vector3(-90, -90, 90);
        Debug.Log(StartRotation);
        isRotating = true;
    }

	private void RotateToRight()
	{
        StartRotation.x -= (movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(StartRotation);
        
        if (this.transform.rotation.eulerAngles.x <= 180)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            isRotating = false;
            StopCoroutine("WaitToRecogniseOtherPerson");
            StartCoroutine("WaitToRecogniseOtherPerson");
        }
    }

    IEnumerator WaitToRecogniseOtherPerson()
    {
        yield return new WaitForSeconds(2);
        dialogueManagerObj.ResumeDialogues();
    }
}