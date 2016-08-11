using SerialGenerator.Constants;
using SerialGenerator.Utils;

namespace SerialGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var newSerail = Cryptography.Encrypt("31/12/2016", Companies.Bepensa, Keys.FoodManagerKey);

            //var serial = Cryptography.Decrypt(newSerail, Keys.FoodManagerKey);

            if (newSerail == null)
            {}
        }
    }
}
