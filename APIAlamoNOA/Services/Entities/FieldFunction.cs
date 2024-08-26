namespace APIAlamoNoa.Services.Entities
{
    public class FieldFunction
    {
        public string Name { get; set; }
        public List<FunctionParameters> Parameters { get; set; }
    }

    public class FunctionParameters
    {
        public string Name { get; set; }
        public string? PropertyName { get; set; }
        public string? FixedValue { get; set; }
    }
}
