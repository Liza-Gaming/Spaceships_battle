using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class connectToServer : MonoBehaviourPunCallbacks
{
    public GameObject player1;
    public GameObject player2;

    // Two different spawn points for player 1 and player 2
    public Transform spawnPointBottom;
    public Transform spawnPointTop;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // Determine spawn point and rotation based on the current number of players
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount == 1)
        {
            // First player, spawn at bottom, facing up
            GameObject _player1 = PhotonNetwork.Instantiate(
                player1.name,
                spawnPointBottom.position,
                Quaternion.Euler(0, 0, 0)  // Rotated to face up
            );
        }
        else if (playerCount == 2)
        {
            // Second player, spawn at top, facing down
            GameObject _player2 = PhotonNetwork.Instantiate(
                player2.name,
                spawnPointTop.position,
                Quaternion.Euler(0, 0, 180) // Rotated to face down
            );
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView[] photonViews = PhotonView.FindObjectsOfType<PhotonView>();
            foreach (var view in photonViews)
            {
                if (view.Owner.Equals(otherPlayer))
                {
                    // Optionally transfer ownership to MasterClient
                    view.TransferOwnership(PhotonNetwork.MasterClient.ActorNumber);
                    // Or destroy if these objects should not exist without their original owner
                    PhotonNetwork.Destroy(view.gameObject);
                }
            }
        }
    }


}
