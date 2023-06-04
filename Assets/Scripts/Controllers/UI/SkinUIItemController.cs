using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinUIItemController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectingImage;

    public SkinData SkinData { get; private set; }
    public Image Image { get { return image; } }
    public Sprite LockSprite { get { return lockSprite; } }
    public Button Button { get { return button; } }
    public GameObject SelectingImage { get { return selectingImage; } }

    private StateMachine stateMachine;

    public static event Action<SkinUIItemController> OnSkinSelected;


    // Should always be called immediately after object creation
    public void Initialize(SkinData skinData)
    {
        SkinData = skinData;

        stateMachine = new StateMachine();
        State state = skinData.Unlocked ? new SkinUIItemStateUnlocked(this) : new SkinUIItemStateLocked(this);
        stateMachine.Initialize(state);
    }

    public void Select()
    {
        if (SkinData.Unlocked == false) return;

        State state = new SkinUIItemStateSelected(this);
        stateMachine.ChangeState(state);
        OnSkinSelected.Invoke(this);
    }

    public void Unselect()
    {
        State state = new SkinUIItemStateUnlocked(this);
        stateMachine.ChangeState(state);
    }

    public void Unlock()
    {
        State state = new SkinUIItemStateUnlocked(this);
        stateMachine.ChangeState(state);
    }

    public void Lock()
    {
        State state = new SkinUIItemStateLocked(this);
        stateMachine.ChangeState(state);
    }

    public void OnButtonClicked()
    {
        Select();
    }
}
