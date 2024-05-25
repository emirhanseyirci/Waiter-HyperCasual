using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Pool;
using DG.Tweening;
using System;

public class ObjectPoolingLeft : MonoBehaviour
{

    public static ObjectPoolingLeft Instance;
    public int setActiveObjectCount = 0;
    public int activatedPoolsObjectCount=0;
    public int servedPoolsObjectCount = 0;



    [Serializable]

    public struct Pool
    {
        public Queue<GameObject> pooledObjects; //oluşan objelerimizin tutulduğu queue
        public GameObject objectPrefab; //oluşacak objenin prefabı
        public int poolSize; //havuzda kaç tane obje bulunacağını tutar
    }




    [SerializeField] private Pool[] pools = null;
    [SerializeField] private List<GameObject> poolObjects_Left = new List<GameObject>();


    private Vector3 dequeuedLeftObjectPosition; //setactiva true olduğunda objenin pozisyonu (left stack pozisyonu)
                                                //private Vector3 dequeue_right_pos; //setactiva true olduğunda objenin pozisyonu (left stack pozisyonu)

    //public Queue<GameObject> servedPlatesQueue;
    //[SerializeField] private List<GameObject> servedPlates_Left = new List<GameObject>();




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        for (int j = 0; j < pools.Length; j++) //poolsta kaç tne pool varsa
        {
            pools[j].pooledObjects = new Queue<GameObject>(); //o kadar queue sırası oluşacak

            for (int i = 0; i < pools[j].poolSize; i++) //pools listesindeki x indexinin poolsizeı yani oluşması istenen obje sayısı kadar 
            {
                GameObject obj = Instantiate(pools[j].objectPrefab); //x  indexli poolun prefabını oluştur
                obj.SetActive(false);
                poolObjects_Left.Add(obj);
                activatedPoolsObjectCount++;
                pools[j].pooledObjects.Enqueue(obj);
            }
        }

    }


    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(ObjectPoolingProcess_left());
            //RandomPool();

        }


    }



    public void RandomPool()
    {
        // Rastgele bir havuz seç
        int randomPoolIndex = Random.Range(0, pools.Length);

        // Seçilen havuzdaki objelerin sayısı
        int numObjectsInPool = pools[randomPoolIndex].pooledObjects.Count;

        // Seçilen havuzda aktif olmayan bir obje varsa
        if (numObjectsInPool > 0)
        {
            GameObject randomObject = GetPooledObject(randomPoolIndex); // Seçilen havuzdan rastgele bir obje al
            if (randomObject != null)
            {
                randomObject.SetActive(true); // Objeyi set aktif et
                setActiveObjectCount++;
                Debug.Log("Rastgele bir obje çağrıldı: " + randomObject.name + "SetActiveObjectCount: " + setActiveObjectCount);
            }
            else
            {
                Debug.LogWarning("Rastgele bir obje çağrılamadı!");
            }
        }
        else // Seçilen havuzda aktif olmayan bir obje yoksa
        {
            // Havuzun sonunda değilse bir sonraki havuzu kontrol et
            if (randomPoolIndex < pools.Length - 1)
            {
                RandomPool();
            }
            else
            {
                Debug.Log("Tüm havuzlar boş!");
            }
        }



    }




    public GameObject GetPooledObject(int objectType)
    {
        Debug.Log("start");
        if (objectType >= pools.Length)
        {
            return null;
        }
        GameObject obj = pools[objectType].pooledObjects.Dequeue(); //queueun en başındaki obje sıradan çıkarılır.
        obj.SetActive(true); //görünür hale getirilir
                             //pools[objectType].pooledObjects.Enqueue(obj); //queue sırasına geri eklenir arkadan. tekrar kullanabilmek için.
                             //  obj.transform.position = PlatePickUp.Instance.leftStackPosition; //toplanılan objenin pozisyonu leftstack pozisyonuna eşitlenir
        obj.transform.position = this.gameObject.transform.position;
        Debug.Log(obj.transform.position);
        LeftStackControl.Instance.plates_left.Add(obj.gameObject);

        if (LeftStackControl.Instance.plates_left.Count==1)
        {

            obj.transform.DOScale(new Vector3(2.25f,2.25f,2.25f), 0.01f);
            obj.transform.DORotateQuaternion(LeftStackControl.Instance.stackPosition_left.rotation, 0.5f).OnComplete(
                () =>
                {
                    obj.transform.DOJump(LeftStackControl.Instance.stackPosition_left.position + new Vector3(0, 0.003f * LeftStackControl.Instance.left_plateListIndexCounter, 0), 0.03f, 1, 0.4f).OnComplete(
               () =>
               {

                   obj.transform.SetParent(LeftStackControl.Instance.stackPosition_left, true);
                   obj.transform.localPosition = new Vector3(0, 0, 0);
                   obj.transform.localRotation = Quaternion.identity;
                   LeftStackControl.Instance.left_plateListIndexCounter++;
                   //MEYİLLENME FONKSİYONU+

               });
                });
        }
        else if (LeftStackControl.Instance.plates_left.Count>1)
        {
            obj.transform.DOScale(new Vector3(2.25f,2.25f,2.25f), 0.01f);
            obj.transform.DORotateQuaternion(LeftStackControl.Instance.stackPosition_left.rotation, 0.5f).OnComplete(
                () =>
                {
                    obj.transform.DOJump(LeftStackControl.Instance.stackPosition_left.position + new Vector3(0, 0.003f * LeftStackControl.Instance.left_plateListIndexCounter, 0), 0.03f, 1, 0.4f).OnComplete(
                 () =>
                 {
                     obj.transform.SetParent(LeftStackControl.Instance.stackPosition_left, true);
                     obj.transform.localPosition = new Vector3(0, 0.003f * LeftStackControl.Instance.left_plateListIndexCounter, 0);
                     obj.transform.localRotation = Quaternion.identity;
                     LeftStackControl.Instance.left_plateListIndexCounter++;
                     //MEYİLLENME FONKSİYONU+
                     Debug.Log("son");
                 });

                });
        }
        // KitchenDoor.Instance.IncreaseLeftStackedPlateCount();
        Debug.Log("finish");
        return obj;
    }



   public IEnumerator ObjectPoolingProcess_left()
    {
        // poolobjects listesinin eleman sayısı setactiveobjectcount'a eşit olana kadar döngüyü devam ettir
        while (poolObjects_Left.Count != setActiveObjectCount)
        {
            RandomPool();
            yield return new WaitForSeconds(0.2f);

        }
        //kapı kapansın
        KitchenDoorAnimState.Instance.TriggerSetClose();
        //toactivated yani kitchendropposleft activatedtrans objesinin pozisyonuna gitsin
        LeftStackControl.Instance.toActivatedObjectTransform();
    }



    public void isEnough()
    {
        int differenceActivtedandServed=activatedPoolsObjectCount - servedPoolsObjectCount;
        //bu fark ne kadar azsa o kadar iyi

    }
}
