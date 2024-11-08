using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var obj = Runner.Spawn(PlayerPrefab, new Vector3(226.1f, 47f, 364.8f), Quaternion.identity);
            //var obj = Runner.Spawn(PlayerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            //obj.GetComponent<NetworkTransform>().Teleport(new Vector3(465f, 47f, 570f));
            //obj.GetComponent<NetworkTransform>().Teleport(new Vector3(0f, 1.5f, 0f));
            //obj.GetComponent<NetworkTransform>().Teleport(new Vector3(226.1f, 47f, 364.8f));
            
        }
    }
}
