using UnityEngine;
using TMPro;

public class CatchZone : LevelObject
{
    private const string NEW_LEVEL_TEXT = "New Lvl";
    private const int NEW_LEVEL_VALUE = 10;
    private const int MID_VALUE = 5;

    private TextMeshProUGUI _pointsText;    
    private AudioSource _audioSource;

    private void Awake()
    {
        this._pointsText = GetComponentInChildren<TextMeshProUGUI>();
        this._audioSource = GetComponent<AudioSource>();
    }

    public override void LoadLevelObject(LevelObjectData data)
    {
        base.LoadLevelObject(data);

        this.SetPointsText();
        this.SetAudio();
    }

    private void SetPointsText()
    {
        this._pointsText.text = this._value.ToString();

        if (this._value >= NEW_LEVEL_VALUE)
        {
            this._pointsText.text = NEW_LEVEL_TEXT;
        }
    }

    private void SetAudio()
    {
        if (this._value >= NEW_LEVEL_VALUE)
        {
            this._audioSource.clip = Resources.Load<AudioClip>("SoundEffects/PlinkoLevelSwitch");                                   
        }
        else if (this._value > MID_VALUE)
        {
            this._audioSource.clip = Resources.Load<AudioClip>("SoundEffects/PlinkoBigCatch");
        }
        else
        {
            this._audioSource.clip = Resources.Load<AudioClip>("SoundEffects/PlinkoSmallCatch");
        }
    }

    public void PlayCatchAudio()
    {
        this._audioSource.Play();
    }

    public int GetPointsValue()
    {
        return Mathf.CeilToInt(this._value);
    }

    /*    

        private void AwardPoints(CabbageChatter scorer)
        {        
            scorer.shootScore += catchPoints;

            while (scorer.shootScore >= CabbageManager.instance.prestigeThreshold)
            {
                scorer.TriggerPrestige();
            }

            //CabbageManager.instance.chatterScoreHistory[scorer.chatterName.ToLower()] = scorer.shootScore;
            //CabbageManager.instance.chatterPrestigeHistory[scorer.chatterName.ToLower()] = scorer.prestigeLevel;
            LeaderboardManager.instance.QueueLeaderboardUpdate(scorer.chatterName, catchPoints);

                    
        }

        private IEnumerator LoadNextLevel()
        {
            while (this.catchAudio.isPlaying)
            {
                yield return null;
            }

            this.plinkoLevel.plinkoGame.LoadNewPlinkoLevel();
        }
        */
}
