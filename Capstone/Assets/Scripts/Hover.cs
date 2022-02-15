using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Color MouseOverColor = Color.green;
    //Color Original;
    //Color ValidTileColor = Color.cyan;

    Color OriginalColor;

    //get GameObject’s material and color
    MeshRenderer Renderer;

    /*void Start()
    {
        //get mesh renderer component
        Renderer = GetComponent<MeshRenderer>();
        //get original color of the GameObject
        //Original = Renderer.material.color;
        //ValidTileColor = Renderer.material.color;
        OriginalColor = Renderer.material.color;

    }

    void OnMouseOver()
    {
        OriginalColor = Renderer.material.color;
        Renderer.material.color = MouseOverColor;
        //Debug.Log(Renderer);
    }

    void OnMouseExit()
    {
        //reset color
        Renderer.material.color = OriginalColor;
    }*/
}