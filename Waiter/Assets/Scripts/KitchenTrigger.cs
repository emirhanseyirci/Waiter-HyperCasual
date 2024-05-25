using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //LeftStackControl.Instance.DropLeft();
            LeftStackControl.Instance.StartCoroutine(LeftStackControl.Instance.DirtyPlatesDropProcess());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // LeftStackControl.Instance.toActivatedObjectTransform();
        }
    }


}
