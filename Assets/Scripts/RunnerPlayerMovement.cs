using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayerMovement : MonoBehaviour {
	float jumpInputTime;
	bool onGround;
	public float jumpForce;

	public bool canRun;
	public bool isDie;
	public float moveSpeed;
	public float sliding;
	//components
	public Rigidbody rb;
	public Animator anim;
	public CapsuleCollider cc;

//	float initYPos;
	void Start () {
		StartRun ();
//		initYPos = transform.position.y;
	}

	void Update () {
		//jumping
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			JumpInput ();
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			MakeSlide ();
		}
		if (jumpInputTime > 0) {
			jumpInputTime -= Time.deltaTime;
		}
		if (jumpInputTime > 0 && onGround && !isDie) {
			
			MakeJump ();
		}

		//end jumping
		if(!onGround){
			rb.AddForce (-Vector2.up * (jumpForce * 2));
		}

		if (sliding > 0) {
			cc.height = 1;
			sliding -= Time.deltaTime;
		} else {
			cc.height = 2;
		}

		Move ();
	}

	public void StartRun (){
		canRun = true;
		anim.Play ("Run");
	}
	void Move(){
		if(canRun && !isDie){
			transform.position += new Vector3 (moveSpeed * Time.deltaTime, 0, 0);
		}
	}

	public void JumpInput(){
		jumpInputTime = 0.3f;
	}
	void MakeJump(){
		jumpInputTime = 0;
		sliding = 0;
		anim.Play ("Jump");
		rb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
	}

	public void MakeSlide(){
		if (sliding < 0.2f) {
			anim.Rebind ();
			anim.Play ("Slide");

			sliding = 0.8f;
			if (!onGround) {
				ForDownForSlide ();
			}
		}
	}
	void ForDownForSlide(){
		rb.AddForce (-Vector3.up * jumpForce, ForceMode.Impulse);
	}
	void Die(){
		isDie = true;
		canRun = false;
		rb.velocity = Vector3.zero;
//		transform.position = new Vector3 (transform.position.x, initYPos, transform.position.z);
		anim.Rebind ();
		anim.Play ("Die");
	}
	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag == "Ground") {
			onGround = true;
		}
		if (c.gameObject.tag == "Obstacle") {
			Die ();
		}
	}

	void OnCollisionExit(Collision c){
		if (c.gameObject.tag == "Ground") {
			onGround = false;
		}
	}
	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "CP") {
			//call patch generater method to generate new ptach
		}
	}

}
