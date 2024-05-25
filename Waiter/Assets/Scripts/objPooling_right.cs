using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Pool;
using System;

public class objPooling_right : MonoBehaviour
{
    public static objPooling_right Instance;
    public int setActiveObjectCount = 0;


    [Serializable]

    public struct Pool
    {
        public Queue<GameObject> pooledObjects; //oluşan objelerimizin tutulduğu queue
        public GameObject objectPrefab; //oluşacak objenin prefabı
        public int poolSize; //havuzda kaç tane obje bulunacağını tutar
    }

    [SerializeField] private Pool[] pools = null;
    [SerializeField] private List<GameObject> poolObjects_Right = new List<GameObject>();


    private Vector3 dequeuedRightObjectPosition; //setactiva true olduğunda objenin pozisyonu (left stack pozisyonu)
                                                //private Vector3 dequeue_right_pos; //setactiva true olduğunda objenin pozisyonu (left stack pozisyonu)



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
                poolObjects_Right.Add(obj);
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
            StartCoroutine(ObjectPoolingProcess_right());
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





        if (objectType >= pools.Length)
        {
            return null;
        }


        if (pools[objectType].pooledObjects.Count == 0)
        {
            return null;
        }



        GameObject obj = pools[objectType].pooledObjects.Dequeue(); //queueun en başındaki obje sıradan çıkarılır.
        obj.SetActive(true); //görünür hale getirilir
                             //pools[objectType].pooledObjects.Enqueue(obj); //queue sırasına geri eklenir arkadan. tekrar kullanabilmek için.
                             //  obj.transform.position = PlatePickUp.Instance.leftStackPosition; //toplanılan objenin pozisyonu leftstack pozisyonuna eşitlenir
        //if (!pltpickuptestright.Instance.cubeObjects_right.Contains(obj.gameObject))
        //{

        //}

        pltpickuptestright.Instance.cubeObjects_right.Add(obj);






        if (pltpickuptestright.Instance.cubeObjects_right.Count == 1) //left empty objecti listenin içindedir..(değil artık...!!!!!).
        {
            //dequeue_left_pos = new Vector3(obj.transform.position.x,
            //    PickUp.Instance.firstLeft_dirtyplatePos.y, obj.transform.position.z);



            dequeuedRightObjectPosition = new Vector3(obj.transform.position.x,
                pltpickuptestright.Instance.stackEmptyPos_right.y,obj.transform.position.z);
            obj.gameObject.transform.position = dequeuedRightObjectPosition;
            //listeden çıkıp setactive true olan objenin pozisyonu tuttuğumuz vector3 değişkenidir bu. bu noktada değiştirdiğimiz tek şey  yüksekliği
            //dequeue_left_pos = new Vector3(obj.transform.position.x,
            //    obj.transform.position.y + 2f, obj.transform.position.z);  //3.5f değeri objeler arasındaki boşluktur. buraya public bir değer verip oyun içinden kontrol edebilirsin

            dequeuedRightObjectPosition = new Vector3(obj.transform.position.x,
                obj.transform.position.y + 1, obj.transform.position.z);

            obj.gameObject.GetComponent<pltlfllowertestright>().startEqualposit(pltpickuptestright.Instance.stackemptyobjPTransform_right, true);

            //obj.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(PlatePickUp.Instance.thisobjt, true);
            Debug.Log("obj pooling:" + "  " + obj.transform.position);


        }
        else if (pltpickuptestright.Instance.cubeObjects_right.Count > 1)
        {
            obj.gameObject.transform.position = dequeuedRightObjectPosition;
            dequeuedRightObjectPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 1,
                obj.transform.position.z);
            obj.gameObject.GetComponent<pltlfllowertestright>().followlastcubepos(pltpickuptestright.Instance.cubeObjects_right[pltpickuptestright.Instance.right_cubeListIndexCounter].transform, true); //listenin veya stackin üstündeki en üstteki objenin transformu
            pltpickuptestright.Instance.right_cubeListIndexCounter++;

            // Debug.Log("obj pooling:" + "  " + obj.transform.position);
        }

        // KitchenDoor.Instance.IncreaseLeftStackedPlateCount();
        return obj;
    }




    public IEnumerator ObjectPoolingProcess_right()
    {
        // poolobjects listesinin eleman sayısı setactiveobjectcount'a eşit olana kadar döngüyü devam ettir
        while (poolObjects_Right.Count != setActiveObjectCount)
        {
            RandomPool();
            yield return new WaitForSeconds(0.5f);

        }
    }



}
