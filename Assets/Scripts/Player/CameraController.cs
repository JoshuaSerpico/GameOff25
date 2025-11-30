using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float aboveDistance;
    private float lookAhead;
    private float lookAbove;

    private void Update()
    {
        //Follow player
        transform.position = new Vector3(player.position.x + lookAhead, player.position.y + aboveDistance, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x, Time.deltaTime * speed);
        lookAbove = Mathf.Lerp(lookAbove, aboveDistance, Time.deltaTime * speed);
    }
}
