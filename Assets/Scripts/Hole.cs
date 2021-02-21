using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void Start()
    {
        ControllableObjects.controllableObjects.Add(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagEnums.Ball.ToString()))
            StartCoroutine(NestTheBall(other.GetComponent<Rigidbody>()));
    }

    private IEnumerator NestTheBall(Rigidbody ball_rb)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ball"), LayerMask.NameToLayer("Ground"), true);

        ball_rb.isKinematic = false;
        ball_rb.useGravity = true;

        yield return new WaitForSeconds(1f);

        ball_rb.useGravity = false;
        ball_rb.isKinematic = true;
    }
}