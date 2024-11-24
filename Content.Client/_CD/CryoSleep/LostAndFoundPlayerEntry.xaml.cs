using Content.Shared._CD.CryoSleep;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client._CD.CryoSleep;

[GenerateTypedNameReferences]
public sealed partial class LostAndFoundPlayerEntry : BoxContainer
{
    public event Action<string>? OnRetrieveItem;

    public LostAndFoundPlayerEntry(string playerName, List<LostItemData> items)
    {
        RobustXamlLoader.Load(this);

        Heading.Title = playerName;
        UpdateItems(items);
    }

    public void UpdateItems(List<LostItemData> items)
    {
        var wasVisible = Collapsible.BodyVisible;
        Body.Visible = items.Count != 0;

        ItemsList.RemoveAllChildren();

        foreach (var item in items)
        {
            var control = new LostAndFoundItemControl(item.SlotName, item.ItemName);
            control.Button.OnPressed += _ => OnRetrieveItem?.Invoke(item.SlotName);
            ItemsList.AddChild(control);
        }

        Collapsible.BodyVisible = wasVisible;
    }
}