using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoor : MonoBehaviour
{
    public static KitchenDoor Instance;
    public int leftStackedPlateCount; //leftStackedPlateCount
    public int rightStackedPlateCount; //rightStackedPlateCount



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }



    public void ResetPlateCounts()
    {
        leftStackedPlateCount = 0;
        rightStackedPlateCount = 0;
    }



    public void IncreaseLeftStackedPlateCount()
    {
        leftStackedPlateCount++;
        PlayerMovement.Instance.LeftInclination();
    }

    public void IncreaseRightStackedPlateCount()
    {
        rightStackedPlateCount++;
        PlayerMovement.Instance.RightInclination();

    }

    public void DecreaseLeftStackedPlateCount()
    {
        leftStackedPlateCount--;
        PlayerMovement.Instance.LeftInclination();

    }

    public void DecreaseRightStackedPlateCount()
    {
        rightStackedPlateCount--;
        PlayerMovement.Instance.RightInclination();

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlatePickUp.Instance.KitchenDirtyPlates_Put(); //objeleri silme  fonksiyonu
            Debug.Log("temas etti");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LeftObjectPooling.Instance.StartCoroutine(LeftObjectPooling.Instance.ObjectPoolingProcess());
            RightObjectPooling.Instance.StartCoroutine(RightObjectPooling.Instance.ObjectPoolingProcess());
           // KitchenDoorAnimState.Instance.triggerSet_close();

        }
    }
}
