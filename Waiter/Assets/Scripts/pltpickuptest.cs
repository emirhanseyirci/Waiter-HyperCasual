using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pltpickuptest : MonoBehaviour
{

    public static pltpickuptest Instance;
    private Vector3 currentCubePos_left;

   // public Vector3 thisobjectPos;

    //public Transform thisobjectTransform;

    public Vector3 stackEmptyPos_left;
    public Transform stackemptyobjPTransform_left;


    public bool isFirstCube;

    public List<GameObject> cubeObjects_left;


    public int left_cubeListIndexCounter = 0;


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
        //stackemptyobjPTransform_left.position = stackEmptyPos_left;
        //  stackemptyobjPTransform_left.position=new Vector3(0,0,0);
        stackEmptyPos_left = stackemptyobjPTransform_left.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            leftDirtyPlates_put();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ServeLeft();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            objPooling_left.Instance.StartCoroutine(objPooling_left.Instance.ObjectPoolingProcess_left());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left_dirty"))
        {
            //other.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            cubeObjects_left.Add(other.gameObject);
            if (cubeObjects_left.Count==1)
            {
                Debug.Log("temas etti ");
                currentCubePos_left = new Vector3(other.transform.position.x,
                    stackEmptyPos_left.y, other.transform.position.z);
               // currentCubePos_left = new Vector3(other.transform.position.x, stackEmptyPos_left.y + 1, other.transform.position.z);
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
        


    }



    public void leftDirtyPlates_put()
    {
        for (int i = cubeObjects_left.Count-1; i>=0; i--)
        {
            GameObject willPut_left = cubeObjects_left[i];
            Destroy(willPut_left);
            cubeObjects_left.RemoveAt(i);
        }

        left_cubeListIndexCounter = 0;
    }



    public void ServeLeft()
    {
        Destroy(cubeObjects_left[cubeObjects_left.Count - 1].gameObject);
        cubeObjects_left.RemoveAt(cubeObjects_left.Count - 1);
        left_cubeListIndexCounter--;

        if (left_cubeListIndexCounter < 0)
        {
            left_cubeListIndexCounter = 0;
        }

    }


}
