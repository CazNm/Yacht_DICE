﻿using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using Firebase.Unity.Editor;

using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
// 유니티용 포톤 컴포넌트들
// 포톤 서비스 관련 라이브러리

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1.0"; // 게임 버전

    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트

   // public Text customConnectionText;
    public Text customHostText; //커스텀 호스트 UI 텍스트
    public Text customJoinText; //커스텀 입장 UI 텍스트

    public Button joinButton; // 룸 접속 버튼
    public Button customHost;
    public Button customJoin;
    public InputField customRoomH;
    public InputField customRoomJ;


    private RoomOptions randomOptions;
    private RoomOptions customOptions;

    public static FirebaseDatabase firebaseDatabase;
    public static FirebaseApp firebaseApp;
    public static FirebaseAuth firebaseAuth;
    public static DatabaseReference reference;

    public static FirebaseUser User;

    public static int lobbyUser;
    public static int currentUser;

    public Text lu;
    public Text cu;
   

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start()
    {
        // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        // 설정한 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();
        // 룸 접속 버튼을 잠시 비활성화
        joinButton.interactable = false;
        customHost.interactable = false;
        customJoin.interactable = false;
        // 접속을 시도 중임을 텍스트로 표시
        connectionInfoText.text = "서버에 접속중...";

        randomOptions = new RoomOptions();
        randomOptions.CustomRoomPropertiesForLobby = new string[2] { "random", "ai" };
        randomOptions.CustomRoomProperties = new Hashtable() { { "random", 1 } };
        randomOptions.MaxPlayers = 2;

        customOptions = new RoomOptions();
        customOptions.CustomRoomPropertiesForLobby = new string[2] { "custom", "ai" };
        customOptions.CustomRoomProperties = new Hashtable() { { "custom", 1 } };
        customOptions.MaxPlayers = 2;



    }

    public void Update()
    {
        
        currentUser = PhotonNetwork.CountOfPlayers;
        cu.text = "    접속자 수 : " + currentUser;
        lobbyUser = PhotonNetwork.CountOfPlayersOnMaster;
        lu.text = "로비 접속자 수 : " + lobbyUser;
    }


    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        // 룸 접속 버튼을 활성화
        joinButton.interactable = true;
        customHost.interactable = true;
        customJoin.interactable = true;
        // 접속 정보 표시
        connectionInfoText.text = "온라인!";
    }
    // 마스터 서버 접속 실패시 자동 실행

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 룸 접속 버튼을 비활성화
        joinButton.interactable = false;
        // 접속 정보 표시
        connectionInfoText.text = "서버와 연결되지 않음\n접속 재시도 중...";

        // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    // 룸 접속 시도
    public void RandomConnect()
    {
        // 중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
        customHost.interactable = false;
        customJoin.interactable = false;

        // 마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            // 룸 접속 실행
            connectionInfoText.text = "랜덤 매칭중...";
            PhotonNetwork.JoinRandomRoom(randomOptions.CustomRoomProperties, 2);
        }
        else
        {
            // 마스터 서버에 접속중이 아니라면, 마스터 서버에 접속 시도
            connectionInfoText.text = "서버와 연결되지 않음\n접속 재시도 중...";
            // 마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 접속 상태 표시
        connectionInfoText.text = "방 생성 중...";
        // 최대 2명을 수용 가능한 빈방을 생성
        PhotonNetwork.CreateRoom(null, randomOptions);
    }

    public void CustomHost(Button button) {

        button.interactable = false;
        customHostText.text = "방 생성 완료. 상대 기다리는 중 ...";

        PhotonNetwork.CreateRoom(customRoomH.text, customOptions);

    }

    public void CustomConnect(Button button)
    {
        // 중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        button.interactable = false;
        customJoinText.text = "방 아이디를 입력하세요...";


        // 마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            // 룸 접속 실행
            customJoinText.text = "방 접속중...";
            PhotonNetwork.JoinRoom(customRoomJ.text);
        }
        else
        {
            // 마스터 서버에 접속중이 아니라면, 마스터 서버에 접속 시도
            customJoinText.text = "오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";
            // 마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void activeHost() {
        GameObject.Find("Canvas").transform.Find("customUIHost").gameObject.SetActive(true);
    }

    public void UnactiveHost()
    {
        GameObject.Find("Canvas").transform.Find("customUIHost").gameObject.SetActive(false);
    }

    public void activeJoin()
    {
        GameObject.Find("Canvas").transform.Find("customUIJoin").gameObject.SetActive(true);
    }

    public void UnactiveJoin()
    {
        GameObject.Find("Canvas").transform.Find("customUIJoin").gameObject.SetActive(false);
    }


    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        // 접속 상태 표시
        // 모든 룸 참가자들이 Main 씬을 로드하게 
        connectionInfoText.text = "게임 접속";
        customHostText.text = "게임 접속";
        customJoinText.text = "게임 접속";
        PhotonNetwork.LoadLevel("mainGame");
    }
}