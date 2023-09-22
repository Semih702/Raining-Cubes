using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public  Rigidbody leftGreen;
    public  Rigidbody rightGreen;
    public  Rigidbody leftGray;
    public  Rigidbody rightGray;
    public PhysicMaterial bouncy;
    private static Vector3 leftForce = new Vector3(-15.5f, 0, 0);
    private static Vector3 rightForce = new Vector3(15.5f, 0, 0);
    public RawImage[] hearts = new RawImage[3];
    public int noHearts= 3;
    public Vector3 leftBoundry = new Vector3(-15.1f, 0, 0);
    public Vector3 rightBoundry = new Vector3(15.1f, 0, 0);
    public Vector3 upperBoundry = new Vector3(0, 13.3f, 0);
    public GameObject cube;
    public Rigidbody cubeRigidbody;
    public Collider cubeCollider;
    public TMP_Text scoreText;
    // Color is : 635E5E
    public Color grayHeart = new Color(0.3882353f, 0.3686275f, 0.3686275f);
    public Color redHeart = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        createCube();
    }

    // Update is called once per frame
    void Update()
    {
        if (cube.transform.position.y < -1f)
        {
            GainScore();
        }
    }
    private void FixedUpdate()
    {
        if ((leftGreen.velocity.x<0 && leftGreen.position.x < leftBoundry.x) || (rightGreen.velocity.x>0 && rightGreen.position.x > rightBoundry.x))
        {
            leftGray.velocity = Vector3.zero;
            leftGreen.velocity = Vector3.zero;
            rightGray.velocity = Vector3.zero;
            rightGreen.velocity = Vector3.zero;
        }
        if ((Input.GetKey("a") && leftGreen.position.x>leftBoundry.x))
        {
            MoveLeft();

        }
        if ((Input.GetKey("d") && rightGreen.position.x<rightBoundry.x))
        {
            MoveRight();
        }

    }

    void MoveRight()
    {
        leftGray.velocity = rightForce;
        leftGreen.velocity = rightForce;
        rightGray.velocity = rightForce;
        rightGreen.velocity = rightForce;
    }

    void MoveLeft() {
        leftGray.velocity = leftForce;
        leftGreen.velocity = leftForce;
        rightGray.velocity = leftForce;
        rightGreen.velocity = leftForce;
    }

    void createCube()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubeRigidbody = cube.AddComponent<Rigidbody>();
        cubeRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        createPosition();
        cubeCollider = cubeRigidbody.GetComponent<Collider>();
        cubeCollider.material = bouncy;
        cube.AddComponent < CubeCollisionEnter>();
    }
    void createPosition()
    {
        cube.transform.position = new Vector3(Random.Range(leftBoundry.x+0.5f, rightBoundry.x-0.5f), upperBoundry.y, 10);
        cube.transform.rotation = new Quaternion(0, Random.Range(0, 360), Random.Range(0, 360), 0);
        cubeRigidbody.velocity = Vector3.zero;
        cubeRigidbody.angularVelocity = new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1));
    }

    public void LoseLife()
    {
        hearts[hearts.Length - noHearts--].color = grayHeart;
        if (noHearts == 0)
        {
            GameOver();
        }
        else {
            createPosition();
        }

        
    }

    public void GainScore()
    {
        score+=10;
        scoreText.text = score.ToString();
        createPosition();
    }
    
    void GameOver()
    {
        PlayerPrefs.SetInt("lastScore", score);
        if(PlayerPrefs.GetInt("record") < PlayerPrefs.GetInt("lastScore"))
        {
            PlayerPrefs.SetInt("record", score);
        }
        SceneManager.LoadScene("GameLost");
        //StartAgain();
    }

    void StartAgain()
    {
        score = 0;
        for (; noHearts < 3;noHearts++)
        {
            hearts[noHearts].color = redHeart;
        }
        createPosition();
    }
}



