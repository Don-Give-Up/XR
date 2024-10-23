using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var obj = Runner.Spawn(PlayerPrefab, new Vector3(0, 3, 0), Quaternion.identity);
            obj.GetComponent<NetworkTransform>().Teleport(new Vector3(465f, 47f, 570f));
        }
    }
}
