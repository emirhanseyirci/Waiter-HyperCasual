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
    public Vector3 objPoolTransform;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

      

    }

    private void Start()
    {
        //leftStackPosition = firstLeftPlateTransform.localPosition;
      //  rightStackPosition = firstRightPlateTransform.localPosition;
        leftStackPosition = new Vector3(-0.05f, 0.1f, 0f);
        //  leftStackPosition = firstLeftPlateTransform.localPosition;
      
        firstLeftPlateTransform.position = leftStackPosition;
        rightStackPosition=new Vector3(0.05f,0.1f,0f);
        firstRightPlateTransform.position = rightStackPosition;
        Debug.Log(thisobjt.position);
        objPoolTransform = new Vector3(thisobjt.transform.position.x,thisobjt.transform.position.y, thisobjt.transform.position.z);
        
    }








    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left_dirty"))
        {

            if (!leftPlates.Contains(other.gameObject))
            {
                leftPlates.Add(other.gameObject); //temas ettiğimiz objeyi listeye ekledik
                if (leftPlates.Count == 1) //listede eleman yoksa
                {
                    currentLeftPlatePosition = new Vector3(other.transform.position.x, leftStackPosition.y, other.transform.position.z); //tmes ettiğimiz 
                    other.gameObject.transform.position = currentLeftPlatePosition; //burada bunu atıyoruz
                    currentLeftPlatePosition = new Vector3(other.transform.position.x, other.transform.position.y + 0.01f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                    other.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(firstLeftPlateTransform, true);
                    //leftPlateListIndexCounter++;
                }
                else if (leftPlates.Count > 1)
                {
                    other.gameObject.transform.position = currentLeftPlatePosition;
                    currentLeftPlatePosition = new Vector3(other.transform.position.x, other.gameObject.transform.position.y + 0.01f, other.transform.position.z);
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
                if (rightPlates.Count == 1)
                {
                    currentRightPlatePosition = new Vector3(other.transform.position.x, rightStackPosition.y, other.transform.position.z);
                    other.gameObject.transform.position = currentRightPlatePosition;
                    currentRightPlatePosition = new Vector3(other.transform.position.x, other.transform.position.y + 0.01f, other.transform.position.z);
                    other.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(firstRightPlateTransform, true);
                }
                else if (rightPlates.Count>1)
                {
                    other.gameObject.transform.position = currentRightPlatePosition;
                    currentRightPlatePosition = new Vector3(other.transform.position.x, other.gameObject.transform.position.y + 0.01f, other.transform.position.z);
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
        else if(Input.GetKeyDown(KeyCode.X))
        {
            KitchenDirtyPlates_Put();
        }

        Debug.Log(thisobjt.position);

    }

    public void KitchenDirtyPlates_Put()
    {
        for (int i = leftPlates.Count-1; i >= 0; i--)
        {
            GameObject willWash_Left = leftPlates[i];
            Destroy(willWash_Left);
            leftPlates.RemoveAt(i);
        }

        for (int j = rightPlates.Count-1; j >= 0; j--)
        {
            GameObject willWash_right = rightPlates[j];
            Destroy(willWash_right);
            rightPlates.RemoveAt(j);
        }

        leftPlateListIndexCounter = 0;
        rightPlateListIndexCounter = 0;
        //KitchenDoor.Instance.ResetPlateCounts();
       // PlayerMovement.Instance.ResetBalanceValue();
    }


    public void ServeToLeftCustomer()
    {
        
            Destroy(leftPlates[leftPlates.Count - 1].gameObject);
            leftPlates.RemoveAt(leftPlates.Count - 1);
            leftPlateListIndexCounter--;

        if (leftPlateListIndexCounter<0)
        {
            leftPlateListIndexCounter = 0;
        }
            
            //KitchenDoor.Instance.DecreaseLeftStackedPlateCount();
        

    }

    public void ServeToRightCustomer()
    {
        Destroy(rightPlates[rightPlates.Count - 1].gameObject);
        rightPlates.RemoveAt(rightPlates.Count - 1);
        rightPlateListIndexCounter--;

        if (rightPlateListIndexCounter<0)
        {
            rightPlateListIndexCounter = 0;
        }


      //  KitchenDoor.Instance.DecreaseRightStackedPlateCount();
    }


}