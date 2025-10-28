using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBar : MonoBehaviour
{
    [SerializeField] int numberOfLevels;
    [SerializeField] UIBar bar;
    [SerializeField] float barSizeInPixels;

    void Start()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barSizeInPixels * numberOfLevels);
    }
}
