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
        if(Input.touchCount == 0) return;
        
        Rotate();

        float _angle = angle * Mathf.Deg2Rad;
        Vector3 direction = (firePoint.position + firePoint.forward * 100) - firePoint.position;

        line.gameObject.SetActive(true);
        DrawPath(direction.normalized, initialVelocity, _angle, step);

        if(touch.phase == TouchPhase.Ended)
        {
            StopAllCoroutines();
            StartCoroutine(CoroutineMovement(direction.normalized, initialVelocity, _angle));

            firePoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            firePoint.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            line.gameObject.SetActive(false);
        }
    }

    private Touch touch;
    private float rotateSpeedModifier = 0.2f;

    private void Rotate()
    {
        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            Vector2 deltaTouch = touch.deltaPosition;
            Quaternion rotationY = Quaternion.Euler(0f, deltaTouch.x * rotateSpeedModifier, 0f);
            Quaternion rotationX =  Quaternion.Euler(deltaTouch.y * rotateSpeedModifier, 0f, 0f);

            firePoint.transform.rotation = firePoint.transform.rotation * rotationY;
            firePoint.transform.rotation = firePoint.transform.rotation * rotationX;
        }
    }

    private void DrawPath(Vector3 direction, float v0, float angle, float step)
    {
        step = Mathf.Max(0.01f, step);
        float time = 2;

        line.positionCount = (int)(time / step) + 2;
        
        int count = 0;
        for(float i = 0; i < time; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * Physics.gravity.magnitude * Mathf.Pow(i, 2);
            line.SetPosition(count, firePoint.position + direction*x + Vector3.up*y);
            
            count++;
        }

        float xFinal = v0 * time * Mathf.Cos(angle);
        float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * Physics.gravity.magnitude * Mathf.Pow(time, 2);
        line.SetPosition(count, firePoint.position + direction*xFinal + Vector3.up*yFinal);
    }

    private IEnumerator CoroutineMovement(Vector3 direction, float v0, float angle)
    {
        float time = 0;
        while(time < 100)
        {
            float x = v0 * time * Mathf.Cos(angle);
            float y = v0 * time * Mathf.Sin(angle) - 0.5f * Physics.gravity.magnitude * Mathf.Pow(time, 2);
            transform.position = firePoint.position + direction*x + Vector3.up*y;

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
