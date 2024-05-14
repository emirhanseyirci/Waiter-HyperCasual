using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleTest : MonoBehaviour
{

    public Vector3 leftStackPosition;
    public Vector3 rightStackPosition;

    private Vector3 currentLeftPlatePosition; //o an temas edilip toplanan objenin pos.
    private Vector3 currentRightPlatePosition;

    public List<GameObject> leftPlates; //stacklenen platelerin listesi
    public List<GameObject> rightPlates; //stacklenen platelerin listesi

    public Transform thisobject;
    public static scaleTest Instance;


    public int leftPlateListIndexCounter = 0;
    public int rightPlateListIndexCounter = 0;



    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        leftStackPosition = new Vector3(0f, 0.1f, 0);
        rightStackPosition = new Vector3(0f, 0.1f, 0f);
    }



    private void OnTriggerEnter(Collider other)
    {

        
        if (other.CompareTag("left_dirty"))
        {
            leftPlates.Add(other.gameObject);

            if (leftPlates.Count==1)
            {
                currentLeftPlatePosition = new Vector3(leftStackPosition.x, leftStackPosition.y, leftStackPosition.z); //
                other.gameObject.transform.position = currentLeftPlatePosition;
                currentLeftPlatePosition = new Vector3(other.transform.position.x, transform.position.y + 0.005f, other.transform.position.z);
            }
            else if (leftPlates.Count>1)
            {
                other.gameObject.transform.position = currentLeftPlatePosition;
                currentLeftPlatePosition = new Vector3(
                    leftPlates[leftPlateListIndexCounter].gameObject.transform.position.y + 0.05f,
                    leftPlates[leftPlateListIndexCounter].gameObject.transform.position.z);

               // currentLeftPlatePosition = new Vector3(other.transform.position.x, other.gameObject.transform.position.y + 0.005f, other.transform.position.z);
                leftPlateListIndexCounter++;
            }


           
        }
    }





































    //public Vector3 standardScale = new Vector3(1f, 1f, 1f);



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("left_dirty"))
    //    {
    //        other.gameObject.transform.localScale=standardScale;

    //    }
    //    else if (other.CompareTag("right_dirty"))
    //    {
    //        other.gameObject.transform.localScale = standardScale;

    //    }
    //}

}
