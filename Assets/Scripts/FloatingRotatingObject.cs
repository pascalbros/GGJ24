using UnityEngine;
using DG.Tweening;

public class FloatingRotatingObject : MonoBehaviour
{
    public float floatHeight = 1.0f;
    public float floatDuration = 1.0f;
    public float rotationAngle = 45.0f;
    public float rotationDuration = 2.0f;

    void Start()
    {
        // Initial position of the object
        Vector3 initialPosition = transform.position;

        // Set up the floating animation
        transform.DOMoveY(initialPosition.y + floatHeight, floatDuration)
            .SetLoops(-1, LoopType.Yoyo)  // Infinite loop (up and down)
            .SetEase(Ease.InOutQuad);

        // Set up the rotating animation
        transform.DORotate(new Vector3(0, 0, rotationAngle), rotationDuration)
            .SetLoops(-1, LoopType.Yoyo)  // Infinite loop (clockwise and anticlockwise)
            .SetEase(Ease.InOutQuad);
    }
}
