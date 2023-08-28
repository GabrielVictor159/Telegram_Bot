using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.BOT.Application.Interfaces.Services;

namespace Telegram.BOT.Infrastructure.Service
{
    public class ProbabilityOperations : IProbabilityOperations
    {
        public double CalculateJaccardSimilarity(string tagsA, string tagsB)
        {
            var setA = new HashSet<string>(tagsA.Split(' '));
            var setB = new HashSet<string>(tagsB.Split(' '));

            int intersection = setA.Intersect(setB).Count();
            int union = setA.Count + setB.Count - intersection;

            return (double)intersection / union;
        }
    }
}
