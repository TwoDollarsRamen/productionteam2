using UnityEngine;

public class Rock : MonoBehaviour {
	public GameObject destroyParticles;

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Wolf")) {
			Instantiate(destroyParticles, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
