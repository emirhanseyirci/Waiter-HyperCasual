using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pltpickuptest : MonoBehaviour
{

    public static pltpickuptest Instance;
    private Vector3 currentCubePos_left;
    private Vector2 currentCubePos_right;

   // public Vector3 thisobjectPos;

    //public Transform thisobjectTransform;

    private Vector3 stackEmptyPos_left;
    public Transform stackemptyobjPTransform_left;

    private Vector3 stackEmptyPos_right;
    public Transform stackemptyobjPTransform_right;

    public bool isFirstCube;

    public List<GameObject> cubeObjects_left;
    public List<GameObject> cubeObjects_right;


    public int left_cubeListIndexCounter = 0;
    public int right_cubeListIndexCounter = 0;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }


    void Start()
    {
      //  thisobjectTransform.position = thisobjectPos;
       // stackemptyobjPTransform.position = stackEmptyPos;
        stackemptyobjPTransform_left.position = stackEmptyPos_left;
        stackemptyobjPTransform_right.position=stackEmptyPos_right;
        isFirstCube = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left_dirty"))
        {
            cubeObjects_left.Add(other.gameObject);
            if (cubeObjects_left.Count==1)
            {
                currentCubePos_left = new Vector3(other.transform.position.x, stackEmptyPos_left.y + 1, other.transform.position.z);
                other.gameObject.transform.position = currentCubePos_left;
                currentCubePos_left = new Vector3(other.transform.position.x, other.transform.position.y +1f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
               // other.gameObject.GetComponent<pltfllowertest>().followlastcubepos(stackemptyobjPTransform_left, true);
               other.gameObject.GetComponent<pltfllowertest>().startEqualposit(stackemptyobjPTransform_left, true);
            }
            else if (cubeObjects_left.Count>1)
            {
                other.gameObject.transform.position = currentCubePos_left;
                currentCubePos_left = new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                other.gameObject.GetComponent<pltfllowertest>().followlastcubepos(cubeObjects_left[left_cubeListIndexCounter].transform,true);
                left_cubeListIndexCounter++;
            }


        }
        else if (other.CompareTag("right_dirty"))
        {
            
            cubeObjects_right.Add(other.gameObject);
            if (cubeObjects_right.Count==1)
            {
                currentCubePos_right = new Vector3(other.transform.position.x, stackEmptyPos_right.y + 1, other.transform.position.z);
                other.gameObject.transform.position = currentCubePos_right;
                currentCubePos_right = new Vector3(other.transform.position.x, other.transform.position.y +1f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                //other.gameObject.GetComponent<pltfllowertest>().followlastcubepos(stackemptyobjPTransform_right, true);
                other.gameObject.GetComponent<pltfllowertest>().startEqualposit(stackemptyobjPTransform_right, true);
            }
            else if (cubeObjects_left.Count>1)
            {
                other.gameObject.transform.position = currentCubePos_right;
                currentCubePos_right = new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                other.gameObject.GetComponent<pltfllowertest>().followlastcubepos(cubeObjects_right[right_cubeListIndexCounter].transform,true);
                right_cubeListIndexCounter++;
            }
        }


    }




}
