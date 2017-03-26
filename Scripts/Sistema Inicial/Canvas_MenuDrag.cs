using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_MenuDrag : MonoBehaviour {

	public void OnDrag(){ 
		print ("intentando Drag");
		transform.position = Input.mousePosition; } 
	
}
