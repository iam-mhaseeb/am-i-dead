using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour {
    private Vector2 initTouchPos;
	int fingerCount = 0;
    void Start()
    {

    }
 
    void Update()
    {
        InputMovement();
    }
	void InputMovement(){
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				initTouchPos = touch.position;
				fingerCount =1;
			}
			if (fingerCount == 1 && touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended) {
				Vector2 touchFacing = (initTouchPos - touch.position).normalized;
				if (Vector2.Dot (touchFacing, Vector2.up) > 0.8f && Vector2.Distance (initTouchPos, touch.position) > 10) {
					if(fingerCount == 1){
                        //Up swipe
                        if(!RunnerPlayerMovement.Instance.isDie){
                            RunnerPlayerMovement.Instance.MakeSlide();
                        }
                    }
					fingerCount = 0;
				}
				if (Vector2.Dot (touchFacing, -Vector2.up) > 0.8f && Vector2.Distance (initTouchPos, touch.position) > 10) {
					if(fingerCount == 1){
                        //Up swipe
                        if(!RunnerPlayerMovement.Instance.isDie){
                            RunnerPlayerMovement.Instance.JumpInput();
                        }
                    }
					fingerCount = 0;
				}
				if (Vector2.Dot (touchFacing, Vector2.right) > 0.8f && Vector2.Distance (initTouchPos, touch.position) > 10) {
					if(fingerCount == 1) //Left
					fingerCount = 0;
				}
				if (Vector2.Dot (touchFacing, -Vector2.right) > 0.8f && Vector2.Distance (initTouchPos, touch.position) > 10) {

					if(fingerCount == 1) //Right
					fingerCount = 0;
				}
			}
		}
    }
}


