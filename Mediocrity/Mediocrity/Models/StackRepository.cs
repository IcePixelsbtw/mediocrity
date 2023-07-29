namespace Mediocrity.Models
{
    public class StackRepository
    {
        private static List<string> stack = new List<string>();
        public static IEnumerable<string> Stack
        {
            get { return stack; }
        }
        public static IEnumerable<string> UserInfo
        {
            get { return stack; }
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

        public static string[] getStackArray()
        {
            return stack.ToArray();
        }
    }
}
