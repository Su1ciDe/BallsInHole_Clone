using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControllableObjects : SingletonBehaviour<ControllableObjects>
{
    public static List<Transform> controllableObjects = new List<Transform>();
    public LayerMask layerMask;

    [HideInInspector] public Vector3 previousDir;

    public void Move(Vector3 dir)
    {
        previousDir = dir;

        foreach (Transform obj in controllableObjects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            MoveFunction(rb, new Vector3(rb.position.x, .5f, rb.position.z), dir, Ease.InCubic);
        }
    }

    public void MoveFunction(Rigidbody rb, Vector3 startingPos, Vector3 dir, Ease ease = Ease.Linear)
    {
        if (Physics.Raycast(startingPos, dir, out RaycastHit hit, 100f, layerMask))
        {
            GameManager.Instance.CanPlay = false;

            float dist = 0;
            if (dir == Vector3.right || dir == Vector3.left)
                dist = (hit.collider.transform.position - rb.position).x * dir.x;
            if (dir == Vector3.forward || dir == Vector3.back)
                dist = (hit.collider.transform.position - rb.position).z * dir.z;

            rb.GetComponent<Ball>()?.Roll(dir, dist); // for ball's rotation

            rb.DOMove(new Vector3(hit.collider.transform.position.x, rb.position.y, hit.collider.transform.position.z) + dir * (-1), dist * .04f).SetEase(ease).OnComplete(() =>
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