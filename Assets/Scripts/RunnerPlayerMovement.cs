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

	bool isClimb,isClimbEnd;
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
			cc.center = new Vector3(0,0.5f,0);
			sliding -= Time.deltaTime;
		} else if(!isDie){
			cc.height = 2;
			cc.center =  new Vector3(0,1,0);
		}
		Move ();
		if(isClimb){
			Climb();
		}
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
			if(c.gameObject.name=="stairs"){
				anim.Play("Climb");
				canRun = false;
				isClimb = true;
				RunnerEngineManger.Instance.fadeIn = true;
				StartCoroutine(ResetGameState());
			}
			if(c.gameObject.name=="StopCamera"){
				RunnerEngineManger.Instance.isMoveCamera = false;
			}
		}	
	}
	void Climb(){
		transform.position += Vector3.up * 5 * Time.deltaTime;
		rb.isKinematic = true;
	}
	void EndClimbing(){
		isClimbEnd = true;
		isClimb = false;
		rb.isKinematic = false;
	}
	IEnumerator  ResetGameState(){
		yield return new WaitForSeconds(5f);
		RunnerEngineManger.Instance.fadeIn = false;
		RunnerEngineManger.Instance.sprite.color = new Color(0f,0f,0f);
		RunnerEngineManger.Instance.hider.transform.position = new Vector3(Camera.main.transform.position.x+22f,RunnerEngineManger.Instance.hider.transform.position.y,RunnerEngineManger.Instance.hider.transform.position.z);
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x+22f,Camera.main.transform.position.y,Camera.main.transform.position.z);
		transform.position = new Vector3(transform.position.x+22f,transform.position.y,transform.position.z);
		anim.Play("Idle");
		EndClimbing();
		RunnerEngineManger.Instance.fadeOut = true;
		yield return new WaitForSeconds(5f);
		RunnerEngineManger.Instance.fadeOut = false;
		canRun = true;
		anim.Play("Run");
	}
}
