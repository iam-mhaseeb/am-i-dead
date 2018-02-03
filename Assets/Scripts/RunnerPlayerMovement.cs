using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayerMovement : MonoBehaviour {

	private static RunnerPlayerMovement _instance;

    public static RunnerPlayerMovement Instance 
    { 
        get { return _instance; } 
    } 
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

	void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    } 
	void Start () {
		StartRun ();
//		initYPos = transform.position.y;
	}

	void Update () {
		//jumping
		if (Input.GetKeyDown (KeyCode.UpArrow)||Input.GetKeyDown (KeyCode.W)) {
			if(!isDie){
				JumpInput ();
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)||Input.GetKeyDown (KeyCode.S)) {
			if(!isDie){
				MakeSlide ();
			}
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
		jumpInputTime = 0.35f;
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
		transform.position -= new Vector3 (moveSpeed * Time.deltaTime, 0, 0);
//		transform.position = new Vector3 (transform.position.x, initYPos, transform.position.z);
		anim.Rebind ();
		anim.Play ("Die");
		rb.velocity = Vector3.zero;
		
	}
	void OnCollisionStay(Collision c){
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
