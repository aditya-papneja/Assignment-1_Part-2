using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RatingAdjustment.Services;
using BreadmakerReport.Models;

namespace BreadmakerReport
{
    class Program
    {
        static string dbfile = @".\data\breadmakers.db";
        static RatingAdjustmentService ratingAdjustmentService = new RatingAdjustmentService();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bread World");
            var BreadmakerDb = new BreadMakerSqliteContext(dbfile);
            //var BMList = BreadmakerDb.Breadmakers.SelectMany(r => r.Reviews.GroupBy(x => x), (b, r) => new
            //{

            //    Reviews = b.Reviews.Count(),
            //    Avarage = b.Reviews.Select(re => re.stars).Average(),
            //    Description = b.title,
            //    Adjust = ratingAdjustmentService.Adjust(b.Reviews.Select(s => s.stars).Count(), b.Reviews.Count())

            //}).ToList();
            var BMList = BreadmakerDb.Breadmakers.SelectMany(r => r.Reviews).Distinct().GroupBy(x => x.BreadmakerId)
              .Select(r => new {
                  Reviews = r.Count(),
                  Avarage = r.Average(x => x.stars),
                  Adjust = ratingAdjustmentService.Adjust(r.Sum(x => x.stars), r.Count()),
                  bmId = r.Key

              }).ToList();
            Console.WriteLine("[#]  Reviews Average  Adjust    Description");
            for (var j = 0; j < 3; j++)
            {
                var i = BMList[j];
               var br= BreadmakerDb.Breadmakers.FirstOrDefault(b => b.BreadmakerId == i.bmId);

                Console.WriteLine($"{j}1\t{i.Reviews}1\t{i.Avarage.ToString("F1")}1\t{ i.Adjust.ToString("F1")}1\t{br.title}");
            }



            Console.ReadKey();
        }
    }
}
