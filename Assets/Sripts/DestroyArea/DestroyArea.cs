using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Bubble>())
        {
            other.gameObject.SetActive(false);
        }
    }
}
