using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem impactEffect;
    
    private MeshRenderer meshRenderer;
    public Color color { get; private set; }

    public void Initialize(Color color)
    {
        this.color = color;
        
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }

    public IEnumerator Fire(float velocity, float angle, Vector3 direction, Transform firePoint)
    {
        float time = 0;
        while(time < 10)
        {
            // angle = angle * Mathf.Deg2Rad;
            
            float x = velocity * time * Mathf.Cos(angle);
            float y = velocity * time * Mathf.Sin(angle) - 0.5f * Physics.gravity.magnitude * Mathf.Pow(time, 2);
            transform.position = firePoint.position + direction*x + Vector3.up*y;

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent<Bubble>(out Bubble bubble))
        {
            if(bubble.color == color && !bubble.isFall)
            {
                bubble.Fall();
            }

            AudioSystem.current.PlaySFX("Boom");
            
            ParticleSystem asteroidImpact = Instantiate(impactEffect, other.ClosestPoint(transform.position), Quaternion.identity);
            ParticleSystemRenderer renderer = asteroidImpact.GetComponent<ParticleSystemRenderer>();
            renderer.material.color = color; 

            Destroy(asteroidImpact, 2f);
            Destroy(gameObject);
        }
    }
}
