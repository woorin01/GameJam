using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public int buttonType;

    public GameObject option;
    public GameObject howTo;

    bool isOn;
    static bool closeTab = false;

    // Use this for initialization
    void Start()
    {
        isOn = false;

        if (buttonType.Equals(2))
        {
            howTo.transform.position = new Vector3(0, 0.8f, -5f);
            howTo.SetActive(false);
        }
        if (buttonType.Equals(3))
        {
            option.transform.position = new Vector3(0, 0.8f, -5f);
            option.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && isOn)
        {
            closeTab = true;
        }
        if (Input.GetMouseButtonDown(0) && isOn && closeTab)
        {
            if (buttonType.Equals(2))
            {
                howTo.SetActive(false);
                isOn = false;
                
            }
            else if (buttonType.Equals(3))
            {
                option.SetActive(false);
                isOn = false;

            }
            closeTab = false;
        }
    }
    private void OnMouseDown()
    {
        if (buttonType.Equals(1))
        {
            SceneManager.LoadScene("InGame");
        }
        else if (buttonType.Equals(2))
        {
            //설명
            if (closeTab)
                return;

            howTo.SetActive(true);
            isOn = true;
        }
        else if (buttonType.Equals(3))
        {
            //옵션
            if (closeTab)
                return;

            option.SetActive(true);
            isOn = true;
        }
        else if (buttonType.Equals(4))
        {
            Application.Quit();
        }
    }
}
