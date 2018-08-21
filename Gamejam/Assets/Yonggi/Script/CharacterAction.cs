using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ANIMATION_STATE
{
    IDLE,
    JUMP,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    ATTACK4
}
public class CharacterAction : MonoBehaviour {

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    public int speed;
    public bool isJumping;
    private Vector3 p_Velocity;

    public GameObject fireEffect;
    public CameraMove camera;
    public GameObject boom;
    internal ANIMATION_STATE Animation_state { get; set; }

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Animation_state = ANIMATION_STATE.IDLE;
        isJumping = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && Animation_state == ANIMATION_STATE.IDLE)
        {
            StartCoroutine("Jump");
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Attack_All();
        }
        if (ANIMATION_STATE.IDLE == Animation_state)
        {
            if (Input.GetKeyDown(KeyCode.D)) JJump();
            else if (Input.GetKeyDown(KeyCode.F)) JJump();
            else if (Input.GetKeyDown(KeyCode.J)) JJump();
            else if (Input.GetKeyDown(KeyCode.K)) JJump();
        }
    }

    public void Attack_All()
    {
        if (Animation_state != ANIMATION_STATE.IDLE)
        {
            switch (Animation_state)
            {
                case ANIMATION_STATE.JUMP:
                    Attack(ANIMATION_STATE.ATTACK1);
                    fireEffect.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, 1);
                    GameObject temp = Instantiate(fireEffect);
                    Destroy(temp, 0.3f);
                    break;
                case ANIMATION_STATE.ATTACK1:
                    Attack(ANIMATION_STATE.ATTACK2);
                    fireEffect.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, 1);
                    GameObject temp2 = Instantiate(fireEffect);
                    Destroy(temp2, 0.3f);
                    break;
                case ANIMATION_STATE.ATTACK2:
                    Attack(ANIMATION_STATE.ATTACK3);
                    fireEffect.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, 1);
                    GameObject temp3 = Instantiate(fireEffect);
                    Destroy(temp3, 0.3f);
                    break;
                case ANIMATION_STATE.ATTACK3:
                    Attack(ANIMATION_STATE.ATTACK4);
                    fireEffect.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 2.0f, 1);
                    GameObject temp4 = Instantiate(fireEffect);
                    Destroy(temp4, 0.3f);
                    break;
                default:
                    break;
            }
            camera.CameraMoving();
            GameObject tempSound =  Instantiate(boom);
            Destroy(tempSound, 4.0f);
            StartCoroutine("AttackDelay");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            Idle();
            PlayerNotePad.instance.canTouch = false;
            isJumping = false;
        }
    }
    public void JJump()
    {
        if(!isJumping)
            StartCoroutine("Jump");
    }
    IEnumerator Jump()
    {
        isJumping = true;
        m_Animator.SetInteger("Anime_State", (int)ANIMATION_STATE.JUMP);
        Animation_state = ANIMATION_STATE.JUMP;

        yield return new WaitForSeconds(0.5f);

        m_Rigidbody.AddForce(new Vector3(0, 50 * speed));
        PlayerNotePad.instance.canTouch = true;
    }

    void Idle()
    {
        m_Animator.SetInteger("Anime_State", (int)ANIMATION_STATE.IDLE);
        Animation_state = ANIMATION_STATE.IDLE;
    }

    void Attack(ANIMATION_STATE state)
    {
        m_Animator.SetInteger("Anime_State", (int)state);
        Animation_state = state;
    }

    IEnumerator AttackDelay()
    {
        p_Velocity = m_Rigidbody.velocity;
        m_Rigidbody.useGravity = false;
        m_Rigidbody.velocity = new Vector3(0, 0, 0);
        Time.timeScale = 0.7f;

        yield return new WaitForSeconds(0.5f);

        Time.timeScale = 1.0f;
        m_Rigidbody.velocity = p_Velocity;
        m_Rigidbody.useGravity = true;
    }
}
