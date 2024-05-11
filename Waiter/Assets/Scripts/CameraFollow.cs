using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform CharacterTarget;
    public Vector3 camOffset;
    public static CameraFollow Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void LateUpdate()
    {
        if (CharacterTarget != null)
        {
            transform.position = CharacterTarget.position + camOffset;
        }
    }
}
