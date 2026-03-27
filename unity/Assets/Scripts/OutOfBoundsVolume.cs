using UnityEngine;

public class OutOfBoundsVolume : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    if (!other.CompareTag("Ball"))
      return;
    var director = MatchDirector.Instance;
    if (director != null)
      director.QuickResetBall();
  }
}
