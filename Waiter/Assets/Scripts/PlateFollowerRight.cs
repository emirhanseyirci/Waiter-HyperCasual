using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFollowerRight : MonoBehaviour
{

    public static PlateFollowerRight Instance;
    public Transform platePos;
    [SerializeField] private float plateFollowSpeed;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        plateFollowSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
    }

   

    public void followLastPlatePos(Transform lastPlateTransform, bool isfollowStart)
    {


        StartCoroutine(StartFollowLastPlatePos(lastPlateTransform, isfollowStart));

    }

    public void startEqualPosition(Transform stackTransform, bool stackStart)
    {
        StartCoroutine(equalPos(stackTransform, stackStart));
    }


    IEnumerator equalPos(Transform stackTransform, bool stackStart)
    {
        if (stackTransform == null)
        {
            yield break;
        }

        while (stackStart)
        {
            yield return new WaitForEndOfFrame();

            // transform.position = stackTransform.position;
            transform.position = new Vector3(stackTransform.position.x, stackTransform.position.y, stackTransform.position.z);
            transform.eulerAngles = stackTransform.eulerAngles;
        }


    }


    IEnumerator StartFollowLastPlatePos(Transform lastPlateTransform, bool followStart)
    {

        if (lastPlateTransform == null)
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

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, lastPlateTransform.position.x, plateFollowSpeed * Time.deltaTime),
              transform.position.y, lastPlateTransform.position.z);
            transform.eulerAngles = lastPlateTransform.eulerAngles;
        }



    }








}
