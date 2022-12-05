using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Door : MonoBehaviour
{
    public TextMeshPro text;
    public int value;
    public Image doorImage;
    void Update()
    {
        if (value <= 0)
        {
            doorImage.color = new Color(1, 0, 0, 1);
            text.text = value.ToString();
        }
        else
        {
            doorImage.color = new Color(0, 1, 0, 1);
            text.text = "+" + value.ToString();
        }
    }
}
