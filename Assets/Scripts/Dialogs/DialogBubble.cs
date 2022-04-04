using TMPro;
using UnityEngine;

public class DialogBubble : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetText(string context)
    {
        text.text = context;
    }
}