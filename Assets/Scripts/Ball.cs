using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    private void Awake()
    {
        transform.parent = ControllableObjects.Instance.transform;
    }

    void Start()
    {
        ControllableObjects.controllableObjects.Add(transform);
        GameManager.Instance.BallCountInLevel++;

        rb = GetComponent<Rigidbody>();
    }

    public void Roll(Vector3 dir, float dist)
    {
        rb.DORotate(360 * new Vector3(dir.z, 0, -dir.x) * (dist / 4), dist * .05f, RotateMode.FastBeyond360).SetEase(Ease.InCubic);
    }

    private void OnTriggerEnter(Collider other)
    {   // killing traps
        if (other.CompareTag(TagEnums.Obstacle.ToString())) 
        {
            other.tag = TagEnums.Untagged.ToString();

            GameManager.Instance.BallCountInLevel--;

            ControllableObjects.controllableObjects.Remove(transform);

            rb.DOKill();
            GameObject effect = ObjectPooler.Instance.SpawnFromPool("Ball_Death", transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);

            GameManager.Instance.CheckIfLevelFailed();
        }
        // collectables
        if (other.CompareTag(TagEnums.Coin.ToString())) 
        {
            other.tag = TagEnums.Untagged.ToString();

            GameManager.Instance.AddScore(other.GetComponent<Coin>().Value);

            Destroy(other.gameObject);
        }
    }
}