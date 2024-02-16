using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;
	}

	private void Update()
	{
		this.transform.LookAt(_camera.transform, Vector3.up);
	}
}
