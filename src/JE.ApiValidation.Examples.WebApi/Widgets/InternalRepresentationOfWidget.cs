namespace JE.ApiValidation.Examples.WebApi.Widgets
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