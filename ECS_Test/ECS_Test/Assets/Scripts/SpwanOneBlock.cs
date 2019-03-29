using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Rendering;
public class SpwanOneBlock : MonoBehaviour
{
    private float a = 0;

    public static EntityArchetype BlockArchetype;

    public Mesh BlockMesh;
    public Material BlockMaterial;
    public EntityManager manager;
    public Entity entities;
    public GameObject prefeb_ref;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        EntityManager manager = World.Active.GetOrCreateManager<EntityManager>();

        BlockArchetype = manager.CreateArchetype(typeof(Position));
        
    }

    private void Start()
    {
        //manager = World.Active.GetOrCreateManager<EntityManager>();
        //Entity[] entities = new Entity[1];
        //for (int i = 0; i < entities.Length; i++)
        //{
        //    entities[i] = manager.CreateEntity(BlockArchetype);
        //    manager.SetComponentData(entities[i], new Position { Value = new int3(i, 0, 0) });
        //    manager.AddComponentData(entities[i], new BlockTag { });

        //    manager.AddSharedComponentData(entities[i], new MeshInstanceRenderer
        //    {
        //        mesh = BlockMesh,
        //        material = BlockMaterial
        //    });
        //}
        manager = World.Active.GetOrCreateManager<EntityManager>();

        entities = manager.CreateEntity(BlockArchetype);
        manager.SetComponentData(entities, new Position { Value = new int3(2, 0, 0) });
        manager.AddComponentData(entities, new BlockTag { });

        manager.AddSharedComponentData(entities, new MeshInstanceRenderer
        {
            mesh = BlockMesh,
            material = BlockMaterial
        });
        if (prefeb_ref)
        {
            NativeArray<Entity> entityArray = new NativeArray<Entity>(1, Allocator.Temp);
            manager.Instantiate(prefeb_ref, entityArray);

            //manager.SetComponentData(entityArray[0], new Position { Value = new int3(4, 0, 0) });
            entityArray.Dispose();
        }
    }
    private void Update()
    {
    }
}
