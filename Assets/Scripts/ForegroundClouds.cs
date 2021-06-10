using UnityEngine;

public class ForegroundClouds : MonoBehaviour
{
    public GameObject infiniteBG;
    public GameObject infiniteBGIntense;
    public GameObject dummyChangeBGEvent; //this one's purpose is to be destroyed to trigger the next event

    public void ChangeBG()
    {
        infiniteBG.SetActive(false);
        infiniteBGIntense.SetActive(true);
        Destroy(dummyChangeBGEvent);
    }
}
