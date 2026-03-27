using UnityEngine;

/// <summary>
/// Trigger in front of the goal; enables dunk-tier scoring when combined with an aerial touch.
/// </summary>
public class DunkZoneVolume : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    var car = other.GetComponentInParent<ArcadeCarController>();
    if (car != null)
      car.SetInDunkZone(true);
  }

  void OnTriggerExit(Collider other)
  {
    var car = other.GetComponentInParent<ArcadeCarController>();
    if (car != null)
      car.SetInDunkZone(false);
  }
}
