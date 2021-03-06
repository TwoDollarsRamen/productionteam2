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

    //public AudioSource[] heartbeatEmitter = new AudioSource[3];

    public GameObject heartBeatObjectOne;
    public GameObject heartBeatObjectTwo;
    public GameObject heartBeatObjectThree;

    AudioSource heartBeatOne;
    AudioSource heartBeatTwo;
    AudioSource heartBeatThree;

    public AudioSource musicEmitter;
    public GameObject petalParticle;

    // Start is called before the first frame update
    void Start()
    {
        heartBeatOne = heartBeatObjectOne.GetComponent<AudioSource>();
        heartBeatTwo = heartBeatObjectTwo.GetComponent<AudioSource>();
        heartBeatThree = heartBeatObjectThree.GetComponent<AudioSource>();

        heartBeatOne.volume = maxSanity - ((maxSanity / 10) * 9);
        heartBeatTwo.volume = maxSanity - (maxSanity / 3) * 2;
        heartBeatThree.volume = maxSanity;

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
        if (s < minSanity)
        {
            s = minSanity;
        }

        vignette.intensity.value = maxSanity - sanity; // vignette with sanity

        if (heartBeatOne.isActiveAndEnabled)
        {
            if (sanity <= maxSanity / 3) // heartbeat with sanity / 33% or less
            {
                if (!heartBeatThree.isPlaying)
                {
                    heartBeatTwo.Stop();
                    heartBeatOne.Stop();

                    heartBeatThree.Play();
                }
            }
            else if (sanity <= (maxSanity / 3) * 2) // 66%
            {
                if (!heartBeatTwo.isPlaying)
                {
                    heartBeatOne.Stop();
                    heartBeatThree.Stop();

                    heartBeatTwo.Play();
                }
            }
            else // > 66%
            {
                if (!heartBeatOne.isPlaying)
                {
                    heartBeatThree.Stop();
                    heartBeatTwo.Stop();

                    heartBeatOne.Play();
                }
            }

            if (sanity < maxSanity / 2) // music with sanity 
            {
                musicEmitter.volume = sanity / (maxSanity / 2);
            }
            else
            {
                musicEmitter.volume = maxSanity;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Petal"))
        {
            sanityToGain += petalGain;
            var ps = Instantiate(petalParticle, collider.gameObject.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            var particleTime = ps.duration + ps.startLifetime;
            Destroy(ps.gameObject, particleTime);

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
