using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class GuardController: MonoBehaviour {

    public FieldOfViewController fovController;

    public Color playerNotInRangeColor;
    public Color playerInRangeColor;
    public float fovAnimationDuration = 0.2f;
    public Vector2 input;
    public ExclamationMarkFloating exclamationMark;

    private Animator animator;
    private Outline outline;
    private NavMeshAgent agent;

    void Start() {
        outline = GetComponent<Outline>();
        fovController.onTargetStatusChanged += OnPlayerSighted;
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = true;
        agent.updateRotation = true;
        GameManager.Instance.guards.Add(this);
#if !UNITY_EDITOR && UNITY_WEBGL
        outline.OutlineMode = Outline.Mode.OutlineAll;
#endif
    }

    private void Update() {
        Move(input);
        SyncAnimatorAndAgent();
    }

    private void SyncAnimatorAndAgent() {
        if (agent.remainingDistance < agent.stoppingDistance) {
            if (!agent.isStopped) {
                var delta = agent.destination - transform.position;
                Rotate(new Vector2(delta.x, delta.z).normalized);
                agent.isStopped = true;
                SetVelocity(0);
            }
        } else {
            agent.isStopped = false;
            SetVelocity(agent.velocity.magnitude);
        }
    }

    private void OnAnimatorMove() {
        Vector3 rootPosition = animator.rootPosition;
        rootPosition.y = agent.nextPosition.y;
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;
    }

    private void OnPlayerSighted(bool sighted) {
        var color = sighted ? playerInRangeColor : playerNotInRangeColor;
        fovController.AnimateToColor(color, fovAnimationDuration);
        if (sighted) {
            GameManager.Instance.OnPlayerBusted();
        }
    }

    private void SetVelocity(float velocity) {
        animator.SetFloat("velocity", velocity);
    }

    private void Move(Vector2 movement) {
        SetVelocity(Mathf.Clamp01(movement.magnitude));
        Rotate(movement);
    }

    private void Rotate(Vector2 movement, TweenCallback onComplete = null) {
        var inputValue = new Vector3(movement.x, 0, movement.y);
        if (inputValue.magnitude == 0) { return; }
        transform.DOKill();
        var rotate = transform.DORotateQuaternion(Quaternion.LookRotation(inputValue), 0.2f);
        if (onComplete != null) {
            rotate.OnComplete(onComplete);
        }
    }

    private void GoTo(Vector3 position) {
        var delta = position - transform.position;
        Rotate(new Vector2(delta.x, delta.z).normalized, () => {
            agent.SetDestination(position);
        });
    }

    public void OnHiccupNearby(Vector3 position) {
        var delta = position - transform.position;
        Rotate(new Vector2(delta.x, delta.z).normalized);
        exclamationMark.Appear();
        AudioManager.Instance.PlaySfx("uh");
    }

    public void OnBurpNearby(Vector3 position) {
        GoTo(position);
        exclamationMark.Appear();
        AudioManager.Instance.PlaySfx("eh");
    }

    public void PlayerIsNearby(bool isNearby) {
        outline.enabled = isNearby;
    }

    public void DisableGuard() {
        agent.isStopped = true;
        SetVelocity(0);
    }
}
