using Fusion;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("입장~~!@#@!#@!#");
        if (player ==  PhoStartGame.Instance.runner.LocalPlayer)
        {
            Debug.Log("나가라그냥");
            //PhoStartGame.Instance.runner.Spawn(playerPrefab, new Vector3(226f, 47f, 365f), Quaternion.identity); //var obj = Runner.Spawn(PlayerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            //obj.GetComponent<NetworkTransform>().Teleport(new Vector3(465f, 47f, 570f));
            //obj.GetComponent<NetworkTransform>().Teleport(new Vector3(0f, 1.5f, 0f));
            //obj.GetComponent<NetworkTransform>().Teleport(new Vector3(226.1f, 47f, 364.8f));
            
        }
    }
}
