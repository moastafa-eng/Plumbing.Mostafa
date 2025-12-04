namespace ServiceLayer.Messages.Identity
{
    public static class IdentityMessages
    {
        public static string CheckEmailAddress()
        {
            return " value should be in email format!";
        }

        public static string ComparePassword()
        {
            return "Password and confirm password must be same!";
        }
    }
}
