using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public enum Type { Teleport, Linear, FeedbackLoop };

    [SerializeField] 
    private Type       type;
    [SerializeField] 
    private float      maxCameraSpeed;
    [SerializeField] 
    private Transform  targetEntity;
    [SerializeField] 
    private Vector3    offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (targetEntity == null)
        {
            var go = FindFirstObjectByType<Player>();
            if (go == null) return;

            targetEntity = go.transform;
        }

        Vector3 targetPos = GetTargetPos();

        targetPos.z = transform.position.z;
        targetPos = targetPos + offset;

        switch (type)
        {
            case Type.Teleport:
                transform.position = targetPos;
                break;
            case Type.Linear:
                transform.position = Vector3.MoveTowards(transform.position, targetPos, maxCameraSpeed * Time.fixedDeltaTime);
                break;
            case Type.FeedbackLoop:
                {
                    Vector3 toDestination = targetPos - transform.position;
                    transform.position = transform.position + toDestination * maxCameraSpeed;
                }
                break;
            default:
                break;
        }
    }

    Vector3 GetTargetPos()
    {
        return targetEntity.position;
    }
}
