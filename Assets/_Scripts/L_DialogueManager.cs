using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L_DialogueManager : MonoBehaviour {

    [SerializeField]
    private L_PlayerController playerControllerObject;

    [Header("Dialogues Data")]

    [SerializeField]
    private string[] dialogues;

    [SerializeField]
    private Text dialogueField;

    [SerializeField]
    private GameObject[] InputButtons;
    
    private float dialogueFadeSpeed = 0;
    private float dialogueStartTime;
    private float stayTime = 2;

    private int dialogueIndex;

	// Use this for initialization
	void Start ()
    {
        StartDialogues();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDialogues()
    {
        dialogueField.gameObject.SetActive(true);
        dialogueField.text = this.dialogues[dialogueIndex];

        InvokeRepeating("DialoguesFadeIn", 0, 0.1f);
    }

    public void ResumeDialogues()
    {
        dialogueFadeSpeed = 0;
        InvokeRepeating("DialoguesFadeIn", 0, 0.1f);
    }

    private void DialoguesFadeIn()
    {
        dialogueFadeSpeed += 0.05f;
        dialogueField.color = new Color(255, 255, 255, dialogueFadeSpeed);
        
        if(dialogueField.color.a >= 1)
        {
            CancelInvoke("DialoguesFadeIn");
            StartCoroutine("DialogueStayTime");
        }
    }

    IEnumerator DialogueStayTime()
    {
        yield return new WaitForSeconds(stayTime);
        if (dialogueIndex + 1 != dialogues.Length)
        {
            dialogueFadeSpeed = 1;
            InvokeRepeating("DialoguesFadeOut", 0, 0.1f);
        }
        else
        {
            InputButtons[0].SetActive(true);
            InputButtons[1].SetActive(true);
        }
    }

    private void DialoguesFadeOut()
    {
        dialogueFadeSpeed -= 0.05f;
        dialogueField.color = new Color(255, 255, 255, dialogueFadeSpeed);
        
        if (dialogueField.color.a <= 0)
        {
            CancelInvoke("DialoguesFadeOut");
            if (dialogueIndex + 1 != dialogues.Length)
            {
                dialogueIndex++;
                dialogueField.text = this.dialogues[dialogueIndex];

                if (dialogueIndex == 4 && GameManager.levelNumber == 1)
                {
                    playerControllerObject.LevelOneControls();
                }
                else if(dialogueIndex == 4 && GameManager.levelNumber == 3)
                {
                    
                }
                else
                {
                    dialogueFadeSpeed = 0;
                    InvokeRepeating("DialoguesFadeIn", 0, 0.1f);
                }
            }
        }
    }
}