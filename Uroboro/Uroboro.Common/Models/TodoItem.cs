namespace Uroboro.Common.Models
{
    public class TodoItem : BaseItem
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()},Name=[{Name}]";
        }
    }
}
