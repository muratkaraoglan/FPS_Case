using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : Singleton<CollectibleSpawner>
{
    [SerializeField] private List<Collectible> collectibles;
    private void OnEnable()
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            for (int j = 0; j < collectibles[i].InitialNumberOfCollectible; j++)
            {
                Instantiate(collectibles[i].CollectiblePrefab);
            }
        }
    }

    public void Respawn(GameObject collectibleGO, CollectibleType type)
    {
        collectibleGO.SetActive(false);
        Collectible collectible = collectibles.Find(c => c.Type == type);
        float respawnTime = Random.Range(collectible.TimeIntervalForRespawn.x, collectible.TimeIntervalForRespawn.y);
        StartCoroutine(Respawner(collectibleGO, respawnTime));
    }

    IEnumerator Respawner(GameObject collectibleGO, float time)
    {
        yield return new WaitForSeconds(time);
        collectibleGO.SetActive(true);
    }

}

[System.Serializable]
public struct Collectible
{
    public CollectibleType Type;
    public GameObject CollectiblePrefab;
    public int InitialNumberOfCollectible;
    public Vector2 TimeIntervalForRespawn;
}

public enum CollectibleType
{
    Ammo,
    Health
}


