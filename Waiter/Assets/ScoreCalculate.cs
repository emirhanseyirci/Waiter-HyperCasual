using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculate : MonoBehaviour
{
    public static ScoreCalculate Instance;
    public Transform LeftDirtyPlates;
    public  int totalLeftDirtyPlatesCount;
    public int starsCount = 0;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        totalLeftDirtyPlatesCount=LeftDirtyPlates.childCount;
    }
    



    public void CalculateStars(int preparedMealCount, int servedMealCount,int totalLeftDirtyPlatesCount,int stackedLeftDirtyPlatesCount)
    {
       preparedMealCount=ObjectPoolingLeft.Instance.activatedPoolsObjectCount;
        servedMealCount = ObjectPoolingLeft.Instance.servedPoolsObjectCount;
        stackedLeftDirtyPlatesCount = LeftStackControl.Instance.stackedLeftDirtyPlateCount;

        double percentageOfDirtyPlates = (double) LeftStackControl.Instance.stackedLeftDirtyPlateCount / totalLeftDirtyPlatesCount;
        double percentageOfMeal = (double)servedMealCount / preparedMealCount;

        if (percentageOfDirtyPlates >= 0.9 && percentageOfMeal >= 0.7)
        {
            starsCount = 3;
        }
        else if (percentageOfDirtyPlates >= 0.7 && percentageOfMeal >= 0.5)
        {
            starsCount = 2;
        }
        else
        {
            starsCount = 1;
        }


        showStars();
    }


   
    private void showStars()
    {
        Debug.Log(starsCount);
    }





















    //private void Start()
    //{
    //        startLeftDirtyPlatesCount=LeftDirtyPlates.childCount;
    //}

    ///*
    // * leftdirtyplates gameobjectin child sayısı baştaki ve sondaki countunun farkını al
    // */

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        calculateDifference();
    //    }
    //    if (Input.GetKeyDown(KeyCode.V))
    //    {
    //        showStar();
    //    }
    //}

    //private void calculateDifference()
    //{
    //    int finishLeftDirtyPlatesCount = LeftDirtyPlates.childCount;
    //    Debug.Log("finish:" + " " + finishLeftDirtyPlatesCount);
    //    if (startLeftDirtyPlatesCount>finishLeftDirtyPlatesCount)
    //    {
    //        stackedLeftDirtyPlates = startLeftDirtyPlatesCount - finishLeftDirtyPlatesCount;
    //        Debug.Log(stackedLeftDirtyPlates);
    //    }
    //    else if (finishLeftDirtyPlatesCount >startLeftDirtyPlatesCount)
    //    {
    //        stackedLeftDirtyPlates = finishLeftDirtyPlatesCount - startLeftDirtyPlatesCount;
    //        Debug.Log(stackedLeftDirtyPlates);
    //    }
    //}//differencevalue ne kadar büyükse o kadar fazla yıldız ver

    //private void showStar()
    //{
    //    if (stackedLeftDirtyPlates <=2)
    //    {
    //        Debug.Log("1 yıldız");
    //    }
    //    else if (stackedLeftDirtyPlates >2&&stackedLeftDirtyPlates <=4)
    //    {
    //        Debug.Log("2 yıldız");
    //    }
    //    else if (stackedLeftDirtyPlates >4)
    //    {
    //        Debug.Log("3 yıldız");
    //        //restart
    //    }

    //}



}
