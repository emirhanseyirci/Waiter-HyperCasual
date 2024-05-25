using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoorAnimState : MonoBehaviour
{
    public static KitchenDoorAnimState Instance;

    private Animator kitchenDoorAnimator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        kitchenDoorAnimator = GetComponent<Animator>();
    }

    public void TriggerSetOpen()
    {
        kitchenDoorAnimator.SetTrigger("TrOpen");

    }

    public void TriggerSetClose() //object pooling process bitince veya leftstackpos setactivefalse olduğunda kapat
    {
        kitchenDoorAnimator.SetTrigger("TrClose");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerSetOpen();
        }
    }

    




}
