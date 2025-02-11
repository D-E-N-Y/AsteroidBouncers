using System;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Action onFall;
    
    public Color color { get; private set; }
    private MeshRenderer meshRenderer;

    private List<Bubble> neighbors;
    public int layer { get; private set; }
    
    private Rigidbody _rigidbody;
    public bool isFall { get; private set; }

    public void Initialize(int layer, Color color)
    {
        this.layer = layer;
        this.color = color;

        neighbors = new List<Bubble>();

        _rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        meshRenderer.material.color = color;

        isFall = false;
    }

    public void Fall()
    {
        isFall = true;
        _rigidbody.isKinematic = !isFall;

        foreach(Bubble bubble in neighbors)
        {
            if(bubble && bubble.color == color && !bubble.isFall)
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

    void OnDestroy()
    {
        onFall?.Invoke();
    }
}
