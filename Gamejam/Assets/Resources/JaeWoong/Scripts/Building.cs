using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//빨간색, 파란색, 노란색, 보라색
public class Building : MonoBehaviour
{
    private Rigidbody rb;

    public int maxPatternNum;
    public int[] notePattern;

    public float gravityScale = -10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
         
       //  rb.useGravity = false;
    }

    private void Update()
    {
        //transform.position += new Vector3(0, -10 * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
       // rb.AddForce(new Vector3(0, gravityScale, 0), ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //rb.velocity = new Vector3(0, 10, 0);
          //Rigidbody p_Rb = GetComponentInParent<Rigidbody>();
          //p_Rb.velocity = new Vector3(0, 6, 0);
          //p_Rb.useGravity = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Block"))
    //    {
    //        collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //    }
    //}

    public void MakePattern()
    {
        maxPatternNum = Random.Range(1, 1);
        notePattern = new int[maxPatternNum];

        for (int i = 0; i < maxPatternNum; i++)
        {
            notePattern[i] = Random.Range(1, 5);
        }
    }

    public void PatternToColor(int patternNum)
    {
        if (patternNum.Equals(1))
        {
            this.GetComponent<Renderer>().material = Resources.Load<Material>("JaeWoong/Matarial/Red");
        }
        else if (patternNum.Equals(2))
        {
            this.GetComponent<Renderer>().material = Resources.Load<Material>("JaeWoong/Matarial/Blue");
        }
        else if (patternNum.Equals(3))
        {
            this.GetComponent<Renderer>().material = Resources.Load<Material>("JaeWoong/Matarial/Yellow");
        }
        else if (patternNum.Equals(4))
        {
            this.GetComponent<Renderer>().material = Resources.Load<Material>("JaeWoong/Matarial/Purple");
        }
    }
}
