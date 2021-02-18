using System.Collections;
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
            if (Physics.Raycast(new Vector3(obj.position.x, .5f, obj.position.z), dir, out RaycastHit hit, 100f, layerMask))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    GameManager.Instance.CanPlay = false;
                    obj.DOMove(new Vector3(hit.collider.transform.position.x, obj.position.y, hit.collider.transform.position.z) + dir * (-1), 1).OnComplete(() =>
                    {
                        GameManager.Instance.CanPlay = true;
                    });
                }
            }
        }
    }
}
