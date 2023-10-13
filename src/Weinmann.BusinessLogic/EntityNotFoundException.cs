namespace Weinmann.BusinessLogic
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message = "No data found by given request") : base(message)
        {
        }
    }
}
