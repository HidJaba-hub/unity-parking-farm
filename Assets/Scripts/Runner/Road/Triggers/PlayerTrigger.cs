using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    protected PlayerMove PlayerMove;

    private void Awake()
    {
        GameObject player = gameObject.transform.parent.gameObject;
        player.TryGetComponent<PlayerMove>(out PlayerMove);
    }
}
