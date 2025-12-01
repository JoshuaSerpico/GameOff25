using UnityEngine;

public class CompanionController : MonoBehaviour
{

    [SerializeField] private float verticalOffset = 5;
    [SerializeField] private float horizontalOffset = 5;

    [SerializeField] Transform playerTransform;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + horizontalOffset, playerTransform.position.y + verticalOffset, transform.position.z);
    }
}
