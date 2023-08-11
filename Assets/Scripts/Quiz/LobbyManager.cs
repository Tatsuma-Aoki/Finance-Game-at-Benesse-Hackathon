using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Connection Status")]
    public Text connectionStatusText;

    [Header("Login UI Panel")]
    public InputField playerNameInput;
    public GameObject Login_UI_Panel;


    [Header("Game Options UI Panel")]
    public GameObject GameOptions_UI_Panel;
    
    [Header("Create Room UI Panel")]
    public GameObject CreateRoom_UI_Panel;
    public InputField roomNameInputField;
    
    [Header("Inside Room UI Panel")]
    public GameObject InsideRoom_UI_Panel;
    public Text roomInfoText;
    public GameObject playerListPrefab;
    public GameObject playerListContent;
    public GameObject startGameButton;

    [Header("Room List UI Panel")]
    public GameObject RoomList_UI_Panel;
    public GameObject roomListEntryPrefab;
    public GameObject roomListParentGameObject;

    private Dictionary<string, RoomInfo> cachedRoomList; 
    private Dictionary<string, GameObject> roomListGameObjects; 
    private Dictionary<int, GameObject> playerListGameObjects; 
    private byte Max_Player_Number = 4; //部屋に入れる最大プレイヤー数

    public float timer = 0.0f;
    private bool isPlayer1Active = false;
    private bool isPlayer2Active = false;
    private bool isPlayer3Active = false;
    public bool isTimerStarted = false;

    #region Unity Methods

    // Start is called before the first frame update
    private void Start()
    {    
        // Login_UI_Panel.SetActive(true);
        // GameOptions_UI_Panel.SetActive(false);
        ActivatePanel(Login_UI_Panel.name);

        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameObjects = new Dictionary<string, GameObject>();

        PhotonNetwork.AutomaticallySyncScene = true; 
    }

    // Update is called once per frame
    private void Update()
    {
        connectionStatusText.text = "Connection status: " + PhotonNetwork.NetworkClientState;
        
        if(isTimerStarted)
        {
            timer += Time.deltaTime;
        }
        if(timer > 0.5 && !isPlayer1Active)
        {
            playerListContent.transform.Find("Player1").gameObject.SetActive(true);
            isPlayer1Active = true;
        }
        if(timer > 0.7 && !isPlayer2Active)
        {
            playerListContent.transform.Find("Player2").gameObject.SetActive(true);
            isPlayer2Active = true;
        }
        if(timer > 1.1 && !isPlayer3Active)
        {
            playerListContent.transform.Find("Player3").gameObject.SetActive(true);
            isPlayer3Active = true;
            isTimerStarted = false;
        }
    }

    #endregion

    #region UI Callbacks
    public void OnLoginButtonClicked()
    {
        string playerName = playerNameInput.text;
        if(!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.ConnectUsingSettings();

        }
        else
        {
            Debug.Log("PlayerName is invalid!");
        }
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Join Lobby");
        }
    }

    public void OnCreateRoomPanelButtonClicked()
    {
        ActivatePanel(CreateRoom_UI_Panel.name);
    }

    public void OnCreateRoomButtonClicked()
    {
        string roomName = roomNameInputField.text;

        if(string.IsNullOrEmpty(roomName))
        {
            roomName = "Room " + Random.Range(1000,10000);
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = Max_Player_Number;

        PhotonNetwork.CreateRoom(roomName,roomOptions);
        isTimerStarted = true;
    }

    public void OnCancelButtonClicked()
    {
        ActivatePanel(GameOptions_UI_Panel.name);
    }

    public void OnShowRoomListButtonClicked()
    {
        ActivatePanel(RoomList_UI_Panel.name);
        Debug.Log("Number of rooms : " + PhotonNetwork.CountOfRooms);
    }

    public void OnBackButtonClicked()
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivatePanel(GameOptions_UI_Panel.name);
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnStartGameButtonClicked()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("QuizGameMainScene");
        }
    }


    #endregion

    #region Photon Callbacks
    public override void OnConnected()
    {
        Debug.Log("Connected to Internet");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "is connected to Photon");
        ActivatePanel(GameOptions_UI_Panel.name);

    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created.");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
        ActivatePanel(InsideRoom_UI_Panel.name);
        Debug.Log("Number of Players in lobby : " + PhotonNetwork.CountOfPlayers);

        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }

        roomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
        foreach (var player in PhotonNetwork.PlayerList) {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
        }

        if(playerListGameObjects == null)
        {
            playerListGameObjects = new Dictionary<int, GameObject>();
        }

        foreach(Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerListGameObject = Instantiate(playerListPrefab);
            playerListGameObject.SetActive(true);
            playerListGameObject.transform.SetParent(playerListContent.transform);
            playerListGameObject.transform.localPosition = new Vector3(0, 120 - 60 * (PhotonNetwork.PlayerList.Length-1)-150, 0);


            playerListGameObject.transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.NickName;
            if(player.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                playerListGameObject.transform.Find("PlayerIndicator").GetComponent<TextMeshProUGUI>().text = "";

            }
            playerListGameObjects.Add(player.ActorNumber, playerListGameObject);
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        roomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;

        GameObject playerListGameObject = Instantiate(playerListPrefab);
        playerListGameObject.SetActive(true);
        playerListGameObject.transform.SetParent(playerListContent.transform);
        playerListGameObject.transform.localPosition = new Vector3(0, 120 - 60 * (PhotonNetwork.PlayerList.Length-1)-150, 0);

        playerListGameObject.transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = newPlayer.NickName;
        if(newPlayer.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber) //ActorNumber＝出入りで変わる識別番号
        {
            playerListGameObject.transform.Find("PlayerIndicator").GetComponent<TextMeshProUGUI>().text = "";
        }
        playerListGameObjects.Add(newPlayer.ActorNumber, playerListGameObject);

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        roomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;

        Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
        playerListGameObjects.Remove(otherPlayer.ActorNumber);

        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
    }

    public override void OnLeftRoom() 
    {
        Debug.Log("Left Room");
        ActivatePanel(GameOptions_UI_Panel.name);
        foreach(GameObject playerListGameObject in playerListGameObjects.Values)
        {
            Destroy(playerListGameObject);
        }
        playerListGameObjects.Clear();
        playerListGameObjects = null;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();

        foreach(RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if(!room.IsOpen || !room.IsVisible || room.RemovedFromList) 
            {
                if(cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList.Remove(room.Name);
                }
            }
            else
            {
                if(cachedRoomList.ContainsKey(room.Name)) 
                {
                    cachedRoomList[room.Name] = room;
                }
                else
                {
                    cachedRoomList.Add(room.Name, room);
                }
            }
        }

        foreach (RoomInfo room in cachedRoomList.Values)
        {
            GameObject roomListEntryGameObject = Instantiate(roomListEntryPrefab);
            roomListEntryGameObject.transform.SetParent(roomListParentGameObject.transform);
            roomListEntryGameObject.transform.localScale = new Vector3(0, 100 - 100 * (PhotonNetwork.CountOfRooms-1), 0);

            roomListEntryGameObject.transform.Find("RoomNameText").GetComponent<TextMeshProUGUI>().text = room.Name;
            roomListEntryGameObject.transform.Find("RoomPlayerText").GetComponent<TextMeshProUGUI>().text = room.PlayerCount + " / " + room.MaxPlayers;
            roomListEntryGameObject.transform.Find("JoinRoomButton").GetComponent<Button>().onClick.AddListener(()=> OnJoinRoomButtonClicked(room.Name));

            roomListGameObjects.Add(room.Name, roomListEntryGameObject);

        }
    }

    public override void OnLeftLobby()
    {
        ClearRoomListView();
        cachedRoomList.Clear();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        string roomName = "Room " + Random.Range(1000,10000); 

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(roomName,roomOptions);
    }

    #endregion

    #region Private Methods
    void OnJoinRoomButtonClicked(string _roomName)
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(_roomName);

    }
    void ClearRoomListView()
    {
        foreach(var roomListGameObject in roomListGameObjects.Values)
        {
            Destroy(roomListGameObject);
        }

        roomListGameObjects.Clear();
    }
    #endregion

    #region  Public Methods
    public void ActivatePanel(string panelToBeActivated)
    {
        Login_UI_Panel.SetActive(panelToBeActivated.Equals(Login_UI_Panel.name));
        GameOptions_UI_Panel.SetActive(panelToBeActivated.Equals(GameOptions_UI_Panel.name));
        CreateRoom_UI_Panel.SetActive(panelToBeActivated.Equals(CreateRoom_UI_Panel.name));
        InsideRoom_UI_Panel.SetActive(panelToBeActivated.Equals(InsideRoom_UI_Panel.name));
        RoomList_UI_Panel.SetActive(panelToBeActivated.Equals(RoomList_UI_Panel.name));
    }
    #endregion


}