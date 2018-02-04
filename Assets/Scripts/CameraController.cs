﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	// Use this for initialization
	void Start () {
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if(RunnerEngineManger.Instance.isMoveCamera){
			transform.position = new Vector3(player.transform.position.x + offset.x,transform.position.y,transform.position.z);
		}
    }
}
