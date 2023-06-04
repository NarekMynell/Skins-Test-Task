public class SkinUIItemStateSelected : SkinUIItemStateUnlocked
{
    public SkinUIItemStateSelected(SkinUIItemController controller) : base(controller) {}


    public override void Enter()
    {
        base.Enter();
        controller.SelectingImage.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
        controller.SelectingImage.SetActive(false);
    }
}