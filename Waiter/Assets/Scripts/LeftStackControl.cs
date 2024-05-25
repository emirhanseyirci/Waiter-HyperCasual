using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeftStackControl : MonoBehaviour
{
    //LEFTSTACKCONTROL
    public static LeftStackControl Instance;
    //public Vector3 stackEmptyPos_left;
    public Transform stackPosition_left;
    public Transform KitchenDropPosition_left;
    public Transform mealStackPossleft;

    public List<GameObject> plates_left;
    public Transform finishLineStack;
    public Transform activatedObjectsTransform;
    public int stackedLeftDirtyPlateCount=0;
     public int left_plateListIndexCounter = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Update()
    {
        buttonTest();
    }


    private void buttonTest()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(DirtyPlatesDropProcess());
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            toFinishlinemealStackTransform();
        }
        
       
        
    }




    public void PickUpDirtyLeft(Transform _dirtyPlateLeft)
    {
        Debug.Log("listeye eklendi");

        if (!plates_left.Contains(_dirtyPlateLeft.gameObject))
        {
            plates_left.Add(_dirtyPlateLeft.gameObject);

        }

        _dirtyPlateLeft.DORotateQuaternion(stackPosition_left.rotation, 0.15f).OnComplete(
            () =>
            {
                _dirtyPlateLeft.DOScale(new Vector3(2.25f, 2.25f, 2.25f), 0.1f);

                _dirtyPlateLeft.DOJump(stackPosition_left.position + new Vector3(0, 0.003f * left_plateListIndexCounter, 0), 0.05f, 1, 0.3f).OnComplete(
          () => {
              _dirtyPlateLeft.SetParent(stackPosition_left, true);
              _dirtyPlateLeft.localPosition = new Vector3(0, 0.003f * left_plateListIndexCounter, 0);
              _dirtyPlateLeft.localRotation = Quaternion.identity;
              //  _dirtyPlateLeft.gameObject.GetComponent<PlateFollowerLeft>().vibr(stackPosition_left);
              // PlateFollowerLeft.Instance.vibr();
              left_plateListIndexCounter++;
              stackedLeftDirtyPlateCount++;

              //MEYİLLENME FONKSİYONU+

          });
            });
       
       

    }



    //listenin en üstündeki objeyi belirlediğim serve pozisyonuna dotween metodu ile göndermek
    public void DropToKitchen(Transform _droppedDirtyPlateLeft)
    {

        _droppedDirtyPlateLeft.DOJump(KitchenDropPosition_left.position,0.07f,1 ,0.5f).OnComplete(
           () => {
               _droppedDirtyPlateLeft.DOScale(new Vector3(3.6f,3.6f,3.6f), 0.1f);
               _droppedDirtyPlateLeft.SetParent(KitchenDropPosition_left, true);
               Debug.Log("leftplatelistindexcount" + "  :" + left_plateListIndexCounter);
               _droppedDirtyPlateLeft.localPosition = new Vector3(0, -0.005f * left_plateListIndexCounter, 0);
              // _droppedDirtyPlateLeft.localPosition = new Vector3(0, 2f, 0);

               Debug.Log("sıraya eklendi");
               left_plateListIndexCounter--;


           });


    }


    public void DropLeft()
    {
        if (plates_left.Count>=1)
        {
            plates_left[plates_left.Count - 1].gameObject.GetComponent<LeftPlatePickUpTriggered>().ToDrop();

            plates_left.RemoveAt(plates_left.Count-1);

            Debug.Log(left_plateListIndexCounter);

        }
   
    }


    public IEnumerator DirtyPlatesDropProcess()
    {
        Debug.Log("leftplatelistindexcount" + " process öncesi :" + left_plateListIndexCounter);

        for (int i = plates_left.Count - 1; i >= 0; i--)
        {
            DropLeft();
            yield return new WaitForSeconds(0.2f);
        }

        ObjectPoolingLeft.Instance.StartCoroutine(ObjectPoolingLeft.Instance.ObjectPoolingProcess_left());
    }

   
    public void toActivatedObjectTransform()
    {
           KitchenDropPosition_left.transform.gameObject.SetActive(false);
        KitchenDropPosition_left.transform.position = activatedObjectsTransform.position;
    }
    
   public void toFinishLineStackTransform() //finishlinea temas edilince çalışacak
    {
        KitchenDropPosition_left.transform.gameObject.SetActive(true);
        KitchenDropPosition_left.transform.DOMoveY(finishLineStack.position.y, 0.5f); /*completed olunca kamera biraz sarsılsın
                                                                                    level sonu ui açılsın hesaplanan puana göre kaç tane yıldız varsa aktif olsun*/
    }


    public void toFinishlinemealStackTransform()
    {
        // mealStackPossleft.transform.gameObject.SetActive(true);

        for (int i = 0; i < mealStackPossleft.childCount; i++)
        {
            GameObject child = mealStackPossleft.GetChild(i).gameObject;
            child.SetActive(true);
        }
        mealStackPossleft.transform.DOMoveY(finishLineStack.position.y, 0.5f);
    }


}
