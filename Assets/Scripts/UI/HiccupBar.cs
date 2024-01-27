using UnityEngine;
using UnityEngine.UI;

public class HiccupBar: MonoBehaviour {

    Image image;
    RectTransform rectTransform;
    Vector2 initialSize;

    private void Start() {
        image = GetComponent<Image>();
        rectTransform = image.GetComponent<RectTransform>();
        initialSize = rectTransform.sizeDelta;
    }

    public void SetPercentage(float valueZeroToOne) {
        var size = initialSize;
        size.x = initialSize.x * valueZeroToOne;
        rectTransform.sizeDelta = size;
    }
}
