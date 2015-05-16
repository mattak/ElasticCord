using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class LineBehaviour : MonoBehaviour {
	public SpringJoint2D ballJoint;

	// Use this for initialization
	void Start () {
		LineRenderer lineRenderer = this.gameObject.GetComponent<LineRenderer>();

		Observable.EveryUpdate ()
			.Where (_ => ballJoint.enabled)
			.Select (_ => ballJoint.gameObject.transform.position)
			.DistinctUntilChanged()
				.Subscribe(position => {
					Vector3 jointPosition = ballJoint.connectedBody.transform.position;
					Vector3 ballPosition  = this.gameObject.transform.position;
					jointPosition.z = this.transform.position.z;
					ballPosition.z = this.transform.position.z;
					lineRenderer.SetPosition(0, jointPosition);
					lineRenderer.SetPosition(1, ballPosition);
				});

		Observable.EveryUpdate ()
			.Select (_ => ballJoint.enabled)
			.DistinctUntilChanged ()
				.Subscribe (enabled => lineRenderer.enabled = enabled);
	}
}
