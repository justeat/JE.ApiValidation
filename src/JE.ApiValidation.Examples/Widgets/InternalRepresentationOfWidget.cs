namespace JE.ApiValidation.Examples.Widgets
{
    public class InternalRepresentationOfWidget
    {
        public InternalRepresentationOfWidget(Widget widget)
        {
            Name = widget.Name;
        }

        public string Name { get; set; }
    }
}