using UnityEngine;
using System.Collections;

public class MiniMove : MonoBehaviour {

    public Transform Target;
    public float Distance;
    public float tooFar;
    public float weGood;

    public float runSpeed;
    public float walkSpeed;
    public float Damping;

    float GoodRangeRandom;
    float walkMoveSpeed;
    float runMoveSpeed;

    int stopTrying = 60;
    int reallyPls;

    public bool stopTryingToMove;

    public bool YeahCam;

    // Use this for initialization
    void Start () {
        GoodRangeRandom = Random.Range(weGood - 1f, weGood + 1f);
        walkMoveSpeed = Random.Range(walkSpeed - 1f, walkSpeed + 1f);
        runMoveSpeed = Random.Range(runSpeed - 1f, runSpeed + 1f);
        reallyPls = Random.Range(stopTrying + 20, stopTrying - 20);
    }
	
	// Update is called once per frame
	void Update () {

        
        
        Distance = Vector3.Distance(Target.position, transform.position);
        if (Distance > tooFar)
        {
            stopTrying = reallyPls;
            //GetComponent<Renderer>().material.color = Color.red;
            //Run
            lookAt();
            Move(runMoveSpeed);
        }
        if (Distance > weGood && Distance < tooFar)
        {
            stopTrying--;

            if (stopTrying >= 0)
            {
                //GetComponent<Renderer>().material.color = Color.yellow;
                //Walk Back slow
                lookAt();
                Move(walkMoveSpeed);
            }
            else
            {
                //GetComponent<Renderer>().material.color = Color.cyan;
            }
        }
        if (Distance < GoodRangeRandom)
        {
            stopTrying = reallyPls;
            //GetComponent<Renderer>().material.color = Color.green;
            YeahCam = true;
            //Idle?
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
        }

        float lockPos = 0;
        if (transform.rotation.x > 2)
            transform.rotation = Quaternion.Euler(lockPos+1, transform.rotation.y, transform.rotation.z);
        if (transform.rotation.z > 2)
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, lockPos-1);

        //transform.rotation = Quaternion.Euler(lockPos, transform.rotation.y , lockPos);


    }
    void lookAt()
    {
        //float lockPos = 0;
        var rotation = Quaternion.LookRotation(Target.position - transform.position);
        //transform.rotation = Quaternion.Euler(lockPos, rotation, lockPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
    }
    void Move(float Speed)
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
