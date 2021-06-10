using UnityEngine;
using UnityEngine.UI;

public class GameEventPre1Tutorial : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (!transform.Find("SubEvent1"))
        {
            //trigger next sub event instantly
            TriggerLeftToRight_1();
        }

        if (!transform.Find("SubEvent2"))
        {
            //trigger next sub event instantly
            TriggerMiddle_1();
        }
    }
    

    public void TriggerLeftToRight_1()
    {
        if (!animator.GetBool("GamePreEvent1TutLeftToRight_1"))
        {
            animator.SetBool("GamePreEvent1TutLeftToRight_1", true);
        }
        
    }
    public void TriggerMiddle_1()
    {
        if (!animator.GetBool("GamePreEvent1TutMiddle_1"))
        {
            animator.SetBool("GamePreEvent1TutMiddle_1", true);
        }

    }
}
