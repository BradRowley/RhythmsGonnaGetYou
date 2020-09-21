using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RhythmsGonnaGetYou
{
    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public string IsSigned { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public List<Album> Albums { get; set; }

    }

    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IsExplict { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int BandId { get; set; }
        public Band Band { get; set; }



    }
    class RockContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=RockDatabase");
        }

    }



    class Program
    {
        static void Main(string[] args)
        {
            var context = new RockContext();

            var bands = context.Bands;
            var albums = context.Albums;

            // var albumCount = albums.Count();
            // Console.WriteLine($"There are {albumCount} movies!");


            var hasQuitTheApplication = false;
            while (hasQuitTheApplication == false)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine(" View - All Bands");
                Console.WriteLine(" AddBand - Add Dinosaur");
                Console.WriteLine(" AddAlbum - Remove Dinosaur");
                Console.WriteLine(" BandsThatAreSigned - View bands that are signed");
                Console.WriteLine(" BandsThatAreNotSigned - View bands that are not signed");
                Console.WriteLine(" LetBandGo - Un sign Band");
                Console.WriteLine(" ReSignBand - Sign band");
                Console.WriteLine(" ViewAlbumsByDate - View all albums ordered by ReleaseDate");
                Console.WriteLine(" ViewBandAlbums - View Bands Albums");
                Console.WriteLine(" Quit - Quit the program");
                Console.WriteLine();
                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                if (choice == "View")
                {
                    foreach (var band in bands)
                    {
                        Console.WriteLine($" Band name: {band.Name}");
                    }
                }
                if (choice == "AddBand")
                {
                    Console.Write("Name: ");
                    var name = Console.ReadLine();

                    Console.Write("CountryOfOrigin: ");
                    var origin = Console.ReadLine();

                    Console.Write("NumberOfMembers: ");
                    var numberOfMembersString = Console.ReadLine();
                    var members = int.Parse(numberOfMembersString);

                    Console.Write("Style: ");
                    var style = Console.ReadLine();

                    Console.Write("ContactPhoneNumber: ");
                    var contactPhoneNumberString = Console.ReadLine();
                    var contactPhoneNumber = Console.ReadLine();

                    Console.Write("IsSigned: ");
                    var isSigned = Console.ReadLine();

                    Console.Write("ContactName: ");
                    var contactName = Console.ReadLine();

                    var newband = new Band()
                    {
                        Name = name,
                        CountryOfOrigin = origin,
                        NumberOfMembers = members,
                        Style = style,
                        ContactPhoneNumber = contactPhoneNumber,
                        IsSigned = isSigned,
                        ContactName = contactName,
                    };
                    context.Bands.Add(newband);
                    context.SaveChanges();
                }

                if (choice == "AddAlbum")
                {
                    Console.Write("Title: ");
                    var title = Console.ReadLine();

                    Console.Write("IsExplicit TRUE or FALSE: ");
                    var isExplicit = Console.ReadLine();

                    Console.Write("ReleaseDate: ");
                    var releaseDateString = Console.ReadLine();
                    DateTime enteredDate = DateTime.Parse(releaseDateString);


                    Console.Write("BandId: ");
                    var bandIdString = Console.ReadLine();
                    var bandId = int.Parse(bandIdString);

                    var newAlbum = new Album()
                    {
                        Title = title,
                        IsExplict = isExplicit,
                        ReleaseDate = enteredDate,
                        BandId = bandId,
                    };
                }
                if (choice == "Quit")
                {
                    hasQuitTheApplication = true;
                }

                if (choice == "BandsThatAreSigned")
                {

                    // Console.Write("Band name: ");
                    // var whatBand = Console.ReadLine();
                    var bandsSigned = context.Bands.Where(band => band.IsSigned == "TRUE");
                    var bandsNames = bandsSigned.Select(band => band.Name);
                    foreach (var bandsName in bandsNames)
                    {
                        Console.WriteLine($" Band name: {bandsName}");
                    }
                }


                if (choice == "BandsThatAreNotSigned")
                {
                    var bandsSigned = context.Bands.Where(band => band.IsSigned == "FALSE");
                    var bandsNames = bandsSigned.Select(band => band.Name);

                    foreach (var bandsName in bandsNames)
                    {
                        Console.WriteLine($"Band name: {bandsName}");
                    }

                }
                if (choice == "LetBandGo")
                {
                    Console.Write("Band name: ");
                    var newBand = Console.ReadLine();
                    var perviousBand = context.Bands.FirstOrDefault(band => band.Name == newBand);

                    if (perviousBand != null)
                    {
                        perviousBand.IsSigned = "FALSE";

                        context.SaveChanges();
                    }
                    Console.WriteLine("Task Complete");
                }
                if (choice == "ReSignBand")
                {
                    Console.Write("Band name: ");
                    var newBandd = Console.ReadLine();
                    var perviousBand = context.Bands.FirstOrDefault(band => band.Name == newBandd);

                    if (perviousBand != null)
                    {
                        perviousBand.IsSigned = "TRUE";

                        context.SaveChanges();
                    }
                    Console.WriteLine("Task Complete");
                }
                if (choice == "ViewAlbumsByDate")
                {
                    //     var lists = context.Albums.OrderBy(album => album.ReleaseDate).ToList();
                    //     foreach (var list in lists)
                    //     {
                    //         Console.WriteLine(list.Title);
                    //     }



                }
                if (choice == "ViewBandAlbums")
                {
                    // Console.WriteLine("Band name: ");
                    // var nameOfBand = Console.ReadLine();
                    var viewBand = context.Bands.FirstOrDefault(band => band.Name == "Guns N Roses");
                    var albumBand = context.Albums.Include(album => album.Band);
                    foreach (var album in albums)
                    {
                        var bandsAlbum = album.Title;
                    }
                    // var albumTitle = context.Albums.Include(album => album.Title);
                    // var viewBandInAlbum = albumBand.viewBandInAlbum.Select(album => album.Band);
                    // var findBands = viewBandInAlbum.Where(band => band.Name == "Guns N Roses");
                    // var viewBandAlbums = context.Albums.Where(album => album.Title = viewBandInAlbum);

                    foreach (var findBand in findBands)
                    {

                        Console.WriteLine(findBand);
                    }

                    // Console.Write("Band name: ");
                    // var bandView = Console.ReadLine();
                    // var context.Movies.Include(movie => movie.Rating)
                }




            }

        }
    }
}
