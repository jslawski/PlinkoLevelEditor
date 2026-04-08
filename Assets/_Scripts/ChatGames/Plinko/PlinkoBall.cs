using UnityEngine;
using CharacterCustomizer;

public class PlinkoBall : MonoBehaviour
{
    
    public virtual void SetupPlinkoBall(CustomCharacter customCharacter)
    {
        customCharacter.LoadCharacterFromJSON(string.Empty);    
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CatchZone")
        {
            this.HandleCatch(other.gameObject.GetComponentInParent<CatchZone>());            
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
