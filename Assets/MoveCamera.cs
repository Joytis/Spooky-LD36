using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{
    public int BGspriteWidth = 1920 * 2;

    public float transitionAmmountPerFrame;
    Vector3[] cameraPositionsArray = new Vector3[3];

    bool movingLeft = false;
    bool movingRight = false;
    bool lerping = false;
    Vector3 toVector;
    public Button leftButton;
    public Button rightButton;
    GameObject leftImage;
    GameObject rightImage;

    void Awake()
    {
        leftImage = GameObject.Find("fuzzyLeft");
        rightImage = GameObject.Find("fuzzyRight");
        leftImage.SetActive(false);
        rightImage.SetActive(false);
        toVector = Camera.main.transform.position; //default
        int cameraMoveAmount = BGspriteWidth / 460;
        cameraPositionsArray[1] = Camera.main.transform.position;
        cameraPositionsArray[0] = new Vector3(-cameraMoveAmount, 0, cameraPositionsArray[1].z);
        cameraPositionsArray[2] = new Vector3(cameraMoveAmount, 0, cameraPositionsArray[1].z);
    }

    void Update()
    {
        if (movingLeft)
        {
            LerpVectors(Camera.main.transform.position, toVector);
        }

        if (movingRight)
        {
            LerpVectors(Camera.main.transform.position, toVector);
        }
    }

    public void MoveCameraLeft()
    {
        if (!movingLeft)
        {
            movingLeft = true;
            for (int i = 0; i < 3; i++)
            {
                if (Mathf.Round(Camera.main.transform.position.x) == cameraPositionsArray[i].x)
                {
                    if (i == 0) //WE IS LEFT ALREADY
                    {
                        toVector = Camera.main.transform.position; //default
                    }
                    else if (i == 1) //WE IS MID
                    {
                        toVector = cameraPositionsArray[0];
                        leftButton.gameObject.SetActive(false);
                        leftImage.SetActive(true);
                    }
                    else //THIS IS THE RIGHT
                    {
                        toVector = cameraPositionsArray[1];
                        leftButton.gameObject.SetActive(false);
                        leftImage.SetActive(true);
                    }
                }
            }
        }
    }

    public void MoveCameraRight()
    {
        if (!movingRight)
        {
            movingRight = true;
            for (int i = 0; i < 3; i++)
            {
                if (Mathf.Round(Camera.main.transform.position.x) == cameraPositionsArray[i].x)
                {
                    Debug.Log(i);
                    if (i == 2) //WE IS RIGHT ALREADY
                    {
                        toVector = Camera.main.transform.position; //default
                    }
                    else if (i == 1) //WE IS MID
                    {
                        toVector = cameraPositionsArray[2];
                        rightButton.gameObject.SetActive(false);
                        rightImage.SetActive(true);
                    }
                    else //THIS IS THE Left
                    {
                        toVector = cameraPositionsArray[1];
                        rightButton.gameObject.SetActive(false);
                        rightImage.SetActive(true);
                    }
                }
            }
        }
    }

    void LerpVectors(Vector3 from, Vector3 to)
    {
        Camera.main.transform.position = Vector3.Lerp(from, to, Time.deltaTime * transitionAmmountPerFrame);
        if (Mathf.Round(Camera.main.transform.position.x) == to.x)
        {
            movingLeft = false;
            movingRight = false;
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(true);
            leftImage.SetActive(false);
            rightImage.SetActive(false);
        }
    }
}