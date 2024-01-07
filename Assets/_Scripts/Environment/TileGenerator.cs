using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    private GameObject spawnedObject;
    private List<GameObject> tilesSpawned;
    public int zPos = 50; // length each tile
    public bool creatingTiles = false;
    public int indexTile;
    public int lastIndexTile = -1;
    private int gapCountSpecialTile;
    public bool specialTileGenerated;
    public static bool isEnded = false;
    private int chance;

    private void Awake()
    {
        gapCountSpecialTile = 0;
        specialTileGenerated = false;
        tilesSpawned = new List<GameObject>();
        isEnded = false;
        for (int i = 0; i < 4; i++)
        {
            chance = 0;
            do // random index exclude last index
            {
                indexTile = Random.Range(0, tiles.Length - 1);
                chance++;
            } while (indexTile == lastIndexTile && chance < 3);
            lastIndexTile = indexTile;

            // menampilkan objek dilayar
            spawnedObject =
                GameObject.Instantiate(
                    tiles[indexTile],
                    new Vector3(0, 0, zPos),
                    Quaternion.identity
                ) as GameObject;
            // simpan objek yang sudah ditampilkan
            tilesSpawned.Add(spawnedObject);
            zPos += 50;
        }
    }

    void Update()
    {
        if (!creatingTiles && !isEnded)
        {
            creatingTiles = true;
            StartCoroutine(GenerateTile());
        }
        if (tilesSpawned.Count > 25)
        {
            Destroy(tilesSpawned[0]);
            tilesSpawned.RemoveAt(0);
        }
    }

    private IEnumerator GenerateTile()
    {
        do
        {
            // random index sampai tidak sama dengan last index
            indexTile = Random.Range(0, tiles.Length); // random range antara 0 - banyaknya tiles
        } while (indexTile == lastIndexTile || (specialTileGenerated && indexTile == 3));

        lastIndexTile = indexTile; // save last index

        SpecialTile(indexTile);

        // indexTile = 3;
        spawnedObject =
            GameObject.Instantiate(tiles[indexTile], new Vector3(0, 0, zPos), Quaternion.identity)
            as GameObject;
        if (indexTile == tiles.Length - 1)
        {
            Transform child = spawnedObject.transform.GetChild(0);
            child.transform.position = new Vector3(
                0,
                child.transform.position.y,
                child.transform.position.z
            );
        }
        tilesSpawned.Add(spawnedObject);

        zPos += 50;

        yield return new WaitForSeconds(2f);
        creatingTiles = false;
    }

    private void SpecialTile(int index)
    {
        if (!specialTileGenerated && index == 3)
            specialTileGenerated = true;

        if (specialTileGenerated)
            gapCountSpecialTile++;

        if (gapCountSpecialTile > 3)
        {
            specialTileGenerated = false;
            gapCountSpecialTile = 0;
        }
    }
}
