using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Vector3 endPos;
    [SerializeField] private Collider _collider;

    private void Start()
    {
        var position = transform.position;
        endPos = new Vector3(position.x, -2f, position.z);
    }

    private IEnumerator GateOpenAnimation()
    {
        transform.DOMove(endPos, 2);
        yield return new WaitForSeconds(2);
        _collider.enabled = false;
    }

    public void GateOpen()
    {
        StartCoroutine(GateOpenAnimation());
    }    
}
