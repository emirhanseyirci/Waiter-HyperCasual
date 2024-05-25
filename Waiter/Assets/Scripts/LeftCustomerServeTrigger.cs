using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCustomerServeTrigger : MonoBehaviour
{//customer servetriggered
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("leftcustomertriggered");
            LeftStackControl.Instance.plates_left[LeftStackControl.Instance.plates_left.Count - 1].gameObject.GetComponent<PlateServeLeft>().ToServe(this.transform);
        }
    }
}
