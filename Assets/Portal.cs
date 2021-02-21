using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Portal : MonoBehaviour //Cake is a lie
{
    [SerializeField] private float beamingSpeed = .25f;

    void Start()
    {
        GameManager.Portals.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag(TagEnums.Portal.ToString()) && other.CompareTag(TagEnums.Ball.ToString()))
            StartCoroutine(BeamMeUpScotty(other.GetComponent<Rigidbody>()));
    }

    private void OnTriggerExit(Collider other)
    {
        tag = TagEnums.Portal.ToString();
    }

    private IEnumerator BeamMeUpScotty(Rigidbody ball_rb)
    {
        GameManager.Instance.CanPlay = false;

        Portal exitPortal = GameManager.Portals.IndexOf(this) == 0 ? GameManager.Portals[1] : GameManager.Portals[0];
        exitPortal.tag = TagEnums.Untagged.ToString();

        ball_rb.DOKill();

        float prevScale = ball_rb.transform.localScale.x;
        yield return ball_rb.transform.DOScale(0, beamingSpeed).SetEase(Ease.InQuint).WaitForCompletion();
        ball_rb.position = new Vector3(exitPortal.transform.position.x, .5f, exitPortal.transform.position.z);
        yield return ball_rb.transform.DOScale(prevScale, beamingSpeed).SetEase(Ease.InQuint).WaitForCompletion();

        Vector3 dir = ControllableObjects.Instance.previousDir;
        ControllableObjects.Instance.MoveFunction(ball_rb, new Vector3(exitPortal.transform.position.x, .5f, exitPortal.transform.position.z), dir);
    }
}