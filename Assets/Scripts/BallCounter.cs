using UnityEngine;

public class BallCounter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagEnums.Ball.ToString()))
        {
            other.tag = TagEnums.Untagged.ToString();

            GameManager.Instance.BallCollected++;

            ControllableObjects.controllableObjects.Remove(other.transform);
            other.transform.parent = transform.parent;

            GameManager.Instance.CheckIfLevelCompleted();
        }
    }
}