namespace Quack.Validation.Contracts
{
    public interface IValidator
    {
        ValidationResult Validate(object checkableObject);
    }
}