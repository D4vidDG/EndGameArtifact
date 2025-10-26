using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class ButtonScaler : MonoBehaviour
{
    [SerializeField] float scaleMultiplier = 1;
    [SerializeField] bool previewInEditor;

    RectTransform rectTransform;
    float initialScale;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        initialScale = rectTransform.localScale.x;
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && !previewInEditor) return;
#endif
        rectTransform.localScale = initialScale * scaleMultiplier * Vector3.one;
    }
}
