using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController: MonoBehaviour {

    public float movementSpeed = 10f;
    public float rotationSpeed = 9f;

    private CharacterController cc;
    private Vector2 movementInput;

    void Start() {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        HandleInput();
    }

    public void Action(InputAction.CallbackContext context) {
        Debug.Log("Fire!");
    }

    public void Move(InputAction.CallbackContext context) {
        Debug.Log("Move!");
        movementInput = context.ReadValue<Vector2>();
    }

    private void HandleInput() {
        if (movementInput.magnitude == 0f) {
            //Idle
        } else {
            Move(movementInput, movementSpeed);
        }
    }

    private void Move(Vector2 movement, float speed) {
        var input = speed * Time.deltaTime * movement;
        var inputValue = new Vector3(input.x, 0, input.y);
        cc.Move(inputValue);
        transform.DOKill();
        transform.DORotateQuaternion(Quaternion.LookRotation(inputValue), 0.2f);
    }
}
