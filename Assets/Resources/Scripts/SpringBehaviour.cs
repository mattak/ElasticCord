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

		// when collision enter 2d for current joint. disable
		this.OnTriggerEnter2DAsObservable ()
			// XXX: must filter without self
			// .Select (collision => collision.gameObject.GetComponent<Rigidbody2D>() == currentJoint.connectedBody)
			.Where (collider => collider.gameObject.tag == "Joint")
			.Subscribe (collider => {
					if (this.currentJoint.enabled && canRelease) {
						this.currentJoint.enabled = false;
					}
					else {
						this.currentJoint.enabled = true;
						this.canRelease = false;
						this.currentJoint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D>();
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
