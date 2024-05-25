using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pltpickuptestright : MonoBehaviour
{
    private Vector2 currentCubePos_right;
    public Vector3 stackEmptyPos_right;
    public Transform stackemptyobjPTransform_right;
    public List<GameObject> cubeObjects_right;
    public int right_cubeListIndexCounter = 0;
    public static pltpickuptestright Instance;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }

        Debug.Log(stackEmptyPos_right);
    }

    void Start()
    {
        // stackemptyobjPTransform_right.position = new Vector3(1,0,0);
        //stackemptyobjPTransform_right.position = stackEmptyPos_right;
        stackEmptyPos_right = stackemptyobjPTransform_right.position;
       // stackemptyobjPTransform_right.position=new Vector3(stackEmptyPos_right.x, stackEmptyPos_right.y, stackEmptyPos_right.z);
        Debug.Log(stackEmptyPos_right);
        


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            rightDirtyPlates_put();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ServeRight();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("right_dirty"))
        {



            Debug.Log("ilk plate objenin üstüne temas etti");

            //other.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            cubeObjects_right.Add(other.gameObject);
            if (cubeObjects_right.Count == 1)
            {
                //currentCubePos_right = new Vector3(other.transform.position.x, stackemptyobjPTransform_right.position.y+1, other.transform.position.z);

                currentCubePos_right = new Vector3(other.transform.position.x, stackEmptyPos_right.y , other.transform.position.z);
                other.gameObject.transform.position = currentCubePos_right;
                currentCubePos_right = new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                //other.gameObject.GetComponent<pltfllowertest>().followlastcubepos(stackemptyobjPTransform_right, true);
                other.gameObject.GetComponent<pltlfllowertestright>().startEqualposit(stackemptyobjPTransform_right, true);
            }
            else if (cubeObjects_right.Count > 1)
            {
                other.gameObject.transform.position = currentCubePos_right;
                currentCubePos_right = new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z); //bir osnraki objenin yüksekliğini verip
                other.gameObject.GetComponent<pltlfllowertestright>().followlastcubepos(cubeObjects_right[right_cubeListIndexCounter].transform, true);
                right_cubeListIndexCounter++;
            }
        }

    }




    public void rightDirtyPlates_put()
    {
        for (int j = cubeObjects_right.Count - 1; j >= 0; j--)
        {
            GameObject willPut_right = cubeObjects_right[j];
            Destroy(willPut_right);
            cubeObjects_right.RemoveAt(j);
        }
        right_cubeListIndexCounter = 0;
    }


    public void ServeRight()
    {
        Destroy(cubeObjects_right[cubeObjects_right.Count-1].gameObject);
        cubeObjects_right.RemoveAt(cubeObjects_right.Count - 1);
        right_cubeListIndexCounter--;

        if (right_cubeListIndexCounter<0)
        {
            right_cubeListIndexCounter = 0;
        }

    }




}
