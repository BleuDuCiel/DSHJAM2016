using UnityEngine;
using System.Collections;

public class ProjControl : MonoBehaviour
{

    private float dir = 1.0f;
    private bool setup = false;
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float timer = 1.0f;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setup) 
            transform.Translate(new Vector3(dir * speed * Time.deltaTime, 0, 0));
    }

    void Setup(float d)
    {
        dir = d;
        if (dir < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            dir = -dir;
        }

        StartCoroutine(WaitAndDelete(timer));
        setup = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("Hit");
        }

        Destroy(gameObject);
    }


    private IEnumerator WaitAndDelete(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }


}
