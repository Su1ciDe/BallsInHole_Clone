using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControllableObjects : SingletonBehaviour<ControllableObjects>
{
    public static List<Transform> controllableObjects = new List<Transform>();
    public LayerMask layerMask;

    public void Move(Vector3 dir)
    {
        foreach (Transform obj in controllableObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            if (Physics.Raycast(new Vector3(rb.position.x, .5f, rb.position.z), dir, out RaycastHit hit, 100f, layerMask))
            {
                GameManager.Instance.CanPlay = false;

                float dist = 0;
                if (dir == Vector3.right || dir == Vector3.left)
                    dist = (hit.collider.transform.position - rb.position).x * dir.x;
                if (dir == Vector3.forward || dir == Vector3.back)
                    dist = (hit.collider.transform.position - rb.position).z * dir.z;

                rb.GetComponent<Ball>()?.Roll(dir, dist); // for ball's rotation

                rb.DOMove(new Vector3(hit.collider.transform.position.x, rb.position.y, hit.collider.transform.position.z) + dir * (-1), dist * .04f).SetEase(Ease.InCubic).OnComplete(() =>
                {
                    GameManager.Instance.CanPlay = true;

                    if (rb.CompareTag(TagEnums.Ball.ToString()) && hit.collider.CompareTag(TagEnums.Destroyable.ToString()))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                });
            }
        }
    }
}