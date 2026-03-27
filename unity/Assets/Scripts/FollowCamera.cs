using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  [SerializeField] Transform target;
  [SerializeField] Vector3 worldOffset = new Vector3(0f, 5.5f, -11f);
  [SerializeField] float followSharpness = 6f;
  [SerializeField] Vector3 lookOffset = new Vector3(0f, 1.4f, 0f);

  public void Bind(Transform followTarget)
  {
    target = followTarget;
  }

  void LateUpdate()
  {
    if (target == null)
      return;

    var desired = target.position + worldOffset;
    transform.position = Vector3.Lerp(transform.position, desired, 1f - Mathf.Exp(-followSharpness * Time.deltaTime));
    transform.LookAt(target.position + lookOffset, Vector3.up);
  }
}
