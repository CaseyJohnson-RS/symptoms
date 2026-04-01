using UnityEngine;


public class NPC : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Check this if you need several sprite visuals beheviour
    private Animator anim;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public float Enter() // Return animation duration
    {
        // TODO: anim
        return 2f;
    }

    public float GoBack() // Return animation duration
    {
        // TODO: anim
        return 2f;
    }
    
    public float GoForward() // Return animation duration
    {
        // TODO: anim
        return 2f;

    }

    public static NPC CreateNPC(NPCData data, GameObject prefab)
    {
        GameObject npcObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        NPC npcComponent = npcObject.GetComponent<NPC>();

        if (!npcComponent)
            throw new MissingComponentException("Missing NPC component!");

        npcComponent.SetSprite(data.Sprite);

        return npcComponent;
    }
}