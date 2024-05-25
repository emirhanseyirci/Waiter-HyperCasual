using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePickUpRight : MonoBehaviour
{

    public static PlatePickUpRight Instance;
    private Vector3 currentPlatePos_right;
    // public Vector3 thisobjectPos;
    //public Transform thisobjectTransform;
    public Vector3 stackEmptyPos_right;
    public Transform stackemptyObjectTransform_right;



    public List<GameObject> plates_right;


    public int right_plateListIndexCounter = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        //  thisobjectTransform.position = thisobjectPos;
        // stackemptyobjPTransform.position = stackEmptyPos;
        //stackemptyobjPTransform_left.position = stackEmptyPos_left;
        //  stackemptyobjPTransform_left.position=new Vector3(0,0,0);
        stackEmptyPos_right = stackemptyObjectTransform_right.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            rightDirtyPlates_put();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ServeRight();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
           // ObjectPoolingRight.Instance.StartCoroutine(ObjectPoolingRight.Instance.ObjectPoolingProcess_right());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("right_dirty"))
        {
            //other.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            plates_right.Add(other.gameObject);
            if (plates_right.Count == 1)
            {
                Debug.Log("temas etti ");
                currentPlatePos_right = new Vector3(other.transform.position.x,
                    stackEmptyPos_right.y, other.transform.position.z);
                // currentCubePos_left = new Vector3(other.transform.position.x, stackEmptyPos_left.y + 1, other.transform.position.z);
                other.gameObject.transform.position = currentPlatePos_right;
                currentPlatePos_right = new Vector3(other.transform.position.x, other.transform.position.y + 0.001f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                                                                                                                                            // other.gameObject.GetComponent<pltfllowertest>().followlastcubepos(stackemptyobjPTransform_left, true);
                other.gameObject.GetComponent<PlateFollowerRight>().startEqualPosition(stackemptyObjectTransform_right, true);
            }
            else if (plates_right.Count > 1)
            {
                other.gameObject.transform.position = currentPlatePos_right;
                currentPlatePos_right = new Vector3(other.transform.position.x, other.transform.position.y + 0.001f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                other.gameObject.GetComponent<PlateFollowerRight>().followLastPlatePos(plates_right[right_plateListIndexCounter].transform, true);
                right_plateListIndexCounter++;
            }


        }



    }



    public void rightDirtyPlates_put()
    {
        for (int i = plates_right.Count - 1; i >= 0; i--)
        {
            GameObject willPut_right= plates_right[i];
            Destroy(willPut_right);
            plates_right.RemoveAt(i);
        }

        right_plateListIndexCounter = 0;
    }



    public void ServeRight()
    {
        Destroy(plates_right[plates_right.Count - 1].gameObject);
        plates_right.RemoveAt(plates_right.Count - 1);
        right_plateListIndexCounter--;

        if (right_plateListIndexCounter < 0)
        {
            right_plateListIndexCounter = 0;
        }

    }


}
