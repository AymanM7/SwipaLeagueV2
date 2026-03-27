using UnityEngine;

/// <summary>
/// Tracks how the ball was last struck for goal scoring (ground vs aerial, dunk zone).
/// </summary>
public class BallLastTouch : MonoBehaviour
{
  public bool LastTouchWasAerial { get; private set; }
  public bool LastTouchFromDunkZone { get; private set; }

  void OnCollisionEnter(Collision collision)
  {
    var car = collision.collider.GetComponentInParent<ArcadeCarController>();
    if (car == null)
      return;

    LastTouchWasAerial = !car.IsGrounded;
    LastTouchFromDunkZone = car.IsInDunkZone;
  }

  public void Clear()
  {
    LastTouchWasAerial = false;
    LastTouchFromDunkZone = false;
  }
}
