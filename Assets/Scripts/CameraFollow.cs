using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{

	public Transform target;
	public Transform plrControllerOrien;

	public float smoothSpeed = 0.125f;
	public float maxFocusDistance = 15;

	public int dofUpdateRate = 2;

	Vector3 offset;
	Vector3 lookAtOffset;
	public Vector3[] offsets = new Vector3[2];
	public Vector3[] lookAtOffsets = new Vector3[2];

	Vector3 smoothedPosition;

	float turn;

	public Transform plr;

	//public Volume pp;
	//DepthOfField dof;

	//Ray r;
	//RaycastHit rh;

	void Start()
	{
		plr = GameObject.FindGameObjectWithTag("Player").transform;
		plrControllerOrien = plr.GetComponent<PlayerController>().orien.transform;

		//pp.profile.TryGet<DepthOfField>(out dof);
		//r = new Ray(transform.position, transform.forward);
	}

    void Update()
    {
		//offset = Vector3.Lerp(offsets[0], offsets[1], plr.GetComponent<PlayerController>().scaleFactor);
		//lookAtOffset = Vector3.Lerp(lookAtOffsets[0], lookAtOffsets[1], plr.GetComponent<PlayerController>().scaleFactor);

		//turn = Input.GetAxis("Debug Horizontal");

		//target.transform.parent.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Inverse(plr.transform.rotation), .25f);
		//target.transform.parent.rotation = Quaternion.Euler(Vector3.Scale(target.transform.parent.rotation.eulerAngles, new Vector3(1, 0, 1)));
		//plrControllerOrien = plr.GetComponent<PlayerController>().orien.transform;

		/*if(Time.frameCount % dofUpdateRate == 0)
        {
			changeFocus();
		}*/
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

		offset = Vector3.Lerp(offsets[0], offsets[1], plr.GetComponent<PlayerController>().scaleFactor);
		//lookAtOffset = Vector3.Lerp(lookAtOffsets[0], lookAtOffsets[1], plr.GetComponent<PlayerController>().scaleFactor);

		Vector3 desiredPosition = plrControllerOrien.position + plrControllerOrien.forward * offset.z + plrControllerOrien.right * offset.x + plrControllerOrien.up * offset.y;
		//Vector3 desiredPosition = target.position + offset;
		//print(plrControllerOrien.eulerAngles);
		smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 2);
		//transform.position = smoothedPosition;

		//transform.RotateAround(plr.position, plr.up, turn);

		//transform.LookAt(plr.position);
	}

    void LateUpdate()
    {
		transform.position = smoothedPosition;

		//transform.RotateAround(plr.position, plr.up, turn);

		transform.LookAt(plr.position);
	}

	/*void changeFocus()
    {
		r.origin = transform.position;
		r.direction = transform.forward;

		if(Physics.Raycast(r, out rh, maxFocusDistance))
        {
			dof.focusDistance.value = rh.distance;
        }
		else
        {
			dof.focusDistance.value = maxFocusDistance;
		}
    }*/
}
