using UnityEngine;
using System.Collections;

public class ProjControl : MonoBehaviour
{

    private Vector2 dir = Vector2.zero;
    private bool setup = false;
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float timer = 1.0f;

    private Rigidbody2D rb;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setup)
        {
            Debug.Log("ATTACK");
            transform.GetComponent<Rigidbody2D>().AddForce(dir * speed);
            setup = false;
        }
            //transform.Translate(new Vector3(dir * speed * Time.deltaTime, 0, 0));
    }

    void Setup(Vector2 d)
    {
        dir = d;
        if (dir.x < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            //dir.x = -dir.x;
        }

        StartCoroutine(WaitAndDelete(timer));
        setup = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (rb.isKinematic)
            {
                coll.SendMessage("Ammo");
            } else
            {
                coll.gameObject.SendMessage("Hit");
            }
            
            Destroy(gameObject);
        } else 
        {
            setKinematic(true);
            Debug.Log("GROUND");
        }

        
    }

    void setKinematic(bool b)
    {
        rb.isKinematic = b;
    }

    private IEnumerator WaitAndDelete(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Destroy(gameObject);
    }


}
