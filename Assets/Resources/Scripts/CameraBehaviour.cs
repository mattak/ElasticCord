using UnityEngine;
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
				.Subscribe (y => {
					
					if (watchObject.transform.position.y > 10.0f) {
						Vector3 newPosition = new Vector3(this.transform.position.x, y, this.transform.position.z);
						this.transform.position = Vector3.Slerp (this.transform.position, newPosition, 1.5f);
					}
					else {
						this.transform.position = new Vector3 (this.transform.position.x, 0.0f, this.transform.position.z);
					}
				});
	}
}
