using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Color color;

    public void Initialize()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        color = RandomColor();
        meshRenderer.material.color = color;
    }

    private Color RandomColor()
    {
        Color[] colors = {
            new Color(1f, 0f, 0f, 1f),
            new Color(0f, 1f, 0f, 1f),
            new Color(0f, 0f, 1f, 1f)
        };
        
        return colors[Random.Range(0, colors.Length)];
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

            Destroy(gameObject);
        }
    }
}
