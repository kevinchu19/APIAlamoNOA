namespace APIAlamoNoa.Services.Entities
{
    public class FieldMap
    {
        public string Table { get; set; }
        public string ParentTable { get; set; }
        public string ParentProperty { get; set; }
        public List<FieldValue> Fields { get; set; }

    }
}
