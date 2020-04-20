[System.Serializable]
public class ItemStack
{
    public Item item;
    public int count = 1;

    public ItemStack(Item _item, int _count = 1)
    {
        this.item = _item;
        this.count = _count;
    }
}