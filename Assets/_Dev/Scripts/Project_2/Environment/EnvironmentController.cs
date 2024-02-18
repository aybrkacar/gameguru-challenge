using System.Collections;
using UnityEngine;
using Project2.General;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] private float _moveDistance = 570f;
    private void Start()
    {
        StartCoroutine(CheckObjectVisibility());
    }

    private IEnumerator CheckObjectVisibility()
    {
        WaitForSeconds waitTime = new(5f);

        while (true)
        {
            if (!IsObjectVisible())
            {
                MoveObjectForward();
            }

            yield return waitTime;
        }
    }

    bool IsObjectVisible()
    {
        return GameManager.Instance.PlayerController.transform.position.z <= transform.position.z;
    }

    void MoveObjectForward()
    {
        transform.position += Vector3.forward * _moveDistance;
    }
}
