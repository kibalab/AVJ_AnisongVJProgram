namespace AVJ.UIElements
{
    public interface IDragDropHandle
    {
        void OnUIDrag(IDragDropHandle UIConponent);

        void OnUIDrop(IDragDropHandle UIConponent);
    }
}