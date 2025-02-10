using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float initialVelocity;
    [SerializeField] private float angle;
    
    [SerializeField] private LineRenderer line;
    [SerializeField] private float step;

    [SerializeField] private Transform firePoint;

    private void Update() 
    {
        float _angle = angle * Mathf.Deg2Rad;
        Vector3 direction = (firePoint.position + firePoint.forward * 100) - firePoint.position;

        DrawPath(direction.normalized, initialVelocity, _angle, step);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            
            
            StopAllCoroutines();
            StartCoroutine(CoroutineMovement(direction.normalized, initialVelocity, _angle));
        }
    }

    private void DrawPath(Vector3 direction, float v0, float angle, float step)
    {
        step = Mathf.Max(0.01f, step);
        float time = 10;

        line.positionCount = (int)(time / step) + 2;
        
        int count = 0;
        for(float i = 0; i < time; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);
            line.SetPosition(count, firePoint.position + direction*x + Vector3.up*y);
            
            count++;
        }

        float xFinal = v0 * time * Mathf.Cos(angle);
        float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);
        line.SetPosition(count, firePoint.position + direction*xFinal + Vector3.up*yFinal);
    }

    private IEnumerator CoroutineMovement(Vector3 direction, float v0, float angle)
    {
        float time = 0;
        while(time < 100)
        {
            float x = v0 * time * Mathf.Cos(angle);
            float y = v0 * time * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);
            transform.position = firePoint.position + direction*x + Vector3.up*y;

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
