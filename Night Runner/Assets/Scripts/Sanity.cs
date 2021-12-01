using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Sanity : MonoBehaviour
{
    public float maxSanity = 1000;

    public float sanity = 0;

	public float minSanity = 0.001f;

    [Tooltip("The rate at which sanity will decrease.")]
    public float loss = 0.1f;
    public float lossGain = 0.01f;

    [Tooltip("The camera's Post Processing Volume/")]
    public PostProcessVolume psv;

    public float petalGain = 10.0f;
    public float flowerGain = 20.0f;

    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        sanity = maxSanity;

        psv.profile.TryGetSettings(out vignette);
        if (vignette == null)
        {
            Debug.LogError("Post processing volume must have a vignette.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sanity > 0.0f)
        {
            sanity -= loss * Time.deltaTime;
        }

		float s = sanity;
		if (s < minSanity) {
			s = minSanity;
		}
        var unitSanity = (float)maxSanity - s;
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
        else if (collider.CompareTag("Flower"))
        {
            sanity += flowerGain;
            if (sanity > maxSanity)
            {
                sanity = maxSanity;
            }
            Destroy(collider.gameObject);
        }
        if (collider.CompareTag("Speed Up"))
        {
            loss = loss + lossGain;
        }
        
    }
}
