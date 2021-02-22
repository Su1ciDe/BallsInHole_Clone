using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public int Value = 1;

    void Start()
    {
        transform.DORotate(360 * Vector3.up, 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}