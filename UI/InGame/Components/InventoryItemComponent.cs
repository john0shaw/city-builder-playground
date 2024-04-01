using Godot;

public partial class InventoryItemComponent : HBoxContainer
{
    public ItemResource Resource;
    private int _count;
    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            if (_countLabel is Label)
                _countLabel.Text = value.ToString();
        }
    }

    private TextureRect _icon;
    private Label _countLabel;

    public override void _Ready()
    {
        _icon = GetNode<TextureRect>("Icon");
        _countLabel = GetNode<Label>("Count");

        _icon.Texture = Resource.Icon;
        _countLabel.Text = Count.ToString();
    }
}
