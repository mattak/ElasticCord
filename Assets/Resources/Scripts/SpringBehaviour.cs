using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class SpringBehaviour : MonoBehaviour {
	private SpringJoint2D currentJoint;

	// Use this for initialization
	void Start () {
		currentJoint = this.gameObject.GetComponent<SpringJoint2D> ();

		// when collision enter 2d for current joint. disable
		this.OnTriggerEnter2DAsObservable ()
			// XXX: must filter without self
			// .Select (collision => collision.gameObject.GetComponent<Rigidbody2D>() == currentJoint.connectedBody)
			.Where (collider => collider.gameObject.tag == "Joint")
			.Subscribe (collider => {
					if (this.currentJoint.enabled && !Input.GetMouseButton(0)) {
						this.currentJoint.enabled = false;
					}
					else {
						this.currentJoint.enabled = true;
						this.currentJoint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D>();
					}
		});
	}
}
