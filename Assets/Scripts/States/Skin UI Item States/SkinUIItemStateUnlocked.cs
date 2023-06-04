public class SkinUIItemStateUnlocked : State
{
    protected readonly SkinUIItemController controller;

    public SkinUIItemStateUnlocked(SkinUIItemController controller) => this.controller = controller;

    public override void Enter()
    {
        base.Enter();
        controller.Image.sprite = controller.SkinData.Sprite;
        controller.Button.enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
    }
}