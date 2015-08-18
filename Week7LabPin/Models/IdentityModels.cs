using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Week7LabPin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class PinterestUser : IdentityUser
    {
        public string FirstName { get; set; }
        public ICollection<Interest> Interests { get; set; }
        public string LastName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<PinterestUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;

            {

            }
        }
    }

    public class Interest
    {
        public int Id { get; set; }
        public PinterestUser Poster { get; set; }
        public byte[] Image { get; set; }
        public string Comment { get; set; }
        public string Pin { get; set; }
        public string URL { get; set; }
        public string Link
        {
            get
            {
                return "/Interests/GetImage/" + Id.ToString();
            }
        }


        public static byte[] ScaleImage(byte[] source, int maxWidth, int maxHeight)
        {
            var image = System.Drawing.Image.FromStream(new MemoryStream(source));

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            Bitmap bmp = new Bitmap(newImage);

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

    }

    public class ApplicationDbContext : IdentityDbContext<PinterestUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Week7LabPin.Models.Interest> Interests { get; set; }
    }
}
