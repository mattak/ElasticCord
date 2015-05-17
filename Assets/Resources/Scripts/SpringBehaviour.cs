using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class SpringBehaviour : MonoBehaviour {
	private SpringJoint2D currentJoint;
	private bool canRelease;

	// Use this for initialization
	void Start () {
		canRelease = false;
		currentJoint = this.gameObject.GetComponent<SpringJoint2D> ();

		// disable when collision enter 2d for current joint.
		this.OnTriggerEnter2DAsObservable ()
			.Where (_ => !Input.GetMouseButton(0))
			.Where (collider => collider.gameObject.tag == "Joint")
			.Subscribe (collider => {
					if (this.currentJoint.enabled && canRelease) {
						this.currentJoint.enabled = false;
					}
					else {
						this.currentJoint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D>();
						this.currentJoint.enabled = true;
						this.canRelease = false;
					}
		});
	}

	public void SetCanRelease(bool enable) {
		canRelease = enable;
	}

	public bool GetCanRelease() {
		return canRelease;
	}

	public bool IsJointing() {
		return currentJoint.enabled;
	}
}
