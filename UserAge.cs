using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraDeEdad
{
    internal class UserAge
    {
        public int ageYears { set; get; }
        public int ageMonths { set; get; }
        public int ageDays { set; get; }

        public int ageInMonths { set; get; }
        public int ageInDays { set; get; }

        public Dictionary<Planets, double> ageInPlanets { set; get; }
        


        public UserAge(int ageYears, int ageMonths, int ageDays)
        {
            this.ageYears = ageYears;
            this.ageMonths = ageMonths;
            this.ageDays = ageDays;
            ageInPlanets = new Dictionary<Planets, double>();
        }

        public UserAge()
        {
            ageInPlanets = new Dictionary<Planets, double>();
        }
    }
}
