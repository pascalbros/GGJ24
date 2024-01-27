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
        GameManager.Instance.guards.Add(this);
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
        SetVelocity(Mathf.Clamp01(movement.magnitude));
        if (inputValue.magnitude == 0) { return; }
        transform.DOKill();
        transform.DORotateQuaternion(Quaternion.LookRotation(inputValue), 0.2f);
    }

    private void GoTo(Vector3 position) {
        var delta = position - transform.position;
        Move(new Vector2(delta.x, delta.z).normalized);
    }

    public void OnHiccupNearby(Vector3 position) {
        GoTo(position);
    }
}
