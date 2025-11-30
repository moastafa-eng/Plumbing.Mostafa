// Ignore Spelling: Allowence

namespace ServiceLayer.Messages.WebApplication
{
    // Error Messages
    public static class ValidationMessages
    {
        public static string NullEmptyMessage(string propName)
        {
            return $"{propName} cannot be empty,Please enter a value.";
        }

        public static string MaximumCharacterAllowence(string propName, int restriction)
        {
            return $"{propName} can have maximum {restriction} character";
        }

        public static string GreaterThanMessage(string propName, int restriction)
        {
            return $"{propName} must be greater than {restriction}";
        }

        public static string LessThanMessage(string propName, int restriction)
        {
            return $"{propName} must be Less than {restriction}";
        }
    }
}
