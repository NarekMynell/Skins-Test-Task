public class SkinUIItemStateLocked : State
{
    protected readonly SkinUIItemController controller;

    public SkinUIItemStateLocked(SkinUIItemController controller) => this.controller = controller;

    public override void Enter()
    {
        base.Enter();
        controller.Image.sprite = controller.LockSprite;
        controller.Button.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        controller.Image.sprite = null;
        controller.Button.enabled = true;
    }
}
