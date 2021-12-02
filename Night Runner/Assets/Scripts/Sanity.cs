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

    [Tooltip("Rate of which sanity comes back. 0.07 is a good start.")]
    public float gain;

    [Tooltip("Public for debugging, don't bother changing unless you want sanity to take a-bit to start going down.")]
    public float sanityToGain = 0.0f;

    [Tooltip("How much build up of sanity is allowed.")]
    public float maxSanityToGain;

    [Tooltip("How much the max sanity to gain will go up by each speed up.")]
    public float increaseToMaxSanityToGain;

    Vignette vignette;

    public AudioSource heartbeatEmitter;
    public AudioSource musicEmitter;
    public GameObject petalParticle;

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
        gain = loss;

        if (sanityToGain > maxSanityToGain)
        {
            sanityToGain = maxSanityToGain;
        }

        if (sanityToGain > 0 && sanity + gain < maxSanity && sanityToGain >= gain)
        {
            sanity += gain * Time.deltaTime;
            sanityToGain -= gain * Time.deltaTime;
        }
        else
        {
            sanity -= loss * Time.deltaTime;
        }

        float s = sanity;
		if (s < minSanity) {
			s = minSanity;
		}
        var unitSanity = (float)maxSanity - s;
        vignette.intensity.value = unitSanity;

        unitSanity = (float)maxSanity - sanity;
        heartbeatEmitter.volume = unitSanity;

        unitSanity = sanity / maxSanity;
        musicEmitter.volume = unitSanity;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Petal"))
        {
            sanityToGain += petalGain;
            Instantiate(petalParticle, transform.localPosition, Quaternion.identity);
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Flower"))
        {
            sanityToGain += flowerGain;

            Destroy(collider.gameObject);
        }
        if (collider.CompareTag("Speed Up"))
        {
            loss = loss + lossGain;
            maxSanityToGain += increaseToMaxSanityToGain;
        }
    }
}
