using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public Transform plrControllerOrien;

	public float smoothSpeed = 0.125f;
	Vector3 offset;
	Vector3 lookAtOffset;
	public Vector3[] offsets = new Vector3[2];
	public Vector3[] lookAtOffsets = new Vector3[2];

	float turn;

	public Transform plr;

    void Start()
    {
		plr = GameObject.FindGameObjectWithTag("Player").transform;
		plrControllerOrien = plr.GetComponent<PlayerController>().orien.transform;
    }

    void Update()
    {
		offset = Vector3.Lerp(offsets[0], offsets[1], plr.GetComponent<PlayerController>().scaleFactor);
		lookAtOffset = Vector3.Lerp(lookAtOffsets[0], lookAtOffsets[1], plr.GetComponent<PlayerController>().scaleFactor);
		turn = Input.GetAxis("Debug Horizontal");
		//target.transform.parent.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Inverse(plr.transform.rotation), .25f);
		//target.transform.parent.rotation = Quaternion.Euler(Vector3.Scale(target.transform.parent.rotation.eulerAngles, new Vector3(1, 0, 1)));
		//plrControllerOrien = plr.GetComponent<PlayerController>().orien.transform;
	}

    /*void FixedUpdate()
	{
		//Transform newTarget = target;
		//newTarget.position = newTarget.position + lookAtOffset;

		Vector3 desiredPosition = target.position + target.forward * offset.z + target.right * offset.x + transform.up * offset.y;
		//Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 2);
		transform.position = smoothedPosition;

		//transform.RotateAround(plr.position, plr.up, turn);

		transform.LookAt(target);
	}*/

    void FixedUpdate()
    {
		//plrControllerOrien = plr.GetComponent<PlayerController>().orien.transform;

		//Transform newTarget = target;
		//newTarget.position = newTarget.position + lookAtOffset;

		Vector3 desiredPosition = plrControllerOrien.position + plrControllerOrien.forward * offset.z + plrControllerOrien.right * offset.x + plrControllerOrien.up * offset.y;
		//Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 2);
		transform.position = smoothedPosition;

		//transform.RotateAround(plr.position, plr.up, turn);

		transform.LookAt(plrControllerOrien);
	}
}
