namespace ApiCart.Utils
{
    public class GenerateID
    {
        private static int count = 1;
        public static int GenerateNewID()
        {
            int id = count;
            count++;
            return id;
        }
    }
}
