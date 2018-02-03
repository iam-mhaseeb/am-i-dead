using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEngineManger : MonoBehaviour {

	[Header("Grounds")]
	public GameObject [] groundObjects;
	[Header("Initial Position of Ground Instantion")]
	public GameObject initialGroundPoitionObject;
	public float transitionDuration = 2.5f;
	public bool isNextGround=false;

	// Use this for initialization
	void Start () {
		InstantiateGrounds();//Instantiate Grounds
	}
	
	// Update is called once per frame
	void Update () {
	}


	private void InstantiateGrounds(){
		int oldJ=0;
		Instantiate(groundObjects[0],initialGroundPoitionObject.transform.position,initialGroundPoitionObject.transform.rotation);//Always Instantiate First as it is
		UpdatePositions();
		for(int i=0;i<10;i++){
			int j = Random.Range(1,4);
			if(oldJ==j){
				i--;
			}else{
				Instantiate(groundObjects[j],initialGroundPoitionObject.transform.position,initialGroundPoitionObject.transform.rotation);//Randomize 0,1,3 Grounds
				UpdatePositions();
				oldJ = j;
			}
		}
		Instantiate(groundObjects[5],initialGroundPoitionObject.transform.position,initialGroundPoitionObject.transform.rotation);//Instantiate 2 as it is
		UpdatePositions();
		Instantiate(groundObjects[6],initialGroundPoitionObject.transform.position,initialGroundPoitionObject.transform.rotation);//Instantiate 3 as it is
		UpdatePositions();
		Instantiate(groundObjects[7],initialGroundPoitionObject.transform.position,initialGroundPoitionObject.transform.rotation);//Instantiate 4 as it is
		UpdatePositions();
	}

	private void UpdatePositions(){
		initialGroundPoitionObject.transform.position = new Vector3(initialGroundPoitionObject.transform.position.x+22,initialGroundPoitionObject.transform.position.y,initialGroundPoitionObject.transform.position.z);
	}

}
