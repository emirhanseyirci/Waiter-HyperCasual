using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPlatePickUpTriggered : MonoBehaviour
{
    //platepickupco
   // public static PlateFollowerLeft Instance;
    private bool isTriggered=false; //temas edilip edilmediğini kontrol ediyoruz false ise pickupdirtyleftfonk çalışması için debug???!
    //private float targetYPosition = 0.01f;
    //private float duration = 0.2f;


    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //}

   
    public void vibr(Transform stackX)
    {
        float duration = 0.4f;
        float targetYPosition = this.transform.position.y + 0.0005f;
        float targetXPosition = stackX.transform.position.x;
        transform.DOMoveY(targetYPosition, duration).SetLoops(-2, LoopType.Yoyo);
        transform.DOMoveX(targetXPosition, 1f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&isTriggered==false)
        {
            Debug.Log("temas etti");
            //PlatePickUpLeft pickLeft;
            //if (other.TryGetComponent(out pickLeft))
            //{
            //    pickLeft.PickUpDirtyLeft(this.transform);
            //}
            LeftStackControl.Instance.PickUpDirtyLeft(this.transform);
            isTriggered = true;


        }
    }

    public void ToDrop()
    {
        LeftStackControl.Instance.DropToKitchen(this.transform);
    }


    



}
