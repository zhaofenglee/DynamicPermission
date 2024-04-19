namespace JS.Abp.DynamicPermission.Shared
{
    public class LookupDto<TKey>
    {
        public TKey Id { get; set; }

        public string DisplayName { get; set; } = null!;
    }
}