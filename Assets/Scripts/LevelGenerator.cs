using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();
    public Transform levelStartPoint;
    public int initialBlocks = 2;
    public LevelBlock firstBlock;
    private void Awake()
    {
        sharedInstance = this;
    }
    private void Start()
    {
        this.GenerateInitialBlocks();
    }

    public void AddLevelBlock()
    {
        // Generar un numero aleatorio entre a y b
        int randomIndex = Random.Range(0, this.allTheLevelBlocks.Count);
        // Creamos una copia del bloque de nivel desde la carpeta assets hasta la escena
        LevelBlock currentBlock;
        // Pone el nuevo bloque de nivel como hijo del Level Generator
        Vector3 spawnPosition = Vector3.zero;

        if (this.currentBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(this.firstBlock);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = levelStartPoint.position;                     
        }
        else
        {
            currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = this.currentBlocks[this.currentBlocks.Count - 1].exitPoint.position;
        }

        Vector3 correction = new Vector3(
            spawnPosition.x - currentBlock.startPoint.position.x,
            spawnPosition.y - currentBlock.startPoint.position.y,
            0
        );
        currentBlock.transform.position = correction;
        this.currentBlocks.Add(currentBlock);
    }

    public void RemoveOldestLevelBlock()
    {
        LevelBlock oldestBlock = this.currentBlocks[0];
        this.currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while (currentBlocks.Count > 0)
        {
            this.RemoveOldestLevelBlock();
        }
    }

    public void GenerateInitialBlocks()
    {
        for (int i = 0; i < this.initialBlocks; i++)
        {
            this.AddLevelBlock();
        }
    }
}
