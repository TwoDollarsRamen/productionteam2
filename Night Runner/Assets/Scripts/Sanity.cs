using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Sanity : MonoBehaviour
{
    public float maxSanity = 1000;

    float sanity = 0;

    [Tooltip("The rate at which sanity will decrease.")]
    public float loss = 0.1f;

    [Tooltip("The camera's Post Processing Volume/")]
    public PostProcessVolume psv;

    public float petalGain = 10.0f;

    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
    	sanity = maxSanity;

        psv.profile.TryGetSettings(out vignette);
        if (vignette == null) {
			Debug.LogError("Post processing volume must have a vignette.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    	if (sanity > 0.0f) {
        	sanity -= loss * Time.deltaTime;
        }

        var unitSanity = (float)maxSanity - (float)sanity;
        vignette.intensity.value = unitSanity;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Petal"))
        {
            sanity += petalGain;
            if (sanity > maxSanity)
            {
                sanity = maxSanity;
            }
            Destroy(collider.gameObject);
        }
    }
}
