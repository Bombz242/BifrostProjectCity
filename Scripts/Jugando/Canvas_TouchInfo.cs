using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems; // 1
public class Canvas_TouchInfo : MonoBehaviour 
, IPointerEnterHandler
, IPointerExitHandler
{

	//public float xx;
	//public float yy;

	public GameObject TXT;
	public string Info;
	private Text texto;
	public Sprite Img;
	public float PosX = 150f;
	public float Ancho;
	public float PosY;
	public float Largo;


	//Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

	void Start(){
		texto = TXT.transform.GetChild(0).GetComponent<Text> ();
	}

	void OnDisable(){
		TXT.SetActive (false);
	}

	public void OnPointerEnter(PointerEventData eventData){
		TXT.SetActive (true);
		//TXT.transform.position = new Vector3 (Input.mousePosition.x+xx, gameObject.transform.position.y+yy,0);
		//TXT.transform.localScale = new Vector3 (Ancho ,Largo ,0);
		TXT.GetComponent<RectTransform>().sizeDelta = new Vector2 (Ancho , Largo);
		TXT.transform.position = new Vector3 (gameObject.transform.position.x+PosX, gameObject.transform.position.y+PosY,0);
		Info = Info.Replace("\\n","\n");
		texto.text = Info;
	}

	public void OnPointerExit(PointerEventData eventData){
		TXT.SetActive (false);
	}
}
