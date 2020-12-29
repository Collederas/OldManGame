using TMPro;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    public TMP_Text _livesText;
    
    public void SetCount(int count)
    {
        _livesText.text = count.ToString();
    }
}
