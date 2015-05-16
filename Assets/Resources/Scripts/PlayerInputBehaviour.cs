using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class PlayerInputBehaviour : MonoBehaviour {
	public GameObject ballObject;
	private SpringJoint2D springJoint;
	private float distanceThreshold = 5.0f;
	
	// Use this for initialization
	void Start () {
		Rigidbody2D rigidbody = this.ballObject.GetComponent<Rigidbody2D>();
		springJoint = this.ballObject.GetComponent<SpringJoint2D> ();

		// this.OnMouseDownAsObservable ();
		CreateMouseObservable (Observable.EveryUpdate ().Where (_ => Input.GetMouseButtonDown(0)))
				.Subscribe (position => {
					rigidbody.isKinematic = true;
				});

		CreateMouseObservable (Observable.EveryUpdate ().Where (_ => Input.GetMouseButtonUp(0)))
			.Subscribe (position => {
				rigidbody.isKinematic = false;
			});

		CreateMouseObservable (Observable.EveryUpdate ().Where (_ => Input.GetMouseButton (0)))
			.Select (position => {
				Rigidbody2D connectedBody = springJoint.connectedBody.GetComponent<Rigidbody2D>();

				Debug.Log ("rigidbody:" + connectedBody.position);
				Debug.Log ("position:" + position);
				Debug.Log ("distance:" + Vector2.Distance (connectedBody.position, position));

				if (Vector2.Distance (connectedBody.position, position) < distanceThreshold) {
					return position;
				}
				else {
					Vector2 normalizedVector = connectedBody.position - position;
					normalizedVector = normalizedVector.normalized;
					return connectedBody.position - normalizedVector * distanceThreshold;
				}
			})
			.Subscribe (position => {
				rigidbody.position = position;
				this.ballObject.GetComponent<SpringBehaviour> ().SetCanRelease (true);
			});
	}

	IObservable<Vector2> CreateMouseObservable (IObservable<long> observable) {
		return observable
				.Where (_ => this.springJoint.enabled)
				.Select (_ => Input.mousePosition)
				.Select (position => Camera.main.ScreenToWorldPoint(position))
				.Select (position => new Vector2(position.x, position.y));
	}
}
