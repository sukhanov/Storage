namespace DomainObject
{
    public partial class Product
    {
        public string GetDescription(bool full, int count = 150)
        {
            if (full)
                return Description;
            if (Description.Length >= count)
                return Description.Substring(0, count);
            return Description;
        }
    }
}