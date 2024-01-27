using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController: MonoBehaviour {

    public float hiccupTimer = 5f;
    public float movementBlendSpeed = 1f;
    public Animator animator;
    public HiccupBar hiccupBar;

    private float currentHiccupTimer = 0f;
    private CharacterController cc;
    private Vector2 movementInput;
    private Vector2 targetMovementInput;

    void Start() {
        currentHiccupTimer = hiccupTimer;
        cc = GetComponent<CharacterController>();
        SetDrunkedness(1.0f);
    }

    void Update() {
        HandleInput();
        HandleSob();
    }

    public void Action(InputAction.CallbackContext context) {
        Debug.Log("Fire!");
    }

    public void Move(InputAction.CallbackContext context) {
        targetMovementInput = context.ReadValue<Vector2>();
    }

    private void HandleInput() {
        movementInput = Vector3.Lerp(movementInput, targetMovementInput, Time.deltaTime * movementBlendSpeed);
        if (Mathf.Abs(movementInput.magnitude) < 0.02f) {
            //Idle
        } else {
            Move(movementInput);
        }
        SetVelocity(Mathf.Clamp01(movementInput.magnitude) / 1.1f);
    }

    private void SetDrunkedness(float value) {
        animator.SetFloat("drunkedness", Mathf.Clamp01(value));
    }

    private void SetVelocity(float velocity) {
        animator.SetFloat("velocity", velocity);
    }

    private void Move(Vector2 movement) {
        var inputValue = new Vector3(movement.x, 0, movement.y);
        transform.DOKill();
        transform.DORotateQuaternion(Quaternion.LookRotation(inputValue), 0.2f);
    }

    private void HandleSob() {
        currentHiccupTimer -= Time.deltaTime;
        if (currentHiccupTimer <= 0) {
            DoSob();
            currentHiccupTimer = hiccupTimer;
        }
        hiccupBar.SetPercentage(currentHiccupTimer / hiccupTimer);
    }

    private void DoSob() {

    }
}
