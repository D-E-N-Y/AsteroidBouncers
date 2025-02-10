using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratePlanet : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;

    private float r;

    private void Start() 
    {
        r = 3;
        StartCoroutine(Generate(4));
    }

    private IEnumerator Generate(int layers)
    {    
        float bubbleDiameter = bubblePrefab.GetComponent<SphereCollider>().radius * 1.8f;
        
        for(int layer = 0; layer < layers; layer++)
        {
            int segmentsTheta = Mathf.RoundToInt((Mathf.PI * r) / bubbleDiameter);
            float stepTheta = Mathf.PI / segmentsTheta;

            Vector3 center = transform.position;

            for (float theta = 0; theta < Mathf.PI + stepTheta; theta += stepTheta)
            {
                int segmentsPhi = Mathf.RoundToInt((2 * Mathf.PI * r * Mathf.Sin(theta)) / bubbleDiameter);
                float stepPhi = 2 * Mathf.PI / segmentsPhi;

                for (float phi = 0; phi < 2 * Mathf.PI; phi += stepPhi)
                {
                    float x = center.x + r * Mathf.Sin(theta) * Mathf.Cos(phi);
                    float y = center.y + r * Mathf.Sin(theta) * Mathf.Sin(phi);
                    float z = center.z + r * Mathf.Cos(theta);

                    Vector3 spawnPosition = new Vector3(x, y, z);

                    Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);

                    yield return new WaitForSeconds(0.01f);
                }
            }

            yield return new WaitForSeconds(0.1f);

            r += bubbleDiameter;
        }
    }
}
