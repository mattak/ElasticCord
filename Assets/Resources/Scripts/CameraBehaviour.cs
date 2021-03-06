﻿using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CameraBehaviour : MonoBehaviour {
	public GameObject watchObject;

	// Use this for initialization
	void Start () {
		SpringBehaviour springBehaviour = watchObject.GetComponent<SpringBehaviour> ();
		Observable.EveryUpdate()
			.Where (_ => !Input.GetMouseButton(0) || !springBehaviour.IsJointing())
			.Select (_ => watchObject.transform.position.y)
				.Select (y => {
					if (watchObject.transform.position.y > 10.0f || watchObject.transform.position.y < 0.0f) {
						Vector3 newPosition = new Vector3(this.transform.position.x, y, this.transform.position.z);
						float distance = Vector3.Distance (this.transform.position, newPosition);
						
						return (distance > 2.0f) ?
							Vector3.Lerp (this.transform.position, newPosition, 0.1f)
								: this.transform.position;
					}
					return new Vector3 (this.transform.position.x, 0.0f, this.transform.position.z);
				})
				.Subscribe (position => this.transform.position = position );

		float ratio = 2.0f / 640.0f * 1136.0f;
		Camera.main.orthographicSize = (float)(ratio / Screen.width * Screen.height);
		Debug.Log ("resolusion:" + Screen.width + "x" + Screen.height);
		Debug.Log ("aspect:" + this.GetComponent<Camera>().aspect);
		Debug.Log ("orthographicSize:" + this.GetComponent<Camera>().orthographicSize);
		Debug.Log ("cameraPixel:" + GetComponent<Camera>().pixelWidth + "x" + GetComponent<Camera>().pixelHeight);
	}
}
