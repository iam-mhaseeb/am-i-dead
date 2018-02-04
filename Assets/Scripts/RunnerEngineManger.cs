using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunnerEngineManger : MonoBehaviour {


	public float minimum = 0.0f;
	public float maximum = 1.0f;
	public float duration;
	public bool fadeIn,fadeOut;
	public SpriteRenderer sprite;

	public GameObject hider;

	private bool isStairsDone;

	public bool isMoveCamera = true;


	private static RunnerEngineManger _instance;

    public static RunnerEngineManger Instance 
    { 
        get { return _instance; } 
    } 

	void Awake(){
		if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeIn) {
			duration += 0.8f * Time.deltaTime;
			float t = Mathf.Lerp(minimum, maximum, duration);
			if(t<1){
				fadeOut = true;
			}
            sprite.color = new Color(0f,0f,0f,Mathf.SmoothStep(minimum, maximum, t)); 
		}else if(fadeOut) {
			fadeIn = false;
			duration -= 0.8f * Time.deltaTime;
			float t = Mathf.Lerp(maximum, minimum, duration);
			if(t<0){
				fadeOut = false;
			}
            sprite.color = new Color(0f,0f,0f,Mathf.SmoothStep(maximum, minimum, t));
			isStairsDone = true; 
		}
	}

	
}
