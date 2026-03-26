using UnityEngine;

public class PlinkoBall : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CatchZone")
        {
            this.HandleCatch(other.gameObject.GetComponent<CatchZone>());            
        }
    }

    protected virtual void HandleCatch(CatchZone catchZone)
    { 
        catchZone.PlayCatchAudio();
        this.AwardPoints(catchZone.GetPointsValue());
        Destroy(this.gameObject);
    }

    protected virtual void AwardPoints(int pointsValue)
    { 
    
    }
}
