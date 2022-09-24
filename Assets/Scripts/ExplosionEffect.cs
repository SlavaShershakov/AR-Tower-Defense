using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private Transform explosionPlace;
    [SerializeField] private bool playOnEnable;
    [SerializeField] private bool playOnDisable;

    private void OnEnable()
    {
        if (playOnEnable)
            Play();
    }
    private void OnDisable()
    {
        if (playOnDisable)
            Play();
    }
    public void Play()
    {
        Instantiate(explosionParticle, explosionPlace.position, explosionParticle.transform.rotation);
    }
}