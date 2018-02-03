using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class L_PlayerMovementController : MonoBehaviour {

    [SerializeField]
    private float speed;

    [SerializeField]
    private Avatar RunningAvatr;

    private Avatar IdleAvatar;

    private Animator animatorObj;

    private Vector3 newPos;
    private Vector3 cameraOffset;

    private bool isMoving;

	// Use this for initialization
	void Start ()
    {
        animatorObj = this.GetComponent<Animator>();
        cameraOffset = Camera.main.transform.position;
        cameraOffset = transform.position - Camera.main.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isMoving)
        {
            newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //speed *= Time.deltaTime;
            newPos.z += (speed * Time.deltaTime);
            transform.position = newPos;
            Vector3 camNewPos = Camera.main.transform.position;
            camNewPos.z = transform.position.z - cameraOffset.z;
            Camera.main.transform.position = camNewPos;
        }
	}

    public void MoveButtonDown()
    {
        IdleAvatar = GetComponent<Animator>().avatar;
        GetComponent<Animator>().avatar = RunningAvatr;
        animatorObj.SetTrigger("Run");
        isMoving = true;
    }

    public void MoveButtonUp()
    {
        GetComponent<Animator>().avatar = IdleAvatar;
        transform.position = newPos;
        animatorObj.SetTrigger("Stop");
        isMoving = false;
    }
}