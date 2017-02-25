using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    public bool playerMoving;
    private GameObject player;
    Vector3 offset;
    Vector3 previousRotation;
    Vector3 xRotation;
    Vector3 yRotation;
    public float rotateSpeed;

    private void Start()
    {
        offset = transform.position - target.position;
        player = GameObject.FindWithTag("Player");
        previousRotation = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        if (!playerMoving)
        {
            float verticalRot = Input.GetAxis("Mouse X");
            xRotation = new Vector3(verticalRot, 0f, 0f).normalized * rotateSpeed;

            float yRot = Input.GetAxis("Mouse Y");
            yRotation = new Vector3(0f, yRot, 0f).normalized * rotateSpeed;

            Vector3 newRotation = xRotation + yRotation;

            transform.LookAt(player.transform);
            transform.Translate(Vector3.Lerp(previousRotation, newRotation, 1f) * Time.deltaTime);
            previousRotation = transform.rotation.eulerAngles;
        }
        else 
            transform.position = target.transform.position + offset;

    }
}
