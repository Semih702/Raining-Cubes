using UnityEngine;

public class CubeCollisionEnter : MonoBehaviour
{
    public string loserTag = "gameloser";
    public string nameGameObject = "GameManager";
    public float bounceFactor = 1f;
    public Rigidbody cubeRigidbody;
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        cubeRigidbody = GetComponent<Rigidbody>();
        manager = GameObject.Find(nameGameObject).GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == loserTag)
        {
            manager.LoseLife();
        }
        cubeRigidbody.velocity = new Vector3(cubeRigidbody.velocity.x, cubeRigidbody.velocity.y*bounceFactor, cubeRigidbody.velocity.z);
    }
}
