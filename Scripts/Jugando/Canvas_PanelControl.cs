using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_PanelControl : MonoBehaviour {

	public GameObject Obj;
	//public GameObject 

	public void Change(){
		if (Obj.activeSelf) {
			Obj.SetActive (false);
		} else {
			Obj.SetActive (true);
		}
	}

}
