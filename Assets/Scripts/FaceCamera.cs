using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] private string targetTag = "MainCamera";
    private Transform target;

    private void Start()
    {
        // Find the first object with the specified tag
        GameObject obj = GameObject.FindGameObjectWithTag(targetTag);
        if (obj != null)
            target = obj.transform;
    }

    private void LateUpdate()
    {
        Vector3 direction = target.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-direction);
            transform.rotation = targetRotation;
        }
    }
}
