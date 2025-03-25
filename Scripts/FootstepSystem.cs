using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource AudioSource; //izvor zvuka

    public AudioClip concrete; // nekakvi mp3 zvukovi
    public AudioClip grass;
    public AudioClip dirt;
    public AudioClip gravel;
    public AudioClip wood;
    public AudioClip gore;
    public AudioClip water;

    RaycastHit hit; // info o tome sto je ray pogodio
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;

    public void Footsteps()
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask))
        {
            if (hit.collider.CompareTag("concrete"))
            {
                PlayFootstepSoundL(concrete);
            }
            if (hit.collider.CompareTag("grass"))
            {
                PlayFootstepSoundL(grass);
            }
            if (hit.collider.CompareTag("dirt"))
            {
                PlayFootstepSoundL(dirt);
            }
            if (hit.collider.CompareTag("gravel"))
            {
                PlayFootstepSoundL(gravel);
            }
            if (hit.collider.CompareTag("wood"))
            {
                PlayFootstepSoundL(wood);
            }
            if (hit.collider.CompareTag("gore"))
            {
                PlayFootstepSoundL(gore);
            }
            if (hit.collider.CompareTag("water"))
            {
                PlayFootstepSoundL(water);
            }


        }
    }

    void PlayFootstepSoundL(AudioClip audio)
    {
        AudioSource.pitch = Random.Range(0.8f, 1f);
        AudioSource.PlayOneShot(audio);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
