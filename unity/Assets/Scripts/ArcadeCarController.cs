using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Arcade car: forces + yaw steering, double jump, boost, readable ball hits.
/// Tuned via inspector defaults chosen for a readable first playable.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ArcadeCarController : MonoBehaviour
{
  [Header("Movement")]
  [Tooltip("Forward acceleration applied while holding throttle.")]
  [SerializeField] float moveAcceleration = 48f;
  [Tooltip("Planar speed cap so hits stay readable.")]
  [SerializeField] float maxForwardSpeed = 18f;
  [SerializeField] float turnSpeed = 95f;
  [Tooltip("Linear damping while grounded (Unity 6 Rigidbody).")]
  [SerializeField] float groundedDrag = 2.5f;
  [Tooltip("Lower air damping keeps aerials punchy.")]
  [SerializeField] float airDrag = 0.6f;

  [Header("Jump / air")]
  [SerializeField] float jumpImpulse = 9f;
  [SerializeField] int maxAirJumps = 1;

  [Header("Boost")]
  [SerializeField] float boostImpulse = 16f;
  [SerializeField] float boostCooldown = 1.35f;

  [Header("Ball contact assist")]
  [Tooltip("Base impulse added along the strike vector when colliding with the ball.")]
  [SerializeField] float hitAssistImpulse = 7f;
  [Tooltip("Hard cap on assist so physics stays fair.")]
  [SerializeField] float maxHitAssist = 14f;

  [Header("Grounding")]
  [SerializeField] LayerMask groundMask;
  [SerializeField] float groundProbeDistance = 0.65f;
  [SerializeField] Vector3 groundProbeOffset = new Vector3(0f, 0.35f, 0f);

  Rigidbody body;
  float yaw;
  int airJumpsLeft;
  float nextBoostTime;
  bool grounded;

  public bool IsGrounded => grounded;
  public bool IsInDunkZone { get; private set; }

  void Awake()
  {
    body = GetComponent<Rigidbody>();
    body.interpolation = RigidbodyInterpolation.Interpolate;
    body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    body.angularDamping = 1.25f;
  }

  void Start()
  {
    if (groundMask == 0)
      groundMask = LayerMask.GetMask("Ground");
    yaw = transform.eulerAngles.y;
    airJumpsLeft = maxAirJumps;
  }

  void Update()
  {
    UpdateGrounded();
    var keyboard = Keyboard.current;
    if (keyboard == null)
      return;

    if (keyboard.spaceKey.wasPressedThisFrame)
      TryJump();

    if (keyboard.leftShiftKey.wasPressedThisFrame || keyboard.rightShiftKey.wasPressedThisFrame)
      TryBoost();
  }

  void FixedUpdate()
  {
    var keyboard = Keyboard.current;
    float throttle = 0f;
    float steer = 0f;
    if (keyboard != null)
    {
      if (keyboard.wKey.isPressed)
        throttle += 1f;
      if (keyboard.sKey.isPressed)
        throttle -= 1f;
      if (keyboard.aKey.isPressed)
        steer -= 1f;
      if (keyboard.dKey.isPressed)
        steer += 1f;
    }

    body.linearDamping = grounded ? groundedDrag : airDrag;

    yaw += steer * turnSpeed * Time.fixedDeltaTime;
    var rot = Quaternion.Euler(0f, yaw, 0f);
    body.MoveRotation(rot);
    var forward = rot * Vector3.forward;

    var planarVel = Vector3.ProjectOnPlane(body.linearVelocity, Vector3.up);
    if (throttle > 0f && planarVel.magnitude < maxForwardSpeed)
      body.AddForce(forward * (moveAcceleration * throttle), ForceMode.Acceleration);
    else if (throttle < 0f && planarVel.magnitude < maxForwardSpeed * 0.65f)
      body.AddForce(forward * (moveAcceleration * throttle * 0.65f), ForceMode.Acceleration);

    planarVel = Vector3.ProjectOnPlane(body.linearVelocity, Vector3.up);
    if (planarVel.sqrMagnitude > maxForwardSpeed * maxForwardSpeed)
    {
      var clamped = planarVel.normalized * maxForwardSpeed + Vector3.up * body.linearVelocity.y;
      body.linearVelocity = clamped;
    }
  }

  void UpdateGrounded()
  {
    var origin = transform.position + groundProbeOffset;
    grounded = Physics.Raycast(origin, Vector3.down, groundProbeDistance, groundMask, QueryTriggerInteraction.Ignore);
    if (grounded)
      airJumpsLeft = maxAirJumps;
  }

  void TryJump()
  {
    if (grounded)
    {
      body.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
      return;
    }

    if (airJumpsLeft > 0)
    {
      body.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
      airJumpsLeft--;
    }
  }

  void TryBoost()
  {
    if (Time.time < nextBoostTime)
      return;
    var forward = transform.forward;
    var boost = Mathf.Min(boostImpulse, maxHitAssist + boostImpulse * 0.25f);
    body.AddForce(forward * boost, ForceMode.VelocityChange);
    nextBoostTime = Time.time + boostCooldown;
  }

  void OnCollisionEnter(Collision collision)
  {
    if (!collision.collider.CompareTag("Ball"))
      return;

    var rbBall = collision.rigidbody;
    if (rbBall == null || collision.contactCount == 0)
      return;

    var contact = collision.GetContact(0);
    var pushDir = (contact.point - transform.position).normalized;
    pushDir.y = Mathf.Clamp(pushDir.y, 0.1f, 0.85f);
    pushDir.Normalize();

    var strength = Mathf.Min(hitAssistImpulse, maxHitAssist);
    rbBall.AddForce(pushDir * strength, ForceMode.Impulse);
  }

  public void SetInDunkZone(bool value)
  {
    IsInDunkZone = value;
  }

  public void TeleportTo(Vector3 position, Quaternion rotation)
  {
    body.linearVelocity = Vector3.zero;
    body.angularVelocity = Vector3.zero;
    transform.SetPositionAndRotation(position, rotation);
    yaw = rotation.eulerAngles.y;
    airJumpsLeft = maxAirJumps;
    nextBoostTime = 0f;
  }
}
