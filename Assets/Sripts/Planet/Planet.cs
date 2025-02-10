using System.Collections;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;

    private float r;

    private void Start() 
    {
        r = 4;
        Generate(4);
    }

    private IEnumerator Rotate()
    {
        while(true)
        {
            transform.Rotate(Vector3.up * 5 * Time.deltaTime);
            yield return null;
        }
    }

    private void Generate(int layers)
    {    
        float bubbleDiameter = bubblePrefab.GetComponent<SphereCollider>().radius * 1.7f * bubblePrefab.transform.localScale.x;
        
        for(int layer = 0; layer < layers; layer++)
        {
            int segmentsTheta = Mathf.RoundToInt((Mathf.PI * r) / bubbleDiameter);
            float stepTheta = Mathf.PI / segmentsTheta;

            Vector3 center = transform.position;

            for (float theta = 0; theta < Mathf.PI + stepTheta; theta += stepTheta)
            {
                int segmentsPhi = Mathf.RoundToInt((2 * Mathf.PI * r * Mathf.Sin(theta)) / bubbleDiameter);
                if (segmentsPhi < 1) segmentsPhi = 1;
                float stepPhi = 2 * Mathf.PI / segmentsPhi;


                for (float phi = 0; phi < 2 * Mathf.PI; phi += stepPhi)
                {
                    float x = center.x + r * Mathf.Sin(theta) * Mathf.Cos(phi);
                    float y = center.y + r * Mathf.Sin(theta) * Mathf.Sin(phi);
                    float z = center.z + r * Mathf.Cos(theta);

                    Vector3 spawnPosition = new Vector3(x, y, z);

                    Bubble bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity).GetComponent<Bubble>();
                    bubble.transform.SetParent(transform);
                    bubble.Initialize(layer);
                }
            }
            r += bubbleDiameter;
        }

        StartCoroutine(Rotate());
    }
}
