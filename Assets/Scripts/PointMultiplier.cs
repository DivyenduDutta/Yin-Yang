using UnityEngine;

public class PointMultiplier : MonoBehaviour
{
    public int pointAmt;
    public void AddPointMultiplierOnExitAnimation()
    {
        ScoreHandler.GetInstance().AddToScore((pointAmt * 10) - pointAmt);
    }

    public void DestroyPointMultiplierOnExitAnimation()
    {
        Destroy(transform.gameObject);
    }
}
