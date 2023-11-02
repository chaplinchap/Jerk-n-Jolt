using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    public float Distance = 5f; //How much it will float up and down
    public float speed = 1f; //The speed at which it will float
    Vector2 MovePos, StartPos;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position; //Finds the objects location
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal
        MovePos.y = StartPos.y + Mathf.Sin(Time.time * speed) * Distance;
        transform.position = new Vector2(transform.position.x, MovePos.y);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            playerRb.interpolation = RigidbodyInterpolation2D.None;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            playerRb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set the color for the Gizmos

        // Draw a line representing the floating range of the platform
        Vector2 startPos = StartPos + new Vector2(0, -Distance);
        Vector2 endPos = StartPos + new Vector2(0, Distance);

        Gizmos.DrawLine(startPos, endPos);
    }
}