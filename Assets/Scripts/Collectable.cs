using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Attr para saber si la moneda ha sido recogida o no
    bool isCollected = false;
    public int value = 0;
    //Metodo para activar la moneda y su collider
    void Show()
    {
        //Activamos la imagen de la moneda
        this.GetComponent<SpriteRenderer>().enabled = true;
        //Activar el collider de la moneda para ser recogida
        //POonemos que no hemos cogido la moneda actual
        this.GetComponent<CircleCollider2D>().enabled = true;
        this.isCollected = false;
    }

    //Metodo para desactivar la moneda y su collider
    void Hide()
    {       
        this.GetComponent<SpriteRenderer>().enabled = false;       
        this.GetComponent<CircleCollider2D>().enabled = false;     
    }

    //Metodo para recolectar la moneda
    void Collect()
    {        
        this.isCollected = true;        
        this.Hide();
        GameManager.sharedInstance.CollectObject(this.value);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            this.Collect();
        }
    }
}
