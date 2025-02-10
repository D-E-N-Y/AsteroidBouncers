using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Color color { get; private set; }
    private MeshRenderer meshRenderer;

    private List<Bubble> neighbors;
    public int layer { get; private set; }
    
    private Rigidbody _rigidbody;
    public bool isFall { get; private set; }

    public void Initialize(int layer)
    {
        this.layer = layer;
        
        neighbors = new List<Bubble>();

        _rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        color = RandomColor();
        meshRenderer.material.color = color;

        isFall = false;
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

    public void Fall()
    {
        isFall = true;
        _rigidbody.isKinematic = !isFall;

        foreach(Bubble bubble in neighbors)
        {
            if(bubble.color == color && !bubble.isFall)
            {
                bubble.Fall();
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent<Bubble>(out Bubble bubble))
        {
            if(bubble.layer == layer)
            {
                neighbors.Add(bubble);
            }
        }
    }
}
