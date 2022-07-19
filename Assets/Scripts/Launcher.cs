using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    // Made this script to be a static instance = visible to other scripts.
    public static Launcher instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject loadingScreen;
    public TMP_Text loadingText;

    public GameObject menuButtons;

    public GameObject createRoomScreen;
    public TMP_InputField roomNameInput;

    public GameObject roomScreen;
    public TMP_Text roomNameText;

    public GameObject errorScreen;
    public TMP_Text errorText;

    public GameObject roomBrowserScreen;
    public RoomButton theRoomButton;
    private List<RoomButton> allRoomButtons = new List<RoomButton>();

    // At start, close all menus and bring up loading screen with a "connecting to network" text.
    void Start()
    {
        CloseMenus();

        loadingScreen.SetActive(true);
        loadingText.text = "Connecting To Network...";

        // Actually start connecting to network.
        PhotonNetwork.ConnectUsingSettings();
    }

    // Close menus function closes all loading screens and menu buttons.
    void CloseMenus()
    {
        loadingScreen.SetActive(false);
        menuButtons.SetActive(false);
        createRoomScreen.SetActive(false);
        roomScreen.SetActive(false);
        errorScreen.SetActive(false);
        roomBrowserScreen.SetActive(false);
    }

    // Connect to the master server and join a lobby & display that on a loading screen.
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        loadingText.text = "Joining Lobby...";
    }

    // When connected to a lobby, display menubuttons.
    public override void OnJoinedLobby()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    public void OpenRoomCreate()
    {
        CloseMenus();
        createRoomScreen.SetActive(true);
    }

    // Function for creating a room.
    public void CreateRoom()
    {
        if(!string.IsNullOrEmpty(roomNameInput.text)) // Checking if the input field is empty. If not empty, room can be created.
        {
            RoomOptions options = new RoomOptions(); // Configuring the room options.
            options.MaxPlayers = 8; // Setting the maximum amount of players in a room.

            PhotonNetwork.CreateRoom(roomNameInput.text, options);

            CloseMenus();
            loadingText.text = "Creating Room...";
            loadingScreen.SetActive(true);
        }
    }

    // Function when joining a room.
    public override void OnJoinedRoom()
    {
        CloseMenus();
        roomScreen.SetActive(true);

        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    // Function to display error message if room creation fails.
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Failed to Create Room: " + message;
        CloseMenus();
        errorScreen.SetActive(true);
    }

    // Closing the error screen and returning back to main menu.
    public void CloseErrorScreen()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        CloseMenus();
        loadingText.text = "Leaving room...";
        loadingScreen.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    // Open and close room browser.
    public void OpenRoomBrowser()
    {
        CloseMenus();
        roomBrowserScreen.SetActive(true);
    }

    public void CloseRoomBrowser()
    {
        CloseMenus();
        menuButtons.SetActive(true);
    }

    // Called everytime there is an update in the room list.
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomButton rb in allRoomButtons)
        {
            Destroy(rb.gameObject);
        }
        allRoomButtons.Clear();

        theRoomButton.gameObject.SetActive(false);

        for(int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].PlayerCount != roomList[i].MaxPlayers && !roomList[i].RemovedFromList)
            {
                RoomButton newButton = Instantiate(theRoomButton, theRoomButton.transform.parent);
                newButton.SetButtonDetails(roomList[i]);
                newButton.gameObject.SetActive(true);

                allRoomButtons.Add(newButton);
            }
        }
    }
}
