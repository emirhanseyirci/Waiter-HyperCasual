using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateServeLeft : MonoBehaviour
{//serve olayı gerçekleşiyor


    private void Start()
    {
    }


    public void ToServe(Transform customerLeft)
    {
        //PlateServeLeft.Instance.ServeLeft();
        //rotasyon düzeltmesi gerekebilir
        this.transform.DOJump(customerLeft.position, 0.07f, 1, 0.5f).OnComplete(
            () =>
            {
                this.transform.SetParent(null);
                StartCoroutine(tomealstackPos());
                //MEYİLLENME FONKSİYONU-

            });
        LeftStackControl.Instance.plates_left.RemoveAt(LeftStackControl.Instance.plates_left.Count - 1);
        LeftStackControl.Instance.left_plateListIndexCounter--;
        ObjectPoolingLeft.Instance.servedPoolsObjectCount++;


    }

      public IEnumerator tomealstackPos()
    {
        GameObject mealStackPosLeft = GameObject.FindWithTag("mealStack");
        yield return new WaitForSeconds(0.9f);
      //  this.transform.gameObject.SetActive(false);
        this.transform.SetParent(mealStackPosLeft.transform, true);
        yield return new WaitForSeconds(0.03f);
        this.transform.gameObject.SetActive(false);
        this.transform.DOScale(new Vector3(3.6f,3.6f,3.6f), 0.1f);
        this.transform.localPosition = new Vector3(0, -0.005f * LeftStackControl.Instance.left_plateListIndexCounter,0);

    }



}
