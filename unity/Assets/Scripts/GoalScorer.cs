using UnityEngine;

public class GoalScorer : MonoBehaviour
{
  [SerializeField] MatchDirector director;

  void Reset()
  {
    director = FindFirstObjectByType<MatchDirector>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (director == null || !other.CompareTag("Ball"))
      return;

    var touch = other.GetComponent<BallLastTouch>();
    director.RegisterGoal(touch);
  }
}
