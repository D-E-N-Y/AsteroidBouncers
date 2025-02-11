using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Bubble>())
        {
            Destroy(other.gameObject);
        }
    }
}
