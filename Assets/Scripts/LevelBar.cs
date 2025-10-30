using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelBar : MonoBehaviour
{
    [SerializeField] int numberOfLevels;
    [SerializeField] UIBar bar;
    [SerializeField] float barSizeInPixels;
    [SerializeField] TextMeshProUGUI levelText;

    void Start()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barSizeInPixels * numberOfLevels);
    }

    public void SetLevel(int level)
    {
        bar.SetPercentage(level * 100f / numberOfLevels);
        levelText.text = level.ToString();
    }
}
