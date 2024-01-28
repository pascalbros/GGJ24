using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController: MonoBehaviour {

    public float hiccupTimer = 5f;
    public float movementBlendSpeed = 1f;
    public Animator animator;
    public HiccupBar hiccupBar;
    public bool invertControls = false;

    private float currentHiccupTimer = 0f;
    private CharacterController cc;
    private Vector2 movementInput;
    private Vector2 targetMovementInput;

    public static PlayerController Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

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
        targetMovementInput = (invertControls ? -1 : 1) * context.ReadValue<Vector2>();
    }

    private void HandleInput() {
        movementInput = Vector3.Lerp(movementInput, targetMovementInput, Time.deltaTime * movementBlendSpeed);
        if (Mathf.Abs(movementInput.magnitude) < 0.02f) {
            //Idle
        } else {
            Move(movementInput);
        }
        SetVelocity(Mathf.Clamp01(movementInput.magnitude) / 1.7f);
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
            OnHiccup();
            currentHiccupTimer = hiccupTimer;
        }
        HiccupManager.Instance.hiccupBar.SetPercentage(currentHiccupTimer / hiccupTimer);
    }

    private void OnHiccup() {
        HiccupManager.Instance.OnHiccup();
    }
}
