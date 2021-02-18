using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Vector3 mouseStartPos;
    private float swipeThreshold;

    void Start()
    {
        swipeThreshold = Screen.width * 5 / 100;
    }

    void Update()
    {
        PlayerControls();
    }

    private void PlayerControls()
    {
        if (!GameManager.Instance.CanPlay)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 swipe = Input.mousePosition - mouseStartPos;
            if (swipe.magnitude > swipeThreshold)
            {
                Vector3 dir = swipe.normalized;

                if (Mathf.RoundToInt(dir.y) == 1) //up
                {
                    ControllableObjects.Instance.Move(new Vector3(0, 0, 1));
                }
                else if (Mathf.RoundToInt(dir.y) == -1) //down
                {
                    ControllableObjects.Instance.Move(new Vector3(0, 0, -1));
                }
                else if (Mathf.RoundToInt(dir.x) == 1) //right
                {
                    ControllableObjects.Instance.Move(new Vector3(1, 0, 0));
                }
                else if (Mathf.RoundToInt(dir.x) == -1) //left
                {
                    ControllableObjects.Instance.Move(new Vector3(-1, 0, 0));
                }
            }
        }
    }
}
