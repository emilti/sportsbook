using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Common
{
    public interface IRandomGenerator
    {
        string RandomString(int minLength = 5, int maxLength = 50);
        int RandomNumber(int min, int max);
    }
}
