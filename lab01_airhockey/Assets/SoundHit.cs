using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PuckCollisionSound : MonoBehaviour
{
    public AudioClip hitClip;
    public float minVolume = 0.2f;
    public float maxVolume = 1.0f;
    public float minImpactSpeed = 0.5f; // colisões muito fracas não tocam som

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // força da batida
        float impact = collision.relativeVelocity.magnitude;
        if (impact < minImpactSpeed)
            return;

        // mapeia impacto -> volume
        float t = Mathf.InverseLerp(minImpactSpeed, 10f, impact);
        float volume = Mathf.Lerp(minVolume, maxVolume, t);

        // se não setou via Inspector, tenta usar o clip do AudioSource
        AudioClip clip = hitClip != null ? hitClip : audioSource.clip;
        if (clip == null)
            return;

        audioSource.PlayOneShot(clip, volume);
    }
}
