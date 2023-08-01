using Casgem_BigData.DAL.DTOS;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casgem_BigData.Controllers
{
    public class DefaultController : Controller
    {
        private readonly string _connectionString = "Server = DESKTOP-FCHRJ42\\SQLEXPRESS; initial catalog = BigData; integrated security = true";

        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connectionString);

            var brandMax = (await connection.QueryAsync<BrandResult>("SELECT TOP 1 Brand, COUNT(*) AS count FROM PLATES GROUP BY Brand ORDER BY count DESC")).FirstOrDefault();
            ViewData["brandMax"] = brandMax.Brand;
            ViewData["countMax"] = brandMax.Count;
            
            var brandMin = (await connection.QueryAsync<BrandResult>("SELECT TOP 1 Brand, COUNT(*) AS count FROM PLATES GROUP BY Brand ORDER BY count ASC")).FirstOrDefault();
            ViewData["brandMin"] = brandMin.Brand;
            ViewData["countMin"] = brandMin.Count;




            var fuelTypeMax = (await connection.QueryAsync<FuelResult>("SELECT TOP 1 FUEL, COUNT(*) AS count FROM PLATES GROUP BY FUEL ORDER BY count DESC")).FirstOrDefault();
            ViewData["fuelTypeMax"] = fuelTypeMax.Fuel;
            ViewData["fuelTypeCountMax"] = fuelTypeMax.Count;

            var fuelTypeMin = (await connection.QueryAsync<FuelResult>("SELECT TOP 1 FUEL, COUNT(*) AS count FROM PLATES GROUP BY FUEL ORDER BY count ASC")).FirstOrDefault();
            ViewData["fuelTypeMin"] = fuelTypeMin.Fuel;
            ViewData["fuelTypeCountMin"] = fuelTypeMin.Count;




            var shiftTypeMax = (await connection.QueryAsync<ShiftTypeResult>("SELECT TOP 1 SHIFTTYPE, COUNT(*) AS count FROM PLATES GROUP BY SHIFTTYPE ORDER BY count DESC")).FirstOrDefault();
            ViewData["shiftTypeMax"] = shiftTypeMax.ShiftType;
            ViewData["shiftTypeCountMax"] = shiftTypeMax.Count;

            var shiftTypeMin = (await connection.QueryAsync<ShiftTypeResult>("SELECT TOP 1 SHIFTTYPE, COUNT(*) AS count FROM PLATES GROUP BY SHIFTTYPE ORDER BY count ASC")).FirstOrDefault();
            ViewData["shiftTypeMin"] = shiftTypeMin.ShiftType;
            ViewData["shiftTypeCountMin"] = shiftTypeMin.Count;



            var cityNumberMax = (await connection.QueryAsync<CityNumberResult>("SELECT TOP 1 CITYNR, COUNT(*) AS count FROM PLATES GROUP BY CITYNR ORDER BY count DESC")).FirstOrDefault();
            ViewData["cityNumberMax"] = cityNumberMax.CITYNR;
            ViewData["cityNumberCountMax"] = cityNumberMax.Count;

            var cityNumberMin = (await connection.QueryAsync<CityNumberResult>("SELECT TOP 1 CITYNR, COUNT(*) AS count FROM PLATES GROUP BY CITYNR ORDER BY count ASC")).FirstOrDefault();
            ViewData["cityNumberMin"] = cityNumberMin.CITYNR;
            ViewData["cityNumberCountMin"] = cityNumberMin.Count;


            return View();
        }



        public async Task<IActionResult> Search(string keyword)
        {

            string query = @"
            SELECT TOP 1000 BRAND, SUBSTRING(PLATE, 1, 2) AS PlatePrefix, SHIFTTYPE, FUEL, CASETYPE
            FROM PLATES
            WHERE BRAND LIKE '%' + @Keyword + '%'
               OR PLATE LIKE '%' + @Keyword + '%'
               OR SHIFTTYPE LIKE '%' + @Keyword + '%'
               OR FUEL LIKE '%' + @Keyword + '%'
               OR CASETYPE LIKE '%' + @Keyword + '%'
        ";

            await using var connection = new SqlConnection(_connectionString);
            connection.Open();

            // Sorguyu çalıştırın ve sonuçları alın
            var searchResults = await connection.QueryAsync<SearchResult>(query, new { Keyword = keyword });

            // Sonuçları JSON formatında döndürün
            return Json(searchResults);

        }
    }
}
