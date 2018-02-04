using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L_PlayerMovementController : MonoBehaviour {

    [SerializeField]
    private GameObject MainCanvas;

    [SerializeField]
    private GameObject DialoguesCanvas;

    [SerializeField]
    private float speed;

    private Animator animatorObj;

    private Vector3 newPos;
    private Vector3 cameraOffset;

    private bool isMoving;

    private bool lockInputs = false;

    private float turningAnimLength;

    // Use this for initialization
    void Start ()
    {
        animatorObj = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
        cameraOffset = Camera.main.transform.position;
        cameraOffset = transform.position - Camera.main.transform.position;
        newPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isMoving)
        {
            if (animatorObj.rootRotation.y <= 0)
            {
                newPos = transform.position;
                newPos.z += (speed * Time.deltaTime);
                transform.position = newPos;
                Vector3 camNewPos = Camera.main.transform.position;
                camNewPos.z = transform.position.z - cameraOffset.z;
                Camera.main.transform.position = camNewPos;
            }
            else
            {
                newPos = transform.position;
                newPos.x += (speed * Time.deltaTime);
                transform.position = newPos;
                Vector3 camNewPos = Camera.main.transform.position;
                camNewPos.x = transform.position.x - cameraOffset.x;
                Camera.main.transform.position = camNewPos;
            }
        }

        //if(transform.position.y == 90 && lockInputs)
        //{
        //    lockInputs = false;
        //}

        if(turningAnimLength > 0)
        {
            turningAnimLength -= Time.deltaTime;
        }
        else
        {
            if(lockInputs)
            {
                lockInputs = false;
                //transform.position = animatorObj.rootPosition;
                //transform.rotation = animatorObj.rootRotation;
            }
        }
	}

    public void MoveButtonDown()
    {
        if (!lockInputs)
        {
            animatorObj.applyRootMotion = false;
            animatorObj.Play("run");
            isMoving = true;
        }
    }

    public void MoveButtonUp()
    {
        if (!lockInputs)
        {
            animatorObj.Play("Idle");
            isMoving = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Turn Right")
        {
            isMoving = false;
            lockInputs = true;
            animatorObj.Play("turn_right");
            animatorObj.applyRootMotion = true;
            turningAnimLength = 1;
        }
        else if(other.gameObject.name == "Stop")
        {
            isMoving = false;
            lockInputs = true;
            animatorObj.Play("Idle");

            MainCanvas.SetActive(false);
            DialoguesCanvas.SetActive(true);
        }
    }
}