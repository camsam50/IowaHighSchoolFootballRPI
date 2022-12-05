using Models.Data.BcMoore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Models.Interfaces;

namespace DataAccess.BcMoore;

public class BcMooreDataAccess : ISourceDataAccess
{
    private const string CURRENT_YEAR = "2022";
    private static Uri uri = new($"http://ia.bcmoorerankings.com/fb/{CURRENT_YEAR}/latest/");

    public async Task<IEnumerable<Team>> GetTeams()
    {
        return GetLocalCsvData<Team>("team", ProcessTeam);
        //return await GetCsvData<Team>("team", ProcessTeam);
    }

    //public async Task<IEnumerable<Schedule>> GetSchedules()
    //{
    //    return await GetCsvData<Schedule>("schedule", ProcessSchedule);
    //}

    //public async Task<IEnumerable<Score>> GetScores()
    //{
    //    return await GetCsvData<Score>("score", ProcessScore);

    //}


    //public async Task<IEnumerable<Ranking>> GetRankings()
    //{
    //    List<Ranking> rankings = new();
    //    var classes = new List<string>() { "5A", "4A", "3A", "2A", "1A", "A", "8" };

    //    TODO: get from factory
    //    using HttpClient client = new() { BaseAddress = uri };

    //    foreach (var c in classes)
    //    {

    //        string responseBody = await client.GetStringAsync($"{c}Rank.html");

    //        string data = GetBetween(responseBody, "<pre>", "</pre>");
    //        var classRankings = ProcessHTML(data);

    //        rankings.AddRange(classRankings);


    //    }

    //    return rankings;
    //}


    private static IEnumerable<T> GetLocalCsvData<T>(string fileName, Func<string[], T> processor)
    {
        List<T> returnValues = new();
        string filePath = $"C:\\data\\source\\GitHub\\IowaHighSchoolFootballRPI\\DataAccess\\LocalDataSource\\2022\\{fileName}.csv";
        TextFieldParser parser = new(filePath)
        {
            TextFieldType = FieldType.Delimited
        };
        parser.SetDelimiters(new string[] { "," });

        while (!parser.EndOfData)
        {
            string[] row = parser.ReadFields();
            T obj = processor(row);
            if (obj is not null)
            {
                returnValues.Add(obj);
            }
        }
        return returnValues;
    }

    private static async Task<IEnumerable<T>> GetCsvData<T>(string fileName, Func<string[], T> processor)
    {


        //TODO: get from factory
        using HttpClient httpClient = new() { BaseAddress = uri };

        string line;
        string[] parts;
        List<T> returnValues = new();


        using Stream response = await httpClient.GetStreamAsync($"{fileName}.csv");

        using var readFile = new StreamReader(response);


        while ((line = await readFile.ReadLineAsync()) != null)
        {
            parts = line.Split(',');

            var s = processor(parts);
            if (s is not null)
            {
                returnValues.Add(s);
            }

        }

        return returnValues;



    }

    //private static HttpClient GetHttpClient()
    //{
    //    return new HttpClient
    //    {
    //        BaseAddress = uri
    //    };
    //}



    //private static IEnumerable<Ranking> ProcessHTML(string data)
    //{
    //    List<Ranking> rankings = new();
    //    string[] stringSeparators = new string[] { "\n" };
    //    string[] lines = data.Split(stringSeparators, StringSplitOptions.None);
    //    foreach (string s in lines)
    //    {
    //        if (s.Length > 90)
    //        {
    //            Ranking r = GetRankingsData(s);
    //            rankings.Add(r);
    //        }
    //    }
    //    return rankings;
    //}

    //private static Ranking GetRankingsData(string data)
    //{

    //    string namePart = GetBetween(data, "<a href=", "</a>");

    //    string longName = GetBetween(namePart, "/", ".html");

    //    var i1 = namePart.IndexOf(">");
    //    var shortName = namePart.Substring(i1 + 1);


    //    var i2 = data.IndexOf("-");
    //    var rankingPart = data.Substring(i2 + 11);


    //    _ = decimal.TryParse(rankingPart.AsSpan(0, 6), out decimal currentRanking);//81
    //    _ = decimal.TryParse(rankingPart.AsSpan(11, 6), out decimal scheduleRanking);//92
    //    _ = decimal.TryParse(rankingPart.AsSpan(24, 6), out decimal offensiveRanking);//105
    //    _ = decimal.TryParse(rankingPart.AsSpan(37, 6), out decimal defensiveRanking);//118


    //    return new Ranking
    //    {
    //        LongName = longName,
    //        ShortName = shortName,
    //        Rank = currentRanking,
    //        ScheduleAverage = scheduleRanking,
    //        OffensiveAverage = offensiveRanking,
    //        DefensiveAverage = defensiveRanking
    //    };
    //}

    //private static string GetBetween(string strSource, string strStart, string strEnd)
    //{
    //    int start, end, subStringLength;
    //    if (strSource.Contains(strStart) && strSource.Contains(strEnd))
    //    {
    //        start = strSource.IndexOf(strStart, 0) + strStart.Length;
    //        end = strSource.IndexOf(strEnd, start);
    //        subStringLength = end - start;
    //        return strSource.Substring(start, subStringLength);
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}









    private static Team ProcessTeam(string[] parts)
    {
        if (parts == null || parts.Length != 5 || parts[0] == "Long name")
        { return null; }
        else
        {
            return new Team()
            {
                LongName = parts[0],
                ShortName = parts[1],
                Classification = parts[2],
                District = byte.Parse(parts[3])
            };
        }

    }





    //private static Schedule ProcessSchedule(string[] parts)
    //{
    //    if (parts == null) { return null; }
    //    if (parts.Length != 4) { return null; }
    //    if (parts[0] == "Date") { return null; }

    //    return new Schedule()
    //    {
    //        Date = DateTime.Parse(parts[0]),
    //        Visitor = parts[1],
    //        Home = parts[2],
    //        Location = byte.Parse(parts[3])
    //    };

    //}

    //private static Score ProcessScore(string[] parts)
    //{
    //    if (parts == null) { return null; }
    //    if (parts.Length != 6) { return null; }
    //    if (parts[0] == "Date") { return null; }


    //    return new Score()
    //    {
    //        Date = DateTime.Parse(parts[0]),
    //        Visitor = parts[1],
    //        VisitorScore = byte.Parse(parts[2]),
    //        Home = parts[3],
    //        HomeScore = byte.Parse(parts[4]),
    //        Overtimes = byte.Parse(parts[5])
    //    };
    //}


}

