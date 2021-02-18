using UnityEngine;

public class Hole : MonoBehaviour
{
    void Start()
    {
        ControllableObjects.controllableObjects.Add(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("ball");
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ball"), LayerMask.NameToLayer("Ground"), true);

            Rigidbody ball_rb = other.GetComponent<Rigidbody>();
            ball_rb.isKinematic = false;
            ball_rb.useGravity = true;
        }
    }
}