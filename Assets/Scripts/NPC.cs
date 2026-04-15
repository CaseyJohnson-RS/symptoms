using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{

    [Space(5)] public UnityEvent onEnter;
    [Space(5)] public UnityEvent onPass;
    [Space(5)] public UnityEvent onStay;

    [SerializeField] private Image mainImage;
    [SerializeField] private Image outerShadowImage;
    [SerializeField] private TextMeshProUGUI _name;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private float GetAnimLength()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        float speed = state.speed * state.speedMultiplier * anim.speed;
        float duration = state.length / speed;
        return duration;
    }

    public float Enter() // Return animation duration
    {
        anim.Play("Enter");
        onEnter.Invoke();

        return GetAnimLength();
    }

    public float Stay() // Return animation duration
    {
        // TODO: anim
        anim.Play("Stay");
        onStay.Invoke();
        return GetAnimLength();

    }
    
    public float Pass() // Return animation duration
    {
        // TODO: anim
        anim.Play("Pass");
        onPass.Invoke();
        return GetAnimLength();
    }

    public void UpdateNPC(NPCData data)
    {
        mainImage.sprite = data.Sprite;
        outerShadowImage.sprite = data.Sprite;
        _name.text = data.Name;
    }
}