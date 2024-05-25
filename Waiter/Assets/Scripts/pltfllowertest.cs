using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pltfllowertest : MonoBehaviour
{

    public static pltfllowertest Instance;
    public Transform cubePos;
    [SerializeField] private float cubeFollowSpeed;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        cubeFollowSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cubePosESİT();
        }
    }

    void cubePosESİT()
    {
        transform.position=new Vector3(cubePos.transform.position.x,
            cubePos.transform.position.y+1,cubePos.transform.position.z);
    }

    public void followlastcubepos(Transform lastcubetrans,bool isfollowStart)
    {
        

        StartCoroutine(StartFollowLastCubePos(lastcubetrans,isfollowStart));

    }

    public void startEqualposit(Transform stackTransform, bool stackStart)
    {
        StartCoroutine(Equalpos(stackTransform, stackStart));
    }


    IEnumerator Equalpos(Transform stackTransform, bool stackStart)
    {
        if (stackTransform == null)
        {
            yield break;
        }

        while (stackStart)
        {
            yield return new WaitForEndOfFrame();

           // transform.position = stackTransform.position;
            transform.position=new Vector3(stackTransform.position.x,stackTransform.position.y,stackTransform.position.z);
            transform.eulerAngles = stackTransform.eulerAngles;
        }


    }


    IEnumerator StartFollowLastCubePos(Transform lastcubetrans,bool followStart)
    {

        if (lastcubetrans==null)
        {
            yield break;
        }

        while (followStart)
        {
            yield return new WaitForEndOfFrame();

            //if (pltpickuptest.Instance.cubeObjects_left.Count==1)
            //{
            //    transform.position = new Vector3(lastcubetrans.position.x, lastcubetrans.position.y, lastcubetrans.position.z);
            //    transform.eulerAngles = lastcubetrans.eulerAngles;
            //}
            //else if(pltpickuptest.Instance.cubeObjects_left.Count>1)
            //{
            //    transform.position = new Vector3(Mathf.Lerp(transform.position.x, lastcubetrans.position.x, cubeFollowSpeed * Time.deltaTime),
            // transform.position.y, lastcubetrans.position.z);
            //    transform.eulerAngles = lastcubetrans.eulerAngles;
            //}

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, lastcubetrans.position.x, cubeFollowSpeed * Time.deltaTime),
              transform.position.y, lastcubetrans.position.z);
            transform.eulerAngles = lastcubetrans.eulerAngles;
        }



    }








}
