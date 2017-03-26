using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Touch_Build : MonoBehaviour {

	public int BuildingID; //-1 BORRA // -2 RESERVA
	public GameObject[] Buildings;
	//string sentence = "10 cats, 20 dogs, 40 fish and 1 programmer.";
	//string sentence = "qq (10)";

	public void OnCall(int id){
		for (int aq = 0; aq < Buildings.Length; aq++) {
			Buildings [aq].SetActive (false);
		}
		BuildingID = id;
		if (id != 0) {
			Buildings [id].SetActive (true);
		}
	}

	void OnMouseOver(){ // Cuando poso el mouse me da informacion acerca del edificio
		if (Input.GetKeyDown(KeyCode.Mouse0)){
			if (BuildingID == 0 && Sistema_Inicio.WaitServer == false && Sistema_Inicio.BuildID > 0 ) {
				Buildings [0].SetActive (true);
				int padre;
				int.TryParse (transform.parent.name, out padre);

				string[] digits = Regex.Split (gameObject.name, @"\D+");
				foreach (string value in digits) {
					int number;
					if (int.TryParse (value, out number)) {
						//print ("Intentando llamar");
						Sistema_Inicio.WaitServer = true;
						StartCoroutine (Sistema_Inicio.TryBuild (padre, value, Sistema_Inicio.BuildID));

						//Sistema_Inicio.TryBuild (value, Sistema_Inicio.BuildID);
						//Debug.Log(value);
					}
				}

				//Sistema_Inicio.TryBuild (gameObject.name, Sistema_Inicio.BuildID);

				//Buildings [Sistema_Inicio.BuildID].SetActive (true);
				//BuildingID = Sistema_Inicio.BuildID;
				//Sistema_Inicio.BuildID = 0;
			} else if (BuildingID != 0 && Sistema_Inicio.WaitServer == false && Sistema_Inicio.BuildID == -1) {
				int padre;
				int.TryParse (transform.parent.name, out padre);

				string[] digits = Regex.Split (gameObject.name, @"\D+");
				foreach (string value in digits) {
					int number;
					if (int.TryParse (value, out number)) {
						//print ("Intentando llamar");
						Sistema_Inicio.WaitServer = true;
						StartCoroutine (Sistema_Inicio.TryBuild (padre, value, Sistema_Inicio.BuildID));

						//Sistema_Inicio.TryBuild (value, Sistema_Inicio.BuildID);
						//Debug.Log(value);
					}
				}
			}//else if (Sistema_Inicio.BuildID == -1){
				//for (int a = 1; a < Buildings.Length; a++) {
				//	Buildings [a].SetActive (false);
				//}
				//BuildingID = 0;
				//Sistema_Inicio.BuildID = 0;
			//}
		}
	}

}
