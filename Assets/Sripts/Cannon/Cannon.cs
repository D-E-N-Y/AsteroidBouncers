using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour 
{
    [SerializeField] private float initialVelocity;
    [SerializeField] private float angle;
    
    [SerializeField] private LineRenderer line;
    [SerializeField] private float step;

    [SerializeField] private GameObject projectilePrefab;
    private Projectile projectile;
    private Color[] colors;

    public void Initialize(Color[] colors) 
    {
        this.colors = colors;
        
        if(projectile)
        {
            Destroy(projectile.gameObject);
        }

        projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.Initialize(colors[Random.Range(0, colors.Length)]);
    }

    private void Update() 
    { 
        if(Input.touchCount == 0) return;
        
        Rotate();

        float _angle = angle * Mathf.Deg2Rad;
        Vector3 direction = (transform.position + transform.forward * 100) - transform.position;

        line.gameObject.SetActive(true);
        DrawPath(direction.normalized, initialVelocity, _angle, step);

        if(touch.phase == TouchPhase.Ended)
        {
            projectile.StartCoroutine(projectile.Fire(initialVelocity, _angle, direction.normalized, transform));

            projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
            projectile.Initialize(colors[Random.Range(0, colors.Length)]);

            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
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

            transform.rotation = transform.rotation * rotationY;
            transform.rotation = transform.rotation * rotationX;
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
            line.SetPosition(count, transform.position + direction*x + Vector3.up*y);
            
            count++;
        }

        float xFinal = v0 * time * Mathf.Cos(angle);
        float yFinal = v0 * time * Mathf.Sin(angle) - 0.5f * Physics.gravity.magnitude * Mathf.Pow(time, 2);
        line.SetPosition(count, transform.position + direction*xFinal + Vector3.up*yFinal);
    }
}