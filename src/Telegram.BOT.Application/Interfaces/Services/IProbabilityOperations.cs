using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Interfaces.Services
{
    public interface IProbabilityOperations
    {
        int CalculateLevenshteinDistance(string s1, string s2);
        double CalculateNormalizedLevenshteinDistance(string s1, string s2);
    }
}
