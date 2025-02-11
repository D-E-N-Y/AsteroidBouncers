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

    private struct BubblePosition
    {
        public Bubble bubble;
        public Vector3 position;

        public BubblePosition(Bubble bubble, Vector3 position)
        {
            this.bubble = bubble;
            this.position = position;
        }
    }
    private List<BubblePosition> bubbles;
    private int countBubbles;

    private bool isRestast;

    public void Initialize() 
    {
        isRestast = false;
        
        transform.Rotate(Vector3.right * axisTilt);
        countBubbles = 0;

        if(bubbles == null)
        {
            bubbles = new List<BubblePosition>();
            Generate();
        }
        else
        {
            foreach(BubblePosition current in bubbles)
            {
                current.bubble.gameObject.SetActive(true);
                current.bubble.Restart(current.position);
            }

            countBubbles = bubbles.Count;
            StartCoroutine(Rotate());
        }
    }

    public Color[] GetColors() => segmentColors;
    public string GetName() => namePlanet;
    public float GetRadius() => radius;

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
        float _radius = radius;
        
        float bubbleDiameter = bubblePrefab.GetComponent<SphereCollider>().radius * 1.7f * bubblePrefab.transform.localScale.x;
        int randomOffset = Random.Range(0, segmentColors.Length); // Рандомный сдвиг цветов
        
        for(int layer = 0; layer < layers; layer++)
        {
            int segmentsTheta = Mathf.RoundToInt((Mathf.PI * _radius) / bubbleDiameter);
            float stepTheta = Mathf.PI / segmentsTheta;

            Vector3 center = transform.position;

            for (float theta = 0; theta < Mathf.PI + stepTheta; theta += stepTheta)
            {
                int segmentsPhi = Mathf.RoundToInt((2 * Mathf.PI * _radius * Mathf.Sin(theta)) / bubbleDiameter);
                if (segmentsPhi < 1) segmentsPhi = 1;
                float stepPhi = 2 * Mathf.PI / segmentsPhi;

                List<Bubble> phiBubbles = new List<Bubble>();

                for (float phi = 0; phi < 2 * Mathf.PI; phi += stepPhi)
                {
                    float x = center.x + _radius * Mathf.Sin(theta) * Mathf.Cos(phi);
                    float y = center.y + _radius * Mathf.Sin(theta) * Mathf.Sin(phi);
                    float z = center.z + _radius * Mathf.Cos(theta);

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
                    bubbles.Add(new BubblePosition(bubble, spawnPosition));
                    bubble.onFall += UpdateBubbleCount;
                    countBubbles++;
                }
            }
            _radius -= bubbleDiameter;
        }

        StartCoroutine(Rotate());
    }

    public void Restart()
    {
        isRestast = true;
    }

    private void UpdateBubbleCount()
    {
        if(isRestast) return;
        
        countBubbles--;
        GameSystem.current.AddScore();

        if(countBubbles <= 0)
        {
            GameSystem.current.NextPlanet();
        }
    }
}
