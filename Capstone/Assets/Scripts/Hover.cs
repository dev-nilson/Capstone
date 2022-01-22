using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    Color MouseOverColor = Color.green;
    Color Original;

    //get GameObject’s material and color
    MeshRenderer Renderer;

    void Start()
    {
        //get mesh renderer component
        Renderer = GetComponent<MeshRenderer>();
        //get original color of the GameObject
        Original = Renderer.material.color;
    }

    void OnMouseOver()
    {
        Renderer.material.color = MouseOverColor;
    }

    void OnMouseExit()
    {
        //reset color
        Renderer.material.color = Original;
    }
}