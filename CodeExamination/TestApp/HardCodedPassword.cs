namespace TestApp
{
    internal class HardCodedPassword
    {
        public bool CheckUser(string username, string password)
        {
            if (password == "Pa$$word")
            {
                return true;
            }
            return false;
        }
    }
}
