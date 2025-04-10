namespace APIAlamoNOA.Services.Entities
{
    public class FieldValue
    {
        public string Field { get; set; }
        public string PropertyName { get; set; }
        public string FixedValue { get; set; }
        public FieldFunction Function { get; set; }
    }
}