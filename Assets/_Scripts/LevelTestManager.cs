using UnityEngine;

public class LevelTestManager : MonoBehaviour
{
    public void DropTestBall(int numBalls)
    {
        DropZone[] allDropZones = GameObject.FindObjectsByType<DropZone>();

        for (int i = 0; i < numBalls; i++)
        {
            int randomIndex = Random.Range(0, allDropZones.Length);

            allDropZones[randomIndex].DropBall();
        }
    }
}
