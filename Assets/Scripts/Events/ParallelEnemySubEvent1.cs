using UnityEngine;

public class ParallelEnemySubEvent1 : MonoBehaviour
{
    public Animator animator;

    public void TriggerPopCornEnemies()
    {
        animator.SetTrigger("ParallelEnemyEventPopcorn");
    }
}
