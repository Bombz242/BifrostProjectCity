using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sistema_Inicio : MonoBehaviour{

	public GameObject[] UI0CTR; //UI Cosas misc random
	public GameObject[] UI2INT; //UI INTERIOR
	public GameObject[] UI4RES; //UI Reserach

	public GameObject[] UI5ACT; //UI Action
	public GameObject[] UI6TRA; //UI Trade

	public GameObject[] UICTRL; //Inicio
	public GameObject[] UIMAP;
	public GameObject[] UIGOVERMENT;
	public GameObject[] UIDIPLOMATIC;
	//public GameObject[] UIRESEARCH;
	public GameObject[] UIACTION;
	public GameObject[] CityMap;
	public Text[] CityUI;
	static public GameObject[] CITYMAP;
	static public Text[] CITYMENU;
	static public int BuildID;

	private int UIACTIONID; //Id del evento

	private int SpyID;//Id del espionaje
	private string SpyTargetName;// Nombre del objetivo

	public void SpySelect(int id){ //Selecciona la accion
		SpyID = id;
		SpyShow ();
	}

	public void SpyName(){ // Asigna el nombre a la mision
		bool temp = !UI5ACT[2].activeSelf;
		UI5ACT [2].SetActive (temp);
		if (UI5ACT [2].activeInHierarchy == false){ // Si no esta activo
			SpyTargetName = UI5ACT [2].GetComponent<InputField>().text;
		}
		SpyShow ();
	}

	public void SpySuccess(){
		SpyShow ();
	}



	void SpyShow(){ //Muestra el informe
		string txt = "";
		int costf = 0;
		int success = 0;
		switch (SpyID) {
		case 0:
			txt = "Select Mission ";
			break;
		case 1:
			txt = "Spy on "+SpyTargetName + "\n \n";
			costf = 5;
			success = 50;
			break;
		case 2:
			txt = "Sabotage on "+SpyTargetName + "\n \n";
			costf = 25;
			success = 40;
			break;
		case 3:
			txt = "Propagand on "+SpyTargetName + "\n \n";
			costf = 40;
			success = 30;
			break;
		case 4:
			txt = "Terrorism on "+SpyTargetName + "\n \n";
			costf = 65;
			success = 30;
			break;
		case 5:
			txt = "Event on "+SpyTargetName + "\n \n";
			costf = 90;
			success = 25;
			break;
		}
		float chance = UI5ACT [4].GetComponent<Slider> ().value;
		float t1 = success + chance;
		int t2 =  (int) (chance / 50 * costf)+costf;
		txt += "Success "+ t1 +"% \n Fait "+ t2 ; //Nombre del usuario
		UI5ACT [3].transform.GetChild(0).GetComponent<Text>().text = txt;

	}

	IEnumerator SpyResult(){ //Envia el espionaje y trae los resultados
		string t = UIGOVERMENT[27].GetComponent<InputField>().text;
		string PS = WEBARMYPOST + "&C="+InfoNewDeal[0]+ "&t="+UIACTIONID;//+ "&C=" + PullData[1]
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else { // Muestra la informacion luego del espionaje
			StartCoroutine (ActionArmyGet ());
		}
	}
		


	public void ActionIDSet(int id){
		UIACTIONID = id;
	}

	IEnumerator ActionArmyPost(){
		string t = UIGOVERMENT[27].GetComponent<InputField>().text;
		string PS = WEBARMYPOST + "&C="+InfoNewDeal[0]+ "&t="+UIACTIONID;//+ "&C=" + PullData[1]
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			StartCoroutine (ActionArmyGet ());
		}
	}

	IEnumerator ActionArmyGet(){ ///
		string t = UIGOVERMENT[27].GetComponent<InputField>().text;
		string PS = WEBARMYGET + "&C="+InfoNewDeal[0]; 		//+ "&C=" + PullData[1]
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			string[] D = new string[6];
			string datos = hs_get.data; //
			for (int a = 0; a < 1; a++) {  //Toda la info se almacena
				D = datos.Split ("+" [a]);
			}
			for (int a = 0; a < 6; a++) {  //Toda la info se almacena
				UIACTION[10+a].transform.GetChild(0).GetComponent<Text>().text = D[a];
				//DealMyInfo = datos.Split ("+" [a]);
			}
		}
		WaitServer = false;
	}

	static public string[] PullData = new string[3];
	static public string[] SERVERUSERBASIC; //Informacion basica y esencial
	static public string[] SERVERUSERINFO; //LA INFORMACION DEL USUARIO
	static public string[] SERVERCITYINFO; //La informacion del server con la que se maneja el juego esta aca.
	static public string[] SERVERLASTFILE; //Ultima fila
	static public string[] SERVERTEMP;
	static public string[] SERVERMSGINFO;
	static public string[] SERVERMARKETINFO; //Storea la info del mercado

	static public bool WaitServer; // Esperando respuesta del server
	//static public bool ActServer; //Actualiza info
	//INICIALIZADORES PARA TODO EL SISTEMA ONLINE

	//static public string WEB="http://craftingcor.tk/Jobs/CityNewReg.php?";
	static public string WEBNEWCITY ="http://www.civonline.ga/Ingame/3-Post/CityPost.php?";
	static public string WEBGETCITY = "http://www.civonline.ga/Ingame/1-Basic/GetCity.php?";
	static public string WEBGETDATA = "http://www.civonline.ga/Ingame/1-Basic/GetData.php?";
	protected string WEBNEWGOB = "http://www.civonline.ga/Ingame/3-Post/GobPost.php?";
	protected string WEBNEWLAW = "http://www.civonline.ga/Ingame/3-Post/GobLawPost.php?";
	protected string WEBTECHPOST = "http://www.civonline.ga/Ingame/3-Post/TechPost.php?";

	protected string WEBLOGIN = "http://craftingcor.tk/Jobs/LogUser.php?";
	protected string WEBREG = "http://craftingcor.tk/Jobs/RegUser.php?";
	protected string WEBGETMARK = "http://craftingcor.tk/Jobs/GetMarket.php?";
	protected string WEBPOSTMARK = "http://craftingcor.tk/Jobs/PostMarket.php?";
	protected string WEBBUYMARK = "http://craftingcor.tk/Jobs/BuyMarket.php?";
	protected string WEBPOSTDEAL = "http://craftingcor.tk/Jobs/DealPostNew.php?";
	protected string WEBGETDEAL = "http://craftingcor.tk/Jobs/DealGet.php?";

	protected string WEBARMYPOST = "http://craftingcor.tk/Jobs/ArmyBuild.php?";
	protected string WEBARMYGET = "http://craftingcor.tk/Jobs/ArmyGet.php?";


	private bool[] Tech = new bool[14]; //Tecnologias, sabe que rama esta activa
	private int[] TechBonus = new int[11];
	private int TechCost;
	private int TechDay = 1;
	public int timer;

	private string[] InfoSellMarket = new string[4]; //Informacion almacenada del sell
	private string[] InfoNewDeal = new string[3];

	private float[] Total = new float[12];
	private int newidgov;
	private int newlaw;
	private int MarketValue;
	private float cal;

	static public GameObject STAC;
	public string[] datagov = new string[11];

	private string[] DealMyInfo;

	private int buyid;
	public int MarketPageID;

	void Start(){
		CITYMAP = CityMap;
		CITYMENU = CityUI;
		STAC = this.gameObject;
		//StartCoroutine (Sistema_Inicio.GetCity());
		//StartCoroutine (GetData());
	}

	public void CallCourutine(int id){
		if (WaitServer == false) {
			switch (id) {
			case 1: //NEW REGISTRO
				StartCoroutine (OnNewUser ());
				break;
			case 2: //TRY LOGIN
				StartCoroutine (Login ());
				UICTRL [0].SetActive (false);
			//StartCoroutine (Sistema_Inicio.GetCity());
			//StartCoroutine (GetData());
				break;
			case 3: //ESTE SI DEBE SER ESTATICO
			//StartCoroutine (TryBuild ());
				break;
			case 4:
				StartCoroutine (GetData ());
				break;
			case 5:
				StartCoroutine (GetCity ());
				break;
			case 10: //SEND COSAS INGAME
				StartCoroutine (SendGoverment ());
				break;
			case 11: 
				StartCoroutine (SendLaw (1));
				break;
			case 12:
				StartCoroutine (SendLaw (2));
				break;
			case 14:
				StartCoroutine (MarketGet ());
				break;
			case 15:
				StartCoroutine (PostMarket ());
				break;
			case 16:
				StartCoroutine (DealPost ());
				break;
			case 17:
				StartCoroutine (DealGet ());
				break;
			case 20:
				StartCoroutine (TechPost ());
				break;
			case 25:
				StartCoroutine (ActionArmyPost ());
				break;
			}
			WaitServer = true;
		}
	}

	//REFERENTE A TODO EL SISTEMA

	public void SeleccionarMenu(int id){ // Habilita el menu seleccionado
		for (int a = 1; a < UIMAP.Length; a++) {
			UIMAP [a].SetActive (false);
		}
		BuildID = 0;
		UIMAP [id].SetActive (true);
	}

	public void Build (int sel){  // Construye el edificio
		BuildID = sel;
	}

	public void DealType(int pos){
		if (pos < 20) { // es un acuerdo
			InfoNewDeal[0] = pos.ToString();
		} else { // es la hora
			InfoNewDeal[1] = (pos-20).ToString(); 
		}
	}
	public IEnumerator DealPost(){
		string t = UIGOVERMENT[27].GetComponent<InputField>().text;
		string PS = WEBPOSTDEAL + "&t="+InfoNewDeal[0]+ "&d="+InfoNewDeal[1]+ "&n="+t; 		//+ "&C=" + PullData[1]
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			StartCoroutine (DealGet ());
		}
	}
	public IEnumerator DealGet(){
		string PS = WEBGETDEAL + "&t="+InfoNewDeal[0]; 		//+ "&C=" + PullData[1]
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			string datos = hs_get.data; //
			UIGOVERMENT [20].GetComponent<Text> ().text = "";
			UIGOVERMENT [21].GetComponent<Text> ().text = "";
			UIGOVERMENT [22].GetComponent<Text> ().text = "";
			for (int a = 0; a < 1; a++) {  //Toda la info se almacena
				DealMyInfo = datos.Split ("+" [a]);
			}
			string[] temp = new string[6];
			for (int a = 0; a < DealMyInfo.Length; a++) {
				for (int c = 0; c < 1; c++) {
					temp = DealMyInfo[a].Split ("/" [c]);
					//for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
						//temp = SERVERMARKETINFO [(MarketPageID*10)+c].Split ("/" [a]);
					}
				UIGOVERMENT [20].GetComponent<Text> ().text += temp[3]+"\n"; //ACA
				UIGOVERMENT [21].GetComponent<Text> ().text += temp[4]+"\n";
				UIGOVERMENT [22].GetComponent<Text> ().text += temp[5]+"\n";
			}
			WaitServer = false;
		}
	}

	private int[] MarkB1;
	private int[] MarkB2;
	private int[] MarkB3;
	private int[] MarkB4;

	private int MarketSearchNumber = 1;

	public IEnumerator MarketGet(){
		string PS = WEBGETMARK+"&MSN="+MarketSearchNumber;
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			string datos = hs_get.data; //print (datos.ToString ());
			for (int a = 0; a < 1; a++) {  //Toda la info se almacena
				SERVERMARKETINFO = datos.Split ("+" [a]);
			}
			cal = SERVERMARKETINFO.Length / 10;

			string[] temp = new string[15]; // Almacena temporalmente la informacion disponible
			int[] MarkBT1 = new int[51];
			int[] MarkBT2 = new int[51];
			int[] MarkBT3 = new int[51];
			int[] MarkBT4 = new int[51];

			UIDIPLOMATIC [11].GetComponent<Text>().text = "" ; //tipo
			UIDIPLOMATIC [12].GetComponent<Text>().text = "" ; //cantidad
			UIDIPLOMATIC [13].GetComponent<Text>().text = "" ; //precio

			int iter = SERVERMARKETINFO.Length-1;
			for (int c = 0; c < iter; c++) {
				for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
					temp = SERVERMARKETINFO [c].Split ("/" [a]);
					print (SERVERMARKETINFO [c].ToString ());
				}
				print (temp [0].ToString ());
				print (temp [1].ToString ());
				int d = int.Parse (temp [1].ToString()); //Dinero
				int q = int.Parse(temp[2]);
				switch (temp [0]) { //Switch tipo
				case "1":
					MarkBT1 [d] += q; // asigna cantidades
					break;
				case "2":
					MarkBT2 [d] += q;
					break;
				case "3":
					MarkBT3 [d] += q;
					break;
				case "4":
					MarkBT4 [d] += q;
					break;
				}
			}
			MarkB1 = MarkBT1;
			MarkB2 = MarkBT2;
			OnShowMarket (1);
			WaitServer = false;
		}
	}

	public void OnShowMarket(int id){//int resto = SERVERMARKETINFO.Length-(10 * MarketPageID);
		UIDIPLOMATIC [11].GetComponent<Text>().text = "" ; //tipo
		UIDIPLOMATIC [12].GetComponent<Text>().text = "" ; //cantidad
		UIDIPLOMATIC [13].GetComponent<Text>().text = "" ; //precio
		//UIDIPLOMATIC [11].GetComponent<Text>().text = MarkB1[1]; //tipo
		if (id == 1){
			//int resto = MarkB1.Length-(10 * MarketPageID);
			for (int c = 0; c < MarkB1.Length; c++) { 
				UIDIPLOMATIC [12].GetComponent<Text>().text += MarkB1[c].ToString() +"\n" ; //cantidad
				UIDIPLOMATIC [13].GetComponent<Text>().text += c.ToString() +"\n" ; //precio
			}
		}
		if (id == 2){
			//int resto = MarkB1.Length-(10 * MarketPageID);
			for (int c = 0; c < MarkB2.Length; c++) { 
				UIDIPLOMATIC [12].GetComponent<Text>().text = MarkB2[c].ToString() ; //cantidad
				UIDIPLOMATIC [13].GetComponent<Text>().text = c.ToString() ; //precio
			}
		}
	}

	public void MarketSell(int sel){
		if (sel < 10) {
			InfoSellMarket [0] = sel.ToString();
		} else if (sel < 20) {
			InfoSellMarket [3] = sel.ToString();
		}
	}

	public void MarketSellAmount(){
		InfoSellMarket [1] = UIDIPLOMATIC [20].transform.GetChild (0).GetComponent<InputField> ().text;
	}
	public void MarketSellPrice(){
		InfoSellMarket [2] = UIDIPLOMATIC [21].transform.GetChild (0).GetComponent<InputField> ().text;
	}

	public IEnumerator PostMarket(){
		if (InfoSellMarket[0] != null && InfoSellMarket[1] != null && InfoSellMarket[3] != null ){
			string PS = WEBPOSTMARK + "&T="+InfoSellMarket[0]+ "&Q="+InfoSellMarket[1]+ "&M="+InfoSellMarket[2]+ "&D="+InfoSellMarket[3]; 		//+ "&C=" + PullData[1]
			WWW hs_get = new WWW (PS);
			yield return hs_get;
			if (hs_get.error != null) {
				print ("ERROR");
			}
			StartCoroutine (MarketGet());
			UIDIPLOMATIC [15].SetActive (false);
			for (int c = 0; c < 4; c++) { 
				InfoSellMarket [c] = null; 
			}
		}
	}

	public void MarketBuy(int pos){
		buyid = pos;
		StartCoroutine (BuyMarket ());
	}
	public void MarketSelect(int id){
	
	}

	public IEnumerator BuyMarket(){
		string[] temp = new string[6];
		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERMARKETINFO [(MarketPageID*10)+buyid].Split ("/" [a]);
		}
		string PS = WEBBUYMARK+"&id="+temp[0];
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			StartCoroutine (MarketGet());//CallCourutine(14);
		}//print ("Comprado: "+temp[0].ToString());
	}

	public void MarketChangePage(int pos){
		if (pos == 1 && MarketPageID > 0) {
			MarketPageID--;
		} else if (pos == 2) {
			MarketPageID++;
		}
		if (MarketPageID < 1) {
			UIDIPLOMATIC [33].SetActive (false);
		} else {
			UIDIPLOMATIC [33].SetActive (true);
		}
		int t = MarketPageID + 1; 
		int tt = (SERVERMARKETINFO.Length/10)+1;

		UIDIPLOMATIC [34].transform.GetChild (0).GetComponent<Text> ().text = t+" / "+tt;
	}

	private int GOB;

	public void GobLocalSelect(int id){
		UI2INT [9].SetActive (true);
		UI2INT [9].transform.GetChild(0).transform.GetChild(0).GetComponent<Text> ().text = "Sure change Goverment?";
		GOB = id;
		//newidgov = id;
	}
	public void LawLocalSelect(int id){
		UI2INT [9].SetActive (true);
		UI2INT [9].transform.GetChild(0).transform.GetChild(0).GetComponent<Text> ().text = "Sure change Law?";
		GOB = id;
	}

	public void GobLocalPost(){ //Llama algo
		UI2INT [9].SetActive (false);
		if (GOB < 99) {
			CallCourutine (10); //Envia el nuevo gobierno
		} else {
			UI2INT [10].SetActive (true);
			UI2INT [10].transform.GetChild(0).transform.GetChild(0).GetComponent<Text> ().text = "Select Law position";
		}
	}
	public void LawLocalPost(int id){
		UI2INT [10].SetActive (false);
		if (id == 1) {
			CallCourutine (11); //Envia ley n1
		} else {
			CallCourutine (12); //Envia ley n2
		}
	}

	public IEnumerator SendGoverment(){
		string PS = WEBNEWGOB + "&C=" + PullData[1] + "&V=" + GOB;  //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			StartCoroutine (GetData ());
			yield return new WaitForSeconds(0.5f);
			WaitServer = false;
		}
	}
	public IEnumerator SendLaw(int slot){
		GOB = GOB - 100;
		string PS = "";
		if (slot == 1) {
			PS = WEBNEWLAW + "&C=" + PullData[1] + "&V=" + GOB;  //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		} else if (slot == 2) {
			//int t = newlaw + 50;
			GOB = GOB+50;
			PS = WEBNEWLAW + "&C=" + PullData[1] + "&V=" + GOB;  //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		}//string PS = WEBNEWLAW + "&C=" + PullData[1] + "&V=" + newidgov;  //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			//Sistema_Inicio example = new Sistema_Inicio();
			//example.NuevaData();
			StartCoroutine (GetData ());
			yield return new WaitForSeconds(0.5f);
			WaitServer = false;
			//Sistema_Inicio.WaitServer = false;
			//print ("Ready:" + slot);
		}
	}

	//public Sistema_Inicio example;

	static public IEnumerator TryBuild(int Fila, string Index, int Value){ //
		string PS = WEBNEWCITY + "&C=" + PullData[1] + "&F=" +Fila +"&I=" + Index + "&V=" + Value; //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		//print (hs_get.data);
		if (hs_get.error != null) {
			print ("ERROR");
			//Console [1].GetComponent<Text> ().text = "Login Error: "+hs_get.error;
		} else {
			/*string datos = hs_get.data; //print (datos.ToString ());
			for (int a = 0; a < 1; a++) {  //La informacion de toda la linea cambiada se almacena
				SERVERLASTFILE = datos.Split ("/" [a]);
				SERVERCITYINFO = SERVERLASTFILE[0].Split ("." [a]);
			}
			ShowCity (int.Parse(SERVERLASTFILE[1]));*/
			//Sistema_Inicio example = new Sistema_Inicio();
			Sistema_Inicio example = STAC.GetComponent<Sistema_Inicio>();
				//STAC.AddComponent<Sistema_Inicio>();
			example.NuevaData();
			yield return new WaitForSeconds(0.5f);
			WaitServer = false;
			//Sistema_Inicio.WaitServer = false;
		}

	}

	public IEnumerator OnNewUser(){
		UICTRL [5].SetActive (false);
		string user = UICTRL[6].GetComponent<InputField>().text ;
		string password = UICTRL [7].GetComponent<InputField> ().text;
		string email = UICTRL[8].GetComponent<InputField>().text ;
		int county = UICTRL [9].GetComponent<Dropdown> ().value;

		string PS = WEBREG + "&A=" + user + "&P=" + password + "&C=" + county + "&E=" + email; //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
			UICTRL [5].SetActive (true);
		} else {
			string datos = hs_get.data; //print (datos.ToString ());
			print (datos);
			if (datos == "Utiliza otro nombre \n") {
				UICTRL [5].SetActive (true);
			} else {
				UICTRL [1].SetActive (true);
			}
			WaitServer = false;
		}
	}

	public IEnumerator Login(){ //Comprueba que el usuario existe y la contraseña es correcta, entonces da el primer listado para fijarse si esta en otr pc y si esta baneado.
		//Una vez hecho este proceso en el servidor, entrega los campos basicos de logueo, ban, id del pc, pais.
		PullData[1]  = UICTRL[2].GetComponent<InputField>().text ;
		PullData[2]  = UICTRL [3].GetComponent<InputField> ().text;
		string PS = WEBLOGIN + "&C=" + PullData[1] + "&P=" + PullData[2]; //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
			UICTRL[0].SetActive (true);
		} else {
			string datos = hs_get.data; //print (datos.ToString ());
			print (datos);
			if (datos == "Username does not exist \n" || datos == "Password does not match \n"){
				UICTRL[0].SetActive (true);
			} else {
				for (int a = 0; a < 1; a++) {  //Toda la info se almacena
					SERVERUSERBASIC = datos.Split ("/" [a]);
				}
				if (SERVERUSERBASIC.Length > 0) {
					//CallCourutine (5); //GetCity
					StartCoroutine (Sistema_Inicio.GetCity ()); // Obtiene la forma de la ciudad
					UIMAP [0].SetActive (true);
					StartCoroutine (GetData ()); //Obtiene la informacion del jugador
				} else {
					print ("Ocurrio un error inesperado logeando");
				}
			}
		}
		//WaitServer = false;
	}

	static public IEnumerator GetCity(){
		string PS = WEBGETCITY + "&C="+PullData[1]; //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			string datos = hs_get.data; //print (datos.ToString ());
			for (int a = 0; a < 1; a++) {  //Toda la info se almacena
				SERVERTEMP = datos.Split ("+" [a]); //Splitea de forma pareja las lineas
			}
			for (int a = 0; a < SERVERTEMP.Length; a++) { //Cuenta 15 lineas que hay y vuelve a splitear, esta vez por tiles
				for (int aa = 0; aa < 1; aa++) {  //una vez para splitear toda la fila
					SERVERLASTFILE = SERVERTEMP[a].Split ("/" [aa]); //Fila
					SERVERCITYINFO = SERVERLASTFILE[aa].Split ("." [aa]); //Tile
				}
				//print (SERVERLASTFILE [a].ToString ()+"V"+a+"S"+SERVERTEMP.Length);
				//if (int.Parse (SERVERLASTFILE [1]) != 0) { // Se refiere al tile en especifico, si es igual a 0 quiere decir que no hay nada
				ShowCity (int.Parse (SERVERLASTFILE [1])); 
				//}
			}
		}
		WaitServer = false;
	}

	static public void ShowCity(int file){ //Muestra la actualizacion de la ciudad de una sola linea
		for (int aa = 0; aa < SERVERCITYINFO.Length - 1; aa++) {
			//int id;
			//int.TryParse (SERVERCITYINFO [aa], out id);
			int id = int.Parse(SERVERCITYINFO [aa]); //Convierte el tile.
			if (id != -2){
				CITYMAP [file].transform.GetChild (aa).gameObject.GetComponent<Touch_Build>().OnCall(id);
			}
		}
		/*for (int aa = 0; aa < SERVERCITYINFO.Length-1; aa++) {
			int id;
			int.TryParse (SERVERCITYINFO [aa], out id);
			if (id != -2){
				CITYMAP [file].transform.GetChild (aa).gameObject.GetComponent<Touch_Build>().OnCall(id);
			}
		}*/
	}


	public void NuevaData(){
		StartCoroutine (GetCity ());
		StartCoroutine (GetData ());
	}

	public IEnumerator GetData(){ //Obtiene la informacion referente al servidor.
		string PS = WEBGETDATA + "&C="+PullData[1]; //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
		WWW hs_get = new WWW (PS);
		yield return hs_get;
		if (hs_get.error != null) {
			print ("ERROR");
		} else {
			string datos = hs_get.data; //print (datos.ToString ());
			for (int a = 0; a < 1; a++) {  //Toda la info se almacena
				SERVERUSERINFO = datos.Split ("+" [a]);
				//OnDisplayInfoGoverment ();
			}
			ShowData();
			WaitServer = false;
		}
	}

	public void ShowData(){
		string[] temp = new string[12] ; // Almacena temporalmente la informacion disponible
		//float c0 = 0;
		float c1 = 0;
		float c2 = 0;
		float c3 = 0;
		float c4 = 0;
		float c5 = 0;
		float c6 = 0;
		float dis = 0;
		//float total = 0;

		datagov [0] = "" ; //BASE
		datagov [1] = "" ; //Bonus
		datagov [2] = "" ; //TOTAL
		datagov [3] = "" ; //Mantenimiento
		datagov [4] = "" ; //Disponible final

		UI2INT [1].GetComponent<Text>().text = "City \n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text = "Spend \n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text = "Bonus \n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text = "Trade \n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text = "Dis \n" ; //Show disp
		UI2INT[8].GetComponent<Text> ().text = "";

		for (int a = 0; a < 1; a++) {  // NATIONAL City / Gasto / Bonus / Trade
			temp = SERVERUSERINFO [1].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2;
		//dis = (c1+c6-(-1*c3)+c1+c4)-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [11].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [2].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [12].GetComponent<Text>().text =dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [3].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [13].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [4].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [14].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [5].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [15].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp


		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [6].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [16].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [7].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [17].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [8].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		UI0CTR [18].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [9].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		//UI0CTR [19].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [10].Split ("/" [a]); }
		c1 = int.Parse (temp [0]); //City
		c2 = int.Parse (temp [1]); //Spend
		c3 = int.Parse (temp [2]); //Bonus
		c4 = int.Parse (temp [3]); //Trade
		c5 = c3 / 100;// Pre-Calculo Bonus
		c6 = c5 * c1; // Calculo Bonus Final
		dis = c1+c6+c4-c2; //Disponible final (lo del trade no suma al bonus)
		//UI0CTR [20].GetComponent<Text>().text = dis.ToString(); //Show on Menu
		UI2INT [1].GetComponent<Text>().text += c1 + "\n" ; //Show City Base
		UI2INT [2].GetComponent<Text>().text += "-"+ c2 + "\n" ; //Show spend
		UI2INT [3].GetComponent<Text>().text += c3 + " %\n" ; //Show Bonus
		UI2INT [4].GetComponent<Text>().text += c4 + "\n" ; //Show trade
		UI2INT [5].GetComponent<Text>().text += dis + "\n" ; //Show disp

		for (int a = 0; a < 1; a++) {  //Split Total / Usando / Bonus / ???
			temp = SERVERUSERINFO [0].Split ("/" [a]); }
		float conv = int.Parse (temp [0]);
		UI2INT[6].transform.GetChild (0).GetComponent<Slider> ().value = conv ;
		UI2INT[6].transform.GetChild (1).GetComponent<Text> ().text = "Stability "+temp [0]+" %" ;

		//print (temp [9]);
		string t = "";
		switch (temp [2]) { //Gobierno
		case "0":
			t = "Select Goverment";
			break;
		case "1":
			t = "Democracy \n +25% Gold \n +15% Happyness \n -25% Fait";
			break;
		case "2":
			t = "Communism \n +25% Hammer \n +15% People \n -25% Food";
			break;
		case "3":
			t = "Nacionalism \n +25% Matter \n +15% Money \n -25% People";
			break;
		case "4":
			t = "Republic \n +25% Energy \n +15% Hammer \n -25% Str";
			break;
		case "5":
			t = "Teology \n +25% Fait \n +15% People \n -25% Energy";
			break;
		case "6":
			t = "Monarchy \n +25% Food \n +15% Str \n -25% Matter";
			break;
		case "7":
			t = "Dictatorship \n +25% Str \n +15% Fait \n -25% Happiness";
			break;
		}
		UI2INT [7].GetComponent<Image> ().sprite = Resources.Load<Sprite>
			("Gob/"+temp [2]) ;
		UI2INT[7].transform.GetChild (0).GetComponent<Text> ().text = t;

		switch (temp [3]) { //Law 1
		case "0":
			t = "Select Law 1";
			break;
		case "1":
			t = "Capitalism \n +5% Money";
			break;
		case "2":
			t = "Mercantilism \n +5% Gold";
			break;
		case "3":
			t = "Urbanization \n +5% People";
			break;
		case "4":
			t = "Feudalism \n +5% Food";
			break;
		case "5":
			t = "Unions \n +5% Matter";
			break;
		case "6":
			t = "Sovereighty \n +5% Hammer";
			break;
		case "7":
			t = "Secularism \n +5% Energy";
			break;
		case "8":
			t = "Meritocracy \n +5% Happiness";
			break;
		case "9":
			t = "Theocracy \n +5% Fait";
			break;
		case "10":
			t = "Discipline \n +5% Str";
			break;
	}
		UI2INT[8].GetComponent<Text> ().text += "Law 1: "+t+"\n";

		switch (temp [4]) { //Law 2
		case "0":
			t = "Select Law 2";
			break;
		case "1":
			t = "Capitalism \n +5% Money";
			break;
		case "2":
			t = "Mercantilism \n +5% Gold";
			break;
		case "3":
			t = "urbanization \n +5% People";
			break;
		case "4":
			t = "Feudalism \n +5% Food";
			break;
		case "5":
			t = "Unions \n +5% Matter";
			break;
		case "6":
			t = "Sovereighty \n +5% Hammer";
			break;
		case "7":
			t = "Secularism \n +5% Energy";
			break;
		case "8":
			t = "Meritocracy \n +5% Happiness";
			break;
		case "9":
			t = "Theocracy \n +5% Fait";
			break;
		case "10":
			t = "Discipline \n +5% Str";
			break;
		}
		UI2INT[8].GetComponent<Text> ().text += "Law 2: "+t;

		//print (SERVERUSERINFO [0].ToString());
		if (temp [8] != "0") { //SI NO ES IGUAL A 0 ESTA INVESTIGANDO ALGO POR TANTO SE BLOQUEA

			for (int a = 0; a < 1; a++) {  //Split tecnologia
				temp = temp [8].Split ("." [a]);}
			
			UI4RES [19].SetActive (true); //Actua como bloqueador del screen search
			UI4RES [16].GetComponent<Text>().text = "Energy Cost: "+temp [14];
			UI4RES [17].transform.GetChild(0).GetComponent<Text>().text = "Days: "+temp [0];
			UI4RES [17].GetComponent<Slider> ().interactable = false;
			UI4RES [18].SetActive (false); //GO
			
			for (int p = 1; p < 14; p++) { // Reinicia todos los objetos
				UI4RES [p].transform.GetChild (0).gameObject.SetActive (false); //Select
				if (p>4){
				UI4RES [p].transform.GetChild (1).gameObject.SetActive (true); // Deselect
				}
				if (temp [p] != "0") { //Esta seleccionado
					UI4RES [p].transform.GetChild (0).gameObject.SetActive (true); //Select
					if (p>4){
						UI4RES [p].transform.GetChild (1).gameObject.SetActive (false); // Deselect
					}
				}
			}
		} else { //Desbloqueo, no esta investigando nada
			UI4RES [17].GetComponent<Slider> ().interactable = true;
			UI4RES [18].SetActive (true);
			UI4RES [19].SetActive (false); //Actua como bloqueador del screen search
			for (int p = 1; p < 14; p++) { // Reinicia todos los objetos
				UI4RES [p].transform.GetChild (0).gameObject.SetActive (false); //Select
				Tech [p] = false;
				if (p>4){
					UI4RES [p].transform.GetChild (1).gameObject.SetActive (true); // Deselect
				}
			}
		}

	}

	IEnumerator TechPost(){ //Obtiene la informacion referente al servidor.
		int bolaint = 0;
		string send = "";
		string sendbol = "";
		send += TechDay + "/";
		for (int a = 1; a < 10; a++) {
			float cal = TechBonus [a];
			send += cal + "/";
			//UIRESEARCH [15].GetComponent<Text> ().text += cal+" \n"; Tech [1 - 13]
		}
		sendbol += TechDay + ".";
		for (int a = 1; a < 14; a++) {
			if (Tech [a] == true) {
				bolaint = 1;
			} else {
				bolaint = 0;
			}
			sendbol += bolaint + ".";
		}
		sendbol += TechTotalC + ".";
			print ("Se ejecuto");
			string PS = WEBTECHPOST + "&C=" + PullData [1] + "&d=" + TechDay + "&t=" + send + "&r=" + sendbol; //string HASH = Md5Sum.CallMd5 (Log[0].text+Log[1].text+Md5Sum.KEY);
			WWW hs_get = new WWW (PS);
			yield return hs_get;
			if (hs_get.error != null) {
				print ("ERROR, REINICIA EL JUEGO");
			} else {
				WaitServer = false;
				CallCourutine (4); //GETDATA()
			}
	}


	public void TechSelect(int id){
		Tech [id] = !Tech [id];
		TechCost = 0;
		for (int p = 1; p < 14; p++) { // Reinicia todos los objetos
			UI4RES [p].transform.GetChild (0).gameObject.SetActive (false);
			if (p > 4) {
				UI4RES [p].transform.GetChild (1).gameObject.SetActive (true);
			}
		}
		for (int p = 0; p < 11; p++) {
			TechBonus [p] = 0;
		}

		if (Tech [1] == true && Tech [2] == true) { //BIO
			UI4RES [5].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [5] = false;
		}

		if (Tech [2] == true) { //BAL
			UI4RES [6].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [6] = false;
		}

		if (Tech [3] == true && Tech [4] == true) { //ELEC
			UI4RES [7].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [7] = false;
		}

		if (Tech [4] == true) { //VUE
			UI4RES [8].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [8] = false;
		}


		if (Tech [8] == true && Tech [5] == true) { //GLOB
			UI4RES [9].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [9] = false;
		}
		if (Tech [6] == true && Tech [8] == true) { //COMP
			UI4RES [10].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [10] = false;
		}

		if (Tech [6] == true && Tech [7] == true) { //ROB
			UI4RES [11].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [11] = false;
		}

		if (Tech [9] == true && Tech [11] == true) { //INTER
			UI4RES [12].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [12] = false;
		}

		if (Tech [10] == true && Tech [11] == true) { //NANO INTER
			UI4RES [13].transform.GetChild (1).gameObject.SetActive (false);
		} else {
			Tech [13] = false;
		}

		if (Tech [1] == true) {//Agricultura
			UI4RES [1].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [4] += 1; //Food
			TechCost += 12;
		}
		if (Tech [2] == true) { //Ingenieria
			UI4RES [2].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [6] += 1; //Hammer
			TechCost += 12;
		}
		if (Tech [3] == true) { //Mineria
			UI4RES [3].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [5] += 1; //Matter
			TechCost += 12;
		}
		if (Tech [4] == true) { //Electricidad
			UI4RES [4].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [8] += 1; //Happy
			TechCost += 12;
		}
		if (Tech [5] == true) { //Biologia
			UI4RES [5].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [3] += 1;
			TechBonus [4] += 1;
			TechCost += 22;
		}
		if (Tech [6] == true) { //Balisitica
			UI4RES [6].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [9] += 1;
			TechCost += 22;
		}
		if (Tech [7] == true) { //Electronica
			UI4RES [7].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [5] += 1;
			TechBonus [7] += 1;
			TechCost += 22;
		}
		if (Tech [8] == true) { //Vuelo
			UI4RES [8].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [1] += 1;
			TechCost += 22;
		}
		if (Tech [9] == true) { //Globalizacion
			UI4RES [9].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [1] += 1;
			TechBonus [2] += 1;
			TechCost += 40;
		}
		if (Tech [10] == true) { //Computacion
			UI4RES [10].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [7] += 1;
			TechCost += 40;
		}
		if (Tech [11] == true) { //Robotica
			UI4RES [11].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [6] += 1;
			TechBonus [3] += 1;
			TechCost += 40;
		}

		if (Tech [12] == true) { //Internet
			UI4RES [12].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [2] += 1;
			TechBonus [8] += 1;
			TechCost += 100;
		}
		if (Tech [13] == true) { //Nanotecnologia
			UI4RES [13].transform.GetChild (0).gameObject.SetActive (true);
			TechBonus [6] += 1;
			TechBonus [9] += 1;
			TechCost += 100;
		}

		OnShowSearch ();
	}

	void TechDays(){
		TechDay = (int) UI4RES[17].GetComponent<Slider>().value;
		OnShowSearch ();
	}

	private int TechTotalC; //Costo de tecnologia en energia total final

	void OnShowSearch (){
		float tt1 = TechCost * TechDay;
		float tt2 = tt1 - ((TechDay /100f)*tt1 ); //16860
		TechTotalC = (int) tt2;
		if (TechTotalC < 0) {
			TechTotalC = 0;
		}
		UI4RES [15].GetComponent<Text> ().text = ""; //Cosas
		UI4RES [16].GetComponent<Text> ().text = "Energy Required: "+TechTotalC.ToString(); //Energia total
		UI4RES [17].transform.GetChild(0).GetComponent<Text>().text = TechDay.ToString();
		for (int a = 1; a < 11; a++) {
			float cal = TechBonus [a] * TechDay;
			UI4RES [15].GetComponent<Text> ().text += cal+" % \n";
		}
	}



}
