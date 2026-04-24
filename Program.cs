using LINQ.DataSources;

namespace Assignment_02_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var products = Source.ProductList;

            #region Q1 - Top 3 Most Expensive

            var top3 = products
                .OrderByDescending(p => p.UnitPrice)
                .Take(3);

            foreach (var p in top3)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

            #endregion

            #region Q2 - Page 2 (size = 5)

            var page2 = products
                .Skip(5)
                .Take(5);

            foreach (var p in page2)
                Console.WriteLine(p.ProductName);

            #endregion

            #region Q3 - Take While Price < 25

            var takeWhile = products
                .OrderBy(p => p.UnitPrice)
                .TakeWhile(p => p.UnitPrice < 25);

            foreach (var p in takeWhile)
                Console.WriteLine(p.ProductName);

            #endregion

            #region Q4 - All Seafood In Stock?

            var allSeafood = products
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);

            Console.WriteLine(allSeafood);

            #endregion

            #region Q5 - Contains ID

            int[] ids = { 3, 9, 13, 18 };

            Console.WriteLine(ids.Contains(9));

            #endregion

            #region Q6 - Group by Category + Count

            var group = products.GroupBy(p => p.Category);

            foreach (var g in group)
                Console.WriteLine($"{g.Key} - Count: {g.Count()}");

            #endregion

            #region Q7 - Group Names Only

            var groupNames = products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Names = g.Select(p => p.ProductName)
                });

            foreach (var g in groupNames)
            {
                Console.WriteLine(g.Category);
                foreach (var name in g.Names)
                    Console.WriteLine($" - {name}");
            }

            #endregion

            #region Q8 - Categories with > 3 Products

            var bigCats = products
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3);

            foreach (var g in bigCats)
                Console.WriteLine(g.Key);

            #endregion

            #region Q9 - Query Syntax Group Customers

            var customers = new[]
            {
                new { CompanyName = "ABC", Country = "USA", Orders = new[]{100.0,200.0} },
                new { CompanyName = "XYZ", Country = "UK", Orders = new[]{150.0} },
                new { CompanyName = "Tech", Country = "USA", Orders = new[]{50.0} }
            };

            var result =
                from c in customers
                group c by c.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    Total = g.Sum(x => x.Orders.Sum())
                };

            foreach (var r in result)
                Console.WriteLine($"{r.Country} - {r.Count} - {r.Total}");

            #endregion

            #region Q10 - Total Units In Stock

            var totalStock = products.Sum(p => p.UnitsInStock);

            Console.WriteLine(totalStock);

            #endregion

            #region Q11 - Cheapest & Most Expensive

            var min = products.Min(p => p.UnitPrice);
            var max = products.Max(p => p.UnitPrice);

            Console.WriteLine($"Min: {min}, Max: {max}");

            #endregion

            #region Q12 - Distinct Categories

            var categories = products
                .Select(p => p.Category)
                .Distinct();

            foreach (var c in categories)
                Console.WriteLine(c);

            #endregion

            #region Q13 - Except (Set Difference)

            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };

            var diff = setA.Except(setB);

            foreach (var x in diff)
                Console.WriteLine(x);

            #endregion

            #region Q14 - Countries Except (Case-Insensitive)

            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };

            var result2 = list1
                .Except(list2, StringComparer.OrdinalIgnoreCase);

            foreach (var c in result2)
                Console.WriteLine(c);

            #endregion

            #region Q15 - Dictionary

            var dict = products.ToDictionary(p => p.ProductID);

            if (dict.TryGetValue(18, out var product))
                Console.WriteLine(product.ProductName);

            #endregion

            #region Q16 - First > 50

            var first50 = products.First(p => p.UnitPrice > 50);

            Console.WriteLine(first50.ProductName);

            #endregion

            #region Q17 - FirstOrDefault > 500

            var first500 = products.FirstOrDefault(p => p.UnitPrice > 500);

            Console.WriteLine(first500 == null ? "Not Found" : first500.ProductName);

            #endregion

            #region Q18 - Multiplication Table (7)

            var table = Enumerable.Range(1, 10)
                .Select(x => $"7 x {x} = {7 * x}");

            foreach (var t in table)
                Console.WriteLine(t);

            #endregion

            #region Q19 - Even Numbers 1 to 30

            var evens = Enumerable.Range(1, 30)
                .Where(x => x % 2 == 0);

            foreach (var e in evens)
                Console.WriteLine(e);

            #endregion

            #region Q20 - Concat Products + Customers

            var custNames = customers.Select(c => c.CompanyName);

            var concat = products.Select(p => p.ProductName).Take(3)
                .Concat(custNames.Take(3));

            foreach (var item in concat)
                Console.WriteLine(item);

            #endregion

            #region Q21 - Zip (Pair Product + Customer)

            var paired = products.Take(3)
                .Zip(customers.Take(3),
                (p, c) => $"{p.ProductName} sold to {c.CompanyName}");

            foreach (var p in paired)
                Console.WriteLine(p);

            #endregion
        }
    }
}