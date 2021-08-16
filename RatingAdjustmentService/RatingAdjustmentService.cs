using System;

namespace RatingAdjustment.Services
{
    /** Service calculating a star rating accounting for the number of reviews
     * 
     */
    public class RatingAdjustmentService
    {
        // TODO: Replace this file with the one from Part A!
        const double MAX_STARS = 5.0;  // Likert scale
        const double Z = 1.96; // 95% confidence interval

        double _q;
        double _percent_positive;

        /** Percentage of positive reviews
         * 
         * In this case, that means X of 5 ==> percent positive
         * 
         * Returns: [0, 1]
         */
        public void SetPercentPositive(double stars)
        {
            // TODO: Implement this!
            _percent_positive = stars * 2 / 10;
        }

        /**
         * Calculate "Q" given the formula in the problem statement
         */
        public void SetQ(double number_of_ratings)
        {
            // TODO: Implement this!

            var n = number_of_ratings;

            // p̂, the fraction of upvotes
            var phat = _percent_positive;

            _q = (phat + Z * Z / (2 * n) - Z * Math.Sqrt((phat * (1 - phat) + Z * Z / (4 * n)) / n)) / (1 + Z * Z / n);
        }


        /** Adjusted lower bound
         * 
         * Lower bound of the confidence interval around the star rating.
         * 
         * Returns: a double, up to 5
         */
        public double Adjust(double stars, double number_of_ratings)
        {
            // TODO: Implement this!
            var phat = 1.0 * number_of_ratings / stars;
            var n = number_of_ratings;
            return (phat + Z * Z / (2 * n) - Z * Math.Sqrt((phat * (1 - phat) + Z * Z / (4 * n)) / n)) / (1 + Z * Z / n);
        }
    }
}
