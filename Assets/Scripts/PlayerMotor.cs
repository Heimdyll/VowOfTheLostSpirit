using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
	private Vector3 velocity=Vector3.zero;
	private Rigidbody rb;
	private Vector3 rotation=Vector3.zero;
	private Vector3 cameraRotation=Vector3.zero;
	public GameObject cam;
	// Use this for initialization
	void Start () {
		rb=GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}
	public void RotateCamera(Vector3 _camRotation)
	{
		cameraRotation = _camRotation;
	}
	void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}
	void PerformMovement()
	{
		rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
	}
	void PerformRotation()
	{
		rb.MoveRotation(transform.rotation*Quaternion.Euler(rotation));
		if(cam != null)
		{
			cam.transform.Rotate(-cameraRotation);
		}
	}
}
