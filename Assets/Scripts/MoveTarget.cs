using UnityEngine;
using System.Collections;

public class MoveTarget : MonoBehaviour {

    float xMoveAmmount;
    float zMoveAmmount;
    public float moveSpeed = 5;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        xMoveAmmount = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        zMoveAmmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(xMoveAmmount, 0, zMoveAmmount);
    }
}
