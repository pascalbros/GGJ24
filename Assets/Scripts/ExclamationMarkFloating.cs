using UnityEngine;
using DG.Tweening;

public class ExclamationMarkFloating : MonoBehaviour
{
    public float floatDistance = 0.2f;
    public float floatDuration = 1f;

    void Start() => FloatUpDown();

    void FloatUpDown()
    {
        transform.DOMoveY(transform.position.y + floatDistance, floatDuration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo); // Infinite loop, back and forth motion
    }
}
