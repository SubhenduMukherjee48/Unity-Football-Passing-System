using UnityEngine;

public class RootMotionSyncManager : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Transform modelTransform;
    [SerializeField] Animator animator;
    private Vector3 cachedVelocity;
    private bool isInRootMotionPhase = false;
   
    void Awake()
    {
        if (!controller) controller = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }
    public void OnRootMotionStart()
    {
         
        cachedVelocity = controller.velocity;    
        isInRootMotionPhase = true;
        controller.enabled = false;
        IsRollingComplete = false;
    }
    public void OnRootMotionEnd()
    {
         
        transform.position = modelTransform.position;
        transform.rotation = modelTransform.rotation;
        controller.enabled = true;
        isInRootMotionPhase = false;
        IsRollingComplete = true;
    }   
    public bool IsRootMotionActive => isInRootMotionPhase;

    public bool IsRollingComplete { get; internal set; } = false;
}
