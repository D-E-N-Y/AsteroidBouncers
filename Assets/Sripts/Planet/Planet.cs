using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private string namePlanet;
    [SerializeField] private float radius;
    [SerializeField] private float layers;

    [SerializeField] private float speedRotate;
    [SerializeField] private float axisTilt;

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField ]private Color[] segmentColors;

    private int countBubbles;

    public void Initialize() 
    {
        transform.Rotate(Vector3.right * axisTilt);
        countBubbles = 0;

        Generate();
    }

    public Color[] GetColors() => segmentColors;
    public string GetName() => namePlanet;

    private IEnumerator Rotate()
    {
        while(true)
        {
            transform.Rotate(Vector3.up * speedRotate * Time.deltaTime);
            yield return null;
        }
    }

    private void Generate()
    {    
        float bubbleDiameter = bubblePrefab.GetComponent<SphereCollider>().radius * 1.7f * bubblePrefab.transform.localScale.x;
        int randomOffset = Random.Range(0, segmentColors.Length); // Рандомный сдвиг цветов

        for(int layer = 0; layer < layers; layer++)
        {
            int segmentsTheta = Mathf.RoundToInt((Mathf.PI * radius) / bubbleDiameter);
            float stepTheta = Mathf.PI / segmentsTheta;

            Vector3 center = transform.position;

            for (float theta = 0; theta < Mathf.PI + stepTheta; theta += stepTheta)
            {
                int segmentsPhi = Mathf.RoundToInt((2 * Mathf.PI * radius * Mathf.Sin(theta)) / bubbleDiameter);
                if (segmentsPhi < 1) segmentsPhi = 1;
                float stepPhi = 2 * Mathf.PI / segmentsPhi;

                List<Bubble> phiBubbles = new List<Bubble>();

                for (float phi = 0; phi < 2 * Mathf.PI; phi += stepPhi)
                {
                    float x = center.x + radius * Mathf.Sin(theta) * Mathf.Cos(phi);
                    float y = center.y + radius * Mathf.Sin(theta) * Mathf.Sin(phi);
                    float z = center.z + radius * Mathf.Cos(theta);

                    Vector3 spawnPosition = new Vector3(x, y, z);

                    if(phiBubbles.Count > 0)
                    {
                        bool isBusy = false;
                        
                        foreach(Bubble phiBubble in phiBubbles)
                        {
                            if(Vector3.Distance(phiBubble.transform.position, spawnPosition) < 0.1f)
                            {
                                isBusy = true;
                                break;
                            }
                        }

                        if(isBusy) continue;
                    }

                    int indexTheta = Mathf.FloorToInt((theta / Mathf.PI) * segmentColors.Length);
                    int indexPhi = Mathf.FloorToInt((phi / (2 * Mathf.PI)) * segmentColors.Length);

                    int randomVariation = Random.Range(-1, 2);
                    int colorIndex = (indexTheta + indexPhi + randomOffset + randomVariation) % segmentColors.Length;
                    if (colorIndex < 0) colorIndex += segmentColors.Length;

                    Color bubbleColor = segmentColors[colorIndex];

                    Bubble bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity).GetComponent<Bubble>();
                    bubble.transform.SetParent(transform);
                    bubble.Initialize(layer, bubbleColor);

                    phiBubbles.Add(bubble);
                    bubble.onFall += UpdateBubbleCount;
                    countBubbles++;
                }
            }
            radius -= bubbleDiameter;
        }

        StartCoroutine(Rotate());
    }

    private void UpdateBubbleCount()
    {
        countBubbles--;
        GameSystem.current.AddScore();

        if(countBubbles <= 0)
        {
            GameSystem.current.NextPlanet();
        }
    }
}
