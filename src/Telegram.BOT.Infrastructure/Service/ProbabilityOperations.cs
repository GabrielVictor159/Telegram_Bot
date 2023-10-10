using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Infrastructure.Service
{
    public static class ProbabilityOperations
    {
        public static int CalculateLevenshteinDistance(string s1, string s2)
        {
            int[,] dp = new int[s1.Length + 1, s2.Length + 1];
        
            for (int i = 0; i <= s1.Length; i++)
            {
                dp[i, 0] = i;
            }
        
            for (int j = 0; j <= s2.Length; j++)
            {
                dp[0, j] = j;
            }
        
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    int cost = (s1[i - 1] == s2[j - 1]) ? 0 : 1;
        
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                }
            }
        
            return dp[s1.Length, s2.Length];
        }

        public static double CalculateNormalizedLevenshteinDistance(string s1, string s2)
        {
            char[] sortedChars1 = s1.ToCharArray().OrderBy(c => c).ToArray();
            char[] sortedChars2 = s2.ToCharArray().OrderBy(c => c).ToArray();
            string sortedString1 = new string(sortedChars1);
            string sortedString2 = new string(sortedChars2);
            int distance = CalculateLevenshteinDistance(sortedString1, sortedString2);
            int maxLength = Math.Max(sortedString1.Length, sortedString2.Length);

            if (maxLength == 0)
            {
                return 0.0;
            }

            return (1 - ((double)distance / maxLength));
        }
    }
}
