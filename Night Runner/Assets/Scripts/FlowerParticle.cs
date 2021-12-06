using UnityEngine;

public class FlowerParticle : MonoBehaviour {
	public float speed = 100;
	public GameObject wolf = null;

	float t = 0.0f;

	void Start() {
		if (wolf != null) {
			wolf = GameObject.Find("Wolf_blockout_3");
			if (wolf == null) {
				Debug.LogError("Cannot find `Wolf_blockout_3'");
			}
		}
	}

	void Update() {
		t += speed * Time.deltaTime;

		transform.position = Vector3.Lerp(transform.position, wolf.transform.position, t);
	}
}
