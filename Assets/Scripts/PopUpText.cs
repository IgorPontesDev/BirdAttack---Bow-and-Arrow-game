using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;//Don't Forget

public class PopUpText : MonoBehaviour
{
    public Color color;
    //The Main Prefab Used To Create The Popups
    public GameObject popUpObject;

    //A List That Holds All Currently Instantiated PopUp Objects
    List<GameObject> popUpList;
    public GameObject clone;
    void Start()
    {
        popUpList = new List<GameObject>();
    }

    public void ProduceText(float qtdMoedas)
    {
        //Create Instance Of Popup
        
        Vector3 PosCorrigida = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);
        clone = Instantiate(popUpObject, Camera.main.WorldToScreenPoint(PosCorrigida), Quaternion.identity);
        clone.transform.GetComponent<TextMeshProUGUI>().text = qtdMoedas.ToString();
        clone.transform.SetParent(GameObject.Find("CanvasGame").transform);
       
        popUpList.Add(clone);
        StartCoroutine(Wait(clone));
    }

    //Wait 1/2 Of A Second Before Removing The Clone From The List, Then Destory It
    IEnumerator Wait(GameObject clone)
    {
        yield return new WaitForSeconds(.7f);
        
        popUpList.Remove(clone);
        Destroy(clone);
    }

    void Update()
    {

       
    }
}