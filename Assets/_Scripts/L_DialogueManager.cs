using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class L_DialogueManager : MonoBehaviour {

    [SerializeField]
    private L_PlayerController playerControllerObject;

    [SerializeField]
    private Animator twinAnimator;

    [SerializeField]
    private Animator EnemyAnimator;

    [Header("Dialogues Data")]

    [SerializeField]
    private string[] dialogues;

    [SerializeField]
    private Text dialogueField;

    [SerializeField]
    private GameObject[] InputButtons;

    [SerializeField]
    private GameObject[] InputButtonsFireLevel;

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
        dialogueField.color = new Color(0, 0, 0, dialogueFadeSpeed);
        
        if(dialogueField.color.a >= 1)
        {
            CancelInvoke("DialoguesFadeIn");
            StartCoroutine("DialogueStayTime");

            if (dialogueIndex == 1 && GameManager.levelNumber == 1)
            {
                twinAnimator.Play("talk");
            }

            if(GameManager.levelNumber == 1 && dialogueIndex + 1 != dialogues.Length)
            {
                StartCoroutine("Wait");
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        GameManager.GoToNextLevel(2);
    }

    IEnumerator DialogueStayTime()
    {
        yield return new WaitForSeconds(stayTime);
       
        if (dialogueIndex + 1 != dialogues.Length)
        {
            dialogueFadeSpeed = 1;
            InvokeRepeating("DialoguesFadeOut", 0, 0.1f);
        }
        else if(GameManager.levelNumber != 2)
        {
            InputButtons[0].SetActive(true);
            InputButtons[1].SetActive(true);
        }
        else if(GameManager.levelNumber == 2)
        {
            Debug.Log("G");
            InputButtonsFireLevel[0].SetActive(true);
            InputButtonsFireLevel[1].SetActive(true);
        }
        else
        {
            dialogueFadeSpeed = 1;
            InvokeRepeating("DialoguesFadeOut", 0, 0.1f);
        }
    }

    private void DialoguesFadeOut()
    {
        dialogueFadeSpeed -= 0.05f;
        dialogueField.color = new Color(0, 0, 0, dialogueFadeSpeed);
        
        if (dialogueField.color.a <= 0)
        {
            CancelInvoke("DialoguesFadeOut");
            if (dialogueIndex + 1 != dialogues.Length)
            {
                dialogueIndex++;
                
                if (dialogueIndex == 3 && GameManager.levelNumber == 0)
                {
                    playerControllerObject.LevelOneControls();
                }
                else if(dialogueIndex == 3 && GameManager.levelNumber == 2)
                {
                    //GameManager.GoToNextLevel(3);
                    //Debug.Log("Go: Level 2 to 3");
                    dialogueFadeSpeed = 0;
                    InvokeRepeating("DialoguesFadeIn", 0, 0.1f);
                    EnemyAnimator.Play("fire");
                }
                else if (dialogueIndex == 3 && GameManager.levelNumber == 1)
                {
                    GameManager.GoToNextLevel(2);
                    dialogueIndex=0;
                }
                else
                {
                    dialogueFadeSpeed = 0;
                    InvokeRepeating("DialoguesFadeIn", 0, 0.1f);
                }

                dialogueField.text = this.dialogues[dialogueIndex];
            }
        }
    }

    public void TwinDeadButton()
    {
        string choice = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(choice);

        if (choice.Contains("Twin"))
        {
            GameManager.GoToNextLevel(1);
            GameManager.done = false;
        }
        else
        {
            GameManager.GoToNextLevel(2);
            GameManager.done = false;
        }
    }
}