using UnityEngine;

public class SubEvent : MonoBehaviour
{
    private void Update()
    {
        if (transform.childCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
