using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Analytics;
public class PhotonManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        // UnityEngine.Analytics
        #if ENABLE_CLOUD_SERVICES_ANALYTICS
        Debug.Log("Start   " + AnalyticsSessionInfo.userId + " " + AnalyticsSessionInfo.sessionState + " " + AnalyticsSessionInfo.sessionId + " " + AnalyticsSessionInfo.sessionElapsedTime);
        AnalyticsSessionInfo.sessionStateChanged += OnSessionStateChanged;
        #endif
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby(){
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions {MaxPlayers = 2}, TypedLobby.Default);
    }
    public override void OnJoinedRoom(){
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject Player1 = PhotonNetwork.Instantiate("Player1", new Vector3(-15, 0, -5), Quaternion.identity);
            Player1.name = "Player1";
            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable(){{"field", 1}});
        }
        else
        {
            int direction = (int)PhotonNetwork.CurrentRoom.CustomProperties["field"];
            GameObject Player2 = PhotonNetwork.Instantiate("Player1", new Vector3(direction*15, 0, -5), Quaternion.identity);
            Player2.name = "Player2";
        }
        
        ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
        ht["what"] = 1;
        PhotonNetwork.LocalPlayer.CustomProperties = ht;
        //Debug.Log(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient) {
        int fieldSide = -1*Player.side;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable(){{"field", fieldSide}});
    }	


    // UnityEngine.Analytics
    #if ENABLE_CLOUD_SERVICES_ANALYTICS
    void OnSessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged)
    {
        Debug.Log("Call    " + AnalyticsSessionInfo.userId  + " " + sessionState + " " + sessionId + " " + sessionElapsedTime + " " + sessionChanged);
    }
    #endif
}
