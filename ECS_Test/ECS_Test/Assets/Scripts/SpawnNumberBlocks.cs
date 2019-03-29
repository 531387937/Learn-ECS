using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Rendering;
public class SpawnNumberBlocks : MonoBehaviour
{
    public static Texture2D Heightmap;

    public static EntityArchetype BlockArchetype;

    public int ChunkBase = 1;

    public Mesh blockMesh;

    public Material Mat0;
    public Material Mat1;
    public Material Mat2;
    public Material Mat3;
    public Material Mat4;
    public Material Mat5;
    public Material Mat6;
    public Material Mat7;

    Material maTemp;
    public EntityManager manager;
    public Entity entities;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

        BlockArchetype = manager.CreateArchetype(typeof(Position));

    }
    // Start is called before the first frame update
    void Start()
    {
        manager = World.Active.GetOrCreateManager<EntityManager>();

        ChunckGenerator(ChunkBase);
    }

    void ChunckGenerator(int amount)
    {
        int heightlevel;
        bool airChecker;

        for(int yBlock=0;yBlock<15;yBlock++)
        {
            for(int xBlock=0;xBlock<10*amount;xBlock++)
            {
                for(int zBlock=0;zBlock<10*amount;zBlock++)
                {
                    heightlevel = (int)(Heightmap.GetPixel(xBlock, zBlock).r * 100) - yBlock;
                    airChecker = false;
                    Vector3 posTemp = new Vector3(xBlock, yBlock, zBlock);

                    switch(heightlevel)
                    {
                        case 0:
                            maTemp = Mat0;
                            break;
                        case 1:
                            maTemp = Mat1;
                            break;
                        case 2:
                            maTemp = Mat2;
                            break;
                        case 3:
                            maTemp = Mat3;
                            break;
                        case 4:
                            maTemp = Mat4;
                            break;
                        case 5:
                            maTemp = Mat5;
                            break;
                        case 6:
                            maTemp = Mat6;
                            break;
                       default:
                            maTemp = Mat7;
                            airChecker = true;
                            break;
                    }

                    if(!airChecker)
                    {
                        entities = manager.CreateEntity(BlockArchetype);
                        manager.SetComponentData(entities, new Position { Value = new int3(xBlock, yBlock, zBlock) });
                        manager.AddComponentData(entities, new BlockTag { });

                        manager.AddSharedComponentData(entities, new MeshInstanceRenderer
                        {
                            mesh = blockMesh, material = maTemp
                        });
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
