using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Infrastructure.Service
{
    public class ProbabilityActions : IProbabilityOperations
    {
        public int CalculateLevenshteinDistance(string s1, string s2)
        {
            return ProbabilityOperations.CalculateLevenshteinDistance(s1,s2);
        }

        public double CalculateNormalizedLevenshteinDistance(string s1, string s2)
        {
            return ProbabilityOperations.CalculateNormalizedLevenshteinDistance(s1,s2);
        }
    }
}
