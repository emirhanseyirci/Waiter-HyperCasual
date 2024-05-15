using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Pool;
using System;


public class objPooltest : MonoBehaviour
{
    public static objPooltest Instance;
    public int setActiveObjectCount = 0;
    public Transform thisobjecttranss;

    [Serializable]

    public struct Pool
    {
        public Queue<GameObject> pooledObjects; //olu?an objelerimizin tutuldu?u queue
        public GameObject objectPrefab; //olu?acak objenin prefab?
        public int poolSize; //havuzda kaç tane obje bulunaca??n? tutar
    }

    [SerializeField] private Pool[] pools = null;
    [SerializeField] private List<GameObject> poolObjects_Left = new List<GameObject>();


    private Vector3 dequeuedLeftObjectPosition; //setactiva true oldu?unda objenin pozisyonu (left stack pozisyonu)
                                                //private Vector3 dequeue_right_pos; //setactiva true oldu?unda objenin pozisyonu (left stack pozisyonu)



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RandomPool();

        }
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        for (int j = 0; j < pools.Length; j++) //poolsta kaç tne pool varsa
        {
            pools[j].pooledObjects = new Queue<GameObject>(); //o kadar queue s?ras? olu?acak

            for (int i = 0; i < pools[j].poolSize; i++) //pools listesindeki x indexinin poolsize? yani olu?mas? istenen obje say?s? kadar 
            {
                GameObject obj = Instantiate(pools[j].objectPrefab); //x  indexli poolun prefab?n? olu?tur
                obj.SetActive(false);
                poolObjects_Left.Add(obj);
                pools[j].pooledObjects.Enqueue(obj);
            }
        }

    }

    public void RandomPool()
    {
        // Rastgele bir havuz seç
        int randomPoolIndex = Random.Range(0, pools.Length);

        // Seçilen havuzdaki objelerin say?s?
        int numObjectsInPool = pools[randomPoolIndex].pooledObjects.Count;

        // Seçilen havuzda aktif olmayan bir obje varsa
        if (numObjectsInPool > 0)
        {
            GameObject randomObject = GetPooledObject(randomPoolIndex); // Seçilen havuzdan rastgele bir obje al
            if (randomObject != null)
            {
                randomObject.SetActive(true); // Objeyi set aktif et
                setActiveObjectCount++;
                Debug.Log("Rastgele bir obje ça?r?ld?: " + randomObject.name + "SetActiveObjectCount: " + setActiveObjectCount);
            }
            else
            {
                Debug.LogWarning("Rastgele bir obje ça?r?lamad?!");
            }
        }
        else // Seçilen havuzda aktif olmayan bir obje yoksa
        {
            // Havuzun sonunda de?ilse bir sonraki havuzu kontrol et
            if (randomPoolIndex < pools.Length - 1)
            {
                RandomPool();
            }
            else
            {
                Debug.Log("Tüm havuzlar bo?!");
            }
        }
    }





    public GameObject GetPooledObject(int objectType)
    {

        if (objectType >= pools.Length)
        {
            return null;
        }

        GameObject obj = pools[objectType].pooledObjects.Dequeue(); //queueun en ba??ndaki obje s?radan ç?kar?l?r.
        obj.SetActive(true); //görünür hale getirilir
                             //pools[objectType].pooledObjects.Enqueue(obj); //queue s?ras?na geri eklenir arkadan. tekrar kullanabilmek için.
                             //  obj.transform.position = PlatePickUp.Instance.leftStackPosition; //toplan?lan objenin pozisyonu leftstack pozisyonuna e?itlenir
        PlatePickUp.Instance.leftPlates.Add(obj);

        if (PlatePickUp.Instance.leftPlates.Count == 0) //left empty objecti listenin içindedir..(de?il art?k...!!!!!).
        {

            dequeuedLeftObjectPosition = new Vector3(thisobjecttranss.position.x, thisobjecttranss.position.y + thisobjecttranss.position.z);


          
            obj.gameObject.transform.position = dequeuedLeftObjectPosition;
           

            dequeuedLeftObjectPosition = new Vector3(obj.transform.position.x,
                obj.transform.position.y + 0.03f, PlatePickUp.Instance.leftStackPosition.z);

            obj.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(PlatePickUp.Instance.thisobjt, true);

            //obj.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(PlatePickUp.Instance.thisobjt, true);
            Debug.Log("obj pooling:" + "  " + obj.transform.position);

        }
        else if (PlatePickUp.Instance.leftPlates.Count >= 1)
        {
            obj.gameObject.transform.position = dequeuedLeftObjectPosition;
            dequeuedLeftObjectPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 0.03f,
                obj.transform.position.z);
            obj.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(PlatePickUp.Instance.leftPlates[PlatePickUp.Instance.leftPlateListIndexCounter].gameObject.transform, true); //listenin veya stackin üstündeki en üstteki objenin transformu
            PlatePickUp.Instance.leftPlateListIndexCounter++;
            Debug.Log("obj pooling:" + "  " + obj.transform.position);
        }

        KitchenDoor.Instance.IncreaseLeftStackedPlateCount();
        return obj;
    }
}
