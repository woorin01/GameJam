using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    
    public GameObject player;
    private Transform p_Transform;

    bool isCameraShake;

	// Use this for initialization
	void Start () {
        p_Transform = player.gameObject.GetComponent<Transform>();
        isCameraShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCameraShake == false)
        {
            float num = 1;
            Vector3 p1 = new Vector3(p_Transform.position.x, p_Transform.position.y + num, transform.position.z);
            transform.position = Vector3.Lerp(p1, transform.position, 0.1f);
            if (transform.position.y < 0)
            {
                transform.position = new Vector3(p1.x, 0, p1.z);
            }
        }
        else
        {
            transform.position += new Vector3(Random.RandomRange(-0.1f, 0.1f), Random.RandomRange(-0.1f, 0.1f), 0);
        }
    }

    public void CameraMoving()
    {
        StartCoroutine("CameraShake");
    }

    IEnumerator CameraShake()
    {
        isCameraShake = true;

        yield return new WaitForSeconds(0.3f);

        isCameraShake = false;
    }
}
