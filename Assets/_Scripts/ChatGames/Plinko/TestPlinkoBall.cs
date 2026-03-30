using CharacterCustomizer;

public class TestPlinkoBall : PlinkoBall
{
    public override void SetupPlinkoBall(CustomCharacter customCharacter)
    {
        customCharacter.LoadCharacterFromJSON(string.Empty);
    }

    protected override void HandleCatch(CatchZone catchZone)
    {
        catchZone.PlayCatchAudio();
        Destroy(this.gameObject);
    }
}
