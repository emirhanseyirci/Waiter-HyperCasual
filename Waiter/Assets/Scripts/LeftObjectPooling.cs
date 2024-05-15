using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Pool;

public class LeftObjectPooling : MonoBehaviour
{
    public static LeftObjectPooling Instance;
    public int setActiveObjectCount = 0;


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
            pools[j].pooledObjects = new Queue<GameObject>(); //o kadar queue sırası oluşacak

            for (int i = 0; i < pools[j].poolSize; i++) //pools listesindeki x indexinin poolsizeı yani oluşması istenen obje sayısı kadar 
            {
                GameObject obj = Instantiate(pools[j].objectPrefab); //x  indexli poolun prefabını oluştur
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

        GameObject obj = pools[objectType].pooledObjects.Dequeue(); //queueun en başındaki obje sıradan çıkarılır.
        obj.SetActive(true); //görünür hale getirilir
        //pools[objectType].pooledObjects.Enqueue(obj); //queue sırasına geri eklenir arkadan. tekrar kullanabilmek için.
      //  obj.transform.position = PlatePickUp.Instance.leftStackPosition; //toplanılan objenin pozisyonu leftstack pozisyonuna eşitlenir
        PlatePickUp.Instance.leftPlates.Add(obj);

        if (PlatePickUp.Instance.leftPlates.Count == 0) //left empty objecti listenin içindedir..(değil artık...!!!!!).
        {
            //dequeue_left_pos = new Vector3(obj.transform.position.x,
            //    PickUp.Instance.firstLeft_dirtyplatePos.y, obj.transform.position.z);



            dequeuedLeftObjectPosition = new Vector3(obj.transform.position.x,
                PlatePickUp.Instance.leftStackPosition.y+1f, PlatePickUp.Instance.leftStackPosition.z);
            obj.gameObject.transform.position = dequeuedLeftObjectPosition;
            //listeden çıkıp setactive true olan objenin pozisyonu tuttuğumuz vector3 değişkenidir bu. bu noktada değiştirdiğimiz tek şey  yüksekliği
            //dequeue_left_pos = new Vector3(obj.transform.position.x,
            //    obj.transform.position.y + 2f, obj.transform.position.z);  //3.5f değeri objeler arasındaki boşluktur. buraya public bir değer verip oyun içinden kontrol edebilirsin

            dequeuedLeftObjectPosition = new Vector3(obj.transform.position.x,
                obj.transform.position.y + 0.03f, PlatePickUp.Instance.leftStackPosition.z);

            obj.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(PlatePickUp.Instance.thisobjt, true);

            //obj.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(PlatePickUp.Instance.thisobjt, true);
            Debug.Log("obj pooling:" + "  " + obj.transform.position);

        }
        else if (PlatePickUp.Instance.leftPlates.Count >=1)
        {
            obj.gameObject.transform.position = dequeuedLeftObjectPosition;
            dequeuedLeftObjectPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 0.03f,
                obj.transform.position.z);
            obj.gameObject.GetComponent<PlateFollower>().FollowLastPlatePosition(PlatePickUp.Instance.leftPlates[PlatePickUp.Instance.leftPlateListIndexCounter].gameObject.transform, true); //listenin veya stackin üstündeki en üstteki objenin transformu
            PlatePickUp.Instance.leftPlateListIndexCounter++;
            Debug.Log("obj pooling:"+"  "+obj.transform.position);
        }

        KitchenDoor.Instance.IncreaseLeftStackedPlateCount();
        return obj;
    }


    //public IEnumerator ObjectPoolingProcess()
    //{
    //    // poolobjects listesinin eleman sayısı setactiveobjectcount'a eşit olana kadar döngüyü devam ettir
    //    while (poolObjects_Left.Count != setActiveObjectCount)
    //    {
    //        RandomPool();
    //        yield return new WaitForSeconds(0.5f);

    //    }
    //}

}