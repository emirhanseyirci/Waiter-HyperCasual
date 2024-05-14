using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePickUp : MonoBehaviour
{
    public Vector3 leftStackPosition;
    public Vector3 rightStackPosition;

    private Vector3 currentLeftPlatePosition; //o an temas edilip toplanan objenin pos.
    private Vector3 currentRightPlatePosition;



    public List<GameObject> leftPlates; //stacklenen platelerin listesi
    public List<GameObject> rightPlates; //stacklenen platelerin listesi

    public int leftPlateListIndexCounter = 0;
    public int rightPlateListIndexCounter = 0;

    public Transform firstLeftPlateTransform;
    public Transform firstRightPlateTransform;
    public Transform thisobjt;
    public static PlatePickUp Instance;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

      

    }

    private void Start()
    {
        leftStackPosition = firstLeftPlateTransform.position;
        rightStackPosition = firstRightPlateTransform.position;

        Debug.Log(leftStackPosition);
        Debug.Log(rightStackPosition);


    }








    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left_dirty"))
        {
            if (!leftPlates.Contains(other.gameObject))
            {
                leftPlates.Add(other.gameObject);
                // Diğer işlemler...
                //leftPlates.Add(other.gameObject);
                KitchenDoor.Instance.IncreaseLeftStackedPlateCount();
                if (leftPlates.Count == 1)
                {
                    currentLeftPlatePosition = new Vector3(other.transform.position.x, leftStackPosition.y, other.transform.position.z); //toplanan objenin pozisyonun yüksekliğini ilk objenin yüksekliğine eşitliyoruz
                    other.gameObject.transform.position = currentLeftPlatePosition; //burada bunu atıyoruz
                    currentLeftPlatePosition = new Vector3(other.transform.position.x, transform.position.y + 0.05f, other.transform.position.z); //daha sonrasında o aradaki boşluğu veriyoruz
                    other.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(transform, true);
                }
                else if (leftPlates.Count > 1)
                {
                    other.gameObject.transform.position = currentLeftPlatePosition;
                    currentLeftPlatePosition = new Vector3(other.transform.position.x, other.gameObject.transform.position.y + 0.05f, other.transform.position.z);
                    other.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(leftPlates[leftPlateListIndexCounter].transform, true);
                    leftPlateListIndexCounter++;
                }
            }
        }
        else if (other.CompareTag("right_dirty"))
        {
            if (!rightPlates.Contains(other.gameObject))
            {
                rightPlates.Add(other.gameObject);
                // Diğer işlemler...
                //rightPlates.Add(other.gameObject);
                KitchenDoor.Instance.IncreaseRightStackedPlateCount();
                if (rightPlates.Count == 1)
                {
                    currentRightPlatePosition = new Vector3(other.transform.position.x, rightStackPosition.y, other.transform.position.z);
                    other.gameObject.transform.position = currentRightPlatePosition; ;
                    currentRightPlatePosition = new Vector3(other.transform.position.x, transform.position.y + 0.05f, other.transform.position.z);
                    other.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(transform, true);
                }
                else if (rightPlates.Count > 1)
                {
                    other.gameObject.transform.position = currentRightPlatePosition; ;
                    currentRightPlatePosition = new Vector3(other.transform.position.x, other.gameObject.transform.position.y + 0.05f, other.transform.position.z);
                    other.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(rightPlates[rightPlateListIndexCounter].transform, true);
                    rightPlateListIndexCounter++;
                }
            }





        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ServeToRightCustomer();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ServeToLeftCustomer();
        }
        else
        {

        }

    }

    public void KitchenDirtyPlates_Put()
    {
        for (int i = leftPlates.Count - 1; i > 0; i--)
        {
            GameObject willWash_Left = leftPlates[i];
            Destroy(willWash_Left);
            leftPlates.RemoveAt(i);
        }

        for (int i = rightPlates.Count - 1; i > 0; i--)
        {
            GameObject willWash_right = rightPlates[i];
            Destroy(willWash_right);
            rightPlates.RemoveAt(i);
        }

        leftPlateListIndexCounter = 0;
        rightPlateListIndexCounter = 0;
        KitchenDoor.Instance.ResetPlateCounts();
        PlayerMovement.Instance.ResetBalanceValue();
    }


    public void ServeToLeftCustomer()
    {
        Destroy(leftPlates[leftPlates.Count - 1].gameObject);
        leftPlates.RemoveAt(leftPlates.Count - 1);
        leftPlateListIndexCounter--;
        KitchenDoor.Instance.DecreaseLeftStackedPlateCount();
    }

    public void ServeToRightCustomer()
    {
        Destroy(rightPlates[rightPlates.Count - 1].gameObject);
        rightPlates.RemoveAt(rightPlates.Count - 1);
        rightPlateListIndexCounter--;
        KitchenDoor.Instance.DecreaseRightStackedPlateCount();
    }


}