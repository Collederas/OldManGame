using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    public TMP_Text livesText;
    
    public void SetCount(int count)
    {
        livesText.text = count.ToString();
    }
}
