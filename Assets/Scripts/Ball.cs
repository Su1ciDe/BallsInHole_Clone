using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    void Start()
    {
        ControllableObjects.controllableObjects.Add(transform);
    }

    public void Roll(Vector3 dir, float time)
    {
        //transform.DORotate();
    }
}
