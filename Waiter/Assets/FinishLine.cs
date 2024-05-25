using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //kameranın pozisyonu yavaşça sondaki stackleri görecek pozisyona domove ile gelsin
            //domovecomplreted olunca tofinish çalışsın
            LeftStackControl.Instance.toFinishLineStackTransform();
            LeftStackControl.Instance.toFinishlinemealStackTransform();

        }
    }

}
