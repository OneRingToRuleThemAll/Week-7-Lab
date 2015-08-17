namespace Week7LabPin.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Week7LabPin.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<PinterestUser>(new UserStore<PinterestUser>(context));

            if (userManager.FindByName("ahudson") == null)
            {
                var aaron = new PinterestUser() { FirstName = "Aaron", LastName = "Hudson", Email = "aaron@theironyard.com", UserName = "aaron@theironyard.com" };
                userManager.Create(aaron, "Password123!");
            }

            context.SaveChanges();

            var img1 = GetImageByteArray("http://scrapetv.com/News/News%20Pages/Entertainment/images-3/star-wars-poster.jpg");
            var img2 = GetImageByteArray("http://fc06.deviantart.net/fs51/f/2009/258/2/f/2fca1bc9ddcc147ef674d2eebb14ee3a.jpg");
            var img3 = GetImageByteArray("http://www.bollygraph.com/wp-content/uploads/2013/01/Godfather-1972.jpg");
            var img4 = GetImageByteArray("http://img4.wikia.nocookie.net/__cb20090618213543/indianajones/images/0/04/Raidersteaser.jpg");
            var img5 = GetImageByteArray("https://pabblogger.files.wordpress.com/2011/03/dark_knight.jpg");
            var img6 = GetImageByteArray("http://4.bp.blogspot.com/-sY6VnEIBCNU/UNSMSN7zo9I/AAAAAAAAAZ8/0OORh5M-V70/s1600/The+Terminator+Poster.jpg");
            var img7 = GetImageByteArray("https://fanart.tv/fanart/movies/278/movieposter/the-shawshank-redemption-5223c8231afe1.jpg");
            var user = context.Users.FirstOrDefault(x => x.UserName == "aaron@theironyard.com");
            context.Interests.AddOrUpdate(p => p.URL,
                 new Interest { Image = img1, URL = "http://www.imdb.com/title/tt0076759", Comment = "They teach a slave boy to become a Jedi, who later becomes mutilated and becomes dedicated to evil. They find a new boy who is pure of heart and teach him to fight Darth Vader.", Poster = user },
                 new Interest { Image = img2, URL = "http://www.imdb.com/title/tt0133093", Comment = "A black man comes to a white guy named Neo and asks him to choose between colours, which unbeknownst to him represented life choices. When he chooses the pill that 'goes down the rabbit hole', he gets thrust into a digital world in which robots have taken over the world secretly and they have super powers.", Poster = user },
                 new Interest { Image = img3, URL = "http://www.imdb.com/title/tt0068646/",Comment  = "It’s about a mob family, and I’m not sure about the rest. There is a quote at the end where the godfather says something, but I don’t remember what it is.", Poster = user },
                 new Interest { Image = img4, URL = "http://www.imdb.com/title/tt0082971/",Comment  = "A guy who runs into tombs and pyramids and tries to collect artifacts, but always runs into boobie traps. Also, there are cursed people and things that he has to deal with as well.", Poster = user },
                 new Interest { Image = img5, URL = "http://www.imdb.com/title/tt0468569", Comment = "A group of people control the financial and political systems of as much as they can get their hands on. Batmans parents donate to the poor and create programs to prevent that from happening. Thus, they try another method to destroy it by releasing a toxin that makes people go criminally insane. However, batman manages to foil their plans.", Poster = user },
                 new Interest { Image = img6, URL = "http://www.imdb.com/title/tt0088247", Comment = "A movie about a robot who travels back in time to save, what I think I remember being as his inventor, from evil robots who try to change the future by killing him.", Poster = user },
                 new Interest { Image = img7, URL = "http://www.imdb.com/title/tt0111161", Comment = "A scary movie about people who run into some crazy person that wants to kill them.", Poster = user }
                );
        }

        public static byte[] GetImageByteArray(string ImageUrl)
        {
            var client = new WebClient();
            var imageArray = client.DownloadData(ImageUrl);
            var bytearraytoputindb = Models.Interest.ScaleImage(imageArray, 100, 100);
            return bytearraytoputindb;
        }
    }
}
