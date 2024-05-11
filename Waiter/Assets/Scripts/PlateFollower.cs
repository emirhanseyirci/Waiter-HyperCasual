using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFollower : MonoBehaviour
{
    [SerializeField] private float plateFollowSpeed;


    public void FollowLastPlatePosition(Transform followedPlateTransform, bool isFollowStart)
    {
        StartCoroutine(StartFollowingLastPlatePosition(followedPlateTransform, isFollowStart));
    }


    IEnumerator StartFollowingLastPlatePosition(Transform followedPlateTransform, bool isFollowStart)
    {
        if (followedPlateTransform == null)
        {
            // Nesne yok edilmiş, Coroutine'i sonlandır
            yield break;
        }

        while (isFollowStart)
        {
            yield return new WaitForEndOfFrame();
            //transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedPlate.position.x, followSpeed * Time.deltaTime),
            //transform.position.y, //kitchen doorda dequeueleftposda yüksekliği arttırdık zaten
            //Mathf.Lerp(transform.position.z, followedPlate.position.z, followSpeed * Time.deltaTime));

            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedPlateTransform.position.x, plateFollowSpeed * Time.deltaTime),
            transform.position.y, followedPlateTransform.position.z);

            // transform.rotation = followedPlate.rotation;
            transform.eulerAngles = followedPlateTransform.eulerAngles;
        }
    }


}
