using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public Transform CameraTransform => _camera.transform;
    private Camera _camera;
    public Animator Animator => _animator;
    private Animator _animator;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private LayerMask carryStateLayerMask;
    public LayerMask InteractableLayerMask => interactableLayerMask;
    public LayerMask CarryStateLayerMask => carryStateLayerMask;

    public IInteractable CarryObject { get; set; }

    public PlayerStateMachine StateMachine => stateMachine;
    private PlayerStateMachine stateMachine;

    public TimescaleManager TimescaleManager { get; set; }

    [Inject]
    public void Construct(TimescaleManager timescaleManager)
    {
        TimescaleManager = timescaleManager;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = GetComponentInChildren<Camera>();
        stateMachine = new PlayerStateMachine(this);
        stateMachine.Initialize(stateMachine.normalState);
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public bool TryGetInteractable<T>(PlayerController player, out T interactable, LayerMask layerMask, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, float rayDistance = 3f)
    {
        interactable = default;
        if (Physics.Raycast(player.CameraTransform.position, player.CameraTransform.forward, out RaycastHit hit, rayDistance, layerMask, triggerInteraction))
        {
            interactable = hit.collider.GetComponent<T>();
            return interactable != null;
        }
        return false;
    }
}
