namespace Grauenwolf.TravellerTools.Web.Data
{
    public record NameDescriptionPair(string Name, string Description)
    {
        public NameDescriptionPair(string name, string description, NameDescriptionPair linkedItem)
            : this(name, description)
            => LinkedItem = linkedItem;

        public NameDescriptionPair? LinkedItem { get; private set; }
        public bool IsMarkdown { get; init; }

        public void AddLinkedItem(NameDescriptionPair item)
        {
            var current = this;
            while (current.LinkedItem != null)
                current = current.LinkedItem;
            //Adds the item to the end of the chain;
            current.LinkedItem = item;
        }
    }
}
