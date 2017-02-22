using UnityEngine;
using System.Collections;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	float speed = 10f;
	float RotateSpeed = 2f;
    private PlayerMotor motor;
    private Animator anim;

    public float minJumpHeight;
    public float jumpHight;
    float jumpPressure;
    bool grounded;

    // Use this for initialization
    void Start () {
		motor = GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float xMov = Input.GetAxisRaw("Horizontal");
		float zMov = Input.GetAxisRaw("Vertical");
		Vector3 MovHorizontal = transform.right*xMov;
		Vector3 MovVertical = transform.forward*zMov;
		Vector3 velocity = (MovHorizontal+MovVertical).normalized*speed;
		motor.Move(velocity);

		float yRot = Input.GetAxisRaw("Mouse X");
		Vector3 rot = new Vector3(0f, yRot, 0f) *RotateSpeed;
		motor.Rotate(rot);

		float xRot = Input.GetAxisRaw("Mouse Y");
		Vector3 CamRot = new Vector3(xRot,0f,0f)*RotateSpeed;
		motor.RotateCamera(CamRot);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("isWalking", true);
        } else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("pickupObject");
            anim.SetBool("isLifting", true);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("throwObject");
            anim.SetBool("isLifting", false);
        }

        /*
        if (grounded == true)
        {*/
            if (Input.GetButton("Jump"))
            {
                if (jumpPressure < jumpHight)
                {
                    jumpPressure += Time.deltaTime * 10f;
                }
                else
                {
                    jumpPressure = jumpHight;
                }
            }
            else
            {
                if (jumpPressure > 0f)
                {
                    jumpPressure = jumpPressure + minJumpHeight;
                    GetComponent<Rigidbody>().velocity = Vector3.up * jumpPressure;
                    jumpPressure = 0f;
                    grounded = false;
                }
            }
        }

        //Vector3 Floor = transform.TransformDirection(Vector3.down);

        //RaycastHit hit;
    /*
    if (Physics.Raycast(transform.position, Floor, 0.1f))
    {
        grounded = true;
    }
    else
    {
        grounded = false;
    }
}*/
}
