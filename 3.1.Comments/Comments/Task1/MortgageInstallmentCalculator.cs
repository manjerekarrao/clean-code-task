using System;
using Comments.Task1.ThirdParty;

namespace Comments.Task1
{
    public static class MortgageInstallmentCalculator
    {
        /// <summary>
        /// Calculates Monthly Payment
        /// </summary>
        /// <param name="principal">principal amount</param>
        /// <param name="term">term of mortgage in years</param>
        /// <param name="rate">rate of interest</param>
        /// <returns>monthly payment amount</returns>
        public static double CalculateMonthlyPayment(int principal, int term, double rate)
        {
            //cannot have negative loanAmount, term duration and rate of interest
            if (principal < 0 || term < 0 || rate < 0)
            {
                throw new InvalidInputException("Negative values are not allowed");
            }

            // convert interest rate into a decimal - eg. 6.5% = 0.065
            rate /= 100;

            // convert term in years to term in months
            int tim = term * 12;

            //for zero interest rates
            if (rate == 0)
                return (double)principal / tim;

            // convert into monthly rate
            double m = rate / 12;

            // Calculate the monthly payment
            // The Math.Pow() method is used calculate values raised to a power
            double monthlyPayment = principal * m / (1 - Math.Pow(1 + m, -tim));

            return monthlyPayment;
        }
    }
}
