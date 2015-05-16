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
					lineRenderer.SetPosition(0, this.gameObject.transform.position);
					lineRenderer.SetPosition(1, ballJoint.connectedBody.transform.position);
				});

		Observable.EveryUpdate ()
			.Select (_ => ballJoint.enabled)
			.DistinctUntilChanged ()
				.Subscribe (enabled => lineRenderer.enabled = enabled);
	}
}
