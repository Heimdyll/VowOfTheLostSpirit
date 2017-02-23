using UnityEngine;
using System.Collections;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    
    public bool isThrowing;
    public bool isLifting;
    public float jumpMagnitude;

    float speed = 10f;
	float RotateSpeed = 2f;
    private Animator anim;
    private Rigidbody rb3d;
    private Camera mainCamera;
    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation;
    private GameObject pickedupParent;
    float jumpPressure;
    public bool grounded = true;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main.gameObject.GetComponent<Camera>();
        rb3d = GetComponent<Rigidbody>();
        Debug.Log("hand_L?: " + transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        Vector3 MovHorizontal = transform.right * xMov;
        Vector3 MovVertical = transform.forward * zMov;
        velocity = (MovHorizontal + MovVertical).normalized * speed;

        float yRot = Input.GetAxisRaw("Mouse X");
        rotation = new Vector3(0f, yRot, 0f) * RotateSpeed;

        float xRot = Input.GetAxisRaw("Mouse Y");
        cameraRotation = new Vector3(xRot, 0f, 0f) * RotateSpeed;


        if (!isThrowing && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }



        if (Input.GetKeyDown(KeyCode.J) && !anim.GetBool("isLifting"))
        {
            DisableMovement(0);
            //DisableThrow(0);
            anim.SetTrigger("pickupObject");
            anim.SetBool("isLifting", true);
        }
        else if (Input.GetKeyDown(KeyCode.K) && anim.GetBool("isLifting") && !isLifting)
        {
            anim.SetTrigger("throwObject");
            anim.SetBool("isLifting", false);
        }

        /*
        if (grounded == true)
        {*/

        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            grounded = true;
            anim.SetBool("isGrounded", true);
        }
        else
        {
            grounded = false;


        }
        if (Input.GetButton("Jump") && anim.GetBool("isGrounded"))
        {
            grounded = false;
            //anim.SetBool("isGrounded", false);
            Debug.Log(anim.GetBool("isGrounded"));

            anim.SetBool("isGrounded", false);
            
            /*if (jumpPressure < jumpHight)
            {
                jumpPressure += Time.deltaTime * 10f;
            }
            else
            {
                jumpPressure = jumpHight;
            }*/
            if (false)//jumpPressure > 0f)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 1f, ForceMode.Impulse);
                jumpPressure = 0f;
                grounded = false;
                anim.SetBool("isGrounded", false);
            }
        }

        
    }
        //Vector3 Floor = transform.TransformDirection(Vector3.down);

        //RaycastHit hit;
    /*
    
}*/
    void FixedUpdate()
    {
        if (!isThrowing)
            rb3d.MovePosition(transform.position + velocity * Time.fixedDeltaTime);

        rb3d.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        if (mainCamera != null)
        {
            mainCamera.transform.Rotate(-cameraRotation);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collider name: " + col.gameObject.name);

        if (col.gameObject.tag == "Pickupable")
        {
            Debug.Log("I'm pickupable");
            pickedupParent = col.transform.parent.gameObject;
            pickedupParent.transform.position += new Vector3(0, -5, 0);
            col.gameObject.GetComponent<BoxCollider>().enabled = false;
            //col.gameObject.GetComponent<Rigidbody>().useGravity = false;
            col.gameObject.transform.SetParent(transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0));
            col.gameObject.transform.localPosition = Vector3.zero;
            transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
        }

    }
    
    public void ThrowObject(int eventInt)
    {
        transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<BoxCollider>().enabled = true;
        pickedupParent.transform.position = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).position;
        pickedupParent.transform.rotation = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).rotation;
        //Debug.Break();
        transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).SetParent(pickedupParent.transform);
        pickedupParent.transform.GetChild(0).localEulerAngles = Vector3.zero;
        pickedupParent.transform.GetChild(0).localScale = Vector3.one;
        //Debug.Break();
        pickedupParent.GetComponent<Rigidbody>().AddForce(transform.forward*100, ForceMode.Impulse);
        //pickedupParent.transform.GetChild(0).localEulerAngles.Set(0, 0, 0);
    }

    public void ActuallyJump(int eventInt)
    {
        if (!false)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpMagnitude, ForceMode.Impulse);
        }
    }
    public void DisableThrow(int eventIn)
    {
        isLifting = true;
    }
    public void EnableThrow(int eventInt)
    {
        isLifting = false;
    }

    public void CheckForPickup(int eventInt)
    {
        transform.GetChild(0).gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void DisableMovement(int eventInt)
    {
        isThrowing = true;
    }

    public void EnableMovement(int eventInt)
    {
        isThrowing = false;
    }
}
