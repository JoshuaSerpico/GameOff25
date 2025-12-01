using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public float speed;
    [SerializeField] private Camera cam;

    [SerializeField] private Renderer bgRenderer;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
        //transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.x);
    }
}
