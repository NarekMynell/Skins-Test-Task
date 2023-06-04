using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData", order = 1)]
public class SkinData : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private int id;
    public bool Unlocked { get; private set; } = false;
    public Sprite Sprite { get { return sprite; } }
    public int Id { get { return id; } }

    public void Initialize(bool isOpened)
    {
        Unlocked = isOpened;
    }

    public void Unlock()
    {
        if(Unlocked == false)
        {
            Unlocked = true;
        }
    }
}
