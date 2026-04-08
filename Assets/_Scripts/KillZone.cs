using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cabbage")
        {
            Destroy(other.gameObject);
        }
    }
}
