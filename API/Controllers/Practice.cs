namespace API.Controllers
{
    public class Practice
    {
        public static string SayHello()
        {
            //Console.WriteLine("Hello World!");
            return "Hello World";
        }

        public static int Add(int a, int b)
        {
            int c = a + b;
            return c;
        }

    }
}
