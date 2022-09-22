using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private Transform explosionPlace;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool playOnDestroy;

    private void Start()
    {
        if (playOnStart)
            Play();
    }
    private void OnDestroy()
    {
        if (playOnDestroy)
            Play();
    }
    public void Play()
    {
        Instantiate(explosionParticle, explosionPlace.position, explosionParticle.transform.rotation);
    }
}