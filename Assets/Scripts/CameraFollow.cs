using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

	float turn;

	public Transform plr;

    void Start()
    {
		plr = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
		turn = Input.GetAxis("Debug Horizontal");
	}
    void FixedUpdate()
	{
		Vector3 desiredPosition = target.position + target.forward * offset.z + target.right * offset.x + transform.up * offset.y;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 2);
		transform.position = smoothedPosition;

		transform.RotateAround(plr.position, plr.up, turn);

		transform.LookAt(target);
	}

}
