using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class changeIMG : MonoBehaviour
{
    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public GameObject character;

    private float hpPlat;


    void Start()
    {
        ChangeImg();
    }

    void ChangeImg()
        {

        gameObject.GetComponent<Image>().sprite = sprite1;


        }
    }