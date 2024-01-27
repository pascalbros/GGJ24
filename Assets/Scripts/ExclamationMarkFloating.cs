using UnityEngine;
using DG.Tweening;

public class ExclamationMarkFloating: MonoBehaviour {
    public float animationDuration = 1f;

    MeshRenderer meshRenderer;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    void FloatUpDown() {
        transform.DOMoveY(transform.position.y + 0.2f, 1)
            .SetEase(Ease.InOutCubic)
            .SetLoops(4, LoopType.Yoyo)
            .OnComplete(() => Dismiss());
    }

    public void Appear() {
        meshRenderer.enabled = true;
        transform.localScale = Vector3.zero;
        transform.DOScale(-0.08459936f, animationDuration)
            .SetEase(Ease.OutBounce).OnComplete(() => FloatUpDown());
    }

    public void Dismiss() {
        transform.DOScale(0, animationDuration)
            .SetEase(Ease.InCubic).OnComplete(() => meshRenderer.enabled = false);
    }
}
