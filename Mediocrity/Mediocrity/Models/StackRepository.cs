namespace Mediocrity.Models
{
    public class StackRepository
    {
        private static List<string> stack = new List<string>();
        private static string CurrentUserEmail = string.Empty;
        public static IEnumerable<string> Stack
        {
            get { return stack; }
        }
        public static string UserInfo
        {
            get { return CurrentUserEmail; }
        }
        public static void addStack(string st)
        {
            if (stack.Contains(st))
            {
                stack.Remove(st);
            }
            else
            {
                stack.Add(st);
            }
        }
        public static void setEmail(string email)
        {
            CurrentUserEmail = email;
        }
        public static string[] getStackArray()
        {
            return stack.ToArray();
        }
    }
}
