namespace AVJ.UIElements
{
    public interface IDragDropHandler
    {
        void OnUIDrag(IDragDropHandler UIConponent);

        void OnUIDrop(IDragDropHandler UIConponent);
    }
}