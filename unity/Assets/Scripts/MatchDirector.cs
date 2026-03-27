using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchDirector : MonoBehaviour
{
  public static MatchDirector Instance { get; private set; }

  [SerializeField] Rigidbody ballBody;
  [SerializeField] Transform ballSpawn;
  [SerializeField] Transform carSpawn;
  [SerializeField] ArcadeCarController car;
  [SerializeField] Text scoreLabel;
  [SerializeField] Text lastGoalLabel;
  [SerializeField] float resetDelay = 0.75f;

  int score;
  bool goalLock;

#if UNITY_EDITOR
  public void Configure(
    Rigidbody ball,
    Transform ballSpawnPoint,
    ArcadeCarController carController,
    Transform carSpawnPoint,
    Text scoreUi,
    Text lastGoalUi)
  {
    ballBody = ball;
    ballSpawn = ballSpawnPoint;
    car = carController;
    carSpawn = carSpawnPoint;
    scoreLabel = scoreUi;
    lastGoalLabel = lastGoalUi;
  }
#endif

  void Awake()
  {
    Instance = this;
  }

  void OnDestroy()
  {
    if (Instance == this)
      Instance = null;
  }

  public void RegisterGoal(BallLastTouch touch)
  {
    if (goalLock)
      return;
    goalLock = true;

    int pts = 2;
    string label = "Goal +2";

    if (touch != null)
    {
      if (touch.LastTouchWasAerial && touch.LastTouchFromDunkZone)
      {
        pts = 4;
        label = "Dunk +4";
      }
      else if (touch.LastTouchWasAerial)
      {
        pts = 3;
        label = "Aerial +3";
      }
    }

    score += pts;
    if (scoreLabel != null)
      scoreLabel.text = $"Score: {score}";
    if (lastGoalLabel != null)
      lastGoalLabel.text = label;

    StartCoroutine(ResetRoutine());
  }

  IEnumerator ResetRoutine()
  {
    yield return new WaitForSeconds(resetDelay);
    ResetRoundPositions();
  }

  public void QuickResetBall()
  {
    if (lastGoalLabel != null)
      lastGoalLabel.text = "Out of bounds — reset";
    ResetRoundPositions();
  }

  void ResetRoundPositions()
  {
    goalLock = false;
    if (ballBody != null && ballSpawn != null)
    {
      ballBody.linearVelocity = Vector3.zero;
      ballBody.angularVelocity = Vector3.zero;
      ballBody.transform.position = ballSpawn.position;
      var touch = ballBody.GetComponent<BallLastTouch>();
      touch?.Clear();
    }

    if (car != null && carSpawn != null)
    {
      car.TeleportTo(carSpawn.position, carSpawn.rotation);
    }
  }
}
