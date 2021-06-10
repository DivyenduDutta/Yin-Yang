using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    public static DebugScript instance;
    public string myName;

    public static DebugScript GetInstance()
    {
        return instance;
    }


    private Text debugText;
    private void Awake()
    {
        debugText = GetComponent<Text>();
        instance = this;
    }

    private void Update()
    {
        /*debugText.text = "x3 " +Koel.GetInstance().x3.ToString() + "y3 " +
            Koel.GetInstance().y3.ToString();*/

        /*debugText.text = Koel.GetInstance().mag.ToString() + "x1" +
            Koel.GetInstance().x1.ToString() + "y1" +
            Koel.GetInstance().y1.ToString() + "x2" +
            Koel.GetInstance().x2.ToString() + "y2" +
            Koel.GetInstance().y2.ToString() + "x3" +
            Koel.GetInstance().x3.ToString() + "y3" +
            Koel.GetInstance().y3.ToString();*/

        //debugText.text = DebugScript.GetInstance().myName;

    }
}
