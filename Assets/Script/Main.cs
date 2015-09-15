using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	public static class ConnectionSettings{
		public static string ip;
		public static string msg="";
		public static TCPServer server;
		public static TCPClient client;
		
		public static Thread serverThread;
		public static Thread clientThread;
	}
	public static class WindowSettings{
		public static int defaultWidth=540;
		public static int defaultHeight=960;
		public static int screenWidth;
		public static int screenHeight;
		public static int startX;
		public static int startY;
		public static float screenRatioFactors=1;


		public static void init(){
			float screenRatioFactorsX=(float)Screen.width/(float)defaultWidth;
			float screenRatioFactorsY=(float)Screen.height/(float)defaultHeight;
			screenWidth=Screen.width;
			screenHeight=Screen.height;
			if(screenRatioFactorsX<screenRatioFactorsY){
				screenRatioFactors=screenRatioFactorsX;
				screenHeight=(int)(defaultHeight*screenRatioFactors);

			}else{
				screenRatioFactors=screenRatioFactorsY;
				screenWidth=(int)(defaultWidth*screenRatioFactors);
			}
			startX=(int)Mathf.Ceil((float)Screen.width/2-(((float)defaultWidth*screenRatioFactors)/2));
			startY=(int)Mathf.Ceil((float)Screen.height/2-(((float)defaultHeight*screenRatioFactors)/2));
		}
	}

	public static class GameGUISettings{
		public static int fontSize=36;
		public static int arrowButtonWidth=50;
		public static int arrowButtonHeight=50;
		public static int functionButtonWidth=200;
		public static int functionButtonHeight=50;

		public static int insideGameButtonWidth=150;
		public static int insideGameButtonHeight=50;
		public static int treasureWidth=36;
		public static int treasureHeight=36;
		public static int treasureMargin=10;

		public static int selectMargin=6;

		public static int buttonMargin=20;

		public static int screenCenterX;
		public static int screenCenterY;

		public static int treasureOffsetX;
		public static int treasureOffsetY;
		public static int buttonOffsetX;
		public static int buttonOffsetY;

		public static Color redText;


		public static void init(){
			fontSize=(int)(fontSize*WindowSettings.screenRatioFactors);
			arrowButtonWidth=(int)(arrowButtonWidth*WindowSettings.screenRatioFactors);
			arrowButtonHeight=(int)(arrowButtonHeight*WindowSettings.screenRatioFactors);
			functionButtonWidth=(int)(functionButtonWidth*WindowSettings.screenRatioFactors);
			functionButtonHeight=(int)(functionButtonHeight*WindowSettings.screenRatioFactors);
			insideGameButtonWidth=(int)(insideGameButtonWidth*WindowSettings.screenRatioFactors);
			insideGameButtonHeight=(int)(insideGameButtonHeight*WindowSettings.screenRatioFactors);
			treasureWidth=(int)(treasureWidth*WindowSettings.screenRatioFactors);
			treasureHeight=(int)(treasureHeight*WindowSettings.screenRatioFactors);
			treasureMargin=(int)(treasureMargin*WindowSettings.screenRatioFactors);
			buttonMargin=(int)(buttonMargin*WindowSettings.screenRatioFactors);
			selectMargin=(int)(selectMargin*WindowSettings.screenRatioFactors);
			screenCenterX=Screen.width/2;
			screenCenterY=Screen.height/2;

			treasureOffsetX=(int)(WindowSettings.startX+(WindowSettings.screenWidth-(treasureWidth*10+treasureMargin*(10-1)))/2);
			treasureOffsetY=(int)(WindowSettings.startY+185*WindowSettings.screenRatioFactors);
			buttonOffsetX=(int)(WindowSettings.startX);
			buttonOffsetY=(int)(WindowSettings.startY+640*WindowSettings.screenRatioFactors);

			redText=new Vector4(0.53f, 0.17f, 0.18f, 1);
		}
	}

	public static class GameTextureList{
		public static GameTexture startGame;
		public static GameTexture endGame;

		public static GameTexture practice;
		public static GameTexture vsComputer;
		public static GameTexture vsPlayer;
		public static GameTexture backPage2;

		public static GameTexture checkingNetwork;
		public static GameTexture pleaseWait;

		public static GameTexture local;
		public static GameTexture backPage4;

		public static GameTexture createRoom;
		public static GameTexture joinGame;
		public static GameTexture backPage5;

		public static GameTexture yourIPAddress;
		public static GameTexture ipAddress;
		public static GameTexture waitingForConnection;
		public static GameTexture backPage6;

		public static GameTexture enterAnotherIP;
		public static GameTexture ipSpace;
		public static GameTexture ipTextField;
		public static GameTexture connect;
		public static GameTexture backPage7;

		public static GameTexture synchronization;

		public static GameTexture easy;
		public static GameTexture normal;
		public static GameTexture hard;
		public static GameTexture backPage9;

		public static GameTexture back;
		public static GameTexture confirm;


		public static GameTexture[] treasureBG;
		public static GameTexture treasureSelect;
		public static GameTexture treasureRemainBG;

		public static GameTexture treasure;
		public static GameTexture[] num;

		public static GameTexture up;
		public static GameTexture down;
		public static GameTexture left;
		public static GameTexture right;

		public static GameTexture yoursOthers;
		public static GameTexture yours;
		public static GameTexture others;

		public static GameTexture waitForYourTurn;
		public static GameTexture youWin;
		public static GameTexture youLost;
		public static GameTexture endConfirm;

		public static GameTexture backGame;

		public static GameTexture commonBackGround;
		public static GameTexture firstPage;
		public static GameTexture playPage;

		public static void init(){
			// Load resource (texture)
			treasure = new GameTexture(0,0,(Texture2D)Resources.Load("Texture/treasure"));

			num=new GameTexture[8];
			for(int i=0;i<8;i++){
				num[i] = new GameTexture(0,0,(Texture2D)Resources.Load("Texture/"+(i+1)));
			}
			treasureBG=new GameTexture[3];
			for(int i=0;i<3;i++){
				treasureBG[i] = new GameTexture(0,0,(Texture2D)Resources.Load("Texture/treasureBG"+(i+1)));
			}
			treasureSelect = new GameTexture(0,0,(Texture2D)Resources.Load("Texture/treasureSelect"));
			treasureRemainBG = new GameTexture(151,73,(Texture2D)Resources.Load("Texture/treasureRemainBG"));

			up=new GameTexture(55,55,(Texture2D)Resources.Load("Texture/up"));
			down=new GameTexture(55,55,(Texture2D)Resources.Load("Texture/down"));
			left=new GameTexture(55,55,(Texture2D)Resources.Load("Texture/left"));
			right=new GameTexture(55,55,(Texture2D)Resources.Load("Texture/right"));

			yoursOthers=new GameTexture(86,100,(Texture2D)Resources.Load("Texture/yoursOthers"));
			yours=new GameTexture(86,50);
			others=new GameTexture(86,50);

			waitForYourTurn=new GameTexture(331,36,(Texture2D)Resources.Load("Texture/waitForYourTurn"));
			youWin=new GameTexture(222,47,(Texture2D)Resources.Load("Texture/youWin"));
			youLost=new GameTexture(232,42,(Texture2D)Resources.Load("Texture/youLost"));



			confirm=new GameTexture(114,32,(Texture2D)Resources.Load("Texture/confirm"));
			endConfirm=new GameTexture(confirm);

			startGame = new GameTexture(188,50,(Texture2D)Resources.Load("Texture/startGame"));
			endGame = new GameTexture(95,50,(Texture2D)Resources.Load("Texture/end"));
			
			back=new GameTexture(139,50,(Texture2D)Resources.Load("Texture/back"));

			checkingNetwork=new GameTexture(518,46,(Texture2D)Resources.Load("Texture/checkingNetwork"));
			pleaseWait=new GameTexture(400,41,(Texture2D)Resources.Load("Texture/pleaseWait"));

			practice=new GameTexture(224,50,(Texture2D)Resources.Load("Texture/practice"));
			vsComputer=new GameTexture(312,50,(Texture2D)Resources.Load("Texture/vsComputer"));
			vsPlayer=new GameTexture(262,50,(Texture2D)Resources.Load("Texture/vsPlayer"));

			local=new GameTexture(141,50,(Texture2D)Resources.Load("Texture/local"));

			createRoom=new GameTexture(264,50,(Texture2D)Resources.Load("Texture/createRoom"));
			joinGame=new GameTexture(236,50,(Texture2D)Resources.Load("Texture/joinGame"));
			
			yourIPAddress=new GameTexture(297,41,(Texture2D)Resources.Load("Texture/yourIPAddress"));
			ipAddress=new GameTexture(540,41);
			waitingForConnection=new GameTexture(405,42,(Texture2D)Resources.Load("Texture/waitingForConnection"));
			
			enterAnotherIP=new GameTexture(330,66,(Texture2D)Resources.Load("Texture/enterAnotherIP"));
			ipSpace=new GameTexture(519,138,(Texture2D)Resources.Load("Texture/ipSpace"));
			ipTextField=new GameTexture(346,92);
			connect=new GameTexture(181,50,(Texture2D)Resources.Load("Texture/connect"));
			
			synchronization=new GameTexture(452,37,(Texture2D)Resources.Load("Texture/synchronization"));

			easy=new GameTexture(122,50,(Texture2D)Resources.Load("Texture/easy"));
			normal=new GameTexture(144,50,(Texture2D)Resources.Load("Texture/normal"));
			hard=new GameTexture(133,50,(Texture2D)Resources.Load("Texture/hard"));


			
			commonBackGround = new GameTexture(0,0,(Texture2D)Resources.Load("Texture/commonBackGround"));
			firstPage = new GameTexture(0,0,(Texture2D)Resources.Load("Texture/firstPage"));
			playPage = new GameTexture(0,0,(Texture2D)Resources.Load("Texture/playPage"));

			backPage2=new GameTexture(back);
			backPage4=new GameTexture(back);
			backPage5=new GameTexture(back);
			backPage6=new GameTexture(back);
			backPage7=new GameTexture(back);
			backPage9=new GameTexture(back);
			backGame=new GameTexture(back);


			// set coordinate of each GUI element
			startGame.set(GameGUISettings.screenCenterX-startGame.width/2,GameGUISettings.screenCenterY+startGame.height*3,startGame.width,startGame.height);
			endGame.set(GameGUISettings.screenCenterX-endGame.width/2,GameGUISettings.screenCenterY+endGame.height*4+GameGUISettings.buttonMargin,endGame.width,endGame.height);

			practice.set(GameGUISettings.screenCenterX-practice.width/2,GameGUISettings.screenCenterY-(practice.height+GameGUISettings.buttonMargin)*2,practice.width,practice.height);
			vsComputer.set(GameGUISettings.screenCenterX-vsComputer.width/2,GameGUISettings.screenCenterY-(vsComputer.height+GameGUISettings.buttonMargin),vsComputer.width,vsComputer.height);
			vsPlayer.set(GameGUISettings.screenCenterX-vsPlayer.width/2,GameGUISettings.screenCenterY,vsPlayer.width,vsPlayer.height);
			backPage2.set(GameGUISettings.screenCenterX-back.width/2,GameGUISettings.screenCenterY+(back.height+GameGUISettings.buttonMargin),back.width,back.height);
			
			checkingNetwork.set(GameGUISettings.screenCenterX-checkingNetwork.width/2,GameGUISettings.screenCenterY-(checkingNetwork.height+GameGUISettings.buttonMargin),checkingNetwork.width,checkingNetwork.height);
			pleaseWait.set(GameGUISettings.screenCenterX-pleaseWait.width/2,GameGUISettings.screenCenterY,pleaseWait.width+GameGUISettings.buttonMargin,pleaseWait.height);

			local.set(GameGUISettings.screenCenterX-local.width/2,GameGUISettings.screenCenterY-(local.height+GameGUISettings.buttonMargin),local.width,local.height);
			backPage4.set(GameGUISettings.screenCenterX-back.width/2,GameGUISettings.screenCenterY+GameGUISettings.buttonMargin,back.width,back.height);
		
			createRoom.set(GameGUISettings.screenCenterX-createRoom.width/2,GameGUISettings.screenCenterY-(createRoom.height+GameGUISettings.buttonMargin)*3/2,createRoom.width,createRoom.height);
			joinGame.set(GameGUISettings.screenCenterX-joinGame.width/2,GameGUISettings.screenCenterY-(joinGame.height+GameGUISettings.buttonMargin)/2,joinGame.width,joinGame.height);
			backPage5.set(GameGUISettings.screenCenterX-back.width/2,GameGUISettings.screenCenterY+(back.height+GameGUISettings.buttonMargin)/2,back.width,back.height);
		

			yourIPAddress.set(GameGUISettings.screenCenterX-yourIPAddress.width/2,GameGUISettings.screenCenterY-(yourIPAddress.height+GameGUISettings.buttonMargin)*2,yourIPAddress.width,yourIPAddress.height);
			ipAddress.set(GameGUISettings.screenCenterX-ipAddress.width/2,GameGUISettings.screenCenterY-(ipAddress.height+GameGUISettings.buttonMargin),ipAddress.width,ipAddress.height);
			waitingForConnection.set(GameGUISettings.screenCenterX-waitingForConnection.width/2,GameGUISettings.screenCenterY,waitingForConnection.width,waitingForConnection.height);
			backPage6.set(GameGUISettings.screenCenterX-back.width/2,GameGUISettings.screenCenterY+(back.height+GameGUISettings.buttonMargin),back.width,back.height);

			enterAnotherIP.set(GameGUISettings.screenCenterX-enterAnotherIP.width/2,GameGUISettings.screenCenterY-(enterAnotherIP.height+GameGUISettings.buttonMargin)*2,enterAnotherIP.width,enterAnotherIP.height);
			ipSpace.set(GameGUISettings.screenCenterX-ipSpace.width/2,GameGUISettings.screenCenterY-(ipSpace.height+GameGUISettings.buttonMargin)/3*2,ipSpace.width,ipSpace.height);
			ipTextField.set(GameGUISettings.screenCenterX-ipTextField.width/2,GameGUISettings.screenCenterY-(ipTextField.height+GameGUISettings.buttonMargin)*3/4,ipTextField.width,ipTextField.height);
			connect.set(GameGUISettings.screenCenterX-connect.width/2,GameGUISettings.screenCenterY+(connect.height+GameGUISettings.buttonMargin)/2,connect.width,connect.height);
			backPage7.set(GameGUISettings.screenCenterX-back.width/2,GameGUISettings.screenCenterY+(back.height+GameGUISettings.buttonMargin)*3/2,back.width,back.height);

			synchronization.set(GameGUISettings.screenCenterX-synchronization.width/2,GameGUISettings.screenCenterY-synchronization.height/2,synchronization.width,synchronization.height);

			easy.set(GameGUISettings.screenCenterX-easy.width/2,GameGUISettings.screenCenterY-(easy.height+GameGUISettings.buttonMargin)*2,easy.width,easy.height);
			normal.set(GameGUISettings.screenCenterX-normal.width/2,GameGUISettings.screenCenterY-(normal.height+GameGUISettings.buttonMargin),normal.width,normal.height);
			hard.set(GameGUISettings.screenCenterX-hard.width/2,GameGUISettings.screenCenterY,hard.width,hard.height);
			backPage9.set(GameGUISettings.screenCenterX-back.width/2,GameGUISettings.screenCenterY+(back.height+GameGUISettings.buttonMargin),back.width,back.height);

			up.set(GameGUISettings.screenCenterX-up.width/2,GameGUISettings.buttonOffsetY+GameGUISettings.treasureMargin,up.width,up.height);
			down.set(GameGUISettings.screenCenterX-down.width/2,GameGUISettings.buttonOffsetY+GameGUISettings.treasureMargin+down.height*5/2,down.width,down.height);
			left.set(GameGUISettings.screenCenterX-left.width*15/8,GameGUISettings.buttonOffsetY+GameGUISettings.treasureMargin+left.height*5/4,left.width,left.height);
			right.set(GameGUISettings.screenCenterX+right.width*7/8,GameGUISettings.buttonOffsetY+GameGUISettings.treasureMargin+right.height*5/4,right.width,right.height);

			confirm.set(GameGUISettings.screenCenterX+confirm.width,GameGUISettings.buttonOffsetY+GameGUISettings.treasureMargin+confirm.height*5/2,confirm.width,confirm.height);
			treasureRemainBG.set(GameGUISettings.screenCenterX-treasureRemainBG.width*5/3,GameGUISettings.buttonOffsetY+GameGUISettings.treasureMargin+treasureRemainBG.height*4/5,treasureRemainBG.width,treasureRemainBG.height);

			yoursOthers.set(WindowSettings.startX+yoursOthers.width/5,WindowSettings.startY+yoursOthers.height/5,yoursOthers.width,yoursOthers.height);
			yours.set(WindowSettings.startX+yours.width*6/5,WindowSettings.startY+yoursOthers.height/5,yours.width,yours.height);
			others.set(WindowSettings.startX+others.width*6/5,WindowSettings.startY+yoursOthers.height/5+others.height,others.width,others.height);

			waitForYourTurn.set(GameGUISettings.screenCenterX-waitForYourTurn.width/2,GameGUISettings.buttonOffsetY+waitForYourTurn.height/2,waitForYourTurn.width,waitForYourTurn.height);
			youWin.set(GameGUISettings.screenCenterX-youWin.width/2,GameGUISettings.buttonOffsetY+youWin.height/2,youWin.width,youWin.height);
			youLost.set(GameGUISettings.screenCenterX-youLost.width/2,GameGUISettings.buttonOffsetY+youLost.height/2,youLost.width,youLost.height);
			endConfirm.set(GameGUISettings.screenCenterX-endConfirm.width/2,GameGUISettings.buttonOffsetY+youWin.height*3/2,endConfirm.width,endConfirm.height);

			backGame.set(WindowSettings.startX+WindowSettings.screenWidth-backGame.width*4/3,WindowSettings.startY+backGame.height,backGame.width,backGame.height);

		}
	}

	public class GameTexture{
		public int width;
		public int height;
		public Rect info;
		public Texture2D texture;

		public GameTexture(){
			info=new Rect(0,0,0,0);
		}
		public GameTexture(GameTexture gameTexture){
			this.width=gameTexture.width;
			this.height=gameTexture.height;
			this.texture=gameTexture.texture;
		}
		public GameTexture(int width, int height){
			this.width=(int)(width*WindowSettings.screenRatioFactors);
			this.height=(int)(height*WindowSettings.screenRatioFactors);
			this.texture=null;
		}
		public GameTexture(int width, int height, Texture2D texture){
			this.width=(int)(width*WindowSettings.screenRatioFactors);
			this.height=(int)(height*WindowSettings.screenRatioFactors);
			this.texture=texture;
		}

		public GameTexture(int x, int y, int width, int height){
			info=new Rect(x,y,width,height);
			this.texture=null;
		}
		public GameTexture(int x, int y, int width, int height,Texture2D texture){
			info=new Rect(x,y,width,height);
			this.texture=texture;
		}
		public void set(int x, int y, int width, int height){
			info=new Rect(x,y,width,height);
		}
	}
	
	public class Pos{
		public int x;
		public int y;
		public Pos(){
		}
		public Pos(int x, int y){
			this.x=x;
			this.y=y;
		}
	}



	public class Treasure{
		public int isTreasure; //0:no, 1:yes
		public int num;
		public int numRemain;
		public bool isFind;
		public int isTreasureAI; 
		public List<Pos> surround;

		public Treasure(){
			isTreasure=0;
			num=0;
			numRemain=0;
			isFind=false;
			isTreasureAI=1;
			surround=new List<Pos>();
		}
	}

	public class TCPServer{
		public bool socketReady = false;
		public TcpListener server;
		public TcpClient mySocket;
		public NetworkStream theStream;
		public StreamWriter theWriter;
		public StreamReader theReader;

		public TCPServer(){
		}

		public void StartServer(){
			server=null;   
			try{
				Int32 port = 13000;
				IPAddress localAddr = IPAddress.Parse(ConnectionSettings.ip);

				server = new TcpListener(localAddr, port);

				server.Start();
				mySocket = server.AcceptTcpClient();
				socketReady = true;

				theStream = mySocket.GetStream();
				theWriter = new StreamWriter(theStream);
				theReader = new StreamReader(theStream);

			}catch(SocketException e){
				socketReady=false;
				print(e);
			}
		}

		public void WriteSocket(string theLine) {
			try{
				if (!socketReady){
					return;
				}
				String tmpString = theLine + "\r\n";
				theWriter.Write(tmpString);
				theWriter.Flush();
			}catch(SocketException e){
				socketReady=false;
				print(e);
			}
		}

		public String ReadSocket() {
			try{
				if (!socketReady){
					return "";
				}
				if (theStream.DataAvailable){
					string line=theReader.ReadLine();
					return line;
				}
			}catch(SocketException e){
				socketReady=false;
				print(e);
			}
			return "";
		}

		public void StopServer(){
			try{
				server.Stop();
				if (!socketReady)
					return;
				theWriter.Close();
				theReader.Close();
				mySocket.Close();
				theStream.Close();
			

				socketReady = false;
			}catch(SocketException e){
				socketReady=false;
				print(e);
			}
		}
	}

	public class TCPClient{
		public bool socketReady = false;
		public String Host="";
		public Int32 Port = 13000; 
		public TcpClient mySocket;
		public NetworkStream theStream;
		public StreamWriter theWriter;
		public StreamReader theReader;
		public string textFieldString = "";

		public void StartClient(){
			SetupSocket();
		}

		public void SetupSocket() {
			try {
				mySocket = new TcpClient(Host, Port);
				theStream = mySocket.GetStream();
				theWriter = new StreamWriter(theStream);
				theReader = new StreamReader(theStream);
				socketReady = true;
			}catch (Exception e) {
				socketReady=false;
				print(e);
			}
		}
		
		public void WriteSocket(string theLine) {
			try{
				if (!socketReady){
					return;
				}
				String tmpString = theLine + "\r\n";
				theWriter.Write(tmpString);
				theWriter.Flush();
			}catch (Exception e) {
				socketReady=false;
				print(e);
			}
		}
		
		public String ReadSocket() {
			try{
				if (!socketReady)
					return "";
				if (theStream.DataAvailable){
					string line=theReader.ReadLine();
					return line;
				}
			}catch (Exception e) {
				socketReady=false;
				print(e);
			}
			return "";
		}

		public void CloseSocket() {
			try{

				if (!socketReady)
					return;
				theWriter.Close();
				theReader.Close();
				mySocket.Close();
				theStream.Close();
				socketReady = false;
			}catch (Exception e) {
				socketReady=false;
				print(e);
			}
		}
		
		public void MaintainConnection(){
			if(!theStream.CanRead) {
				SetupSocket();
			}
		}
	}

	int row=10;
	int col=10;
	int maxNOfTreasure=21;
	int selectX=0;
	int selectY=0;


	int playType=1;
	int computerLevel=1;
	bool yourTurn=true;
	int turn=0;
	int nOfPlayer=2;
	int turnfactor=0;

	int currentNTreasure=0;
	int totalNTreasure=0;

	int currentOtherNTreasure=0;
	int totalOtherNTreasure=0;

	int page=1;

	float lastCheckNetwork=0;
	bool confirmBtn=false;
	bool startServerBtn=false;
	bool startClientBtn=false;
	
	bool end=false;
	bool win=false;

	bool networkStart=false;

	Treasure[,] treasures;
	List<int> unFindTreasure=new List<int>();


	int sendDataState=0;//0:not send, 1:sent but waiting response, 2:timeout, 3:sent but response error, 4:sent and get response
	int recieveDataState=0;//0:not recieve, 1:recieve and finish synchronize, 2:response sent
	
	// Use this for initialization
	void Start () {
		WindowSettings.init();
		GameGUISettings.init();
		ConnectionSettings.server=new TCPServer();
		ConnectionSettings.client=new TCPClient();
		GameTextureList.init();
	}


	void FixedUpdate (){
		if(page==1){
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				Application.Quit(); 
			}
		}else if(page==2){
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				page=1; 
			}
		}else if(page==3){
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				page=2; 
			}
		}else if(page==4){
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				page=2; 
			}
		}else if(page==5){
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				page=4; 
			}
		}else if(page==6){
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				page=5; 
			}
		}else if(page==7){
			if (Input.GetKeyDown(KeyCode.Escape)) { 
				page=6; 
			}
		}else if(page==100){
			if (Input.GetKeyDown(KeyCode.Escape)) {
				endGame ();

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckNetworkEnvironment();
		if(playType==2 && end){
			if(turnfactor==0 && ConnectionSettings.server.socketReady){
				ConnectionSettings.server.StopServer();
				ConnectionSettings.serverThread.Abort();
			}else if(turnfactor==1 && ConnectionSettings.client.socketReady){
				ConnectionSettings.client.CloseSocket();
				ConnectionSettings.clientThread.Abort();
			}
		}
		if(page==3 && networkStart){
			page=4;
		}
		if(page==6 && ConnectionSettings.server.socketReady){
			InitTreasure();
			playType=2;
			turnfactor=0;
			yourTurn=true;
			page=8;
		}
		if(page==7 && ConnectionSettings.client.socketReady){
			InitTreasure();
			playType=2;
			turnfactor=1;
			yourTurn=false;
			page=8;
		}

		if(page==8){
			if(turnfactor==0){
				// send treasure data
				if(sendDataState==0){
					string sendData="";
					for(int i=0;i<row;i++){
						for(int j=0;j<col;j++){
							if(i>0 || (i==0 && j>0)){
								sendData+=" ";
							}
							sendData+=treasures[i,j].isTreasure+","+treasures[i,j].num;
						}
					}
					ConnectionSettings.server.WriteSocket(sendData);
					sendDataState=1;
				}else if(sendDataState==1){
					string readMsg=ConnectionSettings.server.ReadSocket();
					if(readMsg.Equals("wr")){
						sendDataState=4;
						page=100;
					}
				}
			}else{
				//recieve treasure data
				if(recieveDataState==0){
					string readMsg=ConnectionSettings.client.ReadSocket();
					if(readMsg!=""){
						string[] dataRecord=readMsg.Split(' ');
						for(int i=0;i<dataRecord.Length;i++){
							string[] dataAttr=dataRecord[i].Split(',');
							int sX=i/col;
							int sY=i%col;
							treasures[sX,sY].isTreasure=int.Parse(dataAttr[0]);
							treasures[sX,sY].num=int.Parse(dataAttr[1]);
						}

						recieveDataState=1;
					}

				}else if(recieveDataState==1){
					ConnectionSettings.client.WriteSocket("wr");
					recieveDataState=2;
					page=100;
				}
			}

		}



		if(playType==2 && page==100){
			if(!networkStart){
				page=5;
			}
			if(!yourTurn){
				// get data from others
				if(turnfactor==0){
					string readMsg=ConnectionSettings.server.ReadSocket();
					if(!readMsg.Equals("")){
						string[] msgAttr=readMsg.Split(' ');
						string command=msgAttr[0];
						if(command.Equals("ft")){
							int sX=int.Parse(msgAttr[1]);
							int sY=int.Parse(msgAttr[2]);
							selectX=sX;
							selectY=sY;
							FindTreasure(sX,sY);
							ConnectionSettings.server.WriteSocket("cft");
						}else if(command.Equals("et")){
							ConnectionSettings.server.WriteSocket("cet");
							ChangeTurn();
						}
					}
				}else if(turnfactor==1){
					string readMsg=ConnectionSettings.client.ReadSocket();
					if(!readMsg.Equals("")){
						string[] msgAttr=readMsg.Split(' ');
						string command=msgAttr[0];
						if(command.Equals("ft")){
							int sX=int.Parse(msgAttr[1]);
							int sY=int.Parse(msgAttr[2]);
							selectX=sX;
							selectY=sY;
							FindTreasure(sX,sY);
							ConnectionSettings.client.WriteSocket("cft");
						}else if(command.Equals("et")){
							ConnectionSettings.client.WriteSocket("cet");
							ChangeTurn();

						}
					}
				}
			}
		}
		
		if(confirmBtn){
			confirmBtn=false;
			if(playType==2){
				// send data to others
				if(turnfactor==0){
					FindTreasure(selectX,selectY);
					ConnectionSettings.server.WriteSocket("ft "+selectX+" "+selectY);
					bool confirmFindTresure=false;
					while(!confirmFindTresure){
						string readMsg=ConnectionSettings.server.ReadSocket();
						if(readMsg.Equals("cft")){
							confirmFindTresure=true;
						}
					}


					if(currentNTreasure==0){
						ConnectionSettings.server.WriteSocket("et");
						bool confirmEndTurn=false;
						while(!confirmEndTurn){
							string readMsg=ConnectionSettings.server.ReadSocket();
							if(readMsg.Equals("cet")){
								confirmEndTurn=true;
							}
						}
						ChangeTurn();
					}
					currentNTreasure=0;
				}else if(turnfactor==1){
					FindTreasure(selectX,selectY);
					ConnectionSettings.client.WriteSocket("ft "+selectX+" "+selectY);
					bool confirmFindTresure=false;
					while(!confirmFindTresure){
						string readMsg=ConnectionSettings.client.ReadSocket();
						if(readMsg.Equals("cft")){
							confirmFindTresure=true;
						}
					}
					if(currentNTreasure==0){
						ConnectionSettings.client.WriteSocket("et");
						bool confirmEndTurn=false;
						while(!confirmEndTurn){
							string readMsg=ConnectionSettings.client.ReadSocket();
							if(readMsg.Equals("cet")){
								confirmEndTurn=true;
							}
						}
						ChangeTurn();
					}
					currentNTreasure=0;
				}
			}else{
				FindTreasure(selectX,selectY);
				if(currentNTreasure==0){
					ChangeTurn();
				}
				currentNTreasure=0;
			}
		}
		if(startServerBtn){
			ConnectionSettings.serverThread = new Thread (ConnectionSettings.server.StartServer);
			ConnectionSettings.serverThread.Start ();
			startServerBtn=false;
		}
		
		if(startClientBtn){
			ConnectionSettings.clientThread =new Thread(ConnectionSettings.client.StartClient);
			ConnectionSettings.clientThread.Start();
			startClientBtn=false;
		}
	}

	void endGame(){
		if(playType==2){
			if(turnfactor==0){
				ConnectionSettings.server.StopServer();
				ConnectionSettings.serverThread.Abort();
			}else{
				ConnectionSettings.client.CloseSocket();
				ConnectionSettings.clientThread.Abort();
			}
			page=5;
		}else if(playType==1){
			page=9; 
		}else{
			page=2;
		}
		InitGame();

	}

	void CheckNetworkEnvironment(){
		if(Time.time-lastCheckNetwork>2 && !networkStart){
			string ns=Network.TestConnection().ToString();

			if(!ns.Equals("Undetermined")){
				networkStart=true;
				ConnectionSettings.ip=Network.player.ipAddress;
			}
			lastCheckNetwork=Time.time;
		}
	}

	void InitGame(){
		selectX=0;
		selectY=0;

		yourTurn=true;
		turn=0;
		turnfactor=0;
		
		currentNTreasure=0;
		totalNTreasure=0;
		
		currentOtherNTreasure=0;
		totalOtherNTreasure=0;
		
		lastCheckNetwork=0;
		confirmBtn=false;
		startServerBtn=false;
		startClientBtn=false;
		
		end=false;
		win=false;

		unFindTreasure=new List<int>();
		
		sendDataState=0;
		recieveDataState=0;
	}

	List<Pos> getSurroundTreasure(int i, int j){
		List<Pos> treasurePos=new List<Pos>();
		if(i>0){
			if(j>0){
				treasurePos.Add (new Pos(i-1,j-1));
			}
			treasurePos.Add (new Pos(i-1,j));
			if(j<col-1){
				treasurePos.Add (new Pos(i-1,j+1));
			}
		}
		if(j>0){
			treasurePos.Add (new Pos(i,j-1));
		}
		if(j<col-1){
			treasurePos.Add (new Pos(i,j+1));
		}
		if(i<row-1){
			if(j>0){
				treasurePos.Add (new Pos(i+1,j-1));
			}
			treasurePos.Add (new Pos(i+1,j));
			if(j<col-1){
				treasurePos.Add (new Pos(i+1,j+1));
			}
		}
		return treasurePos;
	}

	void InitTreasure(){
		InitGame();
		treasures=new Treasure[row,col];
		List<int> randomN=new List<int>();
		for(int i=0;i<row*col;i++){
			randomN.Add(i);
			treasures[i/col,i%col]=new Treasure();
			unFindTreasure.Add(i);
		}
		for(int i=0;i<maxNOfTreasure;i++){
			int seed=UnityEngine.Random.Range(0,randomN.Count-1);
			treasures[randomN[seed]/col,randomN[seed]%col].isTreasure=1;
			randomN.Remove(randomN[seed]);
		}
		for(int i=0;i<row;i++){
			for(int j=0;j<col;j++){
				int num=0;
				treasures[i,j].surround=getSurroundTreasure(i,j);
				for(int k=0;k<treasures[i,j].surround.Count;k++){
					int x=treasures[i,j].surround[k].x;
					int y=treasures[i,j].surround[k].y;
					num+=treasures[x,y].isTreasure;
				}
				if(treasures[i,j].isTreasure==0){
					treasures[i,j].num=num;
					treasures[i,j].numRemain=num;
				}
			}
		}
	}

	void ChangeTurn(){
		turn++;
		if(playType==1){
			if(turn%nOfPlayer==turnfactor){
				yourTurn=true;
			}else{
				yourTurn=false;
			}
			if(!yourTurn){
				StartCoroutine(AITurn());
			}
		}else if(playType==2){
			if(turn%nOfPlayer==turnfactor){
				yourTurn=true;
			}else{
				yourTurn=false;
			}
		}
		currentNTreasure=0;
	}

	void setIsTreasureAI(){
		for(int i=0;i<row;i++){
			for(int j=0;j<col;j++){
				if(treasures[i,j].numRemain==0 && treasures[i,j].isFind && treasures[i,j].isTreasure==0){
					for(int k=0;k<treasures[i,j].surround.Count;k++){
						int x=treasures[i,j].surround[k].x;
						int y=treasures[i,j].surround[k].y;
						treasures[x,y].isTreasureAI=0;
					}
				}
			}
		}

	}

	void setNumRemain(){
		for(int i=0;i<row;i++){
			for(int j=0;j<col;j++){
				if(treasures[i,j].isTreasure==0){
					int num=0;
					for(int k=0;k<treasures[i,j].surround.Count;k++){
						int x=treasures[i,j].surround[k].x;
						int y=treasures[i,j].surround[k].y;
						if(treasures[x,y].isFind && treasures[x,y].isTreasure==1){
							num++;
						}
					}
					treasures[i,j].numRemain=treasures[i,j].num-num;
				}
			}
		}
		
	}

	void FindTreasure(int i, int j){
		treasures[i,j].isFind=true;
		unFindTreasure.Remove(i*row+j);
		if(treasures[i,j].isTreasure==1){
			if(yourTurn){
				totalNTreasure++;
				currentNTreasure++;
			}else{
				totalOtherNTreasure++;
				currentOtherNTreasure++;
			}
			CheckEndGame();
		}
		setNumRemain();
		setIsTreasureAI();
		

		if(treasures[i,j].num==0 && treasures[i,j].isTreasure==0){
			for(int k=0;k<treasures[i,j].surround.Count;k++){
				int x=treasures[i,j].surround[k].x;
				int y=treasures[i,j].surround[k].y;
				if(!treasures[x,y].isFind && treasures[x,y].isTreasure==0){
					FindTreasure(x,y);
				}
			}
		}		
	}
	

	void CheckEndGame(){
		if(playType==0){
			if(totalNTreasure==maxNOfTreasure){
				end=true;
			}
		}else{
			if(totalNTreasure>=maxNOfTreasure/2+1){
				end=true;
				win=true;
			}else if(totalOtherNTreasure>=maxNOfTreasure/2+1){
				end=true;
				win=false;
			}
		}
	}


	List<Pos> AddUnFind(int i, int j, int level){
		List<Pos> unFind=new List<Pos>();
		for(int k=0;k<treasures[i,j].surround.Count;k++){
			int x=treasures[i,j].surround[k].x;
			int y=treasures[i,j].surround[k].y;
			if(!treasures[x,y].isFind && (treasures[x,y].isTreasureAI==1 || level<3)){
				unFind.Add(new Pos(x,y));
			}
		}
		return unFind;
	}
	
	IEnumerator AITurn(){
		bool hasFound=true;
		if(computerLevel>=3 && hasFound){
			bool findAtLeastOne=true;
			while(findAtLeastOne && !end){
				findAtLeastOne=false;
				for(int i=0;i<row && !end;i++){
					for(int j=0;j<col && !end;j++){
						//print (i+" "+j+" "+treasures[i,j].numRemain);
						if(treasures[i,j].isFind && treasures[i,j].numRemain>0){
							List<Pos> unFind=AddUnFind(i,j,3);

							print ("in num:"+i+" "+j+" "+unFind.Count);
							if(treasures[i,j].numRemain==unFind.Count){
								findAtLeastOne=true;
								for(int l=0;l<unFind.Count && !end;l++){
									int x=unFind[l].x;
									int y=unFind[l].y;
									print ("find3: "+x+" "+y);
									yield return new WaitForSeconds(2);
									FindTreasure(x,y);
									selectX=x;
									selectY=y;
									currentOtherNTreasure=0;
								}
							}
						}
					}
				}
			}
			
		}
		if(computerLevel==2 && hasFound && !end){
			for(int i=0;i<row && !end;i++){
				for(int j=0;j<col && !end;j++){
					if(treasures[i,j].isFind && treasures[i,j].numRemain>0){
						List<Pos> unFind=AddUnFind(i,j,2);
						if(treasures[i,j].numRemain==unFind.Count){
							for(int l=0;l<unFind.Count && !end;l++){
								int x=unFind[l].x;
								int y=unFind[l].y;
								print ("find2: "+x+" "+y);
								yield return new WaitForSeconds(2);
								FindTreasure(x,y);
								selectX=x;
								selectY=y;
								currentOtherNTreasure=0;
							}
						}
					}
				}
			}
			
			
		}

		if(computerLevel>=1 && hasFound){
			for(int i=0;i<row && hasFound && !end;i++){
				for(int j=0;j<col && hasFound && !end;j++){
					if(treasures[i,j].isFind && treasures[i,j].isTreasure==0 && treasures[i,j].numRemain>0){
						List<Pos> unFind=AddUnFind(i,j,computerLevel);
						while(unFind.Count>0 && hasFound && !end && treasures[i,j].numRemain>0){
							int random=UnityEngine.Random.Range(0,unFind.Count-1);
							int x=unFind[random].x;
							int y=unFind[random].y;
							unFind.Remove(unFind[random]);
							print ("find1: "+x+" "+y);
							yield return new WaitForSeconds(2);
							FindTreasure(x,y);
							selectX=x;
							selectY=y;

							if(currentOtherNTreasure==0){
								hasFound=false;
							}
							currentOtherNTreasure=0;
						}
					}
				}
			}
		}
		while(hasFound && !end && unFindTreasure.Count>0){
			int random=UnityEngine.Random.Range(0,unFindTreasure.Count-1);
			int n=unFindTreasure[random];
			int x=n/col;
			int y=n%col;
			print ("find0: "+x+" "+y);
			yield return new WaitForSeconds(2);
			FindTreasure(x,y);
			selectX=x;
			selectY=y;
			if(currentOtherNTreasure==0){
				hasFound=false;
			}
			currentOtherNTreasure=0;
		}
		ChangeTurn();
	}


	//-------------------------------------------Draw GUI start-------------------------------------------
	void DrawEnd(){
		if(playType==0){
			GUI.Box(new Rect(GameTextureList.youWin.info.x,GameTextureList.youWin.info.x,WindowSettings.screenWidth,GameTextureList.youWin.info.height),new GUIContent("You found all treasures"));
		}else{
			if(win){
				GUI.Box(GameTextureList.youWin.info,GameTextureList.youWin.texture);
			}else{
				GUI.Box(GameTextureList.youLost.info,GameTextureList.youLost.texture);
			}
		}
		if(GUI.Button(GameTextureList.endConfirm.info,GameTextureList.endConfirm.texture)){
			endGame();
		}
	}

	void DrawTreasure(){
		GUI.Box(GameTextureList.yoursOthers.info,GameTextureList.yoursOthers.texture);
		GUI.color=GameGUISettings.redText;
		GUI.Label(GameTextureList.yours.info,""+totalNTreasure);
		GUI.Label(GameTextureList.others.info,""+totalOtherNTreasure);
		GUI.color = Color.white;
		if(GUI.Button(GameTextureList.backGame.info,GameTextureList.backGame.texture)){
			endGame();
		}

		GUI.Box(new Rect(GameGUISettings.treasureOffsetX+selectX*(GameGUISettings.treasureWidth+GameGUISettings.treasureMargin)-GameGUISettings.selectMargin,GameGUISettings.treasureOffsetY+selectY*(GameGUISettings.treasureHeight+GameGUISettings.treasureMargin)-GameGUISettings.selectMargin,GameGUISettings.treasureWidth+GameGUISettings.selectMargin*2,GameGUISettings.treasureHeight+GameGUISettings.selectMargin*2),GameTextureList.treasureSelect.texture);

		for(int i=0;i<row;i++){
			for(int j=0;j<col;j++){
				GUIContent showElement=new GUIContent(" ");
				GUIContent showElementBG=new GUIContent(GameTextureList.treasureBG[1].texture);
				int x=GameGUISettings.treasureOffsetX+i*(GameGUISettings.treasureWidth+GameGUISettings.treasureMargin);
				int y=GameGUISettings.treasureOffsetY+j*(GameGUISettings.treasureHeight+GameGUISettings.treasureMargin);
				if(treasures[i,j].isFind){
					if(treasures[i,j].isTreasure==1){
						showElementBG=new GUIContent(GameTextureList.treasureBG[2].texture);
						showElement=new GUIContent(GameTextureList.treasure.texture);
					}else{
						showElementBG=new GUIContent(GameTextureList.treasureBG[0].texture);
						if(treasures[i,j].num>0){
							showElement=new GUIContent(GameTextureList.num[treasures[i,j].num-1].texture);
						}
					}

				}
				GUI.Box(new Rect(x,y,GameGUISettings.treasureWidth,GameGUISettings.treasureHeight),showElementBG);
				GUI.skin.button.padding.top=2;
				GUI.skin.button.padding.bottom=2;
				GUI.skin.button.padding.left=2;
				GUI.skin.button.padding.right=2;
				if(GUI.Button(new Rect(x,y,GameGUISettings.treasureWidth,GameGUISettings.treasureHeight),showElement)){
					selectX=i;
					selectY=j;
				}

			}
		}
		GUI.backgroundColor = Color.clear;
		if(turn%nOfPlayer!=turnfactor && !end && playType!=0){
			GUI.Box(GameTextureList.waitForYourTurn.info,GameTextureList.waitForYourTurn.texture);
		}
		DrawTreasureRemain();
	}

	void DrawTreasureRemain(){
		GUI.Box(GameTextureList.treasureRemainBG.info,GameTextureList.treasureRemainBG.texture);
		GUI.color=GameGUISettings.redText;
		GUI.Label(GameTextureList.treasureRemainBG.info,""+(maxNOfTreasure-totalNTreasure-totalOtherNTreasure));
		GUI.color=Color.white;
	}

	void DrawButton(){
		if(GUI.Button(GameTextureList.up.info,GameTextureList.up.texture)){
			selectY--;
			if(selectY<0){
				selectY=col-1;
			}
		}
		if(GUI.Button(GameTextureList.down.info,GameTextureList.down.texture)){
			selectY++;
			if(selectY>col-1){
				selectY=0;
			}
		}
		if(GUI.Button(GameTextureList.left.info,GameTextureList.left.texture)){
			selectX--;
			if(selectX<0){
				selectX=row-1;
			}
		}
		if(GUI.Button(GameTextureList.right.info,GameTextureList.right.texture)){
			selectX++;
			if(selectX>row-1){
				selectX=0;
			}
		}
		if(GUI.Button(GameTextureList.confirm.info,GameTextureList.confirm.texture)){
			if(!treasures[selectX,selectY].isFind){
				confirmBtn=true;
			}
		}
	}
	
	void DrawMainBackGround(){
		GUI.Box (new Rect(WindowSettings.startX, WindowSettings.startY, WindowSettings.screenWidth, WindowSettings.screenHeight),GameTextureList.firstPage.texture);
	}

	void DrawCommonBackGround(){
		GUI.Box (new Rect(WindowSettings.startX, WindowSettings.startY, WindowSettings.screenWidth, WindowSettings.screenHeight),GameTextureList.commonBackGround.texture);
	}

	void DrawPlayBackGround(){
		GUI.Box (new Rect(WindowSettings.startX, WindowSettings.startY, WindowSettings.screenWidth, WindowSettings.screenHeight),GameTextureList.playPage.texture);
	}

	void DrawMainMenu(){//page 1
		DrawMainBackGround();
		if(GUI.Button(GameTextureList.startGame.info,GameTextureList.startGame.texture)){
			page=2;
		}
		if(GUI.Button(GameTextureList.endGame.info,GameTextureList.endGame.texture)){
			Application.Quit();
		}
	}
	

	
	void DrawStartGame(){//page 2
		DrawCommonBackGround();
		if(GUI.Button(GameTextureList.practice.info,GameTextureList.practice.texture)){
			InitTreasure();
			page=100;
			playType=0;
		}

		if(GUI.Button(GameTextureList.vsComputer.info,GameTextureList.vsComputer.texture)){
			page=9;
		}

		if(GUI.Button(GameTextureList.vsPlayer.info,GameTextureList.vsPlayer.texture)){
			page=3;
		}

		if(GUI.Button(GameTextureList.backPage2.info,GameTextureList.backPage2.texture)){
			page=1;
		}
	}
	
	void DrawCheckNetwork(){//page 3
		DrawCommonBackGround();
		GUI.Box(GameTextureList.checkingNetwork.info,GameTextureList.checkingNetwork.texture);
		GUI.Box(GameTextureList.pleaseWait.info,GameTextureList.pleaseWait.texture);
	}

	void DrawVSOtherPlayer(){//page 4
		DrawCommonBackGround();
		if(GUI.Button(GameTextureList.local.info,GameTextureList.local.texture)){
			page=5;
		}
		if(GUI.Button(GameTextureList.backPage4.info,GameTextureList.backPage4.texture)){
			page=2;
		}
	}

	void DrawLocalConnection(){//page 5
		DrawCommonBackGround();
		if(GUI.Button(GameTextureList.createRoom.info,GameTextureList.createRoom.texture)){
			startServerBtn=true;
			page=6;
		}
		if(GUI.Button(GameTextureList.joinGame.info,GameTextureList.joinGame.texture)){
			page=7;
		}
		if(GUI.Button(GameTextureList.backPage5.info,GameTextureList.backPage5.texture)){
			page=4;
		}
	}

	void DrawCreateRoom(){//page 6
		DrawCommonBackGround();

		GUI.Box(GameTextureList.yourIPAddress.info,GameTextureList.yourIPAddress.texture);
		GUI.Box(GameTextureList.waitingForConnection.info,GameTextureList.waitingForConnection.texture);
		GUI.color=GameGUISettings.redText;
		GUI.Label(GameTextureList.ipAddress.info,""+Network.player.ipAddress.ToString());
		GUI.color=Color.white;
		if(GUI.Button(GameTextureList.backPage6.info,GameTextureList.backPage6.texture)){
			ConnectionSettings.server.StopServer();
			ConnectionSettings.serverThread.Abort();
			page=5;
		}
	}



	void DrawJoinGame(){//page 7
		DrawCommonBackGround();
		GUI.Box(GameTextureList.enterAnotherIP.info,GameTextureList.enterAnotherIP.texture);
		GUI.Box(GameTextureList.ipSpace.info,GameTextureList.ipSpace.texture);
		GUI.skin.textField.alignment=TextAnchor.MiddleCenter;
		GUI.color=GameGUISettings.redText;
		ConnectionSettings.client.Host = GUI.TextField(GameTextureList.ipTextField.info,ConnectionSettings.client.Host);
		GUI.color=Color.white;
		if(GUI.Button(GameTextureList.connect.info,GameTextureList.connect.texture)){
			startClientBtn=true;
		}
		if(GUI.Button(GameTextureList.backPage7.info,GameTextureList.backPage7.texture)){
			page=5;
		}
	}

	void DrawSynchronization(){//page 8
		DrawCommonBackGround();
		GUI.Box(GameTextureList.synchronization.info,GameTextureList.synchronization.texture);

	}

	void DrawComputerDifficulties(){//page 9
		DrawCommonBackGround();
		if(GUI.Button(GameTextureList.easy.info,GameTextureList.easy.texture)){
			InitTreasure();
			playType=1;
			computerLevel=1;
			page=100;
		}
		
		if(GUI.Button(GameTextureList.normal.info,GameTextureList.normal.texture)){
			InitTreasure();
			playType=1;
			computerLevel=2;
			page=100;
		}
		
		if(GUI.Button(GameTextureList.hard.info,GameTextureList.hard.texture)){
			InitTreasure();
			playType=1;
			computerLevel=3;
			page=100;
		}
		
		if(GUI.Button(GameTextureList.backPage9.info,GameTextureList.backPage9.texture)){
			page=2;
		}
	}	

	void DrawGame(){//page 100
		DrawPlayBackGround();
		if((yourTurn || playType==0) && !end){
			DrawButton();
		}
		if(end){
			DrawEnd();
		}
		DrawTreasure();
	}

	void OnGUI(){
		GUI.skin.label.fontSize=GameGUISettings.fontSize;
		GUI.skin.button.fontSize=GameGUISettings.fontSize;
		GUI.skin.textField.fontSize=GameGUISettings.fontSize;
		GUI.skin.label.alignment=TextAnchor.MiddleCenter;
		GUI.skin.button.padding.top=0;
		GUI.skin.button.padding.bottom=0;
		GUI.skin.button.padding.left=0;
		GUI.skin.button.padding.right=0;
		GUI.skin.box.padding.top=0;
		GUI.skin.box.padding.bottom=0;
		GUI.skin.box.padding.left=0;
		GUI.skin.box.padding.right=0;
		GUI.backgroundColor=Color.clear;

		switch (page){
		case 1:
			DrawMainMenu();
			break;
		case 2:
			DrawStartGame();
			break;
		case 3:
			DrawCheckNetwork();
			break;
		case 4:
			DrawVSOtherPlayer();
			break;
		case 5:
			DrawLocalConnection();
			break;
		case 6:
			DrawCreateRoom();
			break;
		case 7:
			DrawJoinGame();
			break;
		case 8:
			DrawSynchronization();
			break;
		case 9:
			DrawComputerDifficulties();
			break;
		case 100:
			DrawGame();
			break;
		}
	}
	//-------------------------------------------Draw GUI end-------------------------------------------
}
