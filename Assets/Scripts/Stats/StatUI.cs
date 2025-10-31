using TMPro;
using UnityEngine;
public class StatUI : MonoBehaviour
{
    public HeroStat targetStat;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI value;
    [SerializeField] TextMeshProUGUI increase;

    private void Start()
    {
        title.text = targetStat.ToString() + ":";
        ShowIncrease(false);
    }

    public void SetValue(float value)
    {
        this.value.text = value.ToString();
    }

    public void SetIncrease(float increase)
    {
        this.increase.text = "+" + increase.ToString();
    }

    public void ShowIncrease(bool show)
    {
        increase.enabled = show;
    }
}