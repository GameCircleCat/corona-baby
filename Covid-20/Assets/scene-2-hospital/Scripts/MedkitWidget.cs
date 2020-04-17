using UnityEngine;
using UnityEngine.UI;

public class MedkitWidget : MonoBehaviour
{
    public Button button = null;
    public TMPro.TextMeshProUGUI label = null;
    public int health = 20;

    [System.NonSerialized]
    public int available = 0;

    public string text {
        get => label.text;
        set => label.text = value;
    }
}