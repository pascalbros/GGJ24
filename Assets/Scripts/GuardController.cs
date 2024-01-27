using UnityEngine;
using DG.Tweening;

public class GuardController: MonoBehaviour {

    public FieldOfViewController fovController;

    public Color playerNotInRangeColor;
    public Color playerInRangeColor;
    public float fovAnimationDuration = 0.2f;
    public Vector2 input;

    private Animator animator;

    void Start() {
        fovController.onTargetStatusChanged += OnPlayerSighted;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        Move(input);
    }

    private void OnPlayerSighted(bool sighted) {
        Debug.Log(sighted);
        var color = sighted ? playerInRangeColor : playerNotInRangeColor;
        fovController.AnimateToColor(color, fovAnimationDuration);
    }

    private void SetVelocity(float velocity) {
        animator.SetFloat("velocity", velocity);
    }

    private void Move(Vector2 movement) {
        var inputValue = new Vector3(movement.x, 0, movement.y);
        transform.DOKill();
        transform.DORotateQuaternion(Quaternion.LookRotation(inputValue), 0.2f);
        SetVelocity(Mathf.Clamp01(input.magnitude));
    }
}
